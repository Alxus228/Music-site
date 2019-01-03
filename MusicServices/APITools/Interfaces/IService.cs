using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicServices.APITools.Interfaces
{
    public interface IService<T>
    {
        Task<T> Get(int? id);
        Task<List<T>> GetAll();
        Task Create(T value);
        Task Update(T value);
        Task Delete(int? id);
    }
}
