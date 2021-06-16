using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using CoBRA.Infra.CrossCutting.EmailService.Interfaces;
using CoBRA.Infra.CrossCutting.EmailService.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CoBRA.Application
{
    public class LoginAppService : ILoginAppService
    {

        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService;
        private IConfiguration _configuration;

        public LoginAppService(IMapper mapper, IUsuarioRepository usuarioRepository, IEmailService emailService, IConfiguration configuration)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
            _configuration = configuration;
        }
        public object Login(UsuarioViewModel usuario)
        {
            
                throw new NotImplementedException();
        }

        public bool ValidaEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public bool ValidaSenha(string senha)
        {
            Match match = Regex.Match(senha, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
            return match.Success;
        }

        public string EsqueceuSenha(string email)
        {
            var usuario = _usuarioRepository.ObterPorEmail(email);
            var emailEnviado = new EmailViewModel();
            if (!string.IsNullOrEmpty(usuario.ContaAd))
            {
                string guid = _usuarioRepository.GerarGuid(usuario);

                emailEnviado.Assunto = "Recuperar Senha";
                emailEnviado.Destinatario = usuario.Email;
                string path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"CorpoHtml\esqueci-senha\index.html");
                string corpoEmail = File.ReadAllText(path, Encoding.UTF8);
                corpoEmail = corpoEmail.Replace("__nome__", usuario.Nome);
                corpoEmail = corpoEmail.Replace("__link__", _configuration["Environment"] == "Teste" ? "http://localhost:4200/#/login/" + guid : "https://cobra.editoradobrasil.com/" + guid);
                emailEnviado.CorpoEmail = corpoEmail;
                _emailService.EnviarEmail(emailEnviado);

            } else
            {
                return "Email não encontrado";
            }

            return "Encaminhado com sucesso! Verifique seu e-mail.";
        }

        public UsuarioViewModel ObterUsuarioGuid(string guid)
        {
            var usuario = _mapper.Map<UsuarioViewModel>(_usuarioRepository.ObterPorGuid(guid));
            return usuario;
        }

        public bool ExpiraGuid(UsuarioViewModel usuario)
        {
            _usuarioRepository.ExpiraGuid(usuario.Id);
            return true;
        }

    }
}
