using System;
using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
     public class PainelIndicadorMetaAppService : IPainelIndicadorMetaAppService
     {
          private readonly IMapper _mapper;
          private readonly IPainelIndicadorMetaRepository _painelIndicadorMetaRepository;


          public PainelIndicadorMetaAppService(IMapper mapper, IPainelIndicadorMetaRepository painelIndicadorMetaRepository)
          {
               _mapper = mapper;
               _painelIndicadorMetaRepository = painelIndicadorMetaRepository;

          }

          public async Task<PainelIndicadorMetaViewModel> ListarPainelIndicadorMetaAprovado()
          {
               var entity = new CabecalhoPainelMeta()
               {
                    Id = new Guid(),
                    StatusPainel = new StatusPainel()
                    {
                         IdStatusPainel = new Guid("32342EC1-0B81-4861-987E-2A3E2C1ACEF2")
                    },
                    GrupoPainel = new GrupoPainel()
                    {
                         IdGrupo = new Guid()
                    },
                    Observacao = null
               };
               var dados = await _painelIndicadorMetaRepository.BuscarCabecalhoPainel(entity, null);
               var listaAprovacoes = _mapper.Map<List<LinhaPainelIndicadorMetaViewModel>>(dados);

               var result = new PainelIndicadorMetaViewModel()
               {
                    Status = "Aprovado",
                    ListaLinhaPainelIndicadorMeta = listaAprovacoes
               };
               return result;
          }

          public async Task<PainelIndicadorMetaViewModel> ListarPainelIndicadorMetaPendente()
          {
               var entity = new CabecalhoPainelMeta()
               {
                    Id = new Guid(),
                    StatusPainel = new StatusPainel()
                    {
                         IdStatusPainel = new Guid("70D0CC08-227F-4BC4-9805-19B184E16D4B")
                    },
                    GrupoPainel = new GrupoPainel()
                    {
                         IdGrupo = new Guid()
                    },
                    Observacao = null
               };
               var dados = await _painelIndicadorMetaRepository.BuscarCabecalhoPainel(entity, null);
               var listaAprovacoes = _mapper.Map<List<LinhaPainelIndicadorMetaViewModel>>(dados);
               var result = new PainelIndicadorMetaViewModel()
               {
                    Status = "Pendente",
                    ListaLinhaPainelIndicadorMeta = listaAprovacoes
               };
               return result;
          }
     }
}
