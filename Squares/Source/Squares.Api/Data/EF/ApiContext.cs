using Microsoft.EntityFrameworkCore;
using Squares.Api.Data.Models;

namespace Squares.Api.Data.EF
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<PointModel> Points { get; set; }

        public DbSet<ListOfPointsModel> SquarePointsLists { get; set; }
    }
}
