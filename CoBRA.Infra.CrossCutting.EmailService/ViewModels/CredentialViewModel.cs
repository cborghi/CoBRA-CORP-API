using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Infra.CrossCutting.EmailService.ViewModels
{
    public class CredentialViewModel
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
