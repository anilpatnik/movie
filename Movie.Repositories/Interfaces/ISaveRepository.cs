using System.Threading.Tasks;

namespace Movie.Repositories
{
    public interface ISaveRepository
    {
        Task CompleteAsync();
    }
}
