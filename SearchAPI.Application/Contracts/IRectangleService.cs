using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;
using SearchAPI.Application.DTOs;
using SearchAPI.Domain.Entities;

namespace SearchAPI.Application.Contracts
{
    public interface IRectangleService
    {
        public Task<Dictionary<Point, List<Rectangle>>> SearchMatch(List<Coordinate> coordinates);
    }
}
