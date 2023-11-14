using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using SearchAPI.Domain.Contracts;
using SearchAPI.Domain.Entities;

namespace SearchAPI.Persistance.Repositories
{
    public class RectangleRepository : BaseRepository<Rectangle>, IRectangleRepository
    {
        public RectangleRepository(SearchAPIDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Dictionary<Point, List<Rectangle>>> SearchMatch(List<Point> coordinates)
        {
            var resultDictionary = new Dictionary<Point, List<Rectangle>>();

            // Query rectangles in batch for all coordinates
            var matchingRectangles = await this.dbContext.Rectangles
                .Where(r => coordinates.Any(coord => r.Geometry.Contains(coord) || r.Geometry.Intersects(coord)))
                .ToListAsync();

            // Organize the results into the dictionary
            foreach (var coordinate in coordinates)
            {
                var matchingRectanglesForCoordinate = matchingRectangles
                    .Where(r => r.Geometry.Contains(coordinate) || r.Geometry.Intersects(coordinate))
                    .ToList();

                resultDictionary.Add(coordinate, matchingRectanglesForCoordinate);
            }

            return resultDictionary;
        }
    }
}
