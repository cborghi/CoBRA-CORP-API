using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;

namespace CoBRA.Application
{
    public class ElvisAppService : IElvisAppService
    {
        private readonly IMapper _mapper;
        private readonly IElvisRepository _elvisRepository;

        public ElvisAppService(IMapper mapper, IElvisRepository elvisRepository)
        {
            _mapper = mapper;
            _elvisRepository = elvisRepository;
        }

        public List<RequisicaoArquivosViewModel> GetFiles(long ID)
        {
            return _mapper.Map<List<RequisicaoArquivosViewModel>>(_elvisRepository.GetFiles(ID));
        }

        public long SetFile(RequisicaoArquivosViewModel arquivo)
        {
            return _elvisRepository.SetFile(_mapper.Map<RequisicaoArquivos>(arquivo));
        }

        public RequisicaoArquivosViewModel DeleteFile(long ID)
        {
            return _mapper.Map<RequisicaoArquivosViewModel>(_elvisRepository.DeleteFile(ID));
        }

        public void DeleteFileRequisicao(int idRequisicao)
        {
            _elvisRepository.DeleteFilesRequisicao(idRequisicao);
        }
    }
}
