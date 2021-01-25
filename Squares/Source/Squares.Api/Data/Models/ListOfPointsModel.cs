using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Squares.Api.Data.Models
{
    public class ListOfPointsModel
    {
        public int Id { get; set; }

        public string ListName { get; set; }

        [ForeignKey("ListId")]
        public List<PointModel> Points { get; set; }
    }
}
