using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class NetlitRepository : BaseRepository, INetlitRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public NetlitRepository(IBaseRepositoryPainelMeta baseRepository, IConfiguration configuration) : base(configuration)
        {
            _baseRepository = baseRepository;
        }

        public ConteudoPaginadoPP CarregarConteudoPP(int NumeroPagina, int RegistrosPagina, string Filtro)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarConteudoPaginado");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@NumeroPagina", NumeroPagina);
                command.Parameters.AddWithValue("@RegistrosPagina", RegistrosPagina);
                command.Parameters.AddWithValue("@Filtro", Filtro);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderConteudoPaginadoPP(reader);
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

        public async Task<ConteudoPaginadoPP> CarregarConteudoNetLitPP(int NumeroPagina, int RegistrosPagina, string Filtro)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarConteudoPaginado");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@NumeroPagina", NumeroPagina);
                command.Parameters.AddWithValue("@RegistrosPagina", RegistrosPagina);
                command.Parameters.AddWithValue("@Filtro", Filtro);
                try
                {
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderConteudoPaginadoPP(reader);
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

        private ConteudoPaginadoPP LerDataReaderConteudoPaginadoPP(SqlDataReader reader)
        {
            var retorno = new ConteudoPaginadoPP();
            retorno.LstConteudosPP = new List<ConteudoPP>();
            var cont = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ConteudoPP conteudo = new ConteudoPP
                    {
                        IdConteudo = Convert.ToInt32(reader["ID_CONTEUDO"]),
                        Nome = reader["NOME"].ToString(),
                        Caminho = reader["CAMINHO"].ToString(),
                        NomeGuid = reader["NOME_GUID"].ToString(),
                        DtCadastro = Convert.ToDateTime(reader["DT_CADASTRO"]),
                        Usuario = Convert.ToInt32(reader["USUARIO"]),
                        Ativo = Convert.ToBoolean(reader["ATIVO"]),
                        PrimeiraPagina = reader["PRIMEIRA_PAGINA"].ToString(),
                    };
                    cont = Convert.ToInt32(reader["CONTAGEM"]);
                    retorno.LstConteudosPP.Add(conteudo);
                }
            }
            retorno.Contagem = cont;

            return retorno;
        }

        public async Task SalvarConteudoPP(ConteudoPP arquivo)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("SalvarConteudoPP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@NOME", arquivo.Nome);
                command.Parameters.AddWithValue("@CAMINHO", arquivo.Caminho);
                command.Parameters.AddWithValue("@NOME_GUID", arquivo.NomeGuid);
                command.Parameters.AddWithValue("@USUARIO", arquivo.Usuario);
                command.Parameters.AddWithValue("@DT_CADASTRO", arquivo.DtCadastro);
                command.Parameters.AddWithValue("@ATIVO", arquivo.Ativo);
                command.Parameters.AddWithValue("@PRIMEIRA_PAGINA", arquivo.PrimeiraPagina);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirConteudoPP(int IdConteudo)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ExcluirConteudoPP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@ID_CONTEUDO", IdConteudo);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
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

        public CaminhoFlipping CaminhoFlippingViewModel(long idProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarCaminhoFlipping");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderCaminhoFlipping(reader);
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

        private CaminhoFlipping LerDataReaderCaminhoFlipping(SqlDataReader reader)
        {
            var retorno = new CaminhoFlipping();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    retorno = new CaminhoFlipping
                    {
                        IdCaminhoFlipping = Convert.ToInt32(reader["ID_CAMINHO_FLIPPING"]),
                        IdProduto = Convert.ToInt64(reader["ID_PRODUTO"]),
                        Caminho = reader["CAMINHO"].ToString(),
                        EBSA = reader["EBSA"].ToString()
                    };
                }
            }

            return retorno;
        }

        public void ExcluirCaminhoFlipp(long idProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ExcluirCaminhoFlipp");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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

        public void SalvarCaminhoFlipp(CaminhoFlipping cam)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("SalvarCaminhoFlipp");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@ID_PRODUTO", cam.IdProduto);
                command.Parameters.AddWithValue("@EBSA", cam.EBSA);
                command.Parameters.AddWithValue("@CAMINHO", cam.Caminho);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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
    }
}
