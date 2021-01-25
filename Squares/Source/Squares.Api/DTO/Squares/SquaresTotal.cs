using System.Collections.Generic;

namespace Squares.Api.DTO.Squares
{
    public class SquaresTotal
    {
        public int ListId { get; set; }

        public string ListName { get; set; }

        public int TotalSquares { get; set; }

        public List<Squares> Squares { get; set; }
    }
}
