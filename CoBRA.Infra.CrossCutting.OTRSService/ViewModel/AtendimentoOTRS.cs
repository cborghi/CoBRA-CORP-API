using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Infra.CrossCutting.OTRSService.ViewModel
{
    public class AtendimentoOTRS
    {
        public string UserLogin { get; set; }
        public string Password {get; set; }
        public string SessionID { get; set; }

        public AtendimentoOTRS()
        {
            this.Article = new ArticleOTRS
            {
                CommunicationChannel = "Email",
                IsVisibleForCustomer = 1,
                From = "helpdesk@editoradobrasil.com.br",
                MimeType = "text/html",
                Charset = "utf-8",
                HistoryType = "AddNote",
                TimeUnit = 10,
                Body = "Solicito regravação de senha do E-mail.",
            };

            this.Ticket = new TicketOTRS
            {
                Title = "Solicitação de regravação de senha do E-mail",
                Queue = "Tecnologia da Informação - TI::Acessos",
                Lock = "unlock",
                SLA = "SLA - 16h (Inicial - 4h)",
                Type = "Requisição",
                State = "new",
                PriorityID = "5",
                Service = "Tecnologia da Informação - TI::Acessos::Usuário de Rede e/ou E-mail"

            };
        }
        public string Message { get; set; }
        public ArticleOTRS Article {get; set;}
        public TicketOTRS Ticket { get; set; }
    }
}
