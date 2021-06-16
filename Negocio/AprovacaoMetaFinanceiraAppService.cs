using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class AprovacaoMetaFinanceiraAppService : IAprovacaoMetaFinanceiraAppService
     {
          private readonly IMapper _mapper;
          private readonly IAprovacaoMetaFinanceiraRepository _aprovacaoMetaFinanceiraRepository;
          private readonly IPainelMetaFinanceiraRepository _painelMetaFinanceiraRepository;


          public AprovacaoMetaFinanceiraAppService(IMapper mapper,
               IPainelMetaFinanceiraRepository painelMetaFinanceiraRepository,
               IAprovacaoMetaFinanceiraRepository aprovacaoMetaFinanceiraRepository)
          {
               _mapper = mapper;
               _painelMetaFinanceiraRepository = painelMetaFinanceiraRepository;
               _aprovacaoMetaFinanceiraRepository = aprovacaoMetaFinanceiraRepository;

          }


          public async Task<AprovacaoMetaFinanceiraViewModel> ObterItem(string id)
          {
               try
               {
                    var dados = await _painelMetaFinanceiraRepository.BuscarMetaFinanceira(new Guid(id));
                    var aprovacao = _mapper.Map<AprovacaoMetaFinanceiraViewModel>(dados);

                    return aprovacao;
               }
               catch (Exception e)
               {
                    Console.WriteLine(e);
                    throw;
               }
          }

          public async Task GravarItem(MetaFinanceiraViewModel dto)
          {
               try
               {
                    var painel = await _painelMetaFinanceiraRepository.BuscarMetaFinanceira(new Guid(dto.IdMetaFinanceira));
                    painel.Observacao = dto.Observacao;
                    painel.IdStatus = "Aprovado".Equals(dto.Aprovacao)
                         ? (Guid)new Guid("32342EC1-0B81-4861-987E-2A3E2C1ACEF2")
                         : (Guid)new Guid("ABB55BC0-38C3-4444-8060-F37FCEA3AF3A");

                    await _aprovacaoMetaFinanceiraRepository.AprovacaoPainelMeta(painel);
               }
               catch (Exception e)
               {
                    Console.WriteLine(e);
                    throw;
               }
          }
     }
}
