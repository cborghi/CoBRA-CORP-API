using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IFornecedoresProtheusRepository
    {
        List<Fornecedor> ObterFornecedores(string nome);
        Fornecedor ObterFornecedorCodigo(string cod);
    }
}
