using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace CoBRA.Infra.Intranet
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public UsuarioRepository(IConfiguration configuration, IBaseRepositoryPainelMeta baseRepository) : base(configuration)
        {
            _baseRepository = baseRepository;
        }

        public Usuario Login(string conta, bool email = false)
        {
            string queryEmail = email ? "EMAIL = @CONTA" : "CONTA_AD = @CONTA";
            string protheusAmbiente = ProdEnvironment() ? "[Protheus]" : "[Protheus_Dev]";

            string consulta = _baseRepository.BuscarArquivoConsulta("Login")
                .Replace("__protheusAmbiente__", protheusAmbiente)
                .Replace("__queryEmail__", queryEmail);

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@CONTA", ((Object)conta) ?? DBNull.Value);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderUsuario(reader, queryEmail, protheusAmbiente);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();

                    command.Dispose();
                }
            }

        }

        private Usuario LerDataReaderUsuario(SqlDataReader reader, string queryEmail, string protheusAmbiente)
        {
            Usuario usuario = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    usuario = new Usuario()
                    {
                        Id = Convert.ToInt32(reader["ID_USUARIO"]),
                        Nome = reader["NOME"].ToString(),
                        Email = reader["EMAIL"].ToString(),
                        ContaAd = reader["CONTA_AD"].ToString(),
                        CodUsuario = reader["COD_USUARIO"].ToString(),
                        Grupo = new Grupo()
                        {
                            Id = Convert.ToInt32(reader["ID_GRUPO"]),
                            Descricao = reader["GRUPODESC"].ToString(),
                            Departamento = reader["DEPARTAMENTo"].ToString(),
                            IdGrupoAD = new Guid(reader["ID_GRUPO_AD"].ToString())
                        },
                        Nivel = new Nivel()
                        {
                            Id = !string.IsNullOrEmpty(reader["ID_NIVEL"].ToString()) ? Convert.ToInt32(reader["ID_NIVEL"]) : 0,
                            Descricao = !string.IsNullOrEmpty(reader["NIVELDESC"].ToString()) ? reader["NIVELDESC"].ToString()  : ""
                        },
                        Cargo = new Cargos()
                        {
                            Id = !string.IsNullOrEmpty(reader["ID_CARGO"].ToString()) ? Convert.ToInt32(reader["ID_CARGO"]) : 0,
                            Descricao = !string.IsNullOrEmpty(reader["CARGODESC"].ToString()) ? reader["CARGODESC"].ToString() : ""
                        }
                    };
                }
                if(usuario != null)
                    usuario.Grupos = ObterGruposUsuario(usuario.Id);
            }

            return usuario;
        }

        public IList<Grupo> ObterGruposUsuario(int idUsuario)
        {
            string protheusAmbiente = ProdEnvironment() ? "[Protheus]" : "[Protheus_Dev]";
            string consulta = _baseRepository.BuscarArquivoConsulta("ObterGrupoUsuario")
                .Replace("__protheusAmbiente__", protheusAmbiente)
                .Replace("__queryEmail__", "U.ID_USUARIO = @ID_USUARIO");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_USUARIO", ((Object)idUsuario) ?? DBNull.Value);
                
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderGrupo(reader);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();

                    command.Dispose();
                }
            }
        }

        private IList<Grupo> LerDataReaderGrupo(SqlDataReader reader)
        {
            IList<Grupo> grupos = null;

            if (reader.HasRows)
            {
                grupos = new List<Grupo>();
                while (reader.Read())
                {
                    Grupo grupo = new Grupo()
                    {
                        Id = Convert.ToInt32(reader["ID_GRUPO"]),
                        Descricao = reader["GRUPODESC"].ToString(),
                        Departamento = reader["DEPARTAMENTo"].ToString(),
                        IdGrupoAD = new Guid(reader["ID_GRUPO_AD"].ToString()),
                    };
                    grupos.Add(grupo);
                }
            }

            return grupos;
        }

        public Usuario Obter(int id)
        {
            try
            {
                Usuario usuario = new Usuario();

                var protheusAmbiente = ProdEnvironment() ? "[Protheus]" : "[Protheus_Dev]";

                string query = $"SELECT U.ID_USUARIO, U.NOME, U.COD_USUARIO, U.EMAIL, U.CONTA_AD, G.DEPARTAMENTO, CT.CTT_CUSTO, U.ID_GRUPO, U.ID_CARGO, U.ID_NIVEL, U.ID_FILIAL, U.RAMAL, U.TELEFONE, U.ATIVO," +
                    $" G.DESCRICAO as 'GRUPODESC', N.DESCRICAO as 'NIVELDESC', C.DESCRICAO as 'CARGODESC', F.DESCRICAO as 'FILIALDESC' FROM USUARIO AS U" +
                    $" INNER JOIN GRUPO AS G ON U.ID_GRUPO = G.ID_GRUPO" +
                    $" LEFT JOIN NIVEL AS N ON U.ID_NIVEL = N.ID_NIVEL" +
                    $" LEFT JOIN CARGO AS C ON U.ID_CARGO = C.ID_CARGO" +
                    $" INNER JOIN {protheusAmbiente}.dbo.CTT010 as CT on CT.CTT_DESC01 COLLATE Latin1_General_CI_AS = G.DEPARTAMENTO COLLATE Latin1_General_CI_AS" +
                    $" INNER JOIN FILIAL AS F ON U.ID_FILIAL = F.ID_FILIAL WHERE U.ID_USUARIO = @idUsuario";


                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@idUsuario", Value = id }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        if (dataReader.Read())
                        {
                            usuario.Id = (int)dataReader["ID_USUARIO"];
                            usuario.Nome = dataReader["NOME"].ToString();
                            usuario.Email = dataReader["EMAIL"].ToString();
                            usuario.ContaAd = dataReader["CONTA_AD"].ToString();
                            usuario.CodUsuario = dataReader["COD_USUARIO"].ToString();
                            usuario.Filial = new Filial()
                            {
                                Id = (int)dataReader["ID_FILIAL"],
                                Descricao = dataReader["FILIALDESC"].ToString()
                            };
                            usuario.Ramal = dataReader["RAMAL"].ToString();
                            usuario.Telefone = dataReader["TELEFONE"].ToString();
                            usuario.Ativo = (bool)dataReader["ATIVO"];
                            usuario.Grupo = new Grupo()
                            {
                                Id = Convert.ToInt32(dataReader["ID_GRUPO"]),
                                Descricao = dataReader["GRUPODESC"].ToString(),
                                Departamento = dataReader["DEPARTAMENTO"].ToString(),
                                CentroCusto = dataReader["CTT_CUSTO"].ToString()
                            };
                            usuario.Nivel = new Nivel()
                            {
                                Id = !string.IsNullOrEmpty(dataReader["ID_NIVEL"].ToString()) ? Convert.ToInt32(dataReader["ID_NIVEL"]) : 0,
                                Descricao = !string.IsNullOrEmpty(dataReader["NIVELDESC"].ToString()) ? dataReader["NIVELDESC"].ToString() : ""
                            };
                            usuario.Cargo = new Cargos()
                            {
                                Id = !string.IsNullOrEmpty(dataReader["ID_CARGO"].ToString()) ? Convert.ToInt32(dataReader["ID_CARGO"]) : 0,
                                Descricao = !string.IsNullOrEmpty(dataReader["CARGODESC"].ToString()) ? dataReader["CARGODESC"].ToString() : ""
                            };
                        };
                    };
                };

                return usuario;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Usuario ObterPorEmail(string email)
        {
            try
            {
                Usuario usuario = new Usuario();

                var protheusAmbiente = ProdEnvironment() ? "[Protheus]" : "[Protheus_Dev]";

                string query = $"SELECT U.ID_USUARIO, U.NOME, U.COD_USUARIO, U.EMAIL, U.CONTA_AD, G.DEPARTAMENTO, CT.CTT_CUSTO, U.ID_GRUPO, U.ID_CARGO, N.ID_NIVEL, U.ID_FILIAL, U.RAMAL, U.TELEFONE, U.ATIVO," +
                    $" G.DESCRICAO as 'GRUPODESC', N.DESCRICAO as 'NIVELDESC', C.DESCRICAO as 'CARGODESC', F.DESCRICAO as 'FILIALDESC' FROM USUARIO AS U" +
                    $" INNER JOIN GRUPO AS G ON U.ID_GRUPO = G.ID_GRUPO" +
                    $" LEFT JOIN NIVEL AS N ON U.ID_NIVEL = N.ID_NIVEL" +
                    $" LEFT JOIN CARGO AS C ON U.ID_CARGO = C.ID_CARGO" +
                    $" INNER JOIN {protheusAmbiente}.dbo.CTT010 as CT on CT.CTT_DESC01 COLLATE Latin1_General_CI_AS = G.DEPARTAMENTO COLLATE Latin1_General_CI_AS" +
                    $" INNER JOIN FILIAL AS F ON U.ID_FILIAL = F.ID_FILIAL WHERE U.EMAIL = @email";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@email", Value = email }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        if (dataReader.Read())
                        {
                            usuario.Id = (int)dataReader["ID_USUARIO"];
                            usuario.Nome = dataReader["NOME"].ToString();
                            usuario.Email = dataReader["EMAIL"].ToString();
                            usuario.ContaAd = dataReader["CONTA_AD"].ToString();
                            usuario.CodUsuario = dataReader["COD_USUARIO"].ToString();
                            usuario.Filial = new Filial()
                            {
                                Id = (int)dataReader["ID_FILIAL"],
                                Descricao = dataReader["FILIALDESC"].ToString()
                            };
                            usuario.Ramal = dataReader["RAMAL"].ToString();
                            usuario.Telefone = dataReader["TELEFONE"].ToString();
                            usuario.Ativo = (bool)dataReader["ATIVO"];
                            usuario.Grupo = new Grupo()
                            {
                                Id = Convert.ToInt32(dataReader["ID_GRUPO"]),
                                Descricao = dataReader["GRUPODESC"].ToString(),
                                Departamento = dataReader["DEPARTAMENTO"].ToString(),
                                CentroCusto = dataReader["CTT_CUSTO"].ToString()
                            };
                            usuario.Nivel = new Nivel()
                            {
                                Id = !string.IsNullOrEmpty(dataReader["ID_NIVEL"].ToString()) ? Convert.ToInt32(dataReader["ID_NIVEL"]) : 0,
                                Descricao = !string.IsNullOrEmpty(dataReader["NIVELDESC"].ToString()) ? dataReader["NIVELDESC"].ToString() : ""
                            };
                            usuario.Cargo = new Cargos()
                            {
                                Id = !string.IsNullOrEmpty(dataReader["ID_CARGO"].ToString()) ? Convert.ToInt32(dataReader["ID_CARGO"]) : 0,
                                Descricao = !string.IsNullOrEmpty(dataReader["CARGODESC"].ToString()) ? dataReader["CARGODESC"].ToString() : ""
                            };
                        };
                    };
                };

                return usuario;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Usuario ObterPorContaAd(string contaAd)
        {
            try
            {
                Usuario usuario = new Usuario();

                var protheusAmbiente = ProdEnvironment() ? "[Protheus]" : "[Protheus_Dev]";

                string query = $"SELECT U.ID_USUARIO, U.NOME, U.COD_USUARIO, U.EMAIL, U.CONTA_AD, G.DEPARTAMENTO, CT.CTT_CUSTO, U.ID_GRUPO, U.ID_CARGO, N.ID_NIVEL, U.ID_FILIAL, U.RAMAL, U.TELEFONE, U.ATIVO," +
                    $" G.DESCRICAO as 'GRUPODESC', N.DESCRICAO as 'NIVELDESC', C.DESCRICAO as 'CARGODESC', F.DESCRICAO as 'FILIALDESC' FROM USUARIO AS U" +
                    $" INNER JOIN GRUPO AS G ON U.ID_GRUPO = G.ID_GRUPO" +
                    $" LEFT JOIN NIVEL AS N ON U.ID_NIVEL = N.ID_NIVEL" +
                    $" LEFT JOIN CARGO AS C ON U.ID_CARGO = C.ID_CARGO" +
                    $" INNER JOIN {protheusAmbiente}.dbo.CTT010 as CT on CT.CTT_DESC01 COLLATE Latin1_General_CI_AS = G.DEPARTAMENTO COLLATE Latin1_General_CI_AS" +
                    $" INNER JOIN FILIAL AS F ON U.ID_FILIAL = F.ID_FILIAL WHERE U.CONTA_AD = @contaAd";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@contaAd", Value = contaAd }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        if (dataReader.Read())
                        {
                            usuario.Id = (int)dataReader["ID_USUARIO"];
                            usuario.Nome = dataReader["NOME"].ToString();
                            usuario.Email = dataReader["EMAIL"].ToString();
                            usuario.ContaAd = dataReader["CONTA_AD"].ToString();
                            usuario.CodUsuario = dataReader["COD_USUARIO"].ToString();
                            usuario.Filial = new Filial()
                            {
                                Id = (int)dataReader["ID_FILIAL"],
                                Descricao = dataReader["FILIALDESC"].ToString()
                            };
                            usuario.Ramal = dataReader["RAMAL"].ToString();
                            usuario.Telefone = dataReader["TELEFONE"].ToString();
                            usuario.Ativo = (bool)dataReader["ATIVO"];
                            usuario.Grupo = new Grupo()
                            {
                                Id = Convert.ToInt32(dataReader["ID_GRUPO"]),
                                Descricao = dataReader["GRUPODESC"].ToString(),
                                Departamento = dataReader["DEPARTAMENTO"].ToString(),
                                CentroCusto = dataReader["CTT_CUSTO"].ToString()
                            };
                            usuario.Nivel = new Nivel()
                            {
                                Id = !string.IsNullOrEmpty(dataReader["ID_NIVEL"].ToString()) ? Convert.ToInt32(dataReader["ID_NIVEL"]) : 0,
                                Descricao = !string.IsNullOrEmpty(dataReader["NIVELDESC"].ToString()) ? dataReader["NIVELDESC"].ToString() : ""
                            };
                            usuario.Cargo = new Cargos()
                            {
                                Id = !string.IsNullOrEmpty(dataReader["ID_CARGO"].ToString()) ? Convert.ToInt32(dataReader["ID_CARGO"]) : 0,
                                Descricao = !string.IsNullOrEmpty(dataReader["CARGODESC"].ToString()) ? dataReader["CARGODESC"].ToString() : ""
                            };
                        };
                    };
                };

                return usuario;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Usuario ObterPorGuid(string guid)
        {
            try
            {
                Usuario usuario = new Usuario();

                var protheusAmbiente = ProdEnvironment() ? "[Protheus]" : "[Protheus_Dev]";

                string query = $"SELECT U.ID_USUARIO, U.NOME, U.COD_USUARIO, U.EMAIL, U.CONTA_AD, G.DEPARTAMENTO, CT.CTT_CUSTO, U.ID_GRUPO, U.ID_CARGO, N.ID_NIVEL, U.ID_FILIAL, U.RAMAL, U.TELEFONE, U.ATIVO," +
                    $" G.DESCRICAO as 'GRUPODESC', N.DESCRICAO as 'NIVELDESC', C.DESCRICAO as 'CARGODESC', F.DESCRICAO as 'FILIALDESC' FROM USUARIO AS U" +
                    $" INNER JOIN GRUPO AS G ON U.ID_GRUPO = G.ID_GRUPO" +
                    $" LEFT JOIN NIVEL AS N ON U.ID_NIVEL = N.ID_NIVEL" +
                    $" LEFT JOIN CARGO AS C ON U.ID_CARGO = C.ID_CARGO" +
                    $" INNER JOIN {protheusAmbiente}.dbo.CTT010 as CT on CT.CTT_DESC01 COLLATE Latin1_General_CI_AS = G.DEPARTAMENTO COLLATE Latin1_General_CI_AS" +
                    $" INNER JOIN FILIAL AS F ON U.ID_FILIAL = F.ID_FILIAL WHERE U.TOKEN = @guid and U.DATA_EXPIRACAO_TOKEN > @dataAtual";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@guid", Value = guid },
                    new SqlParameter() { ParameterName = "@dataAtual", Value = DateTime.Now }

                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        if (dataReader.Read())
                        {
                            usuario.Id = (int)dataReader["ID_USUARIO"];
                            usuario.Nome = dataReader["NOME"].ToString();
                            usuario.Email = dataReader["EMAIL"].ToString();
                            usuario.ContaAd = dataReader["CONTA_AD"].ToString();
                            usuario.CodUsuario = dataReader["COD_USUARIO"].ToString();
                            usuario.Filial = new Filial()
                            {
                                Id = (int)dataReader["ID_FILIAL"],
                                Descricao = dataReader["FILIALDESC"].ToString()
                            };
                            usuario.Ramal = dataReader["RAMAL"].ToString();
                            usuario.Telefone = dataReader["TELEFONE"].ToString();
                            usuario.Ativo = (bool)dataReader["ATIVO"];
                            usuario.Grupo = new Grupo()
                            {
                                Id = Convert.ToInt32(dataReader["ID_GRUPO"]),
                                Descricao = dataReader["GRUPODESC"].ToString(),
                                Departamento = dataReader["DEPARTAMENTO"].ToString(),
                                CentroCusto = dataReader["CTT_CUSTO"].ToString()
                            };
                            usuario.Nivel = new Nivel()
                            {
                                Id = !string.IsNullOrEmpty(dataReader["ID_NIVEL"].ToString()) ? Convert.ToInt32(dataReader["ID_NIVEL"]) : 0,
                                Descricao = !string.IsNullOrEmpty(dataReader["NIVELDESC"].ToString()) ? dataReader["NIVELDESC"].ToString() : ""
                            };
                            usuario.Cargo = new Cargos()
                            {
                                Id = !string.IsNullOrEmpty(dataReader["ID_CARGO"].ToString()) ? Convert.ToInt32(dataReader["ID_CARGO"]) : 0,
                                Descricao = !string.IsNullOrEmpty(dataReader["CARGODESC"].ToString()) ? dataReader["CARGODESC"].ToString() : ""
                            };
                        };
                    };
                };

                return usuario;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GerarGuid(Usuario usuario)
        {
            try
            {
                string query = @"UPDATE USUARIO SET TOKEN = @GUID, DATA_EXPIRACAO_TOKEN = @DATA_EXPIRACAO WHERE ID_USUARIO = @idUsuario";
                Guid guid = Guid.NewGuid();
                List<DbParameter> listaParam = new List<DbParameter>()
                {
                    new SqlParameter() { ParameterName = "@GUID", Value = guid },
                    new SqlParameter() { ParameterName = "@DATA_EXPIRACAO", Value = DateTime.Now.AddHours(1)},
                    new SqlParameter() { ParameterName = "@idUsuario", Value = usuario.Id}
                };

                ExecuteNonQuery(query, listaParam, CommandType.Text);

                return guid.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExpiraGuid(int idUsuario)
        {
            try
            {
                string query = @"UPDATE USUARIO SET DATA_EXPIRACAO_TOKEN = @DATA_EXPIRACAO WHERE ID_USUARIO = @idUsuario";
                List<DbParameter> listaParam = new List<DbParameter>()
                {
                    new SqlParameter() { ParameterName = "@DATA_EXPIRACAO", Value = DateTime.Now},
                    new SqlParameter() { ParameterName = "@idUsuario", Value = idUsuario}
                };

                ExecuteNonQuery(query, listaParam, CommandType.Text);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> ObterTodos()
        {
            try
            {
                List<Usuario> listaUsuarios = new List<Usuario>();

                string query = @"SELECT U.ID_USUARIO, U.NOME, U.EMAIL, U.CONTA_AD, U.ID_GRUPO, U.ID_CARGO, N.ID_NIVEL, U.ID_FILIAL, U.RAMAL, U.TELEFONE, U.ATIVO, 
                    U.COD_USUARIO, G.DESCRICAO as 'GRUPODESC', N.DESCRICAO as 'NIVELDESC', C.DESCRICAO as 'CARGODESC', F.DESCRICAO as 'FILIALDESC' FROM USUARIO AS U INNER JOIN GRUPO AS G
                    ON U.ID_GRUPO = G.ID_GRUPO LEFT JOIN NIVEL AS N ON U.ID_NIVEL = N.ID_NIVEL LEFT JOIN CARGO AS C ON U.ID_CARGO = C.ID_CARGO INNER JOIN FILIAL AS F ON U.ID_FILIAL = F.ID_FILIAL";
                
                using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            listaUsuarios.Add(new Usuario()
                            {
                                Id = (int)dataReader["ID_USUARIO"],
                                Nome = dataReader["NOME"].ToString(),
                                Email = dataReader["EMAIL"].ToString(),
                                ContaAd = dataReader["CONTA_AD"].ToString(),
                                Filial = new Filial()
                                {
                                    Id = (int)dataReader["ID_FILIAL"],
                                    Descricao = dataReader["FILIALDESC"].ToString()
                                },
                                Ramal = dataReader["RAMAL"].ToString(),
                                Telefone = dataReader["TELEFONE"].ToString(),
                                Ativo = (bool)dataReader["ATIVO"],
                                CodUsuario = dataReader["COD_USUARIO"].ToString(),
                                Grupo = new Grupo()
                                {
                                    Id = Convert.ToInt32(dataReader["ID_GRUPO"]),
                                    Descricao = dataReader["GRUPODESC"].ToString()
                                },
                                Nivel = new Nivel()
                                {
                                    Id = !string.IsNullOrEmpty(dataReader["ID_NIVEL"].ToString()) ? Convert.ToInt32(dataReader["ID_NIVEL"]) : 0,
                                    Descricao = !string.IsNullOrEmpty(dataReader["NIVELDESC"].ToString()) ? dataReader["NIVELDESC"].ToString() : ""
                                },
                                Cargo = new Cargos()
                                {
                                    Id = !string.IsNullOrEmpty(dataReader["ID_CARGO"].ToString()) ? Convert.ToInt32(dataReader["ID_CARGO"]) : 0,
                                    Descricao = !string.IsNullOrEmpty(dataReader["CARGODESC"].ToString()) ? dataReader["CARGODESC"].ToString() : ""
                                }
                            });
                        };
                    }
                }
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Usuario> ObterFiltrados(string nome, string email, int grupoId)
        {
            try
            {
                List<Usuario> listaUsuarios = new List<Usuario>();
                nome = string.IsNullOrEmpty(nome) ? "" : nome;
                email = string.IsNullOrEmpty(email) ? "" : email;

                string query = $@"SELECT U.ID_USUARIO, U.NOME, U.EMAIL, U.CONTA_AD, U.ID_GRUPO, U.ID_CARGO, N.ID_NIVEL, U.ID_FILIAL, U.RAMAL, U.TELEFONE, U.ATIVO, 
                        U.COD_USUARIO, G.DESCRICAO as 'GRUPODESC', N.DESCRICAO as 'NIVELDESC', C.DESCRICAO as 'CARGODESC', F.DESCRICAO as 'FILIALDESC' FROM USUARIO AS U INNER JOIN GRUPO AS G
                        ON U.ID_GRUPO = G.ID_GRUPO LEFT JOIN NIVEL AS N ON U.ID_NIVEL = N.ID_NIVEL LEFT JOIN CARGO AS C ON U.ID_CARGO = C.ID_CARGO INNER JOIN FILIAL AS F ON U.ID_FILIAL = F.ID_FILIAL 
                        WHERE (U.NOME LIKE '%' + @nome + '%' OR @nome = '')" +
                        "AND (U.EMAIL LIKE '%' + @email + '%' OR @email = '')" +
                        "AND (U.ID_GRUPO = @grupoId OR @grupoId = 0)";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@nome", Value = nome },
                    new SqlParameter() { ParameterName = "@email", Value = email },
                    new SqlParameter() { ParameterName = "@grupoId", Value = grupoId }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            listaUsuarios.Add(new Usuario()
                            {
                                Id = (int)dataReader["ID_USUARIO"],
                                Nome = dataReader["NOME"].ToString(),
                                Email = dataReader["EMAIL"].ToString(),
                                ContaAd = dataReader["CONTA_AD"].ToString(),
                                Filial = new Filial()
                                {
                                    Id = (int)dataReader["ID_FILIAL"],
                                    Descricao = dataReader["FILIALDESC"].ToString()
                                },
                                Ramal = dataReader["RAMAL"].ToString(),
                                Telefone = dataReader["TELEFONE"].ToString(),
                                Ativo = (bool)dataReader["ATIVO"],
                                CodUsuario = dataReader["COD_USUARIO"].ToString(),
                                Grupo = new Grupo()
                                {
                                    Id = Convert.ToInt32(dataReader["ID_GRUPO"]),
                                    Descricao = dataReader["GRUPODESC"].ToString()
                                },
                                Nivel = new Nivel()
                                {
                                    Id = !string.IsNullOrEmpty(dataReader["ID_NIVEL"].ToString()) ? Convert.ToInt32(dataReader["ID_NIVEL"]) : 0,
                                    Descricao = !string.IsNullOrEmpty(dataReader["NIVELDESC"].ToString()) ? dataReader["NIVELDESC"].ToString() : ""
                                },
                                Cargo = new Cargos()
                                {
                                    Id = !string.IsNullOrEmpty(dataReader["ID_CARGO"].ToString()) ? Convert.ToInt32(dataReader["ID_CARGO"]) : 0,
                                    Descricao = !string.IsNullOrEmpty(dataReader["CARGODESC"].ToString()) ? dataReader["CARGODESC"].ToString() : ""
                                }
                            });
                        };
                    }
                }
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Filial> ObterFiliais()
        {
            try
            {
                List<Filial> listaFiliais = new List<Filial>();

                string query = @"SELECT ID_FILIAL, DESCRICAO FROM FILIAL WHERE ATIVO = 1";

                using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            listaFiliais.Add(new Filial()
                            {
                                Id = (int)dataReader["ID_FILIAL"],
                                Descricao = dataReader["DESCRICAO"].ToString()
                            });
                        };
                    }
                }
                return listaFiliais;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Usuario> ObterColaboradores(int idUsuario, int idEstado = 0)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            StringBuilder query = new StringBuilder(@"SELECT us.ID_USUARIO, us.NOME, us.COD_USUARIO FROM USUARIO us
                            INNER JOIN SUPERVISOR_DIVULGADOR sd ON us.ID_USUARIO = sd.ID_USUARIO_DIVULGADOR
                            WHERE sd.ID_USUARIO_SUPERVISOR = @id_usuario");

            List<SqlParameter> listaParam = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@id_usuario", Value = idUsuario }
            };

            if (idEstado > 0)
            {
                query.Append(" AND sd.ID_ESTADO = @idEstado");
                listaParam.Add(new SqlParameter() { ParameterName = "@idEstado", Value = idEstado });
            }
            query.Append(" ORDER BY us.NOME");
            using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        listaUsuarios.Add(new Usuario()
                        {
                            Id = Convert.ToInt32(dataReader["ID_USUARIO"]),
                            Nome = dataReader["NOME"].ToString(),
                            CodUsuario = dataReader["COD_USUARIO"].ToString()
                        });
                    }
                }
            }

            return listaUsuarios;
        }

        public List<Usuario> ObterColaboradoresGerente(int idUsuario, int idEstado = 0)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            StringBuilder query = new StringBuilder(@"SELECT us.ID_USUARIO, us.NOME, us.COD_USUARIO FROM GERENTE_SUPERVISOR_REGIAO gs
               INNER JOIN SUPERVISOR_DIVULGADOR sd ON sd.ID_USUARIO_SUPERVISOR = gs.ID_USUARIO_SUPERVISOR
			   INNER JOIN USUARIO us ON us.ID_USUARIO = sd.ID_USUARIO_DIVULGADOR
			   WHERE gs.ID_USUARIO_GERENTE = @id_usuario");

            List<SqlParameter> listaParam = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@id_usuario", Value = idUsuario }
            };

            if (idEstado > 0)
            {
                query.Append(" AND sd.ID_ESTADO = @idEstado");
                listaParam.Add(new SqlParameter() { ParameterName = "@idEstado", Value = idEstado });
            }
            query.Append(" ORDER BY us.NOME");
            using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        listaUsuarios.Add(new Usuario()
                        {
                            Id = Convert.ToInt32(dataReader["ID_USUARIO"]),
                            Nome = dataReader["NOME"].ToString(),
                            CodUsuario = dataReader["COD_USUARIO"].ToString()
                        });
                    }
                }
            }
            return listaUsuarios;
        }

        public List<Usuario> ObterDivulgadoresDiretoria(int idEstado = 0)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            StringBuilder query = new StringBuilder(@"SELECT us.ID_USUARIO, us.NOME, us.COD_USUARIO FROM USUARIO us
                            JOIN GRUPO gp on gp.ID_GRUPO = us.ID_GRUPO
                            JOIN FILIAL fl on fl.ID_FILIAL = us.ID_FILIAL
                            JOIN ESTADO es on fl.ID_ESTADO = es.ID_ESTADO
                            WHERE gp.DESCRICAO = 'CN=Divulgador'");

            List<SqlParameter> listaParam = new List<SqlParameter>();

            if (idEstado > 0)
            {
                query.Append(" AND es.ID_Estado = @idEstado");
                listaParam.Add(new SqlParameter() { ParameterName = "@idEstado", Value = idEstado });
            }

            query.Append(" ORDER BY us.NOME");

            using (DbDataReader dataReader = base.GetDataReader(query.ToString(), listaParam, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        listaUsuarios.Add(new Usuario()
                        {
                            Id = Convert.ToInt32(dataReader["ID_USUARIO"]),
                            Nome = dataReader["NOME"].ToString(),
                            CodUsuario = dataReader["COD_USUARIO"].ToString()
                        });
                    }
                }
            }

            return listaUsuarios;
        }

        public string Adicionar(Usuario usuario)
        {
            try
            {
                if (UsuarioCadastrado(usuario.Nome, usuario.Id))
                    return "Usuário já cadastrado!";

                if (!GrupoCadastrado("", usuario.Grupo.Id))
                    return "Grupo não cadastrado!";

                string query = @"INSERT INTO USUARIO (NOME, EMAIL, CONTA_AD, ID_GRUPO, ID_FILIAL, RAMAL, TELEFONE, ATIVO, COD_USUARIO, ID_NIVEL, ID_CARGO)  VALUES (@nome, @email, @contaAD, @idGrupo, @idFilial, @ramal, @telefone, @ativo, @codUsuario, @idNivel, @idCargo)";

                List<DbParameter> listaParam = new List<DbParameter>()
                {
                    new SqlParameter() { ParameterName = "@nome", Value = usuario.Nome },
                    new SqlParameter() { ParameterName = "@email", Value = usuario.Email },
                    new SqlParameter() { ParameterName = "@contaAD", Value = usuario.ContaAd },
                    new SqlParameter() { ParameterName = "@idGrupo", Value = usuario.Grupo.Id },
                    new SqlParameter() { ParameterName = "@idFilial", Value = usuario.Filial.Id },
                    new SqlParameter() { ParameterName = "@ramal", Value = usuario.Ramal },
                    new SqlParameter() { ParameterName = "@telefone", Value = usuario.Telefone },
                    new SqlParameter() { ParameterName = "@ativo", Value = usuario.Ativo },
                    new SqlParameter() { ParameterName = "@codUsuario", Value = String.IsNullOrEmpty(usuario.CodUsuario)?"0":usuario.CodUsuario },
                    new SqlParameter() { ParameterName = "@idNivel", Value = usuario.Nivel.Id },
                    new SqlParameter() { ParameterName = "@idCargo", Value = usuario.Cargo.Id }
                };

                ExecuteNonQuery(query, listaParam, CommandType.Text);
                LimparGrupoUsuario(usuario);
                SalvarGruposUsuario(usuario);

                return "Usuário cadastrado com sucesso!";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Editar(Usuario usuario)
        {
            try
            {
                if (!UsuarioCadastrado(usuario.Nome, usuario.Id))
                    return "Usuário não encontrado!";

                string query = @"UPDATE USUARIO SET NOME = @nome, EMAIL = @email, CONTA_AD = @contaAD, ID_GRUPO = @idGrupo, ID_NIVEL = @idNivel, ID_CARGO = @idCargo, 
                    ID_FILIAL = @idFilial, RAMAL = @ramal, TELEFONE = @telefone, ATIVO = @ativo, COD_USUARIO = @codUsuario
                    WHERE ID_USUARIO = @idUsuario";

                List<DbParameter> listaParam = new List<DbParameter>()
                {
                    new SqlParameter() { ParameterName = "@idUsuario", Value = usuario.Id },
                    new SqlParameter() { ParameterName = "@nome", Value = usuario.Nome },
                    new SqlParameter() { ParameterName = "@email", Value = usuario.Email },
                    new SqlParameter() { ParameterName = "@contaAD", Value = usuario.ContaAd },
                    new SqlParameter() { ParameterName = "@idGrupo", Value = usuario.Grupo.Id },
                    new SqlParameter() { ParameterName = "@idFilial", Value = usuario.Filial.Id },
                    new SqlParameter() { ParameterName = "@ramal", Value = usuario.Ramal },
                    new SqlParameter() { ParameterName = "@telefone", Value = usuario.Telefone },
                    new SqlParameter() { ParameterName = "@ativo", Value = usuario.Ativo },
                    new SqlParameter() { ParameterName = "@codUsuario", Value = String.IsNullOrEmpty(usuario.CodUsuario)?"0":usuario.CodUsuario },
                    new SqlParameter() { ParameterName = "@idNivel", Value = usuario.Nivel.Id },
                    new SqlParameter() { ParameterName = "@idCargo", Value = usuario.Cargo.Id }
                };

                ExecuteNonQuery(query, listaParam, CommandType.Text);
                LimparGrupoUsuario(usuario);
                SalvarGruposUsuario(usuario);

                return "Usuário editado com sucesso!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LimparGrupoUsuario(Usuario usuario)
        {
            SqlTransaction transaction = null;

            try
            {
                using (var connection = new SqlConnection(_baseRepository.Conexao))
                {
                    connection.Open();

                    using (transaction = connection.BeginTransaction())
                    {
                        string script = "DELETE FROM USUARIO_GRUPO WHERE ID_USUARIO = @ID_USUARIO";

                        using (SqlCommand command = new SqlCommand(script, connection, transaction))
                        {
                            command.Parameters.Add("@ID_USUARIO", SqlDbType.VarChar);
                            command.Parameters["@ID_USUARIO"].Value = usuario.Id;

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw ex;
            }
        }

        public void SalvarGruposUsuario(Usuario usuario)
        {
            SqlTransaction transaction = null;

            try
            {
                using (var connection = new SqlConnection(_baseRepository.Conexao))
                {
                    connection.Open();

                    using (transaction = connection.BeginTransaction())
                    {
                        string script = _baseRepository.BuscarArquivoConsulta("SalvarGruposUsuario");

                        using (SqlCommand command = new SqlCommand(script, connection, transaction))
                        {
                            command.Parameters.Add("@ID_USUARIO", SqlDbType.VarChar);
                            command.Parameters.Add("@ID_GRUPO", SqlDbType.VarChar);
                            
                            foreach (var grupo in usuario.Grupos)
                            {
                                command.Parameters["@ID_USUARIO"].Value = usuario.Id;
                                command.Parameters["@ID_GRUPO"].Value = grupo.Id;
                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw ex;
            }
        }

        public string Ativar(Usuario usuario)
        {
            try
            {
                string query = @"UPDATE USUARIO SET ATIVO = @ativo WHERE ID_USUARIO = @id";

                List<DbParameter> listaParam = new List<DbParameter>()
                {
                    new SqlParameter() { ParameterName = "@id", Value = usuario.Id },
                    new SqlParameter() { ParameterName = "@ativo", Value = usuario.Ativo }
                };

                int result = ExecuteNonQuery(query, listaParam, CommandType.Text);

                if (result != 0)
                    return "Alterado com sucesso!";
                else
                    return "Erro ao alterar usuário!";

            }
            catch
            {
                throw;
            }
        }

        public bool UsuarioCadastrado(string nome, int idUsuario)
        {
            try
            {
                string query = idUsuario > 0 ? @"SELECT ID_USUARIO FROM USUARIO WHERE ID_USUARIO = @idUsuario" : @"SELECT NOME FROM USUARIO WHERE NOME = @nome";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@nome", Value = nome },
                    new SqlParameter() { ParameterName = "@idUsuario", Value = idUsuario }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null)
                    {
                        if (dataReader.HasRows)
                        {
                            return true;
                        };
                    }
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        public bool UsuarioFavoritadoCadastrado(int id, int codUsuario = 0, string nomeFavoritado = "", string ramalFavoritado = "")
        {
            try
            {
                string query = id > 0 && (nomeFavoritado == "" || ramalFavoritado == "") ? @"SELECT ID FROM RAMAIS_FAVORITOS WHERE ID = @id" : @"SELECT ID FROM RAMAIS_FAVORITOS WHERE ID_USUARIO = @idUsuario AND NOME_FAVORITADO = @nomeFavoritado AND RAMAL_FAVORITADO = @ramalFavoritado";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@id", Value = id },
                    new SqlParameter() { ParameterName = "@idUsuario", Value = codUsuario },
                    new SqlParameter() { ParameterName = "@nomeFavoritado", Value = nomeFavoritado },
                    new SqlParameter() { ParameterName = "@ramalFavoritado", Value = ramalFavoritado }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null)
                    {
                        if (dataReader.HasRows)
                        {
                            return true;
                        };
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string FavoritarUsuario(UsuarioRamal usuario)
        {
            try
            {
                if (UsuarioFavoritadoCadastrado(usuario.Id, usuario.CodUsuario, usuario.Nome, usuario.Ramal))
                    return "Usuário favoritado já cadastrado!";

                string query = @"INSERT INTO RAMAIS_FAVORITOS VALUES (@idUsuario, @nomeFavoritado, @ramalFavoritado, @departamentoFavoritado)";

                List<DbParameter> listaParam = new List<DbParameter>()
                {
                    new SqlParameter() { ParameterName = "@idUsuario", Value = usuario.CodUsuario },
                    new SqlParameter() { ParameterName = "@nomeFavoritado", Value = usuario.Nome },
                    new SqlParameter() { ParameterName = "@ramalFavoritado", Value = usuario.Ramal },
                    new SqlParameter() { ParameterName = "@departamentoFavoritado", Value = usuario.Departamento }
                };

                ExecuteNonQuery(query, listaParam, CommandType.Text);

                return "Usuário favoritado com sucesso!";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string ExcluirUsuarioFavoritado(int id)
        {
            try
            {
                if (!UsuarioFavoritadoCadastrado(id))
                    return "Usuário favoritado não existe!";

                string query = @"DELETE FROM RAMAIS_FAVORITOS WHERE ID = @id";

                List<DbParameter> listaParam = new List<DbParameter>()
                {
                    new SqlParameter() { ParameterName = "@id", Value = id }
                };

                ExecuteNonQuery(query, listaParam, CommandType.Text);

                return "Usuário favoritado deletado com sucesso!";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<UsuarioRamal> ListarUsuariosFavoritados(int codUsuario)
        {
            try
            {
                List<UsuarioRamal> listaUsuariosFavoritados = new List<UsuarioRamal>();

                string query = @"SELECT ID, ID_USUARIO, NOME_FAVORITADO, RAMAL_FAVORITADO, DEPARTAMENTO_FAVORITADO FROM RAMAIS_FAVORITOS WHERE ID_USUARIO = @codUsuario";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@codUsuario", Value = codUsuario }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            listaUsuariosFavoritados.Add(new UsuarioRamal()
                            {
                                Id = Convert.ToInt32(dataReader["ID"]),
                                CodUsuario = Convert.ToInt32(dataReader["ID_USUARIO"]),
                                Nome = dataReader["NOME_FAVORITADO"].ToString(),
                                Ramal = dataReader["RAMAL_FAVORITADO"].ToString(),
                                Departamento = dataReader["DEPARTAMENTO_FAVORITADO"].ToString()
                            });
                        }
                    };
                };

                return listaUsuariosFavoritados;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}