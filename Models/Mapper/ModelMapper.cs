using AutoMapper;
using BusinessModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModels.Mapper
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<User, UserDto>();
        }
       
    }
}
