using AutoMapper;
using Squares.Api.Data.Models;
using Squares.Api.DTO.Point;

namespace Squares.Api.Maps
{
    public class PointMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PointModel, Point>();
        }
    }
}