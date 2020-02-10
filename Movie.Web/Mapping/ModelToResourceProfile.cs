using AutoMapper;
using Movie.Resources;

namespace Movie.Web.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Models.Genre, GenreDto>();

            CreateMap<Models.Movie, MovieDto>();
        }
    }
}
