using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Squares.Api.Data.EF;
using Squares.Api.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squares.Api.Data.DAL
{
    /// <summary>
    /// Data access layer to work with EF.
    /// </summary>
    public class DAL : IDAL
    {
        private readonly ApiContext apiContext;
        private readonly ILogger<DAL> logger;

        public DAL(ApiContext apiContext, ILogger<DAL> logger)
        {
            this.apiContext = apiContext;
            this.logger = logger;
        }

        /// <summary>
        /// Creates list in database.
        /// </summary>
        /// <param name="model">Filed list of points model.</param>
        /// <returns>Created list.</returns>
        public async Task CreateListOfPoints(ListOfPointsModel model)
        {
            try
            {
                apiContext.SquarePointsLists.Add(model);
                await apiContext.SaveChangesAsync();
            }
            catch
            {
                logger.LogError($"An error occured when trying create list.");
            }
        }

        /// <summary>
        /// Deletes list from database.
        /// </summary>
        /// <param name="id">List id.</param>
        /// <returns>Task</returns>
        public async Task DeleteListOfPoints(int id)
        {
            try
            {
                var item = apiContext.SquarePointsLists.First(x => x.Id == id);

                apiContext.SquarePointsLists.Remove(item);
                await apiContext.SaveChangesAsync();
            }
            catch
            {
                logger.LogError($"An error occured when trying to delete list.");
            }
        }

        /// <summary>
        /// Removes points from list.
        /// </summary>
        /// <param name="id">List ID.</param>
        /// <param name="points">Point list to remove.</param>
        /// <returns>Task</returns>
        public async Task RemovePointsFromList(int id, List<PointModel> points)
        {
            try
            {
                var item = apiContext.SquarePointsLists.Include(x => x.Points).FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    foreach (var point in points)
                    {
                        var existingPoint = item.Points.Where(x => x.X == point.X && x.Y == point.Y).FirstOrDefault();

                        if (existingPoint != null)
                        {
                            item.Points.Remove(existingPoint);
                        }
                        else
                        {
                            logger.LogWarning($"Point (X:{point.X},Y:{point.Y}) does not exists in list with id {id}.");
                        }
                    }

                    apiContext.SquarePointsLists.Attach(item);
                    await apiContext.SaveChangesAsync();
                }
                else
                {
                    logger.LogWarning($"List with ID {id} does not exists.");
                }
            }
            catch 
            {
                logger.LogError($"An error occured when trying to remove some poins from existing list.");
                throw;
            }
            
        }

        /// <summary>
        /// Adds points to list.
        /// </summary>
        /// <param name="id">List ID.</param>
        /// <param name="points">Point list to remove.</param>
        /// <returns>Task</returns>
        public async Task AddPointsToList(int id, List<PointModel> points)
        {
            try
            {
                var item = apiContext.SquarePointsLists.Include(x => x.Points).FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    item.Points.AddRange(points);

                    apiContext.SquarePointsLists.Attach(item);
                    await apiContext.SaveChangesAsync();
                }
                else
                {
                    logger.LogWarning($"List with ID {id} does not exists.");
                }
            }
            catch
            {
                logger.LogError($"An error occured when trying to add some poins to existing list.");
                throw;
            }
        }

        /// <summary>
        /// Gets all list from database.
        /// </summary>
        /// <returns>Lists of points.</returns>
        public IQueryable<ListOfPointsModel> GetLists()
        {
            try
            {
                return apiContext.SquarePointsLists.Include(x => x.Points);
            }
            catch
            {
                logger.LogError($"An error occured when trying to get all list from database.");
                throw;
            }
        }

        /// <summary>
        /// Gets list by ID.
        /// </summary>
        /// <param name="id">List id.</param>
        /// <returns>List of points.</returns>
        public ListOfPointsModel GetList(int id)
        {
            try
            {
                return apiContext.SquarePointsLists.Include(x => x.Points).Where(x => x.Id == id).FirstOrDefault();
            }
            catch 
            {
                logger.LogError($"An error occured when trying to get list from database.");
                throw;
            }
        }
    }
}
