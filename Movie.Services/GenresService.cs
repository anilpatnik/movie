using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Movie.Models;
using Movie.Repositories;
using Movie.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Services
{
    public class GenresService : IGenresService
    {
        private readonly IGenresRepository _genresRepository;
        private readonly ISaveRepository _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly ILogger<GenresService> _logger;

        public GenresService(IGenresRepository genresRepository, ISaveRepository unitOfWork, IMemoryCache cache, ILogger<GenresService> logger)
        {
            this._genresRepository = genresRepository;
            this._unitOfWork = unitOfWork;
            this._cache = cache;
            this._logger = logger;
        }

        public async Task<IEnumerable<Genre>> ListAsync()
        {
            // Get the genres list from the memory cache. If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire one minute ahead and returning the Task that lists the genres from the repository.
            var genres = await _cache.GetOrCreateAsync(CacheKeys.GenresList, (entry) => 
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);

                return _genresRepository.ListAsync();
            });

            return genres;
        }

        public async Task<GenreResponse> SaveAsync(Genre genre)
        {
            try
            {
                await _genresRepository.AddAsync(genre);

                await _unitOfWork.CompleteAsync();

                return new GenreResponse(genre);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred when saving the genre: {ex.Message}";

                _logger.LogError(message);

                return new GenreResponse(message);
            }
        }

        public async Task<GenreResponse> UpdateAsync(int id, Genre genre)
        {
            var existingGenre = await _genresRepository.FindByIdAsync(id);

            if (existingGenre == null)
            {
                return new GenreResponse("Genre not found.");
            }

            existingGenre.Name = genre.Name;

            try
            {
                _genresRepository.Update(existingGenre);

                await _unitOfWork.CompleteAsync();

                return new GenreResponse(existingGenre);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred when updating the genre: {ex.Message}";

                _logger.LogError(message);

                return new GenreResponse(message);                
            }
        }

        public async Task<GenreResponse> DeleteAsync(int id)
        {
            var existingGenre = await _genresRepository.FindByIdAsync(id);

            if (existingGenre == null)
            {
                return new GenreResponse("Genre not found.");
            }

            try
            {
                _genresRepository.Remove(existingGenre);

                await _unitOfWork.CompleteAsync();

                return new GenreResponse(existingGenre);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred when deleting the genre: {ex.Message}";

                _logger.LogError(message);

                return new GenreResponse(message);
            }
        }
    }
}
