using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Linq;

namespace CoBRA.Infra.Corpore
{
    public class UsuarioCorporeRepository : BaseRepository, IUsuarioCorporeRepository
    {
        public UsuarioCorporeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public IEnumerable<Usuario> ObterAniversariantes()
        {
            using (SqlConnection connection = GetConnection())
            {

                IEnumerable<Usuario> aniversariantes = connection.Query<Usuario>(
                    "SELECT NOME, DTNASCIMENTO as DATANASCIMENTO, SECAO, CHAPA as CODUSUARIO " +
                    "FROM VW_ANIVERSARIANTES " +
                    "WHERE CAST(DATEADD(YEAR, (DATEDIFF(YEAR, DTNASCIMENTO, GETDATE())) ,DTNASCIMENTO) AS DATE) >= CAST(GETDATE() AS DATE)" +
                    "AND DATEADD(YEAR, (DATEDIFF(YEAR, DTNASCIMENTO, GETDATE())) ,DTNASCIMENTO) <=  DATEADD(DAY, @limite, GETDATE())" +
                    "ORDER BY DATEADD(YEAR, (DATEDIFF(YEAR, DTNASCIMENTO, GETDATE())), DTNASCIMENTO)",
                    new {
                        limite = 15,
                    });

                return aniversariantes;
            }

        }

        public IEnumerable<Usuario> ObterAdmissoes()
        {
            using(SqlConnection connection = GetConnection())
            {
                IEnumerable<Usuario> admitidos = connection.Query<Usuario>(
                    "SELECT NOME, ADMISSAO as DATAADMISSAO, CHAPA as CODUSUARIO, SECAO " +
                    "FROM VW_ADMITIDOSMES " +
                    "WHERE MONTH(ADMISSAO) = @mes AND YEAR(ADMISSAO) = @ano  " +
                    "ORDER BY DAY(ADMISSAO)",
                    new
                    {
                        ano = DateTime.Now.Year,
                        mes = DateTime.Now.Month
                    }
                );

                return admitidos;
            }
        }
    }
}
