using CoBRA.Domain.Entities;

namespace CoBRA.Domain.Interfaces
{
    public interface IRegraRepository
    {
        Regra ObterRegraPorId(int id);
    }
}
