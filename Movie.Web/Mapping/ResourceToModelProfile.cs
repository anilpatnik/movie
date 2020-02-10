using AutoMapper;
using Movie.Resources;

namespace Movie.Web.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveGenreDto, Models.Genre>();

            CreateMap<SaveMovieDto, Models.Movie>();
        }
    }
}
