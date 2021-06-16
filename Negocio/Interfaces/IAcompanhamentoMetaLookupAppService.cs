using System.Collections.Generic;
using System.Threading.Tasks;
using CoBRA.Application.Bases;
using System;

namespace CoBRA.Application.Interfaces
{
    public interface IAcompanhamentoMetaLookupAppService
    {
        Task<List<LookupDto>> ObterLookupCargo(int idUsuario);
        Task<List<LookupDto>> ListarLookupCargo();
        Task<List<LookupDto>> ObterLookupUf();
        Task<List<LookupDto>> ObterLookupNome(string idCargo, string idRegiao, string idUsuario);
        Task<List<LookupDto>> ObterLookupNomeGeral(string idRegiao, string idCargo);
        Task<Guid> ObterIdUsuarioRM(int idUsuario);
    }
}
