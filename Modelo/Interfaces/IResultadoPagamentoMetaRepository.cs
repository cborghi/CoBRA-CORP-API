using CoBRA.Domain.Entities;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IResultadoPagamentoMetaRepository
    {
        Task<ResultadoPagamentoMeta> ConsultarResultadoPagamentoMeta(int Percentual);
    }
}