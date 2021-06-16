using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IUsuarioCorporeRepository
    {
        IEnumerable<Usuario> ObterAniversariantes();
        IEnumerable<Usuario> ObterAdmissoes();
    }
}
