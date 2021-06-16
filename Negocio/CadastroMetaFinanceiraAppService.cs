using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class CadastroMetaFinanceiraAppService : ICadastroMetaFinanceiraAppService
    {
        private readonly IMapper _mapper;
        private readonly ICadastroMetaFinanceiraRepository _cadastroMetaFinanceiraRepository;

        public CadastroMetaFinanceiraAppService(IMapper mapper
             , ICadastroMetaFinanceiraRepository cadastroMetaFinanceiraRepository)
        {
            _mapper = mapper;
            _cadastroMetaFinanceiraRepository = cadastroMetaFinanceiraRepository;
        }

        public async Task<AgrupadorMetaFinanceiraViewModel> ObterTodos()
        {
            var dados = await _cadastroMetaFinanceiraRepository.BuscarCadastroMetaFinanceira();


            return new AgrupadorMetaFinanceiraViewModel()
            {
                ListaMetaFinanceira = _mapper.Map<List<MetaFinanceiraViewModel>>(dados.ToList())
            };
        }

        public async Task<AgrupadorMetaFinanceiraViewModel> ListarMetaFinanceira(Guid? IdPeriodo)
        {
            var dados = await _cadastroMetaFinanceiraRepository.ListarMetaFinanceira(IdPeriodo);

            return new AgrupadorMetaFinanceiraViewModel()
            {
                ListaMetaFinanceira = _mapper.Map<List<MetaFinanceiraViewModel>>(dados.ToList())
            };
        }

        public async Task<MetaFinanceiraViewModel> ConsultarMetaFinanceira(Guid? UsuarioId, decimal? ValorMeta, Guid? MetaFinanceiraId)
        {
            var dados = await _cadastroMetaFinanceiraRepository.ConsultarCadastroMetaFinanceira(UsuarioId, MetaFinanceiraId);

            var dto = new MetaFinanceiraViewModel();
            dto = _mapper.Map<MetaFinanceiraViewModel>(dados);

            dto.MetaReceitaLiquidaCalc = ValorMeta;
            if (Convert.ToDecimal(dados.MetaReceitaLiquida) < ValorMeta)
            {
                await ModificarItem(dto);
            }
            return _mapper.Map<MetaFinanceiraViewModel>(dados);
        }

        public async Task<MetaFinanceiraViewModel> AprovarItem(MetaFinanceiraViewModel dto)
        {
            try
            {
                var entity = _mapper.Map<PainelMetaFinanceira>(dto);
                entity.IdStatus = new Guid("70d0cc08-227f-4bc4-9805-19b184e16d4b");
                await _cadastroMetaFinanceiraRepository.GravarMetaFinanceira(entity);

                return _mapper.Map<MetaFinanceiraViewModel>(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<MetaFinanceiraViewModel> GravarItem(MetaFinanceiraViewModel dto)
        {
            try
            {
                var entity = _mapper.Map<PainelMetaFinanceira>(dto);
                entity.IdStatus = new Guid("0c03c4ed-2a39-45c7-ad82-11ab223aae27");
                await _cadastroMetaFinanceiraRepository.GravarMetaFinanceira(entity);

                return _mapper.Map<MetaFinanceiraViewModel>(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<MetaFinanceiraViewModel> ModificarItem(MetaFinanceiraViewModel dto)
        {
            try
            {
                var entity = _mapper.Map<PainelMetaFinanceira>(dto);
                await _cadastroMetaFinanceiraRepository.ModificarMetaFinanceira(entity);

                return _mapper.Map<MetaFinanceiraViewModel>(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
