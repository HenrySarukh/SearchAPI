using NetTopologySuite.Geometries;
using SearchAPI.Domain.Entities;

namespace SearchAPI.Persistance.Helper
{
    public static class SeedData
    {
        public static IEnumerable<Rectangle> GetSeedRectangles()
        {
            var rectangles = new List<Rectangle>();
            var random = new Random();

            for (int i = 1; i <= 200; i++)
            {
                var rectangle = new Rectangle
                {
                    RectangleId = i,
                    Geometry = GenerateRandomGeometry(random),
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow
                };

                rectangles.Add(rectangle);
            }

            return rectangles;
        }

        private static Geometry GenerateRandomGeometry(Random random)
        {
            var x1 = random.NextDouble() * 10;
            var y1 = random.NextDouble() * 10;
            var x2 = x1 + random.NextDouble() * 5;
            var y2 = y1 + random.NextDouble() * 5;

            var coordinates = new[]
            {
                new Coordinate(x1, y1),
                new Coordinate(x2, y1),
                new Coordinate(x2, y2),
                new Coordinate(x1, y2),
                new Coordinate(x1, y1),
            };

            return new Polygon(new LinearRing(coordinates)) { SRID = 4326 };
        }
    }
}
