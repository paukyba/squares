using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Squares.Api.Data.EF;
using Squares.Api.Data.Models;
using Squares.Api.Processors;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Squares.Api.Tests.Processors
{
    public class SquareProcessorTests 
    {
        [SetUp]
        public void Setup()
        {
  

  
        }

        [Test]
        public void Test1()
        {
            SquaresProcessor sP = new SquaresProcessor();
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

            Assert.Pass();
        }

        [Test]
        public void Test2()

        {
            Assert.Pass();
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

        private string JsonData => @"[{""ID"":0,""ListId"":0,""X"":1,""Y"":1},
{""ID"":0,""ListId"":0,""X"":2,""Y"":1},
{""ID"":0,""ListId"":0,""X"":3,""Y"":1},
{""ID"":0,""ListId"":0,""X"":4,""Y"":1},
{""ID"":0,""ListId"":0,""X"":5,""Y"":1},
{""ID"":0,""ListId"":0,""X"":6,""Y"":1},
{""ID"":0,""ListId"":0,""X"":7,""Y"":1},
{""ID"":0,""ListId"":0,""X"":8,""Y"":1},
{""ID"":0,""ListId"":0,""X"":9,""Y"":1},
{""ID"":0,""ListId"":0,""X"":10,""Y"":1},
{""ID"":0,""ListId"":0,""X"":1,""Y"":2},
{""ID"":0,""ListId"":0,""X"":2,""Y"":2},
{""ID"":0,""ListId"":0,""X"":3,""Y"":2},
{""ID"":0,""ListId"":0,""X"":4,""Y"":2},
{""ID"":0,""ListId"":0,""X"":5,""Y"":2},
{""ID"":0,""ListId"":0,""X"":6,""Y"":2},
{""ID"":0,""ListId"":0,""X"":7,""Y"":2},
{""ID"":0,""ListId"":0,""X"":8,""Y"":2},
{""ID"":0,""ListId"":0,""X"":9,""Y"":2},
{""ID"":0,""ListId"":0,""X"":10,""Y"":2},
{""ID"":0,""ListId"":0,""X"":1,""Y"":3},
{""ID"":0,""ListId"":0,""X"":2,""Y"":3},
{""ID"":0,""ListId"":0,""X"":3,""Y"":3},
{""ID"":0,""ListId"":0,""X"":4,""Y"":3},
{""ID"":0,""ListId"":0,""X"":5,""Y"":3},
{""ID"":0,""ListId"":0,""X"":6,""Y"":3},
{""ID"":0,""ListId"":0,""X"":7,""Y"":3},
{""ID"":0,""ListId"":0,""X"":8,""Y"":3},
{""ID"":0,""ListId"":0,""X"":9,""Y"":3},
{""ID"":0,""ListId"":0,""X"":10,""Y"":3},
{""ID"":0,""ListId"":0,""X"":1,""Y"":4},
{""ID"":0,""ListId"":0,""X"":2,""Y"":4},
{""ID"":0,""ListId"":0,""X"":3,""Y"":4},
{""ID"":0,""ListId"":0,""X"":4,""Y"":4},
{""ID"":0,""ListId"":0,""X"":5,""Y"":4},
{""ID"":0,""ListId"":0,""X"":6,""Y"":4},
{""ID"":0,""ListId"":0,""X"":7,""Y"":4},
{""ID"":0,""ListId"":0,""X"":8,""Y"":4},
{""ID"":0,""ListId"":0,""X"":9,""Y"":4},
{""ID"":0,""ListId"":0,""X"":10,""Y"":4},
{""ID"":0,""ListId"":0,""X"":1,""Y"":5},
{""ID"":0,""ListId"":0,""X"":2,""Y"":5},
{""ID"":0,""ListId"":0,""X"":3,""Y"":5},
{""ID"":0,""ListId"":0,""X"":4,""Y"":5},
{""ID"":0,""ListId"":0,""X"":5,""Y"":5},
{""ID"":0,""ListId"":0,""X"":6,""Y"":5},
{""ID"":0,""ListId"":0,""X"":7,""Y"":5},
{""ID"":0,""ListId"":0,""X"":8,""Y"":5},
{""ID"":0,""ListId"":0,""X"":9,""Y"":5},
{""ID"":0,""ListId"":0,""X"":10,""Y"":5},
{""ID"":0,""ListId"":0,""X"":1,""Y"":6},
{""ID"":0,""ListId"":0,""X"":2,""Y"":6},
{""ID"":0,""ListId"":0,""X"":3,""Y"":6},
{""ID"":0,""ListId"":0,""X"":4,""Y"":6},
{""ID"":0,""ListId"":0,""X"":5,""Y"":6},
{""ID"":0,""ListId"":0,""X"":6,""Y"":6},
{""ID"":0,""ListId"":0,""X"":7,""Y"":6},
{""ID"":0,""ListId"":0,""X"":8,""Y"":6},
{""ID"":0,""ListId"":0,""X"":9,""Y"":6},
{""ID"":0,""ListId"":0,""X"":10,""Y"":6},
{""ID"":0,""ListId"":0,""X"":1,""Y"":7},
{""ID"":0,""ListId"":0,""X"":2,""Y"":7},
{""ID"":0,""ListId"":0,""X"":3,""Y"":7},
{""ID"":0,""ListId"":0,""X"":4,""Y"":7},
{""ID"":0,""ListId"":0,""X"":5,""Y"":7},
{""ID"":0,""ListId"":0,""X"":6,""Y"":7},
{""ID"":0,""ListId"":0,""X"":7,""Y"":7},
{""ID"":0,""ListId"":0,""X"":8,""Y"":7},
{""ID"":0,""ListId"":0,""X"":9,""Y"":7},
{""ID"":0,""ListId"":0,""X"":10,""Y"":7},
{""ID"":0,""ListId"":0,""X"":1,""Y"":8},
{""ID"":0,""ListId"":0,""X"":2,""Y"":8},
{""ID"":0,""ListId"":0,""X"":3,""Y"":8},
{""ID"":0,""ListId"":0,""X"":4,""Y"":8},
{""ID"":0,""ListId"":0,""X"":5,""Y"":8},
{""ID"":0,""ListId"":0,""X"":6,""Y"":8},
{""ID"":0,""ListId"":0,""X"":7,""Y"":8},
{""ID"":0,""ListId"":0,""X"":8,""Y"":8},
{""ID"":0,""ListId"":0,""X"":9,""Y"":8},
{""ID"":0,""ListId"":0,""X"":10,""Y"":8},
{""ID"":0,""ListId"":0,""X"":1,""Y"":9},
{""ID"":0,""ListId"":0,""X"":2,""Y"":9},
{""ID"":0,""ListId"":0,""X"":3,""Y"":9},
{""ID"":0,""ListId"":0,""X"":4,""Y"":9},
{""ID"":0,""ListId"":0,""X"":5,""Y"":9},
{""ID"":0,""ListId"":0,""X"":6,""Y"":9},
{""ID"":0,""ListId"":0,""X"":7,""Y"":9},
{""ID"":0,""ListId"":0,""X"":8,""Y"":9},
{""ID"":0,""ListId"":0,""X"":9,""Y"":9},
{""ID"":0,""ListId"":0,""X"":10,""Y"":9},
{""ID"":0,""ListId"":0,""X"":1,""Y"":10},
{""ID"":0,""ListId"":0,""X"":2,""Y"":10},
{""ID"":0,""ListId"":0,""X"":3,""Y"":10},
{""ID"":0,""ListId"":0,""X"":4,""Y"":10},
{""ID"":0,""ListId"":0,""X"":5,""Y"":10},
{""ID"":0,""ListId"":0,""X"":6,""Y"":10},
{""ID"":0,""ListId"":0,""X"":7,""Y"":10},
{""ID"":0,""ListId"":0,""X"":8,""Y"":10},
{""ID"":0,""ListId"":0,""X"":9,""Y"":10},
{""ID"":0,""ListId"":0,""X"":10,""Y"":10}]
            ";

    }

}