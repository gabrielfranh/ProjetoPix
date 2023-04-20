using AutoMapper;
using KeyAPI.DTO;
using KeyAPI.Models;

namespace KeyAPI.Configuration
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<KeyDTO, Key>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
