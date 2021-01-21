using System.Collections.Generic;

namespace Squares.Models.List
{
    public class SquarePointsList
    {
        public int Id { get; set; }

        public string ListName { get; set; }

        public List<Point> Points { get; set; }

    }
}
