using Movie.Models;

namespace Movie.Services
{
    public class GenreResponse : BaseResponse<Genre>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="genre">Saved genre.</param>
        /// <returns>Response.</returns>
        public GenreResponse(Genre genre) : base(genre) { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public GenreResponse(string message) : base(message) { }
    }
}
