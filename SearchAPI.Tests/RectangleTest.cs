using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NetTopologySuite.Geometries;
using SearchAPI.Application.Services;
using SearchAPI.Domain.Entities;
using SearchAPI.Persistance;
using SearchAPI.Persistance.Repositories;

namespace SearchAPI.Tests
{
    public class RectangleTest
    {
        [Fact]
        public async Task SearchMatch_ShouldReturnMatchingRectangles()
        {
            // Arrange
            var dbContextMock = new Mock<SearchAPIDbContext>();
            var mapperMock = new Mock<IMapper>();
            var rectangleRepository = new RectangleRepository(dbContextMock.Object);
            var rectangleService = new RectangleService(mapperMock.Object, rectangleRepository);

            var coordinates = new List<Coordinate>
            {
                new Coordinate(1.5, 1.5),
                new Coordinate(2.0, 2.0)
            };

            var points = new List<Point>()
            {
                new Point(coordinates[0]),
                new Point(coordinates[1]),
            };
            
            var seedRectangles = new List<Rectangle>();

            for (int i = 1; i <= 8; i++)
            {
                seedRectangles.Add(new Rectangle { RectangleId = i, Geometry = GenerateRandomGeometry(i), CreatedDate = DateTime.UtcNow, LastModifiedDate = DateTime.UtcNow });
            }

            var dbContextOptions = new DbContextOptionsBuilder<SearchAPIDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var dbContext = new SearchAPIDbContext(dbContextOptions);
            dbContext.AddRange(seedRectangles);
            dbContext.SaveChanges();

            dbContextMock.SetupGet(x => x.Rectangles).Returns(dbContext.Rectangles);

            var result = await rectangleService.SearchMatch(coordinates);

            Assert.NotNull(result);
            Assert.Equal(coordinates.Count, result.Count);

            foreach (var point in points)
            {
                Assert.True(result.ContainsKey(point));

                var matchingRectanglesForCoordinate = result[point];
            }
        }

        private Geometry GenerateRandomGeometry(int i)
        {
            var x1 = i / 10 + 1;
            var y1 = i / 10 + 1;
            var x2 = i / 10 + 1;
            var y2 = i / 10 + 1;

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
