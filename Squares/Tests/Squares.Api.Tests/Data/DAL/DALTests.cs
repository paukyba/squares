using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Squares.Api.Data.EF;
using Squares.Api.Data.Models;
using System.Collections.Generic;

namespace Squares.Api.Tests.Data.DAL
{
    public class DALTests : EFmock
    {
        public DALTests() : base(
            new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase("InMemorySquares")
                .Options)
        {
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            SeedLists(new List<ListOfPointsModel> { new ListOfPointsModel
            {
                ListName = "First",
                Points = new List<PointModel>
                {
                    new PointModel
                    {
                        X = 1,
                        Y = 1
                    },
                    new PointModel
                    {
                        X = 1,
                        Y = 2
                    },
                    new PointModel
                    {
                        X = 2,
                        Y = 1
                    },
                    new PointModel
                    {
                        X = 2,
                        Y = 2
                    },
                }
            } });

            using (var context = new ApiContext(ContextOptions))
            {
                
            }
            Assert.Pass();
        }


    }

}