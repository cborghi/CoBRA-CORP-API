using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CoBRA.Domain.Enum.Enum;

namespace CoBRA.Application
{
    public class PermissaoRequisicaoService : IPermissaoRequisicaoService
    {
        private readonly IMapper _mapper;
        private readonly IPermissaoRequisicaoRepository _permissaoRepository;
        private readonly ILogAppService _logAppService;

        public PermissaoRequisicaoService(IPermissaoRequisicaoRepository permissaoRepository, ILogAppService logAppService, IMapper mapper)
        {
            _mapper = mapper;
            _permissaoRepository = permissaoRepository;
            _logAppService = logAppService;
        }

        public async Task GravarPermissaoUsuario(PermissaoRequisicaoViewModel permissao, int idUsuarioAcao, string ipRequisicao)
        {
            try
            {
                await _permissaoRepository.GravarPermissaoUsuario(_mapper.Map<PermissaoRequisicao>(permissao));

                _logAppService.GravarLog(new LogViewModel
                {
                    Data = DateTime.Now,
                    Descricao = $"Alterado permissão de requisição do usuário: {permissao.Usuario.Id}.  " +
                    $"AprovaRequisicaoSupervisor: {permissao.AprovaRequisicaoSupervisor}. " +
                    $"ReprovaRequisicaoSupervisor: {permissao.ReprovaRequisicaoSupervisor}. " +
                    $"CancelaRequisicaoSupervisor: {permissao.CancelaRequisicaoSupervisor}. " +
                    $"AprovaRequisicaoGerente: {permissao.AprovaRequisicaoGerente}. " +
                    $"ReprovaRequisicaoGerente: {permissao.ReprovaRequisicaoGerente}. " +
                    $"CancelaRequisicaoGerente: {permissao.CancelaRequisicaoGerente}. " +
                    $"Usuário ação: {idUsuarioAcao}",
                    TipoLog = (int)TipoLog.Permissao,
                    IpAdress = ipRequisicao
                });
            }
            catch (Exception ex)
            {
                _logAppService.GravarLog(new LogViewModel
                {
                    Data = DateTime.Now,
                    Descricao = $"Erro no método: GravarPermissaoUsuario. Classe: PermissaoRequisicaoService",
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = ipRequisicao,
                    Erro = ex
                });

                throw ex;
            }


        }
        public async Task<IList<PermissaoRequisicaoViewModel>> ListarPermissaoUsuario(UsuarioViewModel usuario)
        {
            return _mapper.Map<IList<PermissaoRequisicaoViewModel>>(await _permissaoRepository.ListarPermissaoUsuario(
                _mapper.Map<Usuario>(usuario)));
        }

        public async Task<IList<PermissaoRequisicaoViewModel>> FiltrarPermissaoUsuario(PermissaoRequisicaoViewModel permissao)
        {
            return _mapper.Map<IList<PermissaoRequisicaoViewModel>>(await _permissaoRepository.FiltrarPermissaoUsuario(
                _mapper.Map<PermissaoRequisicao>(permissao)));
        }
    }
}
