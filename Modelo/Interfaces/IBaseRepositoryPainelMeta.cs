using CoBRA.Domain.Entities;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IBaseRepositoryPainelMeta
    {
        Task<string> BuscarRegiaoUsuario(PainelMetaAnual meta);
        string BuscarArquivoConsulta(string nomeArquivo);
        string Conexao { get; }
    }
}