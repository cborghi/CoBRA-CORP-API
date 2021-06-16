using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace CoBRA.Infra.Intranet
{
    public class GrupoRepository : BaseRepository, IGrupoRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public GrupoRepository(IConfiguration configuration, IBaseRepositoryPainelMeta baseRepository) :base(configuration)
        {
            _baseRepository = baseRepository;
        }

        public List<Grupo> ObterGrupos()
        {
            try
            {
                List<Grupo> grupos = new List<Grupo>();

                string query = @"SELECT ID_GRUPO, DESCRICAO, ATIVO, DATA_CRIACAO, DATA_ALTERACAO, USUARIO, 
                    DEPARTAMENTO FROM GRUPO";

                using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {

                            grupos.Add(new Grupo()
                            {
                                Id = Convert.ToInt32(dataReader["ID_GRUPO"]),
                                Descricao = dataReader["DESCRICAO"].ToString(),
                                Ativo = Convert.ToBoolean(dataReader["ATIVO"]),
                                DataCriacao = Convert.ToDateTime(dataReader["DATA_CRIACAO"]),
                                DataAlteracao = Convert.ToDateTime(dataReader["DATA_ALTERACAO"]),
                                Usuario = dataReader["USUARIO"].ToString(),
                                Departamento = dataReader["DEPARTAMENTO"].ToString()
                            });
                        }
                    }
                }

                return grupos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Grupo ObterPorId(int id)
        {
            try
            {
                Grupo grupo = new Grupo();

                string query = @"SELECT ID_GRUPO, DESCRICAO, ATIVO, DATA_CRIACAO, DATA_ALTERACAO, USUARIO, 
                    DEPARTAMENTO FROM GRUPO WHERE ID_GRUPO = @id";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@id", Value = id }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {

                            grupo = new Grupo()
                            {
                                Id = Convert.ToInt32(dataReader["ID_GRUPO"]),
                                Descricao = dataReader["DESCRICAO"].ToString(),
                                Ativo = Convert.ToBoolean(dataReader["ATIVO"]),
                                DataCriacao = Convert.ToDateTime(dataReader["DATA_CRIACAO"]),
                                DataAlteracao = Convert.ToDateTime(dataReader["DATA_ALTERACAO"]),
                                Usuario = dataReader["USUARIO"].ToString(),
                                Departamento = dataReader["DEPARTAMENTO"].ToString()
                            };
                        }
                    }
                }

                return grupo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Adicionar(Grupo grupo)
        {
            try
            {
                if (GrupoCadastrado(grupo.Descricao))
                    return "Grupo já cadastrado!";

                string query = @"INSERT INTO GRUPO VALUES (@descricao, @ativo, @dataCriacao, @dataAlteracao, @usuario, @departamento, @idAD)";

                List<DbParameter> listaParam = new List<DbParameter>()
                {
                    new SqlParameter() { ParameterName = "@descricao", Value = grupo.Descricao },
                    new SqlParameter() { ParameterName = "@ativo", Value = grupo.Ativo },
                    new SqlParameter() { ParameterName = "@dataCriacao", Value = DateTime.Now },
                    new SqlParameter() { ParameterName = "@dataAlteracao", Value = DateTime.Now },
                    new SqlParameter() { ParameterName = "@usuario", Value = grupo.Usuario },
                    new SqlParameter() { ParameterName = "@idAD", Value = grupo.IdGrupoAD },
                    new SqlParameter() { ParameterName = "@departamento", Value = grupo.Departamento },
                };

                ExecuteNonQuery(query, listaParam, CommandType.Text);

                grupo.Id = ObterIdGrupoCadastrado(grupo.Descricao);

                if (grupo.Id == 0)
                    return "Erro ao obter id do grupo";

                AdicionarMenusGrupo(grupo);

                return "Grupo cadastrado com sucesso";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Editar(Grupo grupo)
        {
            try
            {
                if (!GrupoCadastrado(string.Empty, grupo.Id))
                    return "Grupo não cadastrado!";

                string query = @"UPDATE GRUPO SET DESCRICAO = @descricao, USUARIO = @usuario, DATA_ALTERACAO = @dataAlteracao, DEPARTAMENTO = @departamento, ID_GRUPO_AD = @idAD WHERE ID_GRUPO = @id";

                List<DbParameter> listaParam = new List<DbParameter>()
                {
                    new SqlParameter() { ParameterName = "@id", Value = grupo.Id },
                    new SqlParameter() { ParameterName = "@descricao", Value = grupo.Descricao },
                    new SqlParameter() { ParameterName = "@usuario", Value = grupo.Usuario },
                    new SqlParameter() { ParameterName = "@dataAlteracao", Value = DateTime.Now },
                    new SqlParameter() { ParameterName = "@idAD", Value = grupo.IdGrupoAD },
                    new SqlParameter() { ParameterName = "@departamento", Value = grupo.Departamento }
                };

                ExecuteNonQuery(query, listaParam, CommandType.Text);

                AtualizarMenusGrupo(grupo);

                return "Grupo editado com sucesso!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void Ativar(Grupo grupo)
        {
            try
            {
                string query = @"UPDATE GRUPO SET ATIVO = @ativo, USUARIO = @usuario, DATA_ALTERACAO = @dataAlteracao WHERE ID_GRUPO = @id";

                List<DbParameter> listaParam = new List<DbParameter>()
                {
                    new SqlParameter() { ParameterName = "@id", Value = grupo.Id },
                    new SqlParameter() { ParameterName = "@ativo", Value = grupo.Ativo },
                    new SqlParameter() { ParameterName = "@usuario", Value = grupo.Usuario },
                    new SqlParameter() { ParameterName = "@dataAlteracao", Value = DateTime.Now }
                };

                ExecuteNonQuery(query, listaParam, CommandType.Text);
            }
            catch
            {
                throw;
            }
        }

        public void AdicionarMenusGrupo(Grupo grupo)
        {

            VerificarMenuPai(ref grupo);

            try
            {
                foreach (var item in grupo.Menus)
                {
                    if (item.Id > 0)
                    {
                        string query = @"INSERT INTO MENU_GRUPO VALUES (@idMenu, @ativo, @idGrupo)";

                        List<DbParameter> listaParam = new List<DbParameter>()
                        {
                            new SqlParameter() { ParameterName = "@idMenu", Value = item.Id },
                            new SqlParameter() { ParameterName = "@ativo", Value = grupo.Ativo },
                            new SqlParameter() { ParameterName = "@idGrupo", Value = grupo.Id }
                        };

                        ExecuteNonQuery(query, listaParam, CommandType.Text);

                    }
                }

            }
            catch
            {
                throw;
            }
        }

        public void AtualizarMenusGrupo(Grupo grupo)
        {
            try
            {
                DeletarMenusGrupo(grupo.Id);

                AdicionarMenusGrupo(grupo);
            }
            catch
            {
                throw;
            }
        }

        protected void DeletarMenusGrupo(int idGrupo)
        {
            try
            {
                string query = @"DELETE FROM MENU_GRUPO WHERE ID_GRUPO = @id";

                List<DbParameter> listaParam = new List<DbParameter>()
                {
                    new SqlParameter() { ParameterName = "@id", Value = idGrupo }
                };

                ExecuteNonQuery(query, listaParam, CommandType.Text);

            }
            catch
            {
                throw;
            }
        }

        protected int ObterIdGrupoCadastrado(string descricao)
        {
            try
            {
                string query = @"SELECT ID_GRUPO FROM GRUPO WHERE DESCRICAO = @descricao";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@descricao", Value = descricao }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            return Convert.ToInt32(dataReader["ID_GRUPO"]);
                        };
                    }
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        public int ObterIdGrupoPorIdGrupoAD(Guid idGrupoAd)
        {
            try
            {
                string query = @"SELECT ID_GRUPO FROM GRUPO WHERE ID_GRUPO_AD = @idGrupoAd";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@idGrupoAd", Value = idGrupoAd }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            return Convert.ToInt32(dataReader["ID_GRUPO"]);
                        };
                    }
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        public void VerificarMenuPai(ref Grupo grupo)
        {
            try
            {
                using (var connection = new SqlConnection(_baseRepository.Conexao))
                {
                    connection.Open();

                    string script = "SELECT ID_MENU_PAI FROM MENU WHERE ID_MENU = @ID_MENU";

                    using (SqlCommand command = new SqlCommand(script, connection))
                    {
                        command.Parameters.AddWithValue("@ID_MENU", ((Object)grupo.Menus.Last().Id) ?? DBNull.Value);
                        SqlDataReader reader = command.ExecuteReader();

                        reader.Read();

                        if (!string.IsNullOrEmpty(reader["ID_MENU_PAI"].ToString()))
                        {
                            grupo.Menus.Add(new Menu
                            {
                                Id = int.Parse(reader["ID_MENU_PAI"].ToString())
                            });

                            VerificarMenuPai(ref grupo);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
