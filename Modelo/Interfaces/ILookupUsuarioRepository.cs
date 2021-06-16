using CoBRA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface ILookupUsuarioRepository
     {
        Task<IEnumerable<LookupUsuario>> ObterLookupUsuariosAcompanhamento(string idCargo, string idRegiao, string idUsuario);

        Task<IEnumerable<LookupUsuario>> ObterLookupUsuariosAcompanhamentoGeral(string idCargo, string idRegiao);

        Task<IEnumerable<LookupUsuario>> ObterLookupUsuarioAcompanhamento(string idUsuario);
     }
}
