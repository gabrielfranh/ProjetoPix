using AutoMapper;
using LoginAPI.DTO;
using LoginAPI.Models;

namespace LoginAPI.Configuration
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CostumerDTO, Costumer>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
