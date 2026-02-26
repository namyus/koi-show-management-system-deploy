using KoiShow.Service.Base;
using KoiShow.Service;
using Microsoft.AspNetCore.Mvc;
using KoiShow.Data.Models;
using KoiShow.Data.DTO.PointDTO;
using Microsoft.AspNetCore.SignalR;
using KoiShow.APIService.Hubs;

namespace KoiShow.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly PointService _pointService;
        private readonly IHubContext<KoiHub> _hubContext;

        public PointsController(PointService pointService,
                                IHubContext<KoiHub> hubContext)
        {
            _pointService = pointService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IBusinessResult> GetPoints()
        {
            return await _pointService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetPoint(int id)
        {
            return await _pointService.GetById(id);
        }

        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutPoint(int id, Point point)
        {
            var result = await _pointService.Save(point);

            // 🔥 Push realtime update
            await _hubContext.Clients.All.SendAsync("ReceivePointUpdate", point);

            return result;
        }

        [HttpPost]
        public async Task<IBusinessResult> PostAnimal(Point point)
        {
            var result = await _pointService.Save(point);

            // 🔥 Push realtime update
            await _hubContext.Clients.All.SendAsync("ReceivePointUpdate", point);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteAnimal(int id)
        {
            var result = await _pointService.DeleteById(id);

            // 🔥 Push realtime delete event
            await _hubContext.Clients.All.SendAsync("ReceivePointDeleted", id);

            return result;
        }

        private bool AnimalExists(int id)
        {
            return _pointService.GetById(id) != null;
        }
    }
}