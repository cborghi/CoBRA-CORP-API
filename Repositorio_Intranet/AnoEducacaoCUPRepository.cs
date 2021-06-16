using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CoBRA.Infra.Intranet
{
    public class AnoEducacaoCUPRepository : BaseRepository, IAnoEducacaoCUPRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public AnoEducacaoCUPRepository(IBaseRepositoryPainelMeta baseRepository, IConfiguration configuration) : base(configuration)
        {
            _baseRepository = baseRepository;
        }

        public List<AnoEducacaoCUP> CarregarAnoEducacao()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAnoEducacaoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderAnoEducacaoCUP(reader);
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

        private List<AnoEducacaoCUP> LerDataReaderAnoEducacaoCUP(SqlDataReader reader)
        {
            var lstAnoEducacao = new List<AnoEducacaoCUP>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AnoEducacaoCUP anoeducacao = new AnoEducacaoCUP
                    {
                        aedu_id = Convert.ToInt32(reader["aedu_id"]),
                        aedu_descricao = reader["aedu_descricao"].ToString(),
                        aedu_segm = Convert.ToInt32(reader["aedu_segm"]),
                        Abreviacao = reader["ABREVIACAO"].ToString()
                    };
                    lstAnoEducacao.Add(anoeducacao);
                }
            }
            return lstAnoEducacao;
        }
    }
}
