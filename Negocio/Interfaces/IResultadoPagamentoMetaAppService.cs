using CoBRA.Application.ViewModels;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IResultadoPagamentoMetaAppService
    {
        Task<ResultadoPagamentoMetaViewModel> ConsultarResultadoPagamentoMeta(int Percentual);
    }
}