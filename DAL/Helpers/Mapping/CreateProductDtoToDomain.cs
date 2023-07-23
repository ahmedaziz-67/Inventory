using AutoMapper;
using DAL.Domain.Models;
using DAL.Persistence.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers.Mapping
{
    public class CreateProductDtoToDomain : Profile
    {
        public CreateProductDtoToDomain()
        {
            CreateMap<CreateProductDto, Products>();
        }
    }
}
