using System.Collections.Generic;
using System.Threading.Tasks;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Add(T item, bool saveImmediately = true);
        Task<List<T>> All();
        Task<T> Find(int key);
        Task<T> Find(string alternateKey);
        Task<T> Update(T item, bool saveImmediately = true);
        Task SaveChanges();
    }
}
