using AutoMapper;
using NetTopologySuite.Geometries;
using SearchAPI.Application.Contracts;
using SearchAPI.Domain.Contracts;
using SearchAPI.Domain.Entities;

namespace SearchAPI.Application.Services
{
    public class RectangleService : IRectangleService
    {
        private readonly IMapper mapper;
        private readonly IRectangleRepository rectangleRepository;

        public RectangleService(IMapper mapper, IRectangleRepository rectangleRepository)
        {
            this.mapper = mapper;
            this.rectangleRepository = rectangleRepository;
        }

        public async Task<Dictionary<Point, List<Rectangle>>> SearchMatch(List<Coordinate> coordinates)
        {
            var points = coordinates.Select(coordinate => new Point(coordinate)).ToList();
            var result = await this.rectangleRepository.SearchMatch(points);
            return result;
        }
    }
}
