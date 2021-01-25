using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Squares.Api.Data.Models;
using Squares.Api.Processors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Squares.Api.Tests.Processors
{
    public class SquareProcessorTests 
    {
        private ILogger<SquaresProcessor> logger = NSubstitute.Substitute.For<ILogger<SquaresProcessor>>();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenRunningSquaresProcessorToLongShouldThrowTimeOutException()
        {
            try
            {
                // Assign
                SquaresProcessor sP = new SquaresProcessor(logger);

                // Act
                var result = sP.FindAllSquares(new ListOfPointsModel { Points = new List<PointModel> { new PointModel { } } }, 0).Result;

            }
            catch (AggregateException aggEx)
            {
                if (aggEx.InnerExceptions != null && aggEx.InnerExceptions.Any(x=>x.GetType() ==  typeof(TimeoutException)))
                {
                    Assert.Pass();
                }
            }
            catch
            {
                Assert.Fail();
            }

            Assert.Fail();
            // Assert



            /*SquaresProcessor sP = new SquaresProcessor();
            var listOfPointsModel = new ListOfPointsModel
            {
                ListName = "Test",
                Points = new System.Collections.Generic.List<PointModel>
                {
                    new PointModel{ X = 0, Y = 10},
                    new PointModel{ X = 10, Y = 0},
                    new PointModel{ X = -10, Y = 0},
                    new PointModel{ X = 0, Y = -10},

                }

            };

            var jsonData = JsonSerializer.Deserialize<List<PointModel>>(JsonData);
            //listOfPointsModel.Points = GenerateCoordinateGrid(300);

            var results = sP.FindAllSquares(listOfPointsModel, 1000000).Result;

            Assert.Pass();*/
        }

        [Test]
        public void GivenListWithoutPointsShouldReturnNull()
        {
            // Assign
            SquaresProcessor sP = new SquaresProcessor(logger);

            // Act
            var result = sP.FindAllSquares(new ListOfPointsModel { Points = new List<PointModel> { } }, Constants.SquareCalculationTimeoutMs).Result;

            // Asssert

            Assert.IsNull(result);
        }

        [Test]
        public void GivenSquareDuplicatePointsShouldReturnOnlyOne()
        {
            SquaresProcessor sP = new SquaresProcessor(logger);

            // Act
            var result = sP.FindAllSquares(new ListOfPointsModel 
                { Points = new List<PointModel> 
                    {
                        new PointModel{ X = 0, Y = 10},
                        new PointModel{ X = 10, Y = 0},
                        new PointModel{ X = -10, Y = 0},
                        new PointModel{ X = 0, Y = -10},
                        new PointModel{ X = 0, Y = 10},
                        new PointModel{ X = 10, Y = 0},
                        new PointModel{ X = -10, Y = 0},
                        new PointModel{ X = 0, Y = -10},
                    } 
                }
                , Constants.SquareCalculationTimeoutMs).Result;

            // Asssert

            Assert.AreEqual(result.TotalSquares, 1);
        }

        [Test]
        public void Given5on5PointGridShouldReturnAllSquares()
        {
            // Assign
            SquaresProcessor sP = new SquaresProcessor(logger);

            // Act
            var result = sP.FindAllSquares(new ListOfPointsModel
                {
                    Points = GenerateCoordinateGrid(5)
                }
                , Constants.SquareCalculationTimeoutMs).Result;

            // Asssert
            Assert.AreEqual(result.TotalSquares, 80);
        }

        private List<PointModel> GenerateCoordinateGrid(int n)
        {
            var returnList = new List<PointModel>();
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if(j == i)
                    {
                        continue;
                    }

                    returnList.Add(new PointModel { X = i, Y = j });
                }
            }

            return returnList;
        }
    }
}