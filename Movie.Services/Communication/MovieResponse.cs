namespace Movie.Services
{
    public class MovieResponse : BaseResponse<Models.Movie>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="movie">Saved movie.</param>
        /// <returns>Response.</returns>
        public MovieResponse(Models.Movie movie) : base(movie) { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public MovieResponse(string message) : base(message) { }
    }
}
