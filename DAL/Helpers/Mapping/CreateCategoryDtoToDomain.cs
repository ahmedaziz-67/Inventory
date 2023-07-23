﻿using AutoMapper;
using DAL.Domain.Models;
using DAL.Persistence.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers.Mapping
{
    public class CreateCategoryDtoToDomain : Profile
    {
        public CreateCategoryDtoToDomain()
        {
            CreateMap<CreateCategoryDto, Categories>();
        }
    }
}
