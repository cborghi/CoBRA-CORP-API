using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class NetlitAppService : INetlitAppService
    {
        private readonly INetlitRepository _netlitRepository;

        public NetlitAppService(INetlitRepository netlitRepository)
        {
            _netlitRepository = netlitRepository;
        }

        public ConteudoPaginadoPPViewModel CarregarConteudoPP(int NumeroPagina, int RegistrosPagina, string Filtro)
        {
            var Conteudo = _netlitRepository.CarregarConteudoPP(NumeroPagina, RegistrosPagina, Filtro);
            var Retorno = new ConteudoPaginadoPPViewModel();

            Retorno.Contagem = Conteudo.Contagem;
            Retorno.LstConteudosPP = new List<ConteudoPPViewModel>();
            foreach (var item in Conteudo.LstConteudosPP)
            {
                var ret = new ConteudoPPViewModel
                {
                    IdConteudo = item.IdConteudo,
                    Nome = item.Nome,
                    Caminho = item.Caminho,
                    NomeGuid = item.NomeGuid,
                    DtCadastro = item.DtCadastro,
                    Usuario = item.Usuario,
                    Ativo = item.Ativo,
                    PrimeiraPagina = item.PrimeiraPagina,
                };
                Retorno.LstConteudosPP.Add(ret);
            }
            Retorno.RegistrosPagina = RegistrosPagina;
            Retorno.NumeroPagina = NumeroPagina;

            int resultado = System.Math.DivRem(Retorno.Contagem, RegistrosPagina, out int resto);

            Retorno.QtdePaginas = resto != 0 ? resultado + 1 : resultado;

            return Retorno;
        }

        public async Task<ConteudoPaginadoPPViewModel> CarregarConteudoNetLitPP(int NumeroPagina, int RegistrosPagina, string Filtro)
        {
            var Conteudo = await _netlitRepository.CarregarConteudoNetLitPP(NumeroPagina, RegistrosPagina, Filtro);
            var Retorno = new ConteudoPaginadoPPViewModel();

            Retorno.Contagem = Conteudo.Contagem;
            Retorno.LstConteudosPP = new List<ConteudoPPViewModel>();
            foreach (var item in Conteudo.LstConteudosPP)
            {
                var ret = new ConteudoPPViewModel
                {
                    IdConteudo = item.IdConteudo,
                    Nome = item.Nome,
                    Caminho = item.Caminho,
                    NomeGuid = item.NomeGuid,
                    DtCadastro = item.DtCadastro,
                    Usuario = item.Usuario,
                    Ativo = item.Ativo,
                    PrimeiraPagina = item.PrimeiraPagina,
                };
                Retorno.LstConteudosPP.Add(ret);
            }
            Retorno.RegistrosPagina = RegistrosPagina;
            Retorno.NumeroPagina = NumeroPagina;

            int resultado = System.Math.DivRem(Retorno.Contagem, RegistrosPagina, out int resto);

            Retorno.QtdePaginas = resto != 0 ? resultado + 1 : resultado;

            return Retorno;
        }

        public async Task SalvarConteudoPP(ConteudoPPViewModel arquivo)
        {
            ConteudoPP arq = new ConteudoPP
            {
                Nome = arquivo.Nome,
                Caminho = arquivo.Caminho,
                NomeGuid = arquivo.NomeGuid,
                Usuario = arquivo.Usuario,
                DtCadastro = arquivo.DtCadastro,
                Ativo = arquivo.Ativo,
                PrimeiraPagina = arquivo.PrimeiraPagina
            };
            await _netlitRepository.SalvarConteudoPP(arq);
        }

        public async Task ExcluirConteudoPP(int IdConteudo)
        {
            await _netlitRepository.ExcluirConteudoPP(IdConteudo);
        }

        public CaminhoFlippingViewModel CarregarPastasFlippId(long idProduto)
        {
            var Conteudo = _netlitRepository.CaminhoFlippingViewModel(idProduto);

            var ret = new CaminhoFlippingViewModel
            {
                IdCaminhoFlipping = Conteudo.IdCaminhoFlipping,
                Caminho = Conteudo.Caminho,
                IdProduto = Conteudo.IdProduto,
                EBSA = Conteudo.EBSA
            };

            return ret;
        }

        public void SalvarCaminhoFlipp(CaminhoFlippingViewModel caminho)
        {
            var cam = new CaminhoFlipping
            {
                IdCaminhoFlipping = caminho.IdCaminhoFlipping,
                IdProduto = caminho.IdProduto,
                Caminho = caminho.Caminho,
                EBSA = caminho.EBSA
            };
            _netlitRepository.ExcluirCaminhoFlipp(caminho.IdProduto);
            _netlitRepository.SalvarCaminhoFlipp(cam);
        }
    }
}
