using CoBRA.Domain.Entities;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IIntranetRepository
    {
        Task CriarLivro(Livro item);
        Task CriarEnvioObra(EnvioObra item);
        Task CriarEnvioObraResumo(EnvioObraResumo item);
    }
}