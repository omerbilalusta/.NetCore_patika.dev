using AutoMapper;
using Data.Domain;
using Schema.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieResponse>();
            CreateMap<MovieRequest, Movie>();

            CreateMap<Director,  DirectorResponse>();
            CreateMap<DirectorRequest, Director>();

            CreateMap<Customer, CustomerResponse>();
            CreateMap<CustomerRequest, Customer>();

            CreateMap<ActorActress, ActorActressResponse>();
            CreateMap<ActorActressRequest, ActorActress>();
        }
    }
}
