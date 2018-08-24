using System.Collections.Generic;
using System.Threading.Tasks;

namespace PDX.PBOT.Scootertown.Integration.Services.Interfaces
{
    public interface IService<TSource, TDest>
    {
        Task Save(Queue<TSource> items);
    }
}
