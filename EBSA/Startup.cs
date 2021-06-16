using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using CoBRA.API.Authorize;
using CoBRA.API.Providers;
using CoBRA.Application;
using CoBRA.Application.Bases;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using CoBRA.Infra.Corpore;
using CoBRA.Infra.CrossCutting.EmailService.Interfaces;
using CoBRA.Infra.CrossCutting.EmailService.Services;
using CoBRA.Infra.CrossCutting.OTRSService.Interfaces;
using CoBRA.Infra.CrossCutting.OTRSService.Services;
using CoBRA.Infra.Intranet;
using CoBRA.Infra.Intranet.Base;
using CoBRA.Infra.Protheus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace CoBRA
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(x => Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Swagger CoBra",
                    Description = "Swagger CoBra",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Cabeçalho de autorização JWT utilizando Bearer token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });


            });

            ConfigureiOC(services);

            var config = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<RequisicaoArquivosViewModel, RequisicaoArquivos>();
                configuration.CreateMap<RequisicaoComprasViewModel, RequisicaoCompras>();
                configuration.CreateMap<ComissaoViewModel, Comissao>();
                configuration.CreateMap<DepartamentoViewModel, Departamento>();
                configuration.CreateMap<CentroCustoViewModel, CentroCusto>();
                configuration.CreateMap<ObraViewModel, Obra>();
                configuration.CreateMap<EstadoViewModel, Estado>();
                configuration.CreateMap<FilialViewModel, Filial>();
                configuration.CreateMap<FornecedorViewModel, Fornecedor>();
                configuration.CreateMap<GrupoViewModel, Grupo>();
                configuration.CreateMap<CargoViewModel, Cargo>();
                configuration.CreateMap<CargosViewModel, Cargos>();
                configuration.CreateMap<NivelViewModel, Nivel>();
                configuration.CreateMap<ColaboradorViewModel, Colaborador>();
                configuration.CreateMap<MenuViewModel, Menu>();
                configuration.CreateMap<PeriodoViewModel, Periodo>();
                configuration.CreateMap<RequisicaoElvisViewModel, RequisicaoElvis>();
                configuration.CreateMap<RequisicaoComprasViewModel, RequisicaoCompras>();
                configuration.CreateMap<RequisicaoGeradaViewModel, RequisicaoGerada>();
                configuration.CreateMap<RequisicaoObraViewModel, RequisicaoObra>();
                configuration.CreateMap<ParcelaRequisicaoViewModel, ParcelaRequisicao>();
                configuration.CreateMap<ServicoViewModel, Servico>();
                configuration.CreateMap<UsuarioViewModel, Usuario>();
                configuration.CreateMap<UsuarioRamalViewModel, UsuarioRamal>();
                configuration.CreateMap<LogViewModel, Log>();
                configuration.CreateMap<ParametroPesquisaViewModel, ParametroPesquisa>();
                configuration.CreateMap<ParametroPesquisa, ParametroPesquisaViewModel>();

                configuration.CreateMap<CabecalhoPainelMeta, CabecalhoPainelMetaViewModel>();
                configuration.CreateMap<CabecalhoPainelMetaViewModel, CabecalhoPainelMeta>();
                configuration.CreateMap<LinhaPainelMetaViewModel, LinhaPainelMeta>();
                configuration.CreateMap<LinhaPainelMeta, LinhaPainelMetaViewModel>();
                configuration.CreateMap<DadosOrigemPainel, DadosOrigemPainelViewModel>();
                configuration.CreateMap<DadosOrigemPainelViewModel, DadosOrigemPainel>();

                configuration.CreateMap<ObraOrcamento, ObraOrcamentoViewModel>();
                configuration.CreateMap<ObraOrcamentoViewModel, ObraOrcamento>();

                configuration.CreateMap<CabecalhoPainelMeta, AprovacaoGrupoViewModel>()
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(
                        src => src.StatusPainel.Descricao)
                    )
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));


                configuration.CreateMap<LinhaPainelMeta, LinhaAprovacaoGrupoViewModel>()
                    .ForMember(dest => dest.OrigemDados, opt => opt.MapFrom(src => src.DadosOrigemPainel.Descricao))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdLinhaPainelMeta));

                configuration.CreateMap<CabecalhoPainelMeta, LinhaPainelIndicadorMetaViewModel>()
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(
                        src => src.StatusPainel.Descricao)
                    )
                    .ForMember(dest => dest.Grupo, opt => opt.MapFrom(src => src.GrupoPainel.Descricao))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));



                configuration.CreateMap<StatusPainel, StatusPainelViewModel>();
                configuration.CreateMap<GrupoPainel, GrupoPainelViewModel>();
                configuration.CreateMap<ResultadoPagamentoMeta, ResultadoPagamentoMetaViewModel>();
                configuration.CreateMap<PainelMetaAnual, PainelMetaAnualViewModel>();
                configuration.CreateMap<PainelMetaReal, PainelMetaRealViewModel>();
                configuration.CreateMap<SistemaOrigem, SistemaOrigemViewModel>();
                configuration.CreateMap<UnidadeMedida, UnidadeMedidaViewModel>();
                configuration.CreateMap<TabelaOrigem, TabelaOrigemViewModel>();
                configuration.CreateMap<CampoOrigem, CampoOrigemViewModel>();
                configuration.CreateMap<Cargo, LookupDto>()
                    .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.IdCargo))
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Descricao));

                configuration.CreateMap<LookupUsuario, LookupDto>()
                    .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.UsuarioId))
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Nome));

                configuration.CreateMap<PainelMetaAnual, MetaIndividualViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUsuario))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Situacao));


                configuration.CreateMap<MetaIndividualViewModel, PainelMetaAnual>();

                configuration.CreateMap<AcompanhamentoMetaConsultorMetaIndividualViewModel, PainelMetaAnual>();
                configuration.CreateMap<PainelMetaAnual, AcompanhamentoMetaConsultorMetaIndividualViewModel>();

                configuration.CreateMap<AprovacaoMetaFinanceiraViewModel, PainelMetaFinanceira>();
                configuration.CreateMap<PainelMetaFinanceira, AprovacaoMetaFinanceiraViewModel>();

                configuration.CreateMap<AcompanhamentoMetaConsultorMetaIndividualViewModel, PainelMetaFinanceira>();
                configuration.CreateMap<PainelMetaFinanceira, AcompanhamentoMetaConsultorMetaIndividualViewModel>();

                configuration.CreateMap<RegiaoConsultorViewModel, RegiaoConsultor>();
                configuration.CreateMap<RegiaoConsultor, RegiaoConsultorViewModel>();

                configuration.CreateMap<Comissao, ComissaoViewModel>();
                configuration.CreateMap<ArquivoAutorBeneficiarioViewModel, ArquivoAutorBeneficiario>();
                configuration.CreateMap<ArquivoAutorBeneficiario, ArquivoAutorBeneficiarioViewModel>();
                configuration.CreateMap<AutorDAViewModel, AutorDA>();
                configuration.CreateMap<AutorDA, AutorDAViewModel>();
                configuration.CreateMap<EmailAutorBeneficiarioViewModel, EmailAutorBeneficiario>();
                configuration.CreateMap<EmailAutorBeneficiario, EmailAutorBeneficiarioViewModel>();

                configuration.CreateMap<Amarracao, AmarracaoViewModel>();
                configuration.CreateMap<AmarracaoViewModel, Amarracao>();
                configuration.CreateMap<TermoAditivo, TermoAditivoViewModel>();
                configuration.CreateMap<TermoAditivoViewModel, TermoAditivo>();
                configuration.CreateMap<LogAmarracao, LogAmarracaoViewModel>();
                configuration.CreateMap<LogAmarracaoViewModel, LogAmarracao>();
                configuration.CreateMap<AmarracaoAutor, AmarracaoAutorViewModel>();
                configuration.CreateMap<AmarracaoAutorViewModel, AmarracaoAutor>();
                configuration.CreateMap<RegraPagamento, RegraPagamentoViewModel>();
                configuration.CreateMap<RegraPagamentoViewModel, RegraPagamento>();
                configuration.CreateMap<PrazoValidade, PrazoValidadeViewModel>();
                configuration.CreateMap<PrazoValidadeViewModel, PrazoValidade>();
                configuration.CreateMap<BloqueioPagamento, BloqueioPagamentoViewModel>();
                configuration.CreateMap<BloqueioPagamentoViewModel, BloqueioPagamento>();
                configuration.CreateMap<TipoContrato, TipoContratoViewModel>();
                configuration.CreateMap<TipoContratoViewModel, TipoContrato>();
                configuration.CreateMap<TipoParticipacao, TipoParticipacaoViewModel>();
                configuration.CreateMap<TipoParticipacaoViewModel, TipoParticipacao>();

                configuration.CreateMap<LogCorrespAutorBeneficiarioViewModel, LogCorrespAutorBeneficiario>();
                configuration.CreateMap<LogCorrespAutorBeneficiario, LogCorrespAutorBeneficiarioViewModel>();
                configuration.CreateMap<CorrespondenciaDA, CorrespondenciaDAViewModel>();
                configuration.CreateMap<CorrespondenciaDAViewModel, CorrespondenciaDA>();
                configuration.CreateMap<NomeCapa, NomeCapaViewModel>();
                configuration.CreateMap<NomeCapaViewModel, NomeCapa>();
                configuration.CreateMap<AutoresBeneficiariosPaginado, AutoresBeneficiariosPaginadoViewModel>();
                configuration.CreateMap<AutoresBeneficiariosPaginadoViewModel, AutoresBeneficiariosPaginado>();
                configuration.CreateMap<AutoresBeneficiarios, AutoresBeneficiariosViewModel>();
                configuration.CreateMap<AutoresBeneficiariosViewModel, AutoresBeneficiarios>();
                configuration.CreateMap<BeneficiarioDA, BeneficiarioDAViewModel>();
                configuration.CreateMap<BeneficiarioDAViewModel, BeneficiarioDA>();
                configuration.CreateMap<AutorSimplificado, AutorSimplificadoViewModel>();
                configuration.CreateMap<AutorSimplificadoViewModel, AutorSimplificado>();
                configuration.CreateMap<LogAutorBeneficiarioViewModel, LogAutorBeneficiario>();
                configuration.CreateMap<LogAutorBeneficiario, LogAutorBeneficiarioViewModel>();
                configuration.CreateMap<CentroCusto, CentroCustoViewModel>();
                configuration.CreateMap<Obra, ObraViewModel>();
                configuration.CreateMap<Departamento, DepartamentoViewModel>();
                configuration.CreateMap<Estado, EstadoViewModel>();
                configuration.CreateMap<Filial, FilialViewModel>();
                configuration.CreateMap<Fornecedor, FornecedorViewModel>();
                configuration.CreateMap<Grupo, GrupoViewModel>();
                configuration.CreateMap<Cargo, CargoViewModel>();
                configuration.CreateMap<Cargos, CargosViewModel>();
                configuration.CreateMap<Nivel, NivelViewModel>();
                configuration.CreateMap<Colaborador, ColaboradorViewModel>();
                configuration.CreateMap<Menu, MenuViewModel>();
                configuration.CreateMap<Periodo, PeriodoViewModel>();
                configuration.CreateMap<RequisicaoElvis, RequisicaoElvisViewModel>();
                configuration.CreateMap<RequisicaoArquivos, RequisicaoArquivosViewModel>();
                configuration.CreateMap<RequisicaoCompras, RequisicaoComprasViewModel>();
                configuration.CreateMap<RequisicaoGerada, RequisicaoGeradaViewModel>();
                configuration.CreateMap<RequisicaoObra, RequisicaoObraViewModel>();
                configuration.CreateMap<ParcelaRequisicao, ParcelaRequisicaoViewModel>();
                configuration.CreateMap<Servico, ServicoViewModel>();
                configuration.CreateMap<Usuario, UsuarioViewModel>();
                configuration.CreateMap<UsuarioRamal, UsuarioRamalViewModel>();
                configuration.CreateMap<Log, LogViewModel>();
                configuration.CreateMap<StatusPainelViewModel, StatusPainel>();
                configuration.CreateMap<GrupoPainelViewModel, GrupoPainel>();
                configuration.CreateMap<ResultadoPagamentoMetaViewModel, ResultadoPagamentoMeta>();
                configuration.CreateMap<PainelMetaAnualViewModel, PainelMetaAnual>();
                configuration.CreateMap<PainelMetaRealViewModel, PainelMetaReal>();
                configuration.CreateMap<SistemaOrigemViewModel, SistemaOrigem>();
                configuration.CreateMap<UnidadeMedidaViewModel, UnidadeMedida>();
                configuration.CreateMap<TabelaOrigemViewModel, TabelaOrigem>();
                configuration.CreateMap<CampoOrigemViewModel, CampoOrigem>();
                configuration.CreateMap<AprovacaoGrupoViewModel, CabecalhoPainelMeta>();
                configuration.CreateMap<PainelMetaFinanceira, MetaFinanceiraViewModel>()
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(
                        src => src.Situacao)
                    )
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdMetaFinanceira));

                configuration.CreateMap<MetaFinanceiraViewModel, PainelMetaFinanceira>()
                    .ForMember(dest => dest.MetaReceitaLiquida, opt
                        => opt.MapFrom(src => src.MetaReceitaLiquida))
                    .ForMember(dest => dest.ValorRecebimento, opt
                        => opt.MapFrom(src => src.ValorRecebimento));

                configuration.CreateMap<Produto, ProdutoViewModel>();
                configuration.CreateMap<ProdutoViewModel, Produto>();

                configuration.CreateMap<Produtos, ProdutosViewModel>();
                configuration.CreateMap<ProdutosViewModel, Produtos>();

                configuration.CreateMap<ProdutosDocumentosAutorias, ProdutosDocumentosAutoriasViewModel>();
                configuration.CreateMap<ProdutosDocumentosAutoriasViewModel, ProdutosDocumentosAutorias>();

                configuration.CreateMap<ProdutoControleConteudo, ProdutoControleConteudoViewModel>();
                configuration.CreateMap<ProdutoControleConteudoViewModel, ProdutoControleConteudo>();

                configuration.CreateMap<Assuntos, AssuntosViewModel>();
                configuration.CreateMap<AssuntosViewModel, Assuntos>();

                configuration.CreateMap<Autores, AutoresViewModel>();
                configuration.CreateMap<AutoresViewModel, Autores>();

                configuration.CreateMap<Conteudos, ConteudosViewModel>();
                configuration.CreateMap<ConteudosViewModel, Conteudos>();

                configuration.CreateMap<CadastroProduto, CadastroProdutoViewModel>();
                configuration.CreateMap<CadastroProdutoViewModel, CadastroProduto>();

                configuration.CreateMap<Dicionario, DicionarioViewModel>();
                configuration.CreateMap<DicionarioViewModel, Dicionario>();

                configuration.CreateMap<PermissaoRequisicao, PermissaoRequisicaoViewModel>();
                configuration.CreateMap<PermissaoRequisicaoViewModel, PermissaoRequisicao>();

                configuration.CreateMap<RelatorioElvisViewModel, RelatorioElvis>();
                configuration.CreateMap<RelatorioElvis, RelatorioElvisViewModel>();

                configuration.CreateMap<ObraAndamento, ObraAndamentoViewModel>();
                configuration.CreateMap<ObraAndamentoViewModel, ObraAndamento>();

                configuration.CreateMap<FiltroRelatorioElvis, FiltroRelatorioElvisViewModel>();
                configuration.CreateMap<FiltroRelatorioElvisViewModel, FiltroRelatorioElvis>();

                configuration.CreateMap<FiltroObraAndamento, FiltroObraAndamentoViewModel>();
                configuration.CreateMap<FiltroObraAndamentoViewModel, FiltroObraAndamento>();

            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                        .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto


            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());

                auth.AddPolicy("MenuAcesso", Policy => Policy.Requirements.Add(new PerfilRequirement()));

            });


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAuthorizationHandler, AccessPerfilHandler>();

            services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
        }

        private void ConfigureiOC(IServiceCollection services)
        {
            services.AddScoped<IElvisAppService, ElvisAppService>();
            services.AddScoped<IProdutoCUPAppService, ProdutoCUPAppService>();

            services.AddScoped<IAutorBeneficiarioAppService, AutorBeneficiarioAppService>();
            services.AddScoped<IAutorBeneficiarioRepository, AutorBeneficiarioRepository>();

            services.AddScoped<IAmarracaoAppService, AmarracaoAppService>();
            services.AddScoped<IAmarracaoRepository, AmarracaoRepository>();

            services.AddScoped<IRequisicaoComprasAppService, RequisicaoComprasAppService>();
            services.AddScoped<IPainelIndicadorMetaAppService, PainelIndicadorMetaAppService>();
            services.AddScoped<IPainelMetaFinanceiraAppService, PainelMetaFinanceiraAppService>();
            services.AddScoped<IPainelIndividualAppService, PainelIndividualAppService>();
            services.AddScoped<IAprovacaoMetaFinanceiraAppService, AprovacaoMetaFinanceiraAppService>();
            services.AddScoped<IAprovacaoIndividualAppService, AprovacaoIndividualAppService>();
            services.AddScoped<IAprovacaoGrupoAppService, AprovacaoGrupoAppService>();
            services.AddScoped<ILoginAppService, LoginAppService>();
            services.AddScoped<IDepartamentoAppService, DepartamentoAppService>();
            services.AddScoped<IGrupoAppService, GrupoAppService>();
            services.AddScoped<ICargoAppService, CargoAppService>();
            services.AddScoped<INivelAppService, NivelAppService>();
            services.AddScoped<IColaboradorAppService, ColaboradorAppService>();
            services.AddScoped<IInicializacaoAppService, InicializacaoAppService>();
            services.AddScoped<IMenuAppService, MenuAppService>();
            services.AddScoped<IRelatorioAppService, RelatorioAppService>();
            services.AddScoped<IRequisicaoElvisAppService, RequisicaoElvisAppService>();
            services.AddScoped<IRequisicaoGenericaAppService, RequisicaoGenericaAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IRegraAppService, RegraAppService>();
            services.AddScoped<ILogAppService, LogAppService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICadastroMetaFinanceiraAppService, CadastroMetaFinanceiraAppService>();

            services.AddScoped<IAcompanhamentoMetaConsultorAppService, AcompanhamentoMetaConsultorAppService>();
            services.AddScoped<IAcompanhamentoMetaLookupAppService, AcompanhamentoMetaLookupAppService>();
            services.AddScoped<IDadosOrigemPainelRepository, DadosOrigemPainelRepository>();
            services.AddScoped<IDadosOrigemPainelAppService, DadosOrigemPainelAppService>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IServicoRepository, ServicoRepository>();
            services.AddScoped<IElvisRepository, ElvisRepository>();
            services.AddScoped<IRequisicaoCompraRepository, RequisicaoCompraRepository>();
            services.AddScoped<IPainelIndicadorMetaRepository, PainelRepository>();
            services.AddScoped<IAprovacaoMetaFinanceiraRepository, AprovacaoMetaFinanceiraRepository>();
            services.AddScoped<IAprovacaoIndividualRepository, AprovacaoIndividualRepository>();
            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            services.AddScoped<IGrupoRepository, GrupoRepository>();
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<INivelRepository, NivelRepository>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<IInicializacaoRepository, InicializacaoRepository>();
            services.AddScoped<IRegraRepository, RegraRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IRelatorioRepository, RelatorioRepository>();
            services.AddScoped<IRequisicaoABDRepository, RequisicaoABDRepository>();
            services.AddScoped<IRequisicaoGenericaRepository, RequisicaoGenericaRepository>();
            services.AddScoped<IFornecedoresProtheusRepository, FornecedoresProtheusRepository>();
            services.AddScoped<IUsuarioCorporeRepository, UsuarioCorporeRepository>();
            services.AddScoped<IPainelRepository, PainelRepository>();
            services.AddScoped<IRelatorioProtheusRepository, RelatorioProtheusRepository>();
            services.AddScoped<IGrupoPainelRepository, GrupoPainelRepository>();
            services.AddScoped<IGrupoPainelAppService, GrupoPainelAppService>();
            services.AddScoped<IResultadoPagamentoMetaRepository, ResultadoPagamentoMetaRepository>();
            services.AddScoped<IResultadoPagamentoMetaAppService, ResultadoPagamentoMetaAppService>();
            services.AddScoped<IPainelMetaAnualRepository, PainelMetaAnualRepository>();
            services.AddScoped<IPainelMetaAnualAppService, PainelMetaAnualAppService>();
            services.AddScoped<IPainelMetaRealRepository, PainelMetaRealRepository>();
            services.AddScoped<IPainelMetaRealAppService, PainelMetaRealAppService>();
            services.AddScoped<ISistemaOrigemRepository, SistemaOrigemRepository>();
            services.AddScoped<ISistemaOrigemAppService, SistemaOrigemAppService>();
            services.AddScoped<IUnidadeMedidaRepository, UnidadeMedidaRepository>();
            services.AddScoped<IUnidadeMedidaAppService, UnidadeMedidaAppService>();
            services.AddScoped<ITabelaOrigemRepository, TabelaOrigemRepository>();
            services.AddScoped<ITabelaOrigemAppService, TabelaOrigemAppService>();

            services.AddScoped<ICampoOrigemRepository, CampoOrigemRepository>();
            services.AddScoped<ICampoOrigemAppService, CampoOrigemAppService>();

            services.AddScoped<IRegiaoConsultorRepository, RegiaoConsultorRepository>();
            services.AddScoped<IRegiaoConsultorAppService, RegiaoConsultorAppService>();

            services.AddScoped<INetlitRepository, NetlitRepository>();
            services.AddScoped<INetlitAppService, NetlitAppService>();

            services.AddScoped<IObraOrcamentoRepository, ObraOrcamentoRepository>();
            services.AddScoped<IObraOrcamentoAppService, ObraOrcamentoAppService>();

            services.AddScoped<IPainelAppService, PainelAppService>();
            services.AddScoped<ICadastroMetaFinanceiraRepository, CadastroMetaFinanceiraRepository>();
            services.AddScoped<IPainelMetaFinanceiraRepository, PainelMetaFinanceiraRepository>();
            services.AddScoped<IAprovacaoGrupoRepository, AprovacaoGrupoRepository>();
            services.AddScoped<ILookupUsuarioRepository, LookupUsuarioRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<ICadastroProdutoRepository, CadastroProdutoRepository>();
            services.AddScoped<ICadastroProdutoService, CadastroProdutoService>();

            services.AddScoped<IIntranetRepository, IntranetRepository>();

            services.AddScoped<IPermissaoRequisicaoRepository, PermissaoRequisicaoRepository>();
            services.AddScoped<IProdutoCUPRepository, ProdutoCUPRepository>();
            services.AddScoped<IAnoEducacaoCUPRepository, AnoEducacaoCUPRepository>();
            services.AddScoped<IPermissaoRequisicaoService, PermissaoRequisicaoService>();

            services.AddScoped<IRelatorioElvisRepository, RelatorioElvisRepository>();
            services.AddScoped<IRelatorioElvisAppService, RelatorioElvisAppService>();

            services.AddScoped<IBaseRepositoryPainelMeta, BaseRepositoryPainelMeta>(x => new BaseRepositoryPainelMeta(
                conexao: Configuration.GetConnectionString("ConnEBSA")
            ));

            services.AddScoped<IBaseRepositoryIntranet, BaseRepositoryIntranet>(x => new BaseRepositoryIntranet(
                conexao: Configuration.GetConnectionString("ConnProtheus")
            ));

            var serviceProvider = services.BuildServiceProvider();



            services.AddScoped<IOTRSService, OTRSService>(x => new OTRSService(
                               otrsUrl: Configuration.GetSection("EDITORA-HELPDESK").GetSection("API").Value,
                               otrsTicket: Configuration.GetSection("EDITORA-HELPDESK").GetSection("TICKET").Value,
                               otrsUsuario: Configuration.GetSection("EDITORA-HELPDESK").GetSection("USUARIO").Value,
                               (IUsuarioRepository)serviceProvider.GetService(typeof(IUsuarioRepository))));

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var cultureInfo = new CultureInfo("pt-BR");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("Cors");
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "API Test Version 1");
            });
        }
    }
}

