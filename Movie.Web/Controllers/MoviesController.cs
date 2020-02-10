using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie.Services;
using Movie.Resources;

namespace Movie.Web.Controllers
{
    public class MoviesController : BaseController
    {
        private readonly IMoviesService _moviesService;
        private readonly IMapper _mapper;        

        public MoviesController(IMoviesService moviesService, IMapper mapper)
        {
            this._moviesService = moviesService;
            this._mapper = mapper;            
        }

        /// <summary>
        /// Lists all movie.
        /// </summary>
        /// <returns>List movie.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), 200)]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _moviesService.ListAsync();

            var response = _mapper.Map<IEnumerable<Models.Movie>, IEnumerable<MovieDto>>(movies);

            return Ok(response);
        }

        /// <summary>
        /// Saves a new movie.
        /// </summary>
        /// <param name="model">Movie data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(MovieDto), 201)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveMovieDto model)
        {
            var movie = _mapper.Map<SaveMovieDto, Models.Movie>(model);

            var result = await _moviesService.SaveAsync(movie);

            if (!result.Success)
            {
                return BadRequest(new ErrorDto(result.Message));
            }

            var response = _mapper.Map<Models.Movie, MovieDto>(result.Resource);
            
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing movie according to an identifier.
        /// </summary>
        /// <param name="id">Movie identifier.</param>
        /// <param name="model">Updated movie data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MovieDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveMovieDto model)
        {
            var movie = _mapper.Map<SaveMovieDto, Models.Movie>(model);
            
            var result = await _moviesService.UpdateAsync(id, movie);

            if (!result.Success)
            {
                return BadRequest(new ErrorDto(result.Message));
            }

            var response = _mapper.Map<Models.Movie, MovieDto>(result.Resource);

            return Ok(response);
        }

        /// <summary>
        /// Deletes a given movie according to an identifier.
        /// </summary>
        /// <param name="id">Movie identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MovieDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _moviesService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorDto(result.Message));
            }

            var response = _mapper.Map<Models.Movie, MovieDto>(result.Resource);
            
            return Ok(response);
        }
    }
}
