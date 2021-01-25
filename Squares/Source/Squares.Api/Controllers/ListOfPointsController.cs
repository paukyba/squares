using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Squares.Api.Data.Models;
using Squares.Api.DTO.List;
using Squares.Api.DTO.Point;
using Squares.Api.Maps;
using Squares.Api.Processors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squares.Api.Controllers
{
    /// <summary>
    /// List of points controler responsible for GET/POST for managing list with points array 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ListOfPointsController : ControllerBase
    {
        private readonly ILogger<ListOfPointsController> logger;
        private readonly IListOfPointsQueryProcessor queryProcessor;
        private readonly IAutoMapper autoMapper;

        public ListOfPointsController(ILogger<ListOfPointsController> logger, IListOfPointsQueryProcessor queryProcessor, IAutoMapper autoMapper)
        {
            this.logger = logger;
            this.queryProcessor = queryProcessor;
            this.autoMapper = autoMapper;
        }

        /// <summary>
        /// Gets all avaiblable lists.
        /// </summary>
        /// <returns>All available lists with points.</returns>
        [HttpGet]
        public IEnumerable<ListOfPointsModel> Get()
        {
            return queryProcessor.GetLists();
        }

        /// <summary>
        ///  Gets list with points by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List with points</returns>
        [HttpGet("{id}")]
        public ListOfPointsModel Get(int id)
        {
            return queryProcessor.GetList(id); 
        }

        /// <summary>
        /// Loads list to database.
        /// </summary>
        /// <param name="value">List with point array.</param>
        /// <returns>Created list with points.</returns>
        [HttpPost]
        public async Task<ListOfPoints> Post([FromBody] CreateListOfPoints value)
        {
            var dbItem = await queryProcessor.Create(value);
            var item = autoMapper.Map<ListOfPoints>(dbItem);
            return item;
        } 

        /// <summary>
        /// Deletes list.
        /// </summary>
        /// <param name="id">List iD.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await queryProcessor.Delete(id);
        }

        /// <summary>
        /// Add some points to existing list.
        /// </summary>
        /// <param name="id">List Id.</param>
        /// <param name="value">Additional point array.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPoints/{id}")]
        public async Task AddPoints(int id, [FromBody] List<CreatePoint> value)
        {
            await queryProcessor.AddPoints(id, value);
        }

        /// <summary>
        /// Removes some points from existing list.
        /// </summary>
        /// <param name="id">List ID.</param>
        /// <param name="value">Remove point array</param>
        [HttpPost]
        [Route("RemovePoints/{id}")]
        public async void RemovePoints(int id, [FromBody] List<CreatePoint> value)
        {
            await queryProcessor.RemovePoints(id, value);
        }

        /// <summary>
        /// Finds all available Squares which can be drawn with given points.
        /// </summary>
        /// <param name="id">List ID.</param>
        [HttpGet]
        [Route("RemovePoints/{id}")]
        public async void CalculateSquares(int id)
        {
            await queryProcessor.CalculateSquares(id);
        }
    }
}
