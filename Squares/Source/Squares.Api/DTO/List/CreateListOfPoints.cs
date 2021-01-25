using Squares.Api.DTO.Point;

namespace Squares.Api.DTO.List
{
    public class CreateListOfPoints
    {       
        public string ListName { get; set; }

        public CreatePoint[] Points { get; set; } 
    }
}
