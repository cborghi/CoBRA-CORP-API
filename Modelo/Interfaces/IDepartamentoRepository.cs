using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IDepartamentoRepository
    {
        List<Departamento> ObterDepartamentos();
    }
}
