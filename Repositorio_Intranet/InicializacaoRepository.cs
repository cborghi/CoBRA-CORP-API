using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;

namespace CoBRA.Infra.Intranet
{
    public class InicializacaoRepository : BaseRepository, IInicializacaoRepository
    {
        public InicializacaoRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Menu> ObterMenu()
        {
            string query = @"SELECT m.ID_MENU,m.DESCRICAO MENUDESC,m.LINK,m.ROTA, p.ID_GRUPO, p.DESCRICAO as PERFILDESC FROM GRUPO p
                                   INNER JOIN MENU_GRUPO mp ON p.ID_GRUPO = mp.ID_GRUPO
                                   INNER JOIN MENU m ON m.ID_MENU = mp.ID_MENU
                                   WHERE p.ATIVO = 1 and m.ATIVO = 1";

            List<Menu> listaMenus = new List<Menu>();

            using (SqlDataReader dataReader = GetDataReader(query, new List<SqlParameter>(), CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    Menu menu = new Menu();
                    int objetoAtual = 0;

                    while (dataReader.Read())
                    {
                        int idAtual = Convert.ToInt32(dataReader["ID_MENU"]);

                        if (objetoAtual != idAtual)
                        {
                            if (objetoAtual > 0)
                            {
                                listaMenus.Add(menu);
                                menu = new Menu();
                            }
                            new Menu();

                            menu.Id = idAtual;
                            menu.Descricao = dataReader["MENUDESC"].ToString();
                            menu.Link = dataReader["LINK"].ToString();
                            menu.Rota = dataReader["ROTA"].ToString();

                            objetoAtual = idAtual;
                        }
                    };
                    listaMenus.Add(menu);
                }
            }
            return listaMenus;
        }

        public List<Estado> ObterEstados()
        {
            string query = @"SELECT ID_ESTADO,DESCRICAO FROM ESTADO";

            List<Estado> listaEstados = new List<Estado>();

            using (SqlDataReader dataReader = GetDataReader(query, new List<SqlParameter>(), CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        listaEstados.Add(new Estado()
                        {
                            Id = Convert.ToInt32(dataReader["ID_ESTADO"]),
                            Descricao = dataReader["DESCRICAO"].ToString()
                        });
                    }
                }
            }
            return listaEstados;
        }

        public List<Periodo> ObterPeriodos()
        {
            string query = @"SELECT ID_PERIODO,DESCRICAO, qntMes FROM PERIODO";

            List<Periodo> listaPeriodos = new List<Periodo>();

            using (SqlDataReader dataReader = GetDataReader(query, new List<SqlParameter>(), CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        listaPeriodos.Add(new Periodo()
                        {
                            Id = Convert.ToInt32(dataReader["ID_PERIODO"]),
                            Descricao = dataReader["DESCRICAO"].ToString(),
                            QntMes = Convert.ToInt32(dataReader["qntMes"])
                        });
                    }
                }
            }
            return listaPeriodos;
        }

        public string ObterPrevisaoDoTempo(string latitude, string longetude)
        {
            var result = "Erro ao obter a previsão do tempo";
            var url = ApiKeyPrevisaoDoTempo().Replace("[lat]", latitude).Replace("[lon]", longetude);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }

        public List<Usuario> ObterRamais()
        {
            try
            {
                string query = @"SELECT U.NOME, U.RAMAL, G.DEPARTAMENTO FROM USUARIO U
                        INNER JOIN GRUPO G ON G.ID_GRUPO = U.ID_GRUPO";

                List<Usuario> listaUsuariosRamais = new List<Usuario>();

                using (SqlDataReader dataReader = GetDataReader(query, new List<SqlParameter>(), CommandType.Text))
                {
                    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;

                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            var nome = dataReader["NOME"].ToString().ToLower();
                            var departamento = dataReader["DEPARTAMENTO"].ToString().ToLower();
                            listaUsuariosRamais.Add(new Usuario()
                            {
                                Nome = textInfo.ToTitleCase(nome),
                                Ramal = dataReader["RAMAL"].ToString(),
                                Grupo = new Grupo()
                                {
                                    Departamento = textInfo.ToTitleCase(departamento)
                                }
                            });
                        }
                    }
                }
                return listaUsuariosRamais;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
