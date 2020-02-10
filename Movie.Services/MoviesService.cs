using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Movie.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly ISaveRepository _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly ILogger<MoviesService> _logger;

        public MoviesService(IMoviesRepository moviesRepository, ISaveRepository unitOfWork, IMemoryCache cache, ILogger<MoviesService> logger)
        {
            this._moviesRepository = moviesRepository;
            this._unitOfWork = unitOfWork;
            this._cache = cache;
            this._logger = logger;
        }

        public async Task<IEnumerable<Models.Movie>> ListAsync()
        {
            return await _moviesRepository.ListAsync();
        }

        public async Task<MovieResponse> SaveAsync(Models.Movie movie)
        {
            try
            {
                await _moviesRepository.AddAsync(movie);

                await _unitOfWork.CompleteAsync();

                return new MovieResponse(movie);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred when saving the movie: {ex.Message}";

                _logger.LogError(message);

                return new MovieResponse(message);
            }
        }

        public async Task<MovieResponse> UpdateAsync(int id, Models.Movie movie)
        {
            var existingMovie = await _moviesRepository.FindByIdAsync(id);

            if (existingMovie == null)
            {
                return new MovieResponse("Movie not found.");
            }

            existingMovie.Name = movie.Name;

            try
            {
                _moviesRepository.Update(existingMovie);

                await _unitOfWork.CompleteAsync();

                return new MovieResponse(existingMovie);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred when updating the movie: {ex.Message}";

                _logger.LogError(message);

                return new MovieResponse(message);                
            }
        }

        public async Task<MovieResponse> DeleteAsync(int id)
        {
            var existingMovie = await _moviesRepository.FindByIdAsync(id);

            if (existingMovie == null)
            {
                return new MovieResponse("Movie not found.");
            }

            try
            {
                _moviesRepository.Remove(existingMovie);

                await _unitOfWork.CompleteAsync();

                return new MovieResponse(existingMovie);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred when deleting the movie: {ex.Message}";

                _logger.LogError(message);

                return new MovieResponse(message);
            }
        }
    }
}
