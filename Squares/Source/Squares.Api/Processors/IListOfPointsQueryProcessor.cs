using Squares.Api.Data.Models;
using Squares.Api.DTO.List;
using Squares.Api.DTO.Point;
using Squares.Api.DTO.Squares;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squares.Api.Processors
{
    public interface IListOfPointsQueryProcessor
    {
        Task<ListOfPointsModel> Create(CreateListOfPoints value);

        ListOfPointsModel GetList(int id);

        List<ListOfPointsModel> GetLists();

        Task Delete(int id);

        Task AddPoints(int id, List<CreatePoint> points);

        Task RemovePoints(int id, List<CreatePoint> points);
        
        Task<SquaresTotal> CalculateSquares(int id);
    }
}
