using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IAnoEducacaoCUPRepository
    {
        List<AnoEducacaoCUP> CarregarAnoEducacao();
    }
}
