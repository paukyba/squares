using Squares.Api.Data.Models;
using Squares.Api.DTO.Squares;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Squares.Api.Processors
{
    /// <summary>
    /// Finds all distinct available squares for given list of points.
    /// </summary>
    public class SquaresProcessor : ISquaresProcessor
    {
        /// <summary>
        /// Finds all squares. 
        /// </summary>
        /// <param name="listOfPointsModel">List from database containing points array</param>
        /// <param name="timoutMs">Maximum wait time for task to complete</param>
        /// <returns>All found squares with coordinates.</returns>
        public async Task<SquaresTotal> FindAllSquares(ListOfPointsModel listOfPointsModel, int timoutMs)
        {
            var task = SquareCount(listOfPointsModel.Points.Select(point => new Point { X = point.X, Y = point.Y }).ToArray());

            if (await Task.WhenAny(task, Task.Delay(timoutMs)) == task)
            {
                var allSquares = task.Result;

                return new SquaresTotal()
                {
                    ListId = listOfPointsModel.Id,
                    ListName = listOfPointsModel.ListName,
                    TotalSquares = allSquares.Count,
                    Squares = allSquares.Select(x => 
                        new DTO.Squares.Squares { SquarePoints = new List<Point> { x.A, x.B, x.C, x.D } }
                    ).ToList()
                };
            }
            else
            {
                throw new TimeoutException();
            }                                
        }

        /// <summary>
        /// Finds all available squares which can be drawn with given points.
        /// It generates all posible lines with given point.
        /// It asumes that those lines are diagonal line of square.
        /// It aplies simple calculation to find other two posible points ( two points are given in diagonal line ends )
        /// It checks if found square is unique.
        /// </summary>
        /// <param name="input">All points</param>
        /// <returns>All found squares.</returns>
        private Task<List<SquareEdgeModel>> SquareCount(Point[] input)
        {
            return Task.Run(() =>
            {
                // Just to make sure if we have only distinct points, loading list does not have validation.
                var distinctPoints = input.Distinct().ToArray();

                List<SquareEdgeModel> allSquares = new List<SquareEdgeModel>();

                int count = 0;

                HashSet<Point> set = new HashSet<Point>();

                HashSet<string> squareIdentificators = new HashSet<string>();

                foreach (var point in distinctPoints)
                    set.Add(point);

                for (int i = 0; i < distinctPoints.Length; i++)
                {
                    for (int j = 0; j < distinctPoints.Length; j++)
                    {
                        if (i == j)
                            continue;
                        //For each Point i, Point j, check if b&d exist in set.
                        Point[] DiagVertex = GetRestPoints(distinctPoints[i], distinctPoints[j]);
                        if (set.Contains(DiagVertex[0]) && set.Contains(DiagVertex[1]))
                        {
                            var sortedPoints = new[] { distinctPoints[i], distinctPoints[j], DiagVertex[0], DiagVertex[1] }.OrderBy(x => x.X).ThenBy(x => x.Y).ToArray();

                            var square = new SquareEdgeModel { A = sortedPoints[0], B = sortedPoints[1], C = sortedPoints[2], D = sortedPoints[3] };

                            if (!squareIdentificators.Contains(square.ToString()))
                            {
                                squareIdentificators.Add(square.ToString());

                                allSquares.Add(square);
                                count++;
                            }
                        }
                    }
                }
                return allSquares;
            });
        }

        public Point[] GetRestPoints(Point a, Point c)
        {
            Point[] res = new Point[2];

            double midX = (a.X + c.X) / 2.0;
            double midY = (a.Y + c.Y) / 2.0;

            double Ax = a.X - midX;
            double Ay = a.Y - midY;
            double bX = midX - Ay;
            double bY = midY + Ax;
            Point b = new Point { X = (int)bX, Y = (int)bY };

            double cX = (c.X - midX);
            double cY = (c.Y - midY);
            double dX = midX - cY;
            double dY = midY + cX;
            Point d = new Point { X = (int)dX, Y = (int)dY };

            res[0] = b;
            res[1] = d;
            return res;
        }
    }
}
