using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class AmarracaoRepository : IAmarracaoRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;
        public AmarracaoRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }
         
        public List<RegraPagamento> ListarRegraPagamento()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarRegraPagamento");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderRegraPagamento(reader);
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

        private List<RegraPagamento> LerDataReaderRegraPagamento(SqlDataReader reader)
        {
            var lst = new List<RegraPagamento>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RegraPagamento a = new RegraPagamento
                    {
                        IdRegraPagamento = Convert.ToInt32(reader["ID_REGRA_PGTO"]),
                        DescricaoRegraPagamento = reader["DESCRICAO_REGRA_PGTO"].ToString()
                    };

                    lst.Add(a);
                }
            }
            return lst;
        }

        public List<PrazoValidade> ListarPrazoValidade()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarPrazoValidade");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderPrazoValidade(reader);
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

        private List<PrazoValidade> LerDataReaderPrazoValidade(SqlDataReader reader)
        {
            var lst = new List<PrazoValidade>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    PrazoValidade a = new PrazoValidade
                    {
                        IdPrazoValidade = Convert.ToInt32(reader["ID_PRAZO_VALIDADE"]),
                        DescricaoPrazoValidade = reader["DESCRICAO_PRAZO_VALIDADE"].ToString()
                    };

                    lst.Add(a);
                }
            }
            return lst;
        }

        public List<BloqueioPagamento> ListarBloqueioPagamento()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarBloqueioPagamento");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderBloqueioPagamento(reader);
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

        private List<BloqueioPagamento> LerDataReaderBloqueioPagamento(SqlDataReader reader)
        {
            var lst = new List<BloqueioPagamento>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BloqueioPagamento a = new BloqueioPagamento
                    {
                        IdBloqueioPagamento = Convert.ToInt32(reader["ID_BLOQUEIO_PGTO"]),
                        DescricaoBloqueioPagamento = reader["DESCRICAO_BLOQUEIO_PGTO"].ToString()
                    };

                    lst.Add(a);
                }
            }
            return lst;
        }

        public List<TipoContrato> ListarTipoContrato()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarTipoContrato");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderTipoContrato(reader);
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

        private List<TipoContrato> LerDataReaderTipoContrato(SqlDataReader reader)
        {
            var lst = new List<TipoContrato>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TipoContrato a = new TipoContrato
                    {
                        IdTipoContrato = Convert.ToInt32(reader["ID_TIPO_CONTRATO"]),
                        DescricaoTipoContrato = reader["DESCRICAO_TIPO_CONTRATO"].ToString()
                    };

                    lst.Add(a);
                }
            }
            return lst;
        }

        public List<TipoParticipacao> ListarTipoParticipacao()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarTipoParticipacao");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderTipoParticipacao(reader);
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

        private List<TipoParticipacao> LerDataReaderTipoParticipacao(SqlDataReader reader)
        {
            var lst = new List<TipoParticipacao>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TipoParticipacao a = new TipoParticipacao
                    {
                        IdTipoParticipacao = Convert.ToInt32(reader["ID_TIPO_PARTICIPACAO"]),
                        DescricaoTipoParticipacao = reader["DESCRICAO_TIPO_PARTICIPACAO"].ToString()
                    };

                    lst.Add(a);
                }
            }
            return lst;
        }

        public async Task<int> SalvarAmarracao(Amarracao a)
        {
            return 0;
        }
    }
}
