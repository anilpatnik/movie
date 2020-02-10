using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie.Services;
using Movie.Resources;
using Movie.Models;

namespace Movie.Web.Controllers
{
    public class GenresController : BaseController
    {
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;        

        public GenresController(IGenresService genresService, IMapper mapper)
        {
            this._genresService = genresService;
            this._mapper = mapper;            
        }

        /// <summary>
        /// Lists all genre.
        /// </summary>
        /// <returns>List genre.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GenreDto>), 200)]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genresService.ListAsync();

            var response = _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(genres);

            return Ok(response);
        }

        /// <summary>
        /// Saves a new genre.
        /// </summary>
        /// <param name="model">Genre data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GenreDto), 201)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveGenreDto model)
        {
            var genre = _mapper.Map<SaveGenreDto, Genre>(model);

            var result = await _genresService.SaveAsync(genre);

            if (!result.Success)
            {
                return BadRequest(new ErrorDto(result.Message));
            }

            var response = _mapper.Map<Genre, GenreDto>(result.Resource);
            
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing genre according to an identifier.
        /// </summary>
        /// <param name="id">Genre identifier.</param>
        /// <param name="model">Updated genre data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GenreDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveGenreDto model)
        {
            var genre = _mapper.Map<SaveGenreDto, Genre>(model);
            
            var result = await _genresService.UpdateAsync(id, genre);

            if (!result.Success)
            {
                return BadRequest(new ErrorDto(result.Message));
            }

            var response = _mapper.Map<Genre, GenreDto>(result.Resource);

            return Ok(response);
        }

        /// <summary>
        /// Deletes a given genre according to an identifier.
        /// </summary>
        /// <param name="id">Genre identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GenreDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _genresService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorDto(result.Message));
            }

            var response = _mapper.Map<Genre, GenreDto>(result.Resource);
            
            return Ok(response);
        }
    }
}
