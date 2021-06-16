using CoBRA.Domain.Entities;

namespace CoBRA.Application.Interfaces
{
    public interface IRegraAppService
    {
        Regra ObterRegraPorId(int id);
    }
}
