using Grpc.Core;
using KoiShow.Service;
using static KoiShow.APIService.PointGrpcService;

namespace KoiShow.APIService.GrpcServices
{
    public class PointGrpcService : PointGrpcServiceBase
    {
        private readonly PointService _pointService;

        public PointGrpcService(PointService pointService)
        {
            _pointService = pointService;
        }

        public override async Task<PointReply> GetPoint(
            PointRequest request,
            ServerCallContext context)
        {
            var result = await _pointService.GetById(request.PointId);

            if (result?.Data is KoiShow.Data.Models.Point point)
            {
                return new PointReply
                {
                    Id = point.Id, // từ BaseEntity
                    TotalScore = point.TotalScore ?? 0
                };
            }

            return new PointReply();
        }
    }
}