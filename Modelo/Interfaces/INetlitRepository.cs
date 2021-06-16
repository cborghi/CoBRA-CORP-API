using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface INetlitRepository
    {
        ConteudoPaginadoPP CarregarConteudoPP(int NumeroPagina, int RegistrosPagina, string Filtro);
        Task<ConteudoPaginadoPP> CarregarConteudoNetLitPP(int NumeroPagina, int RegistrosPagina, string Filtro);
        Task SalvarConteudoPP(ConteudoPP arquivo);
        Task ExcluirConteudoPP(int IdConteudo);
        CaminhoFlipping CaminhoFlippingViewModel(long idProduto);
        void ExcluirCaminhoFlipp(long idProduto);
        void SalvarCaminhoFlipp(CaminhoFlipping cam);
    }
}
