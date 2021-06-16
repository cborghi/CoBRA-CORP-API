using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class AprovacaoIndividualAppService : IAprovacaoIndividualAppService
     {
          private readonly IMapper _mapper;
          private readonly IAprovacaoIndividualRepository _aprovacaoIndividualRepository;
          private readonly IPainelMetaAnualRepository _painelMetaAnualRepository;


          public AprovacaoIndividualAppService(IMapper mapper,
               IAprovacaoIndividualRepository aprovacaoIndividualRepository,
               IPainelMetaAnualRepository painelMetaAnualRepository)
          {
               _mapper = mapper;
               _aprovacaoIndividualRepository = aprovacaoIndividualRepository;
               _painelMetaAnualRepository = painelMetaAnualRepository;
          }

          public async Task<AprovacaoIndividualViewModel> ObterItem(string id)
          {
               var painel = _painelMetaAnualRepository.GestaoPessoaConsultaAprovacaoMetaAnual(new Guid(id)).Result;
               var dados = painel.FirstOrDefault();

               var teste = new AprovacaoIndividualViewModel()
               {
                    Id = dados.IdUsuario.ToString(),
                    Cargo = dados.Cargo,
                    Nome = dados.Nome,
                    Status = dados.Situacao,
                    ListaLinhaAprovacaoIndividual = _mapper.Map<List<MetaIndividualViewModel>>(painel)
               };

               return teste;
          }

          public async Task GravarItem(MetaIndividualViewModel dto)
          {
               try
               {
                    var painel = await _painelMetaAnualRepository.GestaoPessoaConsultaAprovacaoMetaAnual(
                         new Guid(dto.Id));

                    foreach (var item in painel)
                    {
                         //painel.Observacao = dto.Observacao;
                         item.IdStatus = "Aprovado".Equals(dto.Aprovacao)
                              ? (Guid)new Guid("32342EC1-0B81-4861-987E-2A3E2C1ACEF2")
                              : (Guid)new Guid("ABB55BC0-38C3-4444-8060-F37FCEA3AF3A");

                         await _aprovacaoIndividualRepository.AprovacaoPainelMeta(item);
                    }
               }
               catch (Exception e)
               {
                    Console.WriteLine(e);
                    throw;
               }
          }
     }
}
