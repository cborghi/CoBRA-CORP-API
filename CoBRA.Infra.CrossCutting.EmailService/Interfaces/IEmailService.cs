using CoBRA.Infra.CrossCutting.EmailService.ViewModels;
using System.Threading.Tasks;

namespace CoBRA.Infra.CrossCutting.EmailService.Interfaces
{
    public interface IEmailService
    {
         Task<string> EnviarEmail(EmailViewModel Email);
    }
}
