using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IAmarracaoRepository
    {
        List<RegraPagamento> ListarRegraPagamento();
        List<PrazoValidade> ListarPrazoValidade();
        List<BloqueioPagamento> ListarBloqueioPagamento();
        List<TipoContrato> ListarTipoContrato();
        List<TipoParticipacao> ListarTipoParticipacao();
        Task<int> SalvarAmarracao(Amarracao a);
    }
}
