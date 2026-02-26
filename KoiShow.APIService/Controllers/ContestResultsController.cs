using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KoiShow.Data.Models;
using KoiShow.Service;
using KoiShow.Service.Base;
using Microsoft.AspNetCore.SignalR;
using KoiShow.APIService.Hubs;

namespace KoiShow.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestResultsController : ControllerBase
    {
        private readonly ContestResultService _contestResultService;
        private readonly IHubContext<KoiHub> _hubContext;

        public ContestResultsController(ContestResultService contestResultService,
                                        IHubContext<KoiHub> hubContext)
        {
            _contestResultService = contestResultService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IBusinessResult> GetContestResults()
        {
            return await _contestResultService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetContestResult(int id)
        {
            return await _contestResultService.GetByIdAsync(id);
        }

        [HttpGet("points/{id}")]
        public async Task<IBusinessResult> GetPointsForContestResult(int id)
        {
            return await _contestResultService.GetPointsForContestResult(id);
        }

        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutContestResult(int id, ContestResult contestResult)
        {
            var result = await _contestResultService.SaveAsync(contestResult);

            // 🔥 Push realtime update
            await _hubContext.Clients.All.SendAsync("ReceiveContestResultUpdate", contestResult);

            return result;
        }

        [HttpPost]
        public async Task<IBusinessResult> PostContestResult(ContestResult contestResult)
        {
            var result = await _contestResultService.SaveAsync(contestResult);

            // 🔥 Push realtime update
            await _hubContext.Clients.All.SendAsync("ReceiveContestResultUpdate", contestResult);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteContestResult(int id)
        {
            var result = await _contestResultService.DeleteByIdAsync(id);

            // 🔥 Push realtime delete event
            await _hubContext.Clients.All.SendAsync("ReceiveContestResultDeleted", id);

            return result;
        }

        private async Task<bool> ContestResultExists(int id)
        {
            return await _contestResultService.GetByIdAsync(id) != null;
        }
    }
}