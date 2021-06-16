using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace CoBRA.Infra.Intranet
{
    public class NivelRepository : BaseRepository, INivelRepository
    {
        public NivelRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Nivel> Obter()
        {
            try
            {
                List<Nivel> niveis = new List<Nivel>();

                string query = @"SELECT ID_NIVEL, DESCRICAO, ATIVO, DATA_CRIACAO FROM NIVEL";

                using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {

                            niveis.Add(new Nivel()
                            {
                                Id = Convert.ToInt32(dataReader["ID_NIVEL"]),
                                Descricao = dataReader["DESCRICAO"].ToString(),
                                Ativo = Convert.ToBoolean(dataReader["ATIVO"]),
                                DataCriacao = Convert.ToDateTime(dataReader["DATA_CRIACAO"])
                            });
                        }
                    }
                }

                return niveis;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
