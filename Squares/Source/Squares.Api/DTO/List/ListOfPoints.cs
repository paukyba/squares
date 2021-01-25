namespace Squares.Api.DTO.List
{
    public class ListOfPoints
    {       
        public int Id { get; set; }

        public string ListName { get; set; }

        public Point.Point[] Points { get; set; } 
    }
}
