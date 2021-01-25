using Squares.Api.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squares.Api.Data.DAL
{
    public interface IDAL
    {
        Task CreateListOfPoints(ListOfPointsModel model);

        Task DeleteListOfPoints(int id);

        Task AddPointsToList(int id, List<PointModel> points);

        Task RemovePointsFromList(int id, List<PointModel> points);

        IQueryable<ListOfPointsModel> GetLists();

        ListOfPointsModel GetList(int id);

    }
}
