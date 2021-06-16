﻿SELECT A.[ID_AUTOR_BENEFICIARIO]
      ,A.[NOME]
      ,A.[CODIGO]
      ,A.[LOJA]
      ,A.[CPF_CNPJ]
      ,A.[RG]
      ,A.[SEXO]
      ,A.[NACIONALIDADE]
      ,A.[NATURALIDADE]
      ,A.[DT_NASCIMENTO]
      ,A.[ID_ESTADO_CIVIL]
      ,A.[PROFISSAO]
      ,A.[NIT_PIS]
      ,A.[SITUACAO]
      ,A.[TELEFONE_RES]
      ,A.[TELEFONE_CEL]
      ,A.[TELEFONE_COM]
      ,A.[CONTATO]
      ,A.[CEP]
      ,A.[ID_ESTADO]
      ,A.[CIDADE]
      ,A.[BAIRRO]
      ,A.[LOGRADOURO]
      ,A.[NUMERO]
      ,A.[COMPLEMENTO]
      ,A.[CORRENTISTA]
      ,A.[CORRENTISTA_CPF_CNPJ]
      ,A.[BANCO]
      ,A.[AGENCIA]
      ,A.[ID_TIPO_CONTA]
      ,A.[NUMERO_CONTA]
      ,A.[DATA_AUTORIZACAO]
      ,A.[ENVIAR_DEMONSTRATIVO]
      ,A.[PAGAMENTO_MINIMO]
      ,A.[EMITE_RECIBO]
      ,A.[EMAIL_CONTADOR]
      ,A.[OBSERVACAO]
      ,A.[ID_USUARIO_INCLUSAO]
      ,A.[DATA_INCLUSAO]
      ,A.[ID_TIPO_CADASTRO]
      ,A.[ATIVO]
      ,A.[INCLUIDO_POR]
      ,A.[TIPO_PESSOA]
FROM TB_AUTORES_BENEFICIARIO AB
JOIN TB_AUTOR_BENEFICIARIO A ON A.ID_AUTOR_BENEFICIARIO = AB.ID_AUTOR
WHERE AB.ID_BENEFICIARIO = @ID_AUTOR_BENEFICIARIO;