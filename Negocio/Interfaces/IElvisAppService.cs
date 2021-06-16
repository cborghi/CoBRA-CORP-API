using CoBRA.Application.ViewModels;
using System.Collections.Generic;

namespace CoBRA.Application.Interfaces
{
    public interface IElvisAppService
    {
        List<RequisicaoArquivosViewModel> GetFiles(long ID);
        long SetFile(RequisicaoArquivosViewModel arquivo);
        RequisicaoArquivosViewModel DeleteFile(long ID);
        void DeleteFileRequisicao(int idRequisicao);
    }
}
