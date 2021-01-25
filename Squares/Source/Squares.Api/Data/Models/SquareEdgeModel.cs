using System.Drawing;

namespace Squares.Api.Data.Models
{
    public class SquareEdgeModel
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }
        public Point D { get; set; }

        public override string ToString()
        {
            return $"{A.X};{A.Y},{B.X};{B.Y},{C.X};{C.Y},{D.X};{D.Y}";
        }
    }
}
