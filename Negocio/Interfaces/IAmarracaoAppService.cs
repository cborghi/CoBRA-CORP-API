using CoBRA.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IAmarracaoAppService
    {
        List<RegraPagamentoViewModel> ListarRegraPagamento();
        List<PrazoValidadeViewModel> ListarPrazoValidade();
        List<BloqueioPagamentoViewModel> ListarBloqueioPagamento();
        List<TipoContratoViewModel> ListarTipoContrato();
        List<TipoParticipacaoViewModel> ListarTipoParticipacao();
        Task<int> SalvarAmarracao(AmarracaoViewModel amarracao, int usuario);
    }
}
