using NetTopologySuite.Geometries;
using SearchAPI.Domain.Common;

namespace SearchAPI.Domain.Entities
{
    public class Rectangle : AuditableEntity
    {
        public int RectangleId { get; set; }
        public Geometry Geometry { get; set; }
    }
}
