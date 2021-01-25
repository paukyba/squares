using AutoMapper;
using Squares.Api.Data.Models;
using Squares.Api.DTO.List;

namespace Squares.Api.Maps
{
    public class ListOfPointsMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
           configuration.CreateMap<ListOfPointsModel, ListOfPoints>();
        }
    }
}