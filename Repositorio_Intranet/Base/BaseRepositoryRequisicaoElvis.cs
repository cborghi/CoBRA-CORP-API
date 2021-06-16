using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace CoBRA.Infra.Intranet.Base
{
    public class BaseRepositoryRequisicaoElvis : BaseRepository, IBaseRepositoryRequisicaoElvis
    {
        public BaseRepositoryRequisicaoElvis(IConfiguration configuration) : base(configuration)
        {

        }

        public List<ParcelaRequisicao> ListarParcelasRequisicao(string requisicaoId)
        {
            List<ParcelaRequisicao> parcelas = new List<ParcelaRequisicao>();

            StringBuilder query = new StringBuilder(@"SELECT ID, ID_REQ, NUMERO_PARCELA, VALOR, DATA_PAGTO FROM REQUISICAO_PARCELA WHERE ID_REQ = @requisicaoId");

            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@requisicaoId", Value = requisicaoId }
            };

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        parcelas.Add(new ParcelaRequisicao()
                        {
                            ID = dataReader["ID"].ToString(),
                            RequisicaoID = dataReader["ID_REQ"].ToString(),
                            NumeroParcela = int.Parse(dataReader["NUMERO_PARCELA"].ToString()),
                            Valor = decimal.Parse(dataReader["VALOR"].ToString()),
                            PrazoPagamento = DateTime.Parse(dataReader["DATA_PAGTO"].ToString())
                        });
                    }
                }
            }

            return parcelas;
        }

        public int InserirRequisicao(RequisicaoGerada requisicao, bool elvis)
        {
            string insertSql = @" Insert into REQUISICAO (DEPTO, TIPODOC, EMISSAO, FORNECEDOR, LOJA, VALOR, PRAZO_ENTREGA, PRAZO_PAGTO, PESSOA, OPSIMP, FORMA_PAGTO, CONFIRMAR_PAGTO, DOC, SERIE, TELEFONE, CONTATO, OUTROS, OBS, APROVADO_SUPERVISOR, APROVADO_GERENTE,
                 OUTROS_SERVICOS, INCLUIDOR, INCLUIDO, DESC_PAG, EXCLUIDO, ID_REQUISICAO_DEPTO, MOEDA, ID_ELVIS, DOC_LINK, ELVIS_ORIGEM, NOTA_FISCAL_COMPARTILHADA)
                 output INSERTED.ID values (@departamento, @tipoDoc, @emissao, @fornecedor, @loja, @valor, @prazoEntrega, @prazoPagamento, @pessoa, '', @formaPagamento, @confirmarPagamento, @nf, @nfserie,
                 @telefone, @contato, '', @obs, 'N', 'N', '', @usuario, @dataRequisicao, @descPag, 'N', @iddepto, @moeda, @idElvis, @link, @elvisOrigem, @NOTA_FISCAL_COMPARTILHADA)";

            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@tipoDoc", Value = requisicao.TipoDocumento ?? Convert.DBNull },
                  new SqlParameter() { ParameterName = "@emissao", Value = DateTime.Now },
                  new SqlParameter() { ParameterName = "@fornecedor", Value = requisicao.Fornecedor.Codigo ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@loja", Value = requisicao.Fornecedor.Loja ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@valor", Value = requisicao.ValorTotal},
                  new SqlParameter() { ParameterName = "@prazoEntrega", Value = requisicao.DataEntrega},
                  new SqlParameter() { ParameterName = "@prazoPagamento", Value = requisicao.DataPagamento},
                  new SqlParameter() { ParameterName = "@formaPagamento", Value = requisicao.FormaPagamento ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@confirmarPagamento", Value = requisicao.ConfirmaPagamento ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@nf", Value = requisicao.Nota ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@nfserie", Value = requisicao.Serie ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@telefone", Value = requisicao.Telefone ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@contato", Value = requisicao.Contato ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@obs", Value = requisicao.Observacao ?? Convert.DBNull },
                  new SqlParameter() { ParameterName = "@usuario", Value = requisicao.Usuario.Id },
                  new SqlParameter() { ParameterName = "@dataRequisicao", Value = DateTime.Now},
                  new SqlParameter() { ParameterName = "@descPag", Value = requisicao.Cartao ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@moeda", Value = requisicao.Moeda ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@pessoa", Value = requisicao.Fornecedor.Tipo ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@idElvis", Value = requisicao.IdElvis ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@iddepto", Value = requisicao.Usuario.Grupo.Id},
                  new SqlParameter() { ParameterName = "@link", Value = requisicao.Link ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@elvisOrigem", Value = elvis},
                  new SqlParameter() { ParameterName = "@departamento", Value = requisicao.Usuario.Grupo.CentroCusto ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@NOTA_FISCAL_COMPARTILHADA", Value = requisicao.NotaFiscalCompartilhada},

            };

            return (int)ExecuteScalar(insertSql, param, CommandType.Text);
        }
            
        public int InserirParcelaRequisicao(string requisicaoID, ParcelaRequisicao parcela)
        {
            string insertSql = @"INSERT INTO REQUISICAO_PARCELA(ID_REQ, NUMERO_PARCELA, VALOR, DATA_PAGTO) VALUES (@requisicao, @parcela, @valor, @datapagto)";

            List<DbParameter> param = new List<DbParameter>()
            {
                new SqlParameter() { ParameterName = "@requisicao", Value = requisicaoID },
                new SqlParameter() { ParameterName = "@parcela", Value = parcela.NumeroParcela},
                new SqlParameter() { ParameterName = "@valor", Value = parcela.Valor},
                new SqlParameter() { ParameterName = "@datapagto", Value = parcela.PrazoPagamento}
            };

            return ExecuteNonQuery(insertSql, param, CommandType.Text);
        }

        public void AtualizarRequisicao(RequisicaoAtualizada requisicao)
        {
            string updateSql = @" UPDATE REQUISICAO SET DEPTO = @departamento
					            , TIPODOC = @tipoDoc
					            , EMISSAO = @emissao
					            , FORNECEDOR = @fornecedor
					            , LOJA = @loja
					            , VALOR = @valor
					            , PRAZO_ENTREGA = @prazoEntrega
					            , PRAZO_PAGTO = @prazoPagamento
					            , PESSOA = @pessoa
					            , OPSIMP = ''
					            , FORMA_PAGTO = @formaPagamento
					            , CONFIRMAR_PAGTO = @confirmarPagamento
					            , DOC = @nf
					            , SERIE = @nfserie
					            , TELEFONE = @telefone
					            , CONTATO = @contato
					            , OUTROS = ''
					            , OBS = @obs
					            , APROVADO_SUPERVISOR = 'N'
					            , APROVADO_GERENTE = 'N'
					            , OUTROS_SERVICOS = ''
					            , ALTERADOR = @usuario
					            , ALTERADO = GETDATE()
					            , DESC_PAG = @descPag
					            , EXCLUIDO = 'N'
					            , ID_REQUISICAO_DEPTO = @iddepto
					            , MOEDA = @moeda
					            , ID_ELVIS = @idElvis
					            , DOC_LINK = @link
                                , NOTA_FISCAL_COMPARTILHADA = @NOTA_FISCAL_COMPARTILHADA
                                WHERE ID = @idRequisicao";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@idRequisicao", Value = requisicao.IdRequisicao},
                  new SqlParameter() { ParameterName = "@tipoDoc", Value = requisicao.TipoDocumento ?? Convert.DBNull },
                  new SqlParameter() { ParameterName = "@emissao", Value = DateTime.Now },
                  new SqlParameter() { ParameterName = "@fornecedor", Value = requisicao.Fornecedor.Codigo ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@loja", Value = requisicao.Fornecedor.Loja ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@valor", Value = requisicao.ValorTotal},
                  new SqlParameter() { ParameterName = "@prazoEntrega", Value = requisicao.DataEntrega},
                  new SqlParameter() { ParameterName = "@prazoPagamento", Value = requisicao.DataPagamento},
                  new SqlParameter() { ParameterName = "@formaPagamento", Value = requisicao.FormaPagamento ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@confirmarPagamento", Value = requisicao.ConfirmaPagamento ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@nf", Value = requisicao.Nota ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@nfserie", Value = requisicao.Serie ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@telefone", Value = requisicao.Telefone ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@contato", Value = requisicao.Contato ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@obs", Value = requisicao.Observacao ?? Convert.DBNull },
                  new SqlParameter() { ParameterName = "@usuario", Value = requisicao.Usuario.Id },
                  new SqlParameter() { ParameterName = "@dataRequisicao", Value = DateTime.Now},
                  new SqlParameter() { ParameterName = "@descPag", Value = requisicao.Cartao ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@moeda", Value = requisicao.Moeda ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@pessoa", Value = requisicao.Fornecedor.Tipo ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@idElvis", Value = requisicao.IdElvis ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@iddepto", Value = requisicao.Usuario.Grupo.Id},
                  new SqlParameter() { ParameterName = "@link", Value = requisicao.Link},
                  new SqlParameter() { ParameterName = "@departamento", Value = requisicao.Usuario.Grupo.CentroCusto ?? Convert.DBNull},
                  new SqlParameter() { ParameterName = "@NOTA_FISCAL_COMPARTILHADA", Value = requisicao.NotaFiscalCompartilhada},

            };

            ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public int InserirRequisicaoObra(int requisicao, string obra, decimal valor)
        {
            string updateSql = @"INSERT INTO REQUISICAO_OBRA(ID_REQ, OBRA, RATEIO) VALUES (@requisicao, @obra, @rateio)";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@requisicao", Value = requisicao },
                  new SqlParameter() { ParameterName = "@obra", Value = obra},
                  new SqlParameter() { ParameterName = "@rateio", Value = valor},
            };

            return ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public int InserirRequisicaoServico(int requisicao, string servico)
        {
            string updateSql = @"INSERT INTO REQUISICAO_SERVICO(ID_REQ, SERVICO) VALUES (@requisicao, @servico)";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@requisicao", Value = requisicao },
                  new SqlParameter() { ParameterName = "@servico", Value = servico},
            };

            return ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public int AlterarRequisicaoObra(int requisicao, string obra, decimal valor)
        {
            string updateSql = @"UPDATE REQUISICAO_OBRA SET RATEIO = @rateio WHERE ID_REQ = @requisicao AND OBRA = @obra ";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@requisicao", Value = requisicao },
                  new SqlParameter() { ParameterName = "@obra", Value = obra},
                  new SqlParameter() { ParameterName = "@rateio", Value = valor},
            };

            return ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public void ExcluiRequisicaoObra(int requisicao)
        {
            string updateSql = @"DELETE FROM REQUISICAO_OBRA WHERE ID_REQ = @requisicao ";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@requisicao", Value = requisicao },
            };

            ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public void ExcluirParcelasRequisicao(string requisicaoId)
        {
            string updateSql = @"DELETE FROM REQUISICAO_PARCELA WHERE ID_REQ = @requisicao ";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@requisicao", Value = requisicaoId },
            };

            ExecuteNonQuery(updateSql, param, CommandType.Text);
        }
    }
}
