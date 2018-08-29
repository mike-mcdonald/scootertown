using System.Collections.Generic;
using System.Threading.Tasks;

namespace PDX.PBOT.Scootertown.Integration.Services.Interfaces
{
    public interface IService<TSource>
    {
        Task Save(string company, Queue<TSource> items);
    }
}
