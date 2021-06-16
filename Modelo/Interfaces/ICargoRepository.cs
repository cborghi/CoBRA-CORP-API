using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface INivelRepository
    {
        List<Nivel> Obter();
    }
}
