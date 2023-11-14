using NetTopologySuite.Geometries;
using SearchAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAPI.Domain.Contracts
{
    public interface IRectangleRepository
    {
        public Task<Dictionary<Point, List<Rectangle>>> SearchMatch(List<Point> coordinates);
    }
}
