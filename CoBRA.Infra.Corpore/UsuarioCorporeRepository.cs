using Microsoft.Extensions.Configuration;
namespace CoBRA.Infra.Corpore
{
    public class UsuarioCorporeRepository : BaseRepository, IUsuarioCoporeRepository
    {
        public UsuarioCorporeRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }

}
