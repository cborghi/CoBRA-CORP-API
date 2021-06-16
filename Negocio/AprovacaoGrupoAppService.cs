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
    public class AprovacaoGrupoAppService : IAprovacaoGrupoAppService
     {
          private readonly IMapper _mapper;
          private readonly IPainelIndicadorMetaRepository _painelIndicadorMetaRepository;
          private readonly IAprovacaoGrupoRepository _aprovacaoGrupoRepository;

          public AprovacaoGrupoAppService(IMapper mapper, IPainelIndicadorMetaRepository painelIndicadorMetaRepository, IAprovacaoGrupoRepository aprovacaoGrupoRepository)
          {
               _mapper = mapper;
               _painelIndicadorMetaRepository = painelIndicadorMetaRepository;
               _aprovacaoGrupoRepository = aprovacaoGrupoRepository;

          }

          public async Task<AprovacaoGrupoViewModel> ObterItem(string id)
          {
               var entity = new CabecalhoPainelMeta()
               {
                    Id = new Guid(id),
                    StatusPainel = new StatusPainel()
                    {
                         IdStatusPainel = new Guid()
                    },
                    GrupoPainel = new GrupoPainel()
                    {
                         IdGrupo = new Guid()
                    },
                    Observacao = null
               };
               var dados = _painelIndicadorMetaRepository.BuscarCabecalhoPainel(entity, null).Result.FirstOrDefault();
               var listaAprovacoes = _mapper.Map<AprovacaoGrupoViewModel>(dados);
               listaAprovacoes.ListaLinhaAprovacaoGrupo = _mapper.Map<List<LinhaAprovacaoGrupoViewModel>>(dados?.LinhasPainel);
               return listaAprovacoes;

          }

          public async Task GravarItem(AprovacaoGrupoViewModel dto)
          {
               var entity = new CabecalhoPainelMeta()
               {
                    Id = new Guid(dto.Id),
                    StatusPainel = new StatusPainel()
                    {
                         IdStatusPainel = new Guid(),
                         Descricao = dto.Aprovacao
                    },
                    GrupoPainel = new GrupoPainel()
                    {
                         IdGrupo = new Guid()
                    },
                    Observacao = dto.Observacao
               };

               await _aprovacaoGrupoRepository.AprovacaoPainelMeta(entity);
          }
     }
}
