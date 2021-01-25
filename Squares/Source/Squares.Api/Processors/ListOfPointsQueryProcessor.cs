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

        public ListOfPointsModel GetList(int id)
        {
            return dal.GetList(id);
        }

        public List<ListOfPointsModel> GetLists()
        {
            return dal.GetLists().ToList();
        }

        public async Task Delete(int id)
        {
            await dal.DeleteListOfPoints(id);
        }

        public async Task AddPoints(int id, List<CreatePoint> values)
        {
            var points = values.Select(x => new PointModel { X = x.X, Y = x.Y }).ToList();

            await dal.AddPointsToList(id, points);
        }

        public async Task RemovePoints(int id, List<CreatePoint> values)
        {
            var points = values.Select(x => new PointModel { X = x.X, Y = x.Y }).ToList();

            await dal.RemovePointsFromList(id, points);
        }

        public async Task<SquaresTotal> CalculateSquares(int id)
        {
            var list = this.dal.GetList(id);

            var result = await this.squaresProcessor.FindAllSquares(list, Constants.SquareCalculationTimeoutMs);

            return result;
        }
    }
}
