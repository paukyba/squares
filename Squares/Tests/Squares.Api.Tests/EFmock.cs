using Microsoft.EntityFrameworkCore;
using Squares.Api.Data.EF;
using Squares.Api.Data.Models;
using System.Collections.Generic;

namespace Squares.Api.Tests
{
    public class EFmock
    {
        protected EFmock(DbContextOptions<ApiContext> contextOptions)
        {
            ContextOptions = contextOptions;

            PrepareDb();
        }

        private void PrepareDb()
        {
            using (var context = new ApiContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        protected DbContextOptions<ApiContext> ContextOptions { get; }

        protected void SeedLists(List<ListOfPointsModel> lists, bool resetData = true)
        {
            using (var context = new ApiContext(ContextOptions))
            {
                if (resetData)
                {
                    PrepareDb();
                }

                context.SquarePointsLists.AddRange(lists);

                context.SaveChanges();
            }
        }
    }
}
