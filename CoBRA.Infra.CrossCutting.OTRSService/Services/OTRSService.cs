using CoBRA.Domain.Interfaces;
using CoBRA.Infra.CrossCutting.OTRSService.Interfaces;
using CoBRA.Infra.CrossCutting.OTRSService.ViewModel;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utf8Json;

namespace CoBRA.Infra.CrossCutting.OTRSService.Services
{
    public class OTRSService : IOTRSService
    {
        private string _otrsUrl;
        private string _otrsTicket;
        private string _otrsUsuario;
        private readonly IUsuarioRepository _usuarioRepository;
        public OTRSService(string otrsUrl, string otrsTicket, string otrsUsuario, IUsuarioRepository usuarioRepository)
        {
            _otrsUrl = otrsUrl;
            _otrsTicket = otrsTicket;
            _otrsUsuario = otrsUsuario;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> GerarSecao()
        {
            using (var client = new HttpClient())
            {
                var login = new
                {
                    UserLogin = "webservice",
                    Password = "d10820j21s11!!1429ej21"
                };

                var content = new StringContent(JsonSerializer.ToJsonString(login), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(_otrsUrl, content);
                return await result.Content.ReadAsStringAsync();
            }
        }

        public async Task<UsuarioOTRS> ObterUsuarioOTRS(string usuario)
        {
            UsuarioOTRS usuarioOtrs = new UsuarioOTRS();
            string session = await GerarSecao();
            var sessionId = JsonSerializer.Deserialize<dynamic>(session)["SessionID"];
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(_otrsUsuario + usuario + "?SessionID=" + sessionId);
                if (result.IsSuccessStatusCode)
                {
                    var body = Utf8Json.JsonSerializer.Deserialize<dynamic>(await result.Content.ReadAsStringAsync());
                    if (body.ContainsKey("CustomerUserDataGet"))
                    {
                        usuarioOtrs.UserMailString = body["CustomerUserDataGet"][0]["UserMailString"];
                    };
                }
                return usuarioOtrs;
            }
        }

        public async Task<AtendimentoOTRS> GerarTicket(string usuario)
        {
            var usuarioCoBra = _usuarioRepository.ObterPorContaAd(usuario);
            var usuarioOtrs = await ObterUsuarioOTRS(usuario);
            AtendimentoOTRS ticket = new AtendimentoOTRS();

            if (string.IsNullOrEmpty(usuarioOtrs.UserMailString))
            {
                ticket.Message = "Usuário não cadastrado no Helpdesk Editora do Brasil. Por favor solicite acesso ao departamento de TI.";
                return ticket;
            }

            if (string.IsNullOrEmpty(usuarioCoBra.Nome))
            {
                ticket.Message = "Usuário não cadastrado no CoBra. Por favor solicite acesso ao departamento de TI.";
                return ticket;
            }

            if (string.IsNullOrEmpty(usuario))
            {
                ticket.Message = "Usuário deve ser enviado.";
                return ticket;
            }

            string session = await GerarSecao();
            var sessionId = JsonSerializer.Deserialize<dynamic>(session)["SessionID"];
            ticket.UserLogin = "webservice";
            ticket.Password = "d10820j21s11!!1429ej21";
            if (!string.IsNullOrEmpty(sessionId))
            {
                ticket.SessionID = sessionId;
                ticket.Ticket.CustomerUser = usuario;
                ticket.Article.Subject = ticket.Ticket.Title;
                ticket.Article.From = usuarioOtrs.UserMailString;
            }
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonSerializer.ToJsonString(ticket), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(_otrsTicket, content);
                var ticketNumber = Utf8Json.JsonSerializer.Deserialize<dynamic>(await result.Content.ReadAsStringAsync())["TicketNumber"];
                ticket.Ticket.Number = int.Parse(ticketNumber);
                ticket.Message = "Ticket criado para equipe de TI.Você pode acompanhar o seu chamado acessando: https: https://helpdesk.editoradobrasil.com.br/otrs/customer.pl \nNumero do Ticket:" + ticketNumber;
                return ticket;

            }
        }






    }
}
