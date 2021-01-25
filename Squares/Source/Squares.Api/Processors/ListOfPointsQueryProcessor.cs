using Microsoft.Extensions.Logging;
using Squares.Api.Data.DAL;
using Squares.Api.Data.Models;
using Squares.Api.DTO.List;
using Squares.Api.DTO.Point;
using Squares.Api.DTO.Squares;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squares.Api.Processors
{
    /// <summary>
    /// Processes controler data queries
    /// </summary>
    public class ListOfPointsQueryProcessor : IListOfPointsQueryProcessor
    {
        private readonly IDAL dal;
        private readonly ILogger<ListOfPointsQueryProcessor> logger;
        private readonly ISquaresProcessor squaresProcessor;

        public ListOfPointsQueryProcessor(IDAL dal, ILogger<ListOfPointsQueryProcessor> logger, ISquaresProcessor squaresProcessor)
        {
            this.dal = dal;
            this.logger = logger;
            this.squaresProcessor = squaresProcessor;
        }

        /// <summary>
        /// Creates new list with points
        /// </summary>
        /// <param name="model">List with points</param>
        /// <returns>Created list.</returns>
        public async Task<ListOfPointsModel> Create(CreateListOfPoints model)
        {
            var item = new ListOfPointsModel
            {
                ListName = model.ListName,
                Points = model.Points.Select(x => new PointModel { X = x.X, Y = x.Y }).ToList()
            };

            await dal.CreateListOfPoints(item);
            return item;
        }

        /// <summary>
        /// Gets list by id.
        /// </summary>
        /// <param name="id">List id</param>
        /// <returns>List</returns>
        public ListOfPointsModel GetList(int id)
        {
            return dal.GetList(id);
        }

        /// <summary>
        /// Gets all lists.
        /// </summary>
        /// <returns>All lists.</returns>
        public List<ListOfPointsModel> GetLists()
        {
            return dal.GetLists().ToList();
        }

        /// <summary>
        /// Deletes lists by id.
        /// </summary>
        /// <param name="id">List id.</param>
        /// <returns>Task</returns>
        public async Task Delete(int id)
        {
            await dal.DeleteListOfPoints(id);
        }

        /// <summary>
        /// Adds new points to existing list
        /// </summary>
        /// <param name="id">List id.</param>
        /// <param name="values">New points.</param>
        /// <returns>Task</returns>
        public async Task AddPoints(int id, List<CreatePoint> values)
        {
            var points = values.Select(x => new PointModel { X = x.X, Y = x.Y }).ToList();

            await dal.AddPointsToList(id, points);
        }

        /// <summary>
        /// Removes existing points from list.
        /// </summary>
        /// <param name="id">List id.</param>
        /// <param name="values">Points to delete.</param>
        /// <returns>Tasks</returns>
        public async Task RemovePoints(int id, List<CreatePoint> values)
        {
            var points = values.Select(x => new PointModel { X = x.X, Y = x.Y }).ToList();

            await dal.RemovePointsFromList(id, points);
        }

        /// <summary>
        /// Calculates all posible squares for given points.
        /// </summary>
        /// <param name="id">List id with points.</param>
        /// <returns>All found squares.</returns>
        public async Task<SquaresTotal> CalculateSquares(int id)
        {
            var list = this.dal.GetList(id);

            if (list != null)
            {
                var result = await this.squaresProcessor.FindAllSquares(list, Constants.SquareCalculationTimeoutMs);

                return result;
            }

            this.logger.LogError($"List with ID {id} does not exists.");

            return null;
        }
    }
}
