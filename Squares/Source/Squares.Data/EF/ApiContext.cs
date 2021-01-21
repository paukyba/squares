using Microsoft.EntityFrameworkCore;
using Squares.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squares.Data.EF
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Point> Points { get; set; }

        public DbSet<SquarePointsList> SquarePointsLists { get; set; }
    }
}
