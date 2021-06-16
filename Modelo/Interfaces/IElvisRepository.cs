using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IElvisRepository
    {
        List<RequisicaoArquivos> GetFiles(long ID);
        long SetFile(RequisicaoArquivos arquivo);
        RequisicaoArquivos DeleteFile(long ID);
        void DeleteFilesRequisicao(int idRequisicao);
    }
}
