using Cobra.Console.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Cobra.Console
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static CargaUsuarioRM _cargaUsuarioRM;
        private static IntegraOrigemDados _integraOrigemDados;
        private static IntegraConsultorHierarquia _integraConsultor;
        private static IntegraRegiao _integraRegiao;
        private static IntegraConsultorRegiao _integraConsultorRegiao;
        private static RepositoryBase _repositoryBase;
        private static GravaLog _gravaLog;
        private static ExecutaScript _executaScript;

        static async Task Main(string[] args)
        {
            try
            {
                CarregarConfiguracoes();
                CarregarStringConexao();
                CarregarClasses();
                //await ExecutaScript();

                await IntegrarUsuariosRMIntermediario();
                await IntegrarUsuarioRMFinal();
                await IntegrarDadosOrigem();
                await IntegrarUsuarioHierarquia();
                await IntegrarRegiao();
                await IntegrarRegiaoAtuacao();
            }
            catch (Exception ex)
            {
                await _gravaLog.GravarLog(ex);
            }
        }

        private static async Task ExecutaScript()
        {
            await _executaScript.ExecutarScript();
        }

        private static async Task IntegrarDadosOrigem()
        {
            System.Console.WriteLine("Realizando integração de origem dos dados...");
            await _integraOrigemDados.ExecutarIntegracaoOrigemDados();
        }

        private static async Task IntegrarUsuariosRMIntermediario() 
        {
            System.Console.WriteLine("Realizando integração usuário intermediaria...");
            await _cargaUsuarioRM.ExecutarCargaUsuarioIntermediaria();
        }

        private static async Task IntegrarUsuarioRMFinal()
        {
            System.Console.WriteLine("Realizando integração usuário final...");
            await _cargaUsuarioRM.ExecutarCargaUsuarioFinal();
        }

        private static async Task IntegrarUsuarioHierarquia()
        {
            System.Console.WriteLine("Realizando integração de hierarquia...");
            await _integraConsultor.IntregrarUsuarioHierarquia();
        }

        private static async Task IntegrarRegiao()
        {
            System.Console.WriteLine("Realizando integração de região...");
            await _integraRegiao.IntegrarRegiao();
        }

        private static async Task IntegrarRegiaoAtuacao()
        {
            System.Console.WriteLine("Realizando integração região de atuação...");
            await _integraConsultorRegiao.IntegrarUsuarioRegiaoAtuacao();
        }

        private static void CarregarStringConexao() 
        {
            System.Console.WriteLine("Carregando conexões...");
            string conexaoCorpore = _configuration.GetConnectionString("ConnCorpore");
            string conexaoEBSA = _configuration.GetConnectionString("ConnEBSA");
            string conexaoProtheus = _configuration.GetConnectionString("ConnProtheus");
            string nomeBancoProtheus = _configuration.GetSection("NomeBanco").GetSection("Protheus").Value;
            _repositoryBase = new RepositoryBase(conexaoEBSA, conexaoCorpore, conexaoProtheus, nomeBancoProtheus);
        }

        private static void CarregarConfiguracoes()
        {
            System.Console.WriteLine("Carregando arquivo de configuração...");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        private static void CarregarClasses() {
            System.Console.WriteLine("Carregando classes...");
            _cargaUsuarioRM = new CargaUsuarioRM(_repositoryBase);
            _integraOrigemDados = new IntegraOrigemDados(_repositoryBase);
            _integraConsultor = new IntegraConsultorHierarquia(_repositoryBase);
            _integraRegiao = new IntegraRegiao(_repositoryBase);
            _integraConsultorRegiao = new IntegraConsultorRegiao(_repositoryBase);
            _gravaLog = new GravaLog(_repositoryBase);
            _executaScript = new ExecutaScript(_repositoryBase, _gravaLog);
        }
    }
}
