using Microsoft.CodeAnalysis.CSharp.Syntax;
using Squares.Api.Data.Models;
using Squares.Api.DTO.Squares;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squares.Api.Processors
{
    public interface ISquaresProcessor
    {
        Task<SquaresTotal> FindAllSquares(ListOfPointsModel listOfPointsModel, int timeoutMs);
    }
}
