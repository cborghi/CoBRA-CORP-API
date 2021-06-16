using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace CoBRA.Application
{
    public class GrupoAppService : IGrupoAppService
    {
        private readonly IMapper _mapper;
        private readonly IGrupoRepository _grupoRepository;

        public GrupoAppService(IMapper mapper, IGrupoRepository grupoRepository)
        {
            _mapper = mapper;
            _grupoRepository = grupoRepository;
        }

        public List<GrupoViewModel> ObterTodos()
        {
            var grupos = _mapper.Map<List<GrupoViewModel>>(_grupoRepository.ObterGrupos());
            return grupos;
        }

        public string Adicionar(GrupoViewModel grupo)
        {
            return _grupoRepository.Adicionar(_mapper.Map<Grupo>(grupo));
        }

        public string Editar(GrupoViewModel grupo)
        {
            return _grupoRepository.Editar(_mapper.Map<Grupo>(grupo));
        }

        public void AtivarGrupo(GrupoViewModel grupo)
        {
            _grupoRepository.Ativar(_mapper.Map<Grupo>(grupo));
        }

        public int ObterIdGrupoPorIdGrupoAD(Guid idGrupoAd)
        {
            return _grupoRepository.ObterIdGrupoPorIdGrupoAD(idGrupoAd);
        }

        public GrupoViewModel ObterGrupoPorId(int idGrupo)
        {
            return _mapper.Map<GrupoViewModel>(_grupoRepository.ObterPorId(idGrupo));
        }
    }
}
