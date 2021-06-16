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
    public class AutorBeneficiarioRepository : IAutorBeneficiarioRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;
        public AutorBeneficiarioRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<int> SalvarAutor(AutorDA autorBeneficiario)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("SalvarAutor");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);
                command.Parameters.AddWithValue("@NOME", autorBeneficiario.Nome);
                command.Parameters.AddWithValue("@CODIGO", autorBeneficiario.Codigo);
                command.Parameters.AddWithValue("@LOJA", autorBeneficiario.Loja);
                command.Parameters.AddWithValue("@CPF_CNPJ", autorBeneficiario.CpfCnpj);
                command.Parameters.AddWithValue("@RG", autorBeneficiario.RG);
                command.Parameters.AddWithValue("@SEXO", autorBeneficiario.Sexo);
                command.Parameters.AddWithValue("@NACIONALIDADE", autorBeneficiario.Nacionalidade);
                command.Parameters.AddWithValue("@NATURALIDADE", autorBeneficiario.Naturalidade);
                command.Parameters.AddWithValue("@DT_NASCIMENTO", autorBeneficiario.DataNascimento);
                command.Parameters.AddWithValue("@ID_ESTADO_CIVIL", autorBeneficiario.IdEstadoCivil);
                command.Parameters.AddWithValue("@PROFISSAO", autorBeneficiario.Profissao);
                command.Parameters.AddWithValue("@NIT_PIS", autorBeneficiario.NitPis);
                command.Parameters.AddWithValue("@SITUACAO", autorBeneficiario.Situacao);
                command.Parameters.AddWithValue("@TELEFONE_RES", autorBeneficiario.TelResidencial);
                command.Parameters.AddWithValue("@TELEFONE_CEL", autorBeneficiario.Celular);
                command.Parameters.AddWithValue("@TELEFONE_COM", autorBeneficiario.TelComercial);
                command.Parameters.AddWithValue("@CONTATO", autorBeneficiario.Contato);
                command.Parameters.AddWithValue("@CEP", autorBeneficiario.CEP);
                command.Parameters.AddWithValue("@ID_ESTADO", autorBeneficiario.IdEstado);
                command.Parameters.AddWithValue("@CIDADE", autorBeneficiario.Cidade);
                command.Parameters.AddWithValue("@BAIRRO", autorBeneficiario.Bairro);
                command.Parameters.AddWithValue("@LOGRADOURO", autorBeneficiario.Logradouro);
                command.Parameters.AddWithValue("@NUMERO", autorBeneficiario.Numero);
                command.Parameters.AddWithValue("@COMPLEMENTO", autorBeneficiario.Complemento);
                command.Parameters.AddWithValue("@CORRENTISTA", autorBeneficiario.Correntista);
                command.Parameters.AddWithValue("@CORRENTISTA_CPF_CNPJ", autorBeneficiario.CorrentistaCpfCnpj);
                command.Parameters.AddWithValue("@BANCO", autorBeneficiario.Banco);
                command.Parameters.AddWithValue("@AGENCIA", autorBeneficiario.Agencia);
                command.Parameters.AddWithValue("@ID_TIPO_CONTA", autorBeneficiario.IdTipoConta);
                command.Parameters.AddWithValue("@NUMERO_CONTA", autorBeneficiario.NumeroConta);
                command.Parameters.AddWithValue("@DATA_AUTORIZACAO", autorBeneficiario.DataAutorizacao);
                command.Parameters.AddWithValue("@ENVIAR_DEMONSTRATIVO", autorBeneficiario.EnviarDemonstrativo);
                command.Parameters.AddWithValue("@PAGAMENTO_MINIMO", autorBeneficiario.PagamentoMinimo);
                command.Parameters.AddWithValue("@EMITE_RECIBO", autorBeneficiario.EmiteRecibo);
                command.Parameters.AddWithValue("@EMAIL_CONTADOR", autorBeneficiario.EmailContador);
                command.Parameters.AddWithValue("@OBSERVACAO", autorBeneficiario.Observacao);
                command.Parameters.AddWithValue("@ID_USUARIO_INCLUSAO", autorBeneficiario.IdUsuarioInclusao);
                command.Parameters.AddWithValue("@DATA_INCLUSAO", autorBeneficiario.DataInclusao);
                command.Parameters.AddWithValue("@ID_TIPO_CADASTRO", autorBeneficiario.IdTipoCadastro);
                command.Parameters.AddWithValue("@INCLUIDO_POR", autorBeneficiario.IncluidoPor);
                command.Parameters.AddWithValue("@TIPO_PESSOA", autorBeneficiario.TipoPessoa);
                command.Parameters.AddWithValue("@ATIVO", true);

                try
                {
                    connection.Open();
                    int idAutorBeneficiario = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return idAutorBeneficiario;
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

        public async Task AtualizarAutor(AutorDA autorBeneficiario)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("AtualizarAutor");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", autorBeneficiario.IdAutorBeneficiario);
                command.Parameters.AddWithValue("@NOME", autorBeneficiario.Nome);
                command.Parameters.AddWithValue("@CODIGO", autorBeneficiario.Codigo);
                command.Parameters.AddWithValue("@LOJA", autorBeneficiario.Loja);
                command.Parameters.AddWithValue("@CPF_CNPJ", autorBeneficiario.CpfCnpj);
                command.Parameters.AddWithValue("@RG", autorBeneficiario.RG);
                command.Parameters.AddWithValue("@SEXO", autorBeneficiario.Sexo);
                command.Parameters.AddWithValue("@NACIONALIDADE", autorBeneficiario.Nacionalidade);
                command.Parameters.AddWithValue("@NATURALIDADE", autorBeneficiario.Naturalidade);
                command.Parameters.AddWithValue("@DT_NASCIMENTO", autorBeneficiario.DataNascimento);
                command.Parameters.AddWithValue("@ID_ESTADO_CIVIL", autorBeneficiario.IdEstadoCivil);
                command.Parameters.AddWithValue("@PROFISSAO", autorBeneficiario.Profissao);
                command.Parameters.AddWithValue("@NIT_PIS", autorBeneficiario.NitPis);
                command.Parameters.AddWithValue("@SITUACAO", autorBeneficiario.Situacao);
                command.Parameters.AddWithValue("@TELEFONE_RES", autorBeneficiario.TelResidencial);
                command.Parameters.AddWithValue("@TELEFONE_CEL", autorBeneficiario.Celular);
                command.Parameters.AddWithValue("@TELEFONE_COM", autorBeneficiario.TelComercial);
                command.Parameters.AddWithValue("@CONTATO", autorBeneficiario.Contato);
                command.Parameters.AddWithValue("@CEP", autorBeneficiario.CEP);
                command.Parameters.AddWithValue("@ID_ESTADO", autorBeneficiario.IdEstado);
                command.Parameters.AddWithValue("@CIDADE", autorBeneficiario.Cidade);
                command.Parameters.AddWithValue("@BAIRRO", autorBeneficiario.Bairro);
                command.Parameters.AddWithValue("@LOGRADOURO", autorBeneficiario.Logradouro);
                command.Parameters.AddWithValue("@NUMERO", autorBeneficiario.Numero);
                command.Parameters.AddWithValue("@COMPLEMENTO", autorBeneficiario.Complemento);
                command.Parameters.AddWithValue("@CORRENTISTA", autorBeneficiario.Correntista);
                command.Parameters.AddWithValue("@CORRENTISTA_CPF_CNPJ", autorBeneficiario.CorrentistaCpfCnpj);
                command.Parameters.AddWithValue("@BANCO", autorBeneficiario.Banco);
                command.Parameters.AddWithValue("@AGENCIA", autorBeneficiario.Agencia);
                command.Parameters.AddWithValue("@ID_TIPO_CONTA", autorBeneficiario.IdTipoConta);
                command.Parameters.AddWithValue("@NUMERO_CONTA", autorBeneficiario.NumeroConta);
                command.Parameters.AddWithValue("@DATA_AUTORIZACAO", autorBeneficiario.DataAutorizacao);
                command.Parameters.AddWithValue("@ENVIAR_DEMONSTRATIVO", autorBeneficiario.EnviarDemonstrativo);
                command.Parameters.AddWithValue("@PAGAMENTO_MINIMO", autorBeneficiario.PagamentoMinimo);
                command.Parameters.AddWithValue("@EMITE_RECIBO", autorBeneficiario.EmiteRecibo);
                command.Parameters.AddWithValue("@EMAIL_CONTADOR", autorBeneficiario.EmailContador);
                command.Parameters.AddWithValue("@OBSERVACAO", autorBeneficiario.Observacao);
                command.Parameters.AddWithValue("@ID_USUARIO_INCLUSAO", autorBeneficiario.IdUsuarioInclusao);
                command.Parameters.AddWithValue("@DATA_INCLUSAO", autorBeneficiario.DataInclusao);
                command.Parameters.AddWithValue("@ID_TIPO_CADASTRO", autorBeneficiario.IdTipoCadastro);
                command.Parameters.AddWithValue("@INCLUIDO_POR", autorBeneficiario.IncluidoPor);
                command.Parameters.AddWithValue("@TIPO_PESSOA", autorBeneficiario.TipoPessoa);
                command.Parameters.AddWithValue("@ATIVO", true);

                try
                {
                    connection.Open();
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

        public AutorDA BuscarAutorPorId(int id)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarAutorPorId");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderAutor(reader);
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

        private AutorDA LerDataReaderAutor(SqlDataReader reader)
        {
            var autor = new AutorDA();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    autor = new AutorDA
                    {
                        IdAutorBeneficiario = Convert.ToInt32(reader["ID_AUTOR_BENEFICIARIO"]),
                        Nome = reader["NOME"].ToString(),
                        Codigo = reader["CODIGO"].ToString(),
                        Loja = reader["LOJA"].ToString(),
                        CpfCnpj = reader["CPF_CNPJ"].ToString(),
                        RG = reader["RG"].ToString(),
                        Sexo = reader["SEXO"].ToString(),
                        Nacionalidade = reader["NACIONALIDADE"].ToString(),
                        Naturalidade = reader["NATURALIDADE"].ToString(),
                        DataNascimento = Convert.ToDateTime(reader["DT_NASCIMENTO"]),
                        IdEstadoCivil = Convert.ToInt16(reader["ID_ESTADO_CIVIL"]),
                        Profissao = reader["PROFISSAO"].ToString(),
                        NitPis = reader["NIT_PIS"].ToString(),
                        Situacao = reader["SITUACAO"].ToString(),
                        TelResidencial = reader["TELEFONE_RES"].ToString(),
                        Celular = reader["TELEFONE_CEL"].ToString(),
                        TelComercial = reader["TELEFONE_COM"].ToString(),
                        Contato = reader["CONTATO"].ToString(),
                        CEP = reader["CEP"].ToString(),
                        IdEstado = Convert.ToInt32(reader["ID_ESTADO"]),
                        Cidade = reader["CIDADE"].ToString(),
                        Bairro = reader["BAIRRO"].ToString(),
                        Logradouro = reader["LOGRADOURO"].ToString(),
                        Numero = reader["NUMERO"].ToString(),
                        Complemento = reader["COMPLEMENTO"].ToString(),
                        Correntista = reader["CORRENTISTA"].ToString(),
                        CorrentistaCpfCnpj = reader["CORRENTISTA_CPF_CNPJ"].ToString(),
                        Banco = reader["BANCO"].ToString(),
                        Agencia = reader["AGENCIA"].ToString(),
                        IdTipoConta = Convert.ToInt16(reader["ID_TIPO_CONTA"]),
                        NumeroConta = reader["NUMERO_CONTA"].ToString(),
                        DataAutorizacao = Convert.ToDateTime(reader["DATA_AUTORIZACAO"]),
                        EnviarDemonstrativo = Convert.ToBoolean(reader["ENVIAR_DEMONSTRATIVO"]),
                        PagamentoMinimo = Convert.ToDecimal(reader["PAGAMENTO_MINIMO"]),
                        EmiteRecibo = Convert.ToBoolean(reader["EMITE_RECIBO"]),
                        EmailContador = reader["EMAIL_CONTADOR"].ToString(),
                        Observacao = reader["OBSERVACAO"].ToString(),
                        IdUsuarioInclusao = Convert.ToInt32(reader["ID_USUARIO_INCLUSAO"]),
                        DataInclusao = Convert.ToDateTime(reader["DATA_INCLUSAO"]),
                        IdTipoCadastro = Convert.ToInt16(reader["ID_TIPO_CADASTRO"]),
                        IncluidoPor = reader["INCLUIDO_POR"].ToString(),
                        TipoPessoa = reader["TIPO_PESSOA"].ToString(),
                        Ativo = Convert.ToBoolean(reader["ATIVO"])
                    };
                }
            }
            autor.LstLogAutorBeneficiario = new List<LogAutorBeneficiario>();
            return autor;
        }

        private List<AutorDA> LerDataReaderLstAutor(SqlDataReader reader)
        {
            var lstAutor = new List<AutorDA>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var autor = new AutorDA
                    {
                        IdAutorBeneficiario = Convert.ToInt32(reader["ID_AUTOR_BENEFICIARIO"]),
                        Nome = reader["NOME"].ToString(),
                        Codigo = reader["CODIGO"].ToString(),
                        Loja = reader["LOJA"].ToString(),
                        CpfCnpj = reader["CPF_CNPJ"].ToString(),
                        RG = reader["RG"].ToString(),
                        Sexo = reader["SEXO"].ToString(),
                        Nacionalidade = reader["NACIONALIDADE"].ToString(),
                        Naturalidade = reader["NATURALIDADE"].ToString(),
                        DataNascimento = Convert.ToDateTime(reader["DT_NASCIMENTO"]),
                        IdEstadoCivil = Convert.ToInt16(reader["ID_ESTADO_CIVIL"]),
                        Profissao = reader["PROFISSAO"].ToString(),
                        NitPis = reader["NIT_PIS"].ToString(),
                        Situacao = reader["SITUACAO"].ToString(),
                        TelResidencial = reader["TELEFONE_RES"].ToString(),
                        Celular = reader["TELEFONE_CEL"].ToString(),
                        TelComercial = reader["TELEFONE_COM"].ToString(),
                        Contato = reader["CONTATO"].ToString(),
                        CEP = reader["CEP"].ToString(),
                        IdEstado = Convert.ToInt32(reader["ID_ESTADO"]),
                        Cidade = reader["CIDADE"].ToString(),
                        Bairro = reader["BAIRRO"].ToString(),
                        Logradouro = reader["LOGRADOURO"].ToString(),
                        Numero = reader["NUMERO"].ToString(),
                        Complemento = reader["COMPLEMENTO"].ToString(),
                        Correntista = reader["CORRENTISTA"].ToString(),
                        CorrentistaCpfCnpj = reader["CORRENTISTA_CPF_CNPJ"].ToString(),
                        Banco = reader["BANCO"].ToString(),
                        Agencia = reader["AGENCIA"].ToString(),
                        IdTipoConta = Convert.ToInt16(reader["ID_TIPO_CONTA"]),
                        NumeroConta = reader["NUMERO_CONTA"].ToString(),
                        DataAutorizacao = Convert.ToDateTime(reader["DATA_AUTORIZACAO"]),
                        EnviarDemonstrativo = Convert.ToBoolean(reader["ENVIAR_DEMONSTRATIVO"]),
                        PagamentoMinimo = Convert.ToDecimal(reader["PAGAMENTO_MINIMO"]),
                        EmiteRecibo = Convert.ToBoolean(reader["EMITE_RECIBO"]),
                        EmailContador = reader["EMAIL_CONTADOR"].ToString(),
                        Observacao = reader["OBSERVACAO"].ToString(),
                        IdUsuarioInclusao = Convert.ToInt32(reader["ID_USUARIO_INCLUSAO"]),
                        DataInclusao = Convert.ToDateTime(reader["DATA_INCLUSAO"]),
                        IdTipoCadastro = Convert.ToInt16(reader["ID_TIPO_CADASTRO"]),
                        IncluidoPor = reader["INCLUIDO_POR"].ToString(),
                        TipoPessoa = reader["TIPO_PESSOA"].ToString(),
                        Ativo = Convert.ToBoolean(reader["ATIVO"])
                    };
                    autor.LstLogAutorBeneficiario = new List<LogAutorBeneficiario>();
                    lstAutor.Add(autor);
                }
            }
            return lstAutor;
        }

        public BeneficiarioDA BuscarBeneficiarioPorId(int id)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarBeneficiarioPorId");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderBeneficiario(reader);
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

        private BeneficiarioDA LerDataReaderBeneficiario(SqlDataReader reader)
        {
            var beneficiario = new BeneficiarioDA();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    beneficiario = new BeneficiarioDA
                    {
                        IdAutorBeneficiario = Convert.ToInt32(reader["ID_AUTOR_BENEFICIARIO"]),
                        Nome = reader["NOME"].ToString(),
                        Codigo = reader["CODIGO"].ToString(),
                        Loja = reader["LOJA"].ToString(),
                        CpfCnpj = reader["CPF_CNPJ"].ToString(),
                        TipoEmpresa = reader["TIPO_EMPRESA"].ToString(),
                        Inscricao = reader["INSCRICAO"].ToString(),
                        Situacao = reader["SITUACAO"].ToString(),
                        TelResidencial = reader["TELEFONE_RES"].ToString(),
                        Celular = reader["TELEFONE_CEL"].ToString(),
                        TelComercial = reader["TELEFONE_COM"].ToString(),
                        Contato = reader["CONTATO"].ToString(),
                        CEP = reader["CEP"].ToString(),
                        IdEstado = Convert.ToInt32(reader["ID_ESTADO"]),
                        Cidade = reader["CIDADE"].ToString(),
                        Bairro = reader["BAIRRO"].ToString(),
                        Logradouro = reader["LOGRADOURO"].ToString(),
                        Numero = reader["NUMERO"].ToString(),
                        Complemento = reader["COMPLEMENTO"].ToString(),
                        Correntista = reader["CORRENTISTA"].ToString(),
                        CorrentistaCpfCnpj = reader["CORRENTISTA_CPF_CNPJ"].ToString(),
                        Banco = reader["BANCO"].ToString(),
                        Agencia = reader["AGENCIA"].ToString(),
                        IdTipoConta = Convert.ToInt16(reader["ID_TIPO_CONTA"]),
                        NumeroConta = reader["NUMERO_CONTA"].ToString(),
                        DataAutorizacao = Convert.ToDateTime(reader["DATA_AUTORIZACAO"]),
                        EnviarDemonstrativo = Convert.ToBoolean(reader["ENVIAR_DEMONSTRATIVO"]),
                        PagamentoMinimo = Convert.ToDecimal(reader["PAGAMENTO_MINIMO"]),
                        EmiteRecibo = Convert.ToBoolean(reader["EMITE_RECIBO"]),
                        EmailContador = reader["EMAIL_CONTADOR"].ToString(),
                        Observacao = reader["OBSERVACAO"].ToString(),
                        IdUsuarioInclusao = Convert.ToInt32(reader["ID_USUARIO_INCLUSAO"]),
                        DataInclusao = Convert.ToDateTime(reader["DATA_INCLUSAO"]),
                        IdTipoCadastro = Convert.ToInt16(reader["ID_TIPO_CADASTRO"]),
                        IncluidoPor = reader["INCLUIDO_POR"].ToString(),
                        TipoPessoa = reader["TIPO_PESSOA"].ToString(),
                        Ativo = Convert.ToBoolean(reader["ATIVO"])
                    };
                }
            }
            beneficiario.LstLogAutorBeneficiario = new List<LogAutorBeneficiario>();
            return beneficiario;
        }

        public List<AutorDA> BuscarAutoresBeneficiarioPorId(int id)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAutoresBeneficiariosPorId");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderLstAutor(reader);
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

        public async Task<int> SalvarBeneficiario(BeneficiarioDA autorBeneficiario)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("SalvarBeneficiario");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);
                command.Parameters.AddWithValue("@NOME", autorBeneficiario.Nome);
                command.Parameters.AddWithValue("@CODIGO", autorBeneficiario.Codigo);
                command.Parameters.AddWithValue("@LOJA", autorBeneficiario.Loja);
                command.Parameters.AddWithValue("@CPF_CNPJ", autorBeneficiario.CpfCnpj);
                command.Parameters.AddWithValue("@SITUACAO", autorBeneficiario.Situacao);
                command.Parameters.AddWithValue("@TELEFONE_RES", autorBeneficiario.TelResidencial);
                command.Parameters.AddWithValue("@TELEFONE_CEL", autorBeneficiario.Celular);
                command.Parameters.AddWithValue("@TELEFONE_COM", autorBeneficiario.TelComercial);
                command.Parameters.AddWithValue("@CONTATO", autorBeneficiario.Contato);
                command.Parameters.AddWithValue("@CEP", autorBeneficiario.CEP);
                command.Parameters.AddWithValue("@TIPO_EMPRESA", autorBeneficiario.TipoEmpresa);
                command.Parameters.AddWithValue("@INSCRICAO", autorBeneficiario.Inscricao);
                command.Parameters.AddWithValue("@ID_ESTADO", autorBeneficiario.IdEstado);
                command.Parameters.AddWithValue("@CIDADE", autorBeneficiario.Cidade);
                command.Parameters.AddWithValue("@BAIRRO", autorBeneficiario.Bairro);
                command.Parameters.AddWithValue("@LOGRADOURO", autorBeneficiario.Logradouro);
                command.Parameters.AddWithValue("@NUMERO", autorBeneficiario.Numero);
                command.Parameters.AddWithValue("@COMPLEMENTO", autorBeneficiario.Complemento);
                command.Parameters.AddWithValue("@CORRENTISTA", autorBeneficiario.Correntista);
                command.Parameters.AddWithValue("@CORRENTISTA_CPF_CNPJ", autorBeneficiario.CorrentistaCpfCnpj);
                command.Parameters.AddWithValue("@BANCO", autorBeneficiario.Banco);
                command.Parameters.AddWithValue("@AGENCIA", autorBeneficiario.Agencia);
                command.Parameters.AddWithValue("@ID_TIPO_CONTA", autorBeneficiario.IdTipoConta);
                command.Parameters.AddWithValue("@NUMERO_CONTA", autorBeneficiario.NumeroConta);
                command.Parameters.AddWithValue("@DATA_AUTORIZACAO", autorBeneficiario.DataAutorizacao);
                command.Parameters.AddWithValue("@ENVIAR_DEMONSTRATIVO", autorBeneficiario.EnviarDemonstrativo);
                command.Parameters.AddWithValue("@PAGAMENTO_MINIMO", autorBeneficiario.PagamentoMinimo);
                command.Parameters.AddWithValue("@EMITE_RECIBO", autorBeneficiario.EmiteRecibo);
                command.Parameters.AddWithValue("@EMAIL_CONTADOR", autorBeneficiario.EmailContador);
                command.Parameters.AddWithValue("@OBSERVACAO", autorBeneficiario.Observacao);
                command.Parameters.AddWithValue("@ID_USUARIO_INCLUSAO", autorBeneficiario.IdUsuarioInclusao);
                command.Parameters.AddWithValue("@DATA_INCLUSAO", autorBeneficiario.DataInclusao);
                command.Parameters.AddWithValue("@ID_TIPO_CADASTRO", autorBeneficiario.IdTipoCadastro);
                command.Parameters.AddWithValue("@INCLUIDO_POR", autorBeneficiario.IncluidoPor);
                command.Parameters.AddWithValue("@TIPO_PESSOA", autorBeneficiario.TipoPessoa);
                command.Parameters.AddWithValue("@ATIVO", true);

                try
                {
                    connection.Open();
                    int idAutorBeneficiario = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return idAutorBeneficiario;
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

        public async Task AtualizarBeneficiario(BeneficiarioDA autorBeneficiario)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("AtualizarBeneficiario");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", autorBeneficiario.IdAutorBeneficiario);
                command.Parameters.AddWithValue("@NOME", autorBeneficiario.Nome);
                command.Parameters.AddWithValue("@CODIGO", autorBeneficiario.Codigo);
                command.Parameters.AddWithValue("@LOJA", autorBeneficiario.Loja);
                command.Parameters.AddWithValue("@CPF_CNPJ", autorBeneficiario.CpfCnpj);
                command.Parameters.AddWithValue("@SITUACAO", autorBeneficiario.Situacao);
                command.Parameters.AddWithValue("@TELEFONE_RES", autorBeneficiario.TelResidencial);
                command.Parameters.AddWithValue("@TELEFONE_CEL", autorBeneficiario.Celular);
                command.Parameters.AddWithValue("@TELEFONE_COM", autorBeneficiario.TelComercial);
                command.Parameters.AddWithValue("@CONTATO", autorBeneficiario.Contato);
                command.Parameters.AddWithValue("@CEP", autorBeneficiario.CEP);
                command.Parameters.AddWithValue("@TIPO_EMPRESA", autorBeneficiario.TipoEmpresa);
                command.Parameters.AddWithValue("@INSCRICAO", autorBeneficiario.Inscricao);
                command.Parameters.AddWithValue("@ID_ESTADO", autorBeneficiario.IdEstado);
                command.Parameters.AddWithValue("@CIDADE", autorBeneficiario.Cidade);
                command.Parameters.AddWithValue("@BAIRRO", autorBeneficiario.Bairro);
                command.Parameters.AddWithValue("@LOGRADOURO", autorBeneficiario.Logradouro);
                command.Parameters.AddWithValue("@NUMERO", autorBeneficiario.Numero);
                command.Parameters.AddWithValue("@COMPLEMENTO", autorBeneficiario.Complemento);
                command.Parameters.AddWithValue("@CORRENTISTA", autorBeneficiario.Correntista);
                command.Parameters.AddWithValue("@CORRENTISTA_CPF_CNPJ", autorBeneficiario.CorrentistaCpfCnpj);
                command.Parameters.AddWithValue("@BANCO", autorBeneficiario.Banco);
                command.Parameters.AddWithValue("@AGENCIA", autorBeneficiario.Agencia);
                command.Parameters.AddWithValue("@ID_TIPO_CONTA", autorBeneficiario.IdTipoConta);
                command.Parameters.AddWithValue("@NUMERO_CONTA", autorBeneficiario.NumeroConta);
                command.Parameters.AddWithValue("@DATA_AUTORIZACAO", autorBeneficiario.DataAutorizacao);
                command.Parameters.AddWithValue("@ENVIAR_DEMONSTRATIVO", autorBeneficiario.EnviarDemonstrativo);
                command.Parameters.AddWithValue("@PAGAMENTO_MINIMO", autorBeneficiario.PagamentoMinimo);
                command.Parameters.AddWithValue("@EMITE_RECIBO", autorBeneficiario.EmiteRecibo);
                command.Parameters.AddWithValue("@EMAIL_CONTADOR", autorBeneficiario.EmailContador);
                command.Parameters.AddWithValue("@OBSERVACAO", autorBeneficiario.Observacao);
                command.Parameters.AddWithValue("@ID_USUARIO_INCLUSAO", autorBeneficiario.IdUsuarioInclusao);
                command.Parameters.AddWithValue("@DATA_INCLUSAO", autorBeneficiario.DataInclusao);
                command.Parameters.AddWithValue("@ID_TIPO_CADASTRO", autorBeneficiario.IdTipoCadastro);
                command.Parameters.AddWithValue("@INCLUIDO_POR", autorBeneficiario.IncluidoPor);
                command.Parameters.AddWithValue("@TIPO_PESSOA", autorBeneficiario.TipoPessoa);
                command.Parameters.AddWithValue("@ATIVO", true);

                try
                {
                    connection.Open();
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

        public async Task SalvarAutoresBeneficiario(int idAutor, int idBeneficiario)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("SalvarAutoresBeneficiario");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);

                command.Parameters.AddWithValue("@ID_AUTOR", idAutor);
                command.Parameters.AddWithValue("@ID_BENEFICIARIO", idBeneficiario);

                try
                {
                    connection.Open();
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

        public async Task ExcluirAutoresBeneficiario(int idBeneficiario)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("ExcluirAutoresBeneficiario");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);

                command.Parameters.AddWithValue("@ID_BENEFICIARIO", idBeneficiario);

                try
                {
                    connection.Open();
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

        public List<TipoContaBancaria> ListarContaBancaria()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarContaBancaria");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderContaBancaria(reader);
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

        private List<TipoContaBancaria> LerDataReaderContaBancaria(SqlDataReader reader)
        {
            var lstContaBancaria = new List<TipoContaBancaria>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TipoContaBancaria contaBancario = new TipoContaBancaria
                    {
                        IdTipoConta = Convert.ToInt16(reader["ID_TIPO_CONTA"]),
                        Descricao = reader["DESCRICAO"].ToString()
                    };

                    lstContaBancaria.Add(contaBancario);
                }
            }
            return lstContaBancaria;
        }

        public List<EstadoCivil> ListarEstadoCivil()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarEstadoCivil");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderEstadoCivil(reader);
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

        private List<EstadoCivil> LerDataReaderEstadoCivil(SqlDataReader reader)
        {
            var lstEstadoCivil = new List<EstadoCivil>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EstadoCivil estadoCivil = new EstadoCivil
                    {
                        IdEstadoCivil = Convert.ToInt16(reader["ID_ESTADO_CIVIL"]),
                        Descricao = reader["DESCRICAO"].ToString()
                    };

                    lstEstadoCivil.Add(estadoCivil);
                }
            }
            return lstEstadoCivil;
        }

        public List<AutorDA> ListarAutores(string filtro)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAutores");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@FILTRO", filtro);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderLstAutor(reader);
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

        private List<AutorSimplificado> LerDataReaderAutores(SqlDataReader reader)
        {
            var lstAutores = new List<AutorSimplificado>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AutorSimplificado autores = new AutorSimplificado
                    {
                        IdAutorBeneficiario = Convert.ToInt16(reader["ID_AUTOR_BENEFICIARIO"]),
                        Nome = reader["NOME"].ToString(),
                        CpfCnpj = reader["CPF_CNPJ"].ToString(),
                        Codigo = reader["CODIGO"].ToString(),
                        Loja = reader["LOJA"].ToString()
                    };

                    lstAutores.Add(autores);
                }
            }
            return lstAutores;
        }

        public async Task SalvarLogAutorBeneficiario(int idAutorBeneficiario, int? idUsuarioInclusao, string msg)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("SalvarLogAutorBeneficiario");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);

                command.Parameters.AddWithValue("@ID_AUTOR", idAutorBeneficiario);
                command.Parameters.AddWithValue("@DATA_LOG", DateTime.Now);
                command.Parameters.AddWithValue("@DESCRICAO_LOG", msg);
                command.Parameters.AddWithValue("@ID_USUARIO", idUsuarioInclusao);

                try
                {
                    connection.Open();
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

        public List<Estado> ListarEstados()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarEstados");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderEstados(reader);
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

        private List<Estado> LerDataReaderEstados(SqlDataReader reader)
        {
            var lstEstados = new List<Estado>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Estado estados = new Estado
                    {
                        Id = Convert.ToInt16(reader["ID_ESTADO"]),
                        Descricao = reader["DESCRICAO"].ToString(),
                        Sigla = reader["Sigla"].ToString(),
                    };

                    lstEstados.Add(estados);
                }
            }
            return lstEstados;
        }

        public AutoresBeneficiariosPaginado ListarAutoresBeneficiarios(int? idTipoCadastro, string tipoPessoa, int? idEstado, bool? ativo, int numeroPagina, int registrosPagina)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAutoresBeneficiarios");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@NumeroPagina", numeroPagina);
                command.Parameters.AddWithValue("@RegistrosPagina", registrosPagina);

                if (ativo == null)
                    command.Parameters.AddWithValue("@ATIVO", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ATIVO", ativo);

                if (tipoPessoa == null)
                    command.Parameters.AddWithValue("@TIPO_PESSOA", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@TIPO_PESSOA", tipoPessoa);

                if (idEstado == null)
                    command.Parameters.AddWithValue("@ID_ESTADO", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ID_ESTADO", idEstado);

                if (idTipoCadastro == null)
                    command.Parameters.AddWithValue("@ID_TIPO_CADASTRO", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ID_TIPO_CADASTRO", idTipoCadastro);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderAutoresBeneficiarios(reader);
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

        private AutoresBeneficiariosPaginado LerDataReaderAutoresBeneficiarios(SqlDataReader reader)
        {
            var retorno = new AutoresBeneficiariosPaginado();
            retorno.LstAutoresBeneficiarios = new List<AutoresBeneficiarios>();
            var cont = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AutoresBeneficiarios autorbene = new AutoresBeneficiarios
                    {
                        IdAutorBeneficiario = Convert.ToInt32(reader["ID_AUTOR_BENEFICIARIO"]),
                        IdTipoCadastro = Convert.ToInt16(reader["ID_TIPO_CADASTRO"]),
                        TipoPessoa = reader["TIPO_PESSOA"].ToString(),
                        IdEstado = Convert.ToInt32(reader["ID_ESTADO"]),
                        Ativo = Convert.ToBoolean(reader["ATIVO"]),
                        Codigo = reader["CODIGO"].ToString(),
                        Nome = reader["NOME"].ToString(),
                        TelResidencial = reader["TELEFONE_RES"].ToString(),
                        Celular = reader["TELEFONE_CEL"].ToString(),

                    };
                    cont = Convert.ToInt32(reader["CONTAGEM"]);
                    retorno.LstAutoresBeneficiarios.Add(autorbene);
                }
            }
            retorno.Contagem = cont;

            return retorno;
        }

        public async Task SalvarArquivoAutorBeneficiario(ArquivoAutorBeneficiario arquivo)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("SalvarArquivoAutorBeneficiario");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@ID_ARQUIVO", arquivo.IdArquivo);
                command.Parameters.AddWithValue("@NOME_ARQUIVO", arquivo.Nome);
                command.Parameters.AddWithValue("@CAMINHO_ARQUIVO", arquivo.CaminhoArquivo);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", arquivo.IdAutorBeneficiario);
                command.Parameters.AddWithValue("@DT_CADASTRO", arquivo.DataCadastro);

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

        public void ExcluirArquivoAutorBeneficiario(int idAutorBeneficiario, string nomeArquivo)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ExcluirArquivoAutorBeneficiario");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", idAutorBeneficiario);
                command.Parameters.AddWithValue("@NOME_ARQUIVO", nomeArquivo);

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

        public List<LogAutorBeneficiario> BuscarLogAutorBeneficiarioPorId(int id)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarLogAutorBeneficiarioPorId");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_AUTOR", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderLofAutorBeneficiario(reader);
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

        public List<LogCorrespAutorBeneficiario> BuscarLogCorrespAutorBeneficiarioPorId(long idCorrespondencia)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarLogCorrespPorId");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_CORRESPONDENCIA", idCorrespondencia);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderLogCorresp(reader);
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

        private List<LogAutorBeneficiario> LerDataReaderLofAutorBeneficiario(SqlDataReader reader)
        {
            var log = new List<LogAutorBeneficiario>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    LogAutorBeneficiario l = new LogAutorBeneficiario
                    {
                        IdLogAutorBeneficiario = Convert.ToInt64(reader["ID_LOG_AUTOR"]),
                        IdAutor = Convert.ToInt32(reader["ID_AUTOR"]),
                        DataLog = Convert.ToDateTime(reader["DATA_LOG"]),
                        DescricaoLog = reader["DESCRICAO_LOG"].ToString(),
                        IdUsuario = Convert.ToInt32(reader["ID_USUARIO"]),
                        NomeUsuario = reader["NOME"].ToString()
                    };

                    log.Add(l);
                }
            }
            return log;
        }

        private List<LogCorrespAutorBeneficiario> LerDataReaderLogCorresp(SqlDataReader reader)
        {
            var log = new List<LogCorrespAutorBeneficiario>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    LogCorrespAutorBeneficiario l = new LogCorrespAutorBeneficiario
                    {
                        IdCorrespLogAutorBeneficiario = Convert.ToInt64(reader["ID_LOG_CORRESP_AUTOR"]),
                        DataLog = Convert.ToDateTime(reader["DATA_LOG"]),
                        DescricaoLog = reader["DESCRICAO_LOG"].ToString(),
                        IdUsuario = Convert.ToInt32(reader["ID_USUARIO"]),
                        NomeUsuario = reader["NOME"].ToString(),
                        IdCorrespondencia = Convert.ToInt64(reader["ID_CORRESPONDENCIA"])
                    };

                    log.Add(l);
                }
            }
            return log;
        }

        public async Task SalvarNomeCapa(string nomeCapaDescricao, int idAutorBeneficiario)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("SalvarNomeCapa");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);

                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", idAutorBeneficiario);
                command.Parameters.AddWithValue("@NOME_CAPA", nomeCapaDescricao);

                try
                {
                    connection.Open();
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

        public List<NomeCapa> ListarNomeCapaPorAutor(int idAutorBeneficiario)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarNomeCapaPorAutor");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", idAutorBeneficiario);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderNomeCapaSimples(reader);
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

        public List<NomeCapa> ListarNomeCapaPorAutorDet(int idAutorBeneficiario)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarNomeCapaPorAutorDet");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", idAutorBeneficiario);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderNomeCapaDet(reader);
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

        public List<NomeCapa> ListarNomeCapaPorId(int? idNomeCapa)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarNomeCapaPorId");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_NOME_CAPA", idNomeCapa);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderNomeCapaDet(reader);
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

        private List<NomeCapa> LerDataReaderNomeCapaSimples(SqlDataReader reader)
        {
            var lstNomeCapa = new List<NomeCapa>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    NomeCapa nomeCapa = new NomeCapa
                    {
                        IdNomeCapa = Convert.ToInt32(reader["ID_NOME_CAPA"]),
                        IdAutorBeneficiario = Convert.ToInt32(reader["ID_AUTOR_BENEFICIARIO"]),
                        NomeCapaDescricao = reader["NOME_CAPA"].ToString(),
                    };

                    lstNomeCapa.Add(nomeCapa);
                }
            }
            return lstNomeCapa;
        }

        private List<NomeCapa> LerDataReaderNomeCapaDet(SqlDataReader reader)
        {
            var lstNomeCapa = new List<NomeCapa>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    NomeCapa nomeCapa = new NomeCapa
                    {
                        IdNomeCapa = Convert.ToInt32(reader["ID_NOME_CAPA"]),
                        IdAutorBeneficiario = Convert.ToInt32(reader["ID_AUTOR_BENEFICIARIO"]),
                        NomeCapaDescricao = reader["NOME_CAPA"].ToString(),
                        IdSegmento = reader["ID_SEGMENTO"] != DBNull.Value ? Convert.ToInt32(reader["ID_SEGMENTO"]) : (int?)null,
                        DescricaoSegmento = reader["SEGM_DESCRICAO"].ToString(),
                        IdDisciplina = reader["ID_DISCIPLINA"] != DBNull.Value ? Convert.ToInt32(reader["ID_DISCIPLINA"]) : (int?)null,
                        DescricaoDisciplina = reader["DISC_NOME"].ToString(),
                        IdUsuario = reader["ID_USUARIO"] != DBNull.Value ? Convert.ToInt32(reader["ID_USUARIO"]) : (int?)null,
                        NomeUsuario = reader["NOME"].ToString(),
                        DataInclusao = reader["DATA_INCLUSAO"] != DBNull.Value ? Convert.ToDateTime(reader["DATA_INCLUSAO"]) : (DateTime?)null,
                        Ativo = Convert.ToBoolean(reader["ATIVO"])
                    };

                    lstNomeCapa.Add(nomeCapa);
                }
            }
            return lstNomeCapa;
        }

        public List<ArquivoAutorBeneficiario> ListarArquivoAutorBeneficiario(int idAutorBeneficiario)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarCorrespAutorBeneficiario");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", idAutorBeneficiario);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderArquivoAutorBeneficiario(reader);
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

        private List<ArquivoAutorBeneficiario> LerDataReaderArquivoAutorBeneficiario(SqlDataReader reader)
        {
            var lst = new List<ArquivoAutorBeneficiario>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ArquivoAutorBeneficiario l = new ArquivoAutorBeneficiario
                    {
                        IdArquivo = reader["ID_ARQUIVO"].ToString(),
                        IdAutorBeneficiario = Convert.ToInt32(reader["ID_AUTOR_BENEFICIARIO"]),
                        Nome = reader["NOME_ARQUIVO"].ToString(),
                        CaminhoArquivo = reader["CAMINHO_ARQUIVO"].ToString(),
                        DataCadastro = Convert.ToDateTime(reader["DT_CADASTRO"]),
                    };

                    lst.Add(l);
                }
            }
            return lst;
        }

        public List<CorrespondenciaDA> ListarCorrespondenciaAutorBeneficiario(int idAutorBeneficiario)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarCorrespAutorBeneficiario");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", idAutorBeneficiario);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderCorrespAutorBeneficiario(reader);
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

        private List<CorrespondenciaDA> LerDataReaderCorrespAutorBeneficiario(SqlDataReader reader)
        {
            var lst = new List<CorrespondenciaDA>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CorrespondenciaDA l = new CorrespondenciaDA
                    {
                        IdCorrespondencia = Convert.ToInt64(reader["ID_CORRESPONDENCIA"]),
                        IdAutorBeneficiario = Convert.ToInt32(reader["ID_AUTOR_BENEFICIARIO"]),
                        Agenda = Convert.ToInt32(reader["AGENDA"]),
                        CodigoInterno = Convert.ToInt32(reader["CODIGO_INTERNO"]),
                        Assunto = reader["ASSUNTO"].ToString(),
                        Obs = reader["OBS"].ToString(),
                        Ativo = Convert.ToBoolean(reader["ATIVO"])
                    };

                    lst.Add(l);
                }
            }
            return lst;
        }

        public async Task<int> SalvarCorrespondenciaAutorBeneficiario(CorrespondenciaDA json)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("SalvarCorresp");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", json.IdAutorBeneficiario);
                command.Parameters.AddWithValue("@ASSUNTO", json.Assunto);
                command.Parameters.AddWithValue("@OBS", json.Obs);
                command.Parameters.AddWithValue("@AGENDA", json.Agenda);
                command.Parameters.AddWithValue("@CODIGO_INTERNO", json.CodigoInterno);
                command.Parameters.AddWithValue("@ATIVO", true);

                try
                {
                    connection.Open();
                    int idAutorBeneficiario = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return idAutorBeneficiario;
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

        public async Task AtualizarCorrespondenciaAutorBeneficiario(CorrespondenciaDA json)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("AtualizarCorresp");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);
                command.Parameters.AddWithValue("@ID_CORRESPONDENCIA", json.IdCorrespondencia);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", json.IdAutorBeneficiario);
                command.Parameters.AddWithValue("@ASSUNTO", json.Assunto);
                command.Parameters.AddWithValue("@OBS", json.Obs);
                command.Parameters.AddWithValue("@AGENDA", json.Agenda);
                command.Parameters.AddWithValue("@CODIGO_INTERNO", json.CodigoInterno);
                command.Parameters.AddWithValue("@ATIVO", true);

                try
                {
                    connection.Open();
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

        public async Task SalvarLogCorrespAutorBeneficiario(long idCorrespondencia, int idUsuario, string v)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("SalvarLogCorrespAutorBeneficiario");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);

                command.Parameters.AddWithValue("@DATA_LOG", DateTime.Now);
                command.Parameters.AddWithValue("@DESCRICAO_LOG", v);
                command.Parameters.AddWithValue("@ID_USUARIO", idUsuario);
                command.Parameters.AddWithValue("@ID_CORRESPONDENCIA", idCorrespondencia);

                try
                {
                    connection.Open();
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

        public async Task SalvarEmail(string destinatario, int idAutorBeneficiario)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("SalvarEmail");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);

                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", idAutorBeneficiario);
                command.Parameters.AddWithValue("@DESTINATARIO", destinatario);

                try
                {
                    connection.Open();
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

        public async Task ExcluirEmail(int idAutorBeneficiario)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("ExcluirEmail");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);

                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", idAutorBeneficiario);

                try
                {
                    connection.Open();
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

        public List<EmailAutorBeneficiario> ListarEmailPorAutor(int idAutorBeneficiario)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarEmailPorAutor");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", idAutorBeneficiario);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderEmail(reader);
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

        private List<EmailAutorBeneficiario> LerDataReaderEmail(SqlDataReader reader)
        {
            var lstEmail = new List<EmailAutorBeneficiario>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EmailAutorBeneficiario email = new EmailAutorBeneficiario
                    {
                        IdEmail = Convert.ToInt32(reader["ID_EMAIL_AUTOR_BENEF"]),
                        IdAutorBeneficiario = Convert.ToInt32(reader["ID_AUTOR_BENEFICIARIO"]),
                        Destinatario = reader["DESTINATARIO"].ToString(),
                    };

                    lstEmail.Add(email);
                }
            }
            return lstEmail;
        }

        public List<AutorDA> ListarAutoresPorNome(string nome)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarAutoresPorNome");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@NOME", nome);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderLstAutor(reader);
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

        public async Task AtualizarNomeCapaPorAutor(NomeCapa nomeCapa)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("AtualizarNomeCapaPorAutor");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);

                command.Parameters.AddWithValue("@ID_NOME_CAPA", nomeCapa.IdNomeCapa);
                command.Parameters.AddWithValue("@ID_SEGMENTO", nomeCapa.IdSegmento);
                command.Parameters.AddWithValue("@ID_DISCIPLINA", nomeCapa.IdDisciplina);
                command.Parameters.AddWithValue("@ID_USUARIO", nomeCapa.IdUsuario);
                command.Parameters.AddWithValue("@DATA_INCLUSAO", DateTime.Now);
                command.Parameters.AddWithValue("@ATIVO", nomeCapa.Ativo);

                try
                {
                    connection.Open();
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

        public async Task<int> IncluirNomeCapaPorAutor(NomeCapa nomeCapa)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("SalvarNomeCapaPorAutor");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);

                command.Parameters.AddWithValue("@ID_AUTOR_BENEFICIARIO", nomeCapa.IdAutorBeneficiario);
                command.Parameters.AddWithValue("@NOME_CAPA", nomeCapa.NomeCapaDescricao);
                command.Parameters.AddWithValue("@ID_SEGMENTO", nomeCapa.IdSegmento);
                command.Parameters.AddWithValue("@ID_DISCIPLINA", nomeCapa.IdDisciplina);
                command.Parameters.AddWithValue("@ID_USUARIO", nomeCapa.IdUsuario);
                command.Parameters.AddWithValue("@DATA_INCLUSAO", DateTime.Now);
                command.Parameters.AddWithValue("@ATIVO", nomeCapa.Ativo);

                try
                {
                    connection.Open();
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
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

        public void ExcluirNomeCapaPorAutor(int idNomeCapa)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("ExcluirNomeCapa");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);

                command.Parameters.AddWithValue("@ID_NOME_CAPA", idNomeCapa);

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

        public async Task ExcluirCorrespondenciaAutorBeneficiario(int idCorrespondencia)
        {
            string insertAutorBeneficiario = _baseRepository.BuscarArquivoConsulta("ExcluirCorresp");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertAutorBeneficiario, connection);

                command.Parameters.AddWithValue("@ID_CORRESPONDENCIA", idCorrespondencia);

                try
                {
                    connection.Open();
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

        public CorrespondenciaDA ListarCorrespondenciaAutorBeneficiarioId(int idCorrespondencia)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarCorrespAutorBeneficiarioId");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_CORRESPONDENCIA", idCorrespondencia);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderCorrespAutorBeneficiarioId(reader);
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

        private CorrespondenciaDA LerDataReaderCorrespAutorBeneficiarioId(SqlDataReader reader)
        {
            CorrespondenciaDA l = new CorrespondenciaDA();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    l = new CorrespondenciaDA
                    {
                        IdCorrespondencia = Convert.ToInt64(reader["ID_CORRESPONDENCIA"]),
                        IdAutorBeneficiario = Convert.ToInt32(reader["ID_AUTOR_BENEFICIARIO"]),
                        Agenda = Convert.ToInt32(reader["AGENDA"]),
                        CodigoInterno = Convert.ToInt32(reader["CODIGO_INTERNO"]),
                        Assunto = reader["ASSUNTO"].ToString(),
                        Obs = reader["OBS"].ToString(),
                        Ativo = Convert.ToBoolean(reader["ATIVO"])
                    };
                }
            }
            return l;
        }
    }
}
