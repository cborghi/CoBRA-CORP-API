using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoBRA.Application.Bases;

namespace CoBRA.Application
{
    public class AcompanhamentoMetaLookupAppService : IAcompanhamentoMetaLookupAppService
     {
        private readonly IMapper _mapper;
        private readonly ICargoRepository _cargoRepository;
        private readonly ILookupUsuarioRepository _lookupUsuarioRepository;

        public AcompanhamentoMetaLookupAppService(
            IMapper mapper,
            ILookupUsuarioRepository lookupUsuarioRepository,
            ICargoRepository cargoRepository
            )
        {
            _mapper = mapper;
            _cargoRepository = cargoRepository;
            _lookupUsuarioRepository = lookupUsuarioRepository;
        }


        public async Task<List<LookupDto>> ObterLookupCargo(int idUsuario)
        {
            var dados = await _cargoRepository.ObterCargosAcompanhamento(idUsuario);
            var cargos = _mapper.Map<List<LookupDto>>(dados);
            return cargos;
        }

        public async Task<List<LookupDto>> ListarLookupCargo()
        {
            var dados = await _cargoRepository.ListarCargosAcompanhamento();
            var cargos = _mapper.Map<List<LookupDto>>(dados);
            return cargos;
        }

        public async Task<Guid> ObterIdUsuarioRM(int idUsuario)
        {
            Guid idUsuarioRM = await _cargoRepository.ObterIdUsuarioRM(idUsuario);
            return idUsuarioRM;
        }

        public async Task<List<LookupDto>> ObterLookupUf()
        {
            return new List<LookupDto>();
        }

        public async Task<List<LookupDto>> ObterLookupNome(string idCargo, string idRegiao, string idUsuario)
        {
            var dados = (await _lookupUsuarioRepository.ObterLookupUsuariosAcompanhamento(idCargo, idRegiao, idUsuario)).ToList();
            dados.Add((await _lookupUsuarioRepository.ObterLookupUsuarioAcompanhamento(idUsuario)).First());
            var usuarios = _mapper.Map<List<LookupDto>>(dados.OrderBy(i => i.Nome));
            return usuarios;
        }

        public async Task<List<LookupDto>> ObterLookupNomeGeral(string idRegiao, string idCargo)
        {
            idRegiao = idRegiao == "null" ? null : idRegiao;
            idCargo = idCargo == "null" ? null : idCargo;

            var dados = await _lookupUsuarioRepository.ObterLookupUsuariosAcompanhamentoGeral(idCargo, idRegiao);
            var usuarios = _mapper.Map<List<LookupDto>>(dados);
            return usuarios;
        }
    }
}
