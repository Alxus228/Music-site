using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicRepository.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<ICollection<T>> GetAll();
        Task Create(T value);
        Task Update(T value);
        Task DeleteById(int id);
    }
}
