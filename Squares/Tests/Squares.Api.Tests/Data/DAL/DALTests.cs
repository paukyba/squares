using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Squares.Api.Data.EF;
using Squares.Api.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Squares.Api.Tests.Data.DAL
{
    public class DALTests : EFmock
    {
        private ILogger<Api.Data.DAL.DAL> logger = Substitute.For<ILogger<Api.Data.DAL.DAL>>();

        public DALTests() : base(
            new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase("InMemorySquares")
                .Options)
        {   
        }

        [SetUp]
        public void Setup()
        {
            SeedLists(new List<ListOfPointsModel> { new ListOfPointsModel
            {
                ListName = "Seeded 1",
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
            } ,{
                new ListOfPointsModel
                {
                    ListName = "Seeded 2",
                    Points = new List<PointModel>
                {
                    new PointModel
                    {
                        X = 10,
                        Y = 10
                    },
                    new PointModel
                    {
                        X = 10,
                        Y = 20
                    },
                    new PointModel
                    {
                        X = 20,
                        Y = 10
                    },
                    new PointModel
                    {
                        X = 20,
                        Y = 20
                    },
                }
                } } });
        }

        [Test]
        public void GivenNewListOfPointsShouldBeSavedInDb()
        {
            using (var context = new ApiContext(ContextOptions))
            {
                // Assign
                var dal = new Api.Data.DAL.DAL(context, logger);

                // Act
                _ = dal.CreateListOfPoints(new ListOfPointsModel
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
                });

                // Asserts
                var result = context.SquarePointsLists.Where(x => x.ListName == "First").FirstOrDefault();

                Assert.IsNotNull(result);

            }
        }

        [Test]
        public void GivenExistingListIdShouldDeleteListInDb()
        {
            using (var context = new ApiContext(ContextOptions))
            {
                // Assign
                var dal = new Api.Data.DAL.DAL(context, logger);

                // Act
                var checkIfExists = context.SquarePointsLists.Where(x => x.Id == 1).FirstOrDefault();

                Assert.IsNotNull(checkIfExists);

                _ = dal.DeleteListOfPoints(1);

                // Asserts
                var result = context.SquarePointsLists.Where(x => x.Id == 1).FirstOrDefault();

                Assert.IsNull(result);

            }
        }

        [Test]
        public void GivenNonExistingListIdShouldNotDeleteListInDb()
        {
            using (var context = new ApiContext(ContextOptions))
            {
                // Assign
                var dal = new Api.Data.DAL.DAL(context, logger);

                // Act
                var checkIfExists = context.SquarePointsLists.Where(x => x.Id == 1).FirstOrDefault();

                Assert.IsNotNull(checkIfExists);

                _ = dal.DeleteListOfPoints(2);

                // Asserts
                var result = context.SquarePointsLists.Where(x => x.Id == 1).FirstOrDefault();

                Assert.IsNotNull(result);
            }
        }

        [Test]
        public void WhenQueringListsAllListShouldBeReceivedFromDataBase()
        {
            using (var context = new ApiContext(ContextOptions))
            {
                // Assign
                var dal = new Api.Data.DAL.DAL(context, logger);

                // Act
                var allLists = dal.GetLists();

                // Asserts
                Assert.AreEqual(allLists.Count(), 2);

            }
        }

        [Test]
        public void WhenQueringListByIdShouldBeReceivedFromDataBase()
        {
            using (var context = new ApiContext(ContextOptions))
            {
                // Assign
                var dal = new Api.Data.DAL.DAL(context, logger);

                // Act
                var allLists = dal.GetList(1);

                // Asserts
                Assert.IsNotNull(allLists);

            }
        }

        [Test]
        public void WhenQueringListByNonExsitingIdShouldNotBeReceivedFromDataBase()
        {
            using (var context = new ApiContext(ContextOptions))
            {
                // Assign
                var nonExistingListID = 3;
                var dal = new Api.Data.DAL.DAL(context, logger);

                // Act
                var allLists = dal.GetList(nonExistingListID);

                // Asserts
                Assert.IsNull(allLists);

            }
        }

        [Test]
        public void GivenPointsShouldBeAddedToExistingList()
        {
            using (var context = new ApiContext(ContextOptions))
            {
                // Assign
                var dal = new Api.Data.DAL.DAL(context, logger);
                var points = new List<PointModel>
                {
                    new PointModel
                    {
                        X = 100,
                        Y = 100
                    }
                };
                var list = context.SquarePointsLists.Include(x=>x.Points).Where(x => x.Id == 1).FirstOrDefault();
                Assert.NotNull(list);
                Assert.AreEqual(list.Points.Count, 4);

                // Act
                var allLists = dal.AddPointsToList(1, points);

                // Asserts
                Assert.AreEqual(list.Points.Count, 5);
            }
        }

        [Ignore("Non working mock for extension methods")]
        [Test]
        public void GivenPointsShouldNotBeAddedToNonExistingListAndWarningLogIsWritten()
        {
            using (var context = new ApiContext(ContextOptions))
            {
                // Assign
                var nonExistingListID = 3;

                var dal = new Api.Data.DAL.DAL(context, logger);

                var points = new List<PointModel>
                {
                    new PointModel
                    {
                        X = 100,
                        Y = 100
                    }
                };
                var list = context.SquarePointsLists.Include(x => x.Points).Where(x => x.Id == nonExistingListID).FirstOrDefault();
                Assert.IsNull(list);                

                // Act
                var allLists = dal.AddPointsToList(nonExistingListID, points);

                // Asserts
           
                // Extensions methods not working with moq and nsubstitute.
                logger.Received().LogWarning(Arg.Any<string>());
            }
        }
    }
}