using AutoMapper;
using CostumerAPI.DTO;
using CostumerAPI.Models;

namespace CostumerAPI.Configuration
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ClienteDTO, Cliente>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
