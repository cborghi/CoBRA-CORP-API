﻿SELECT [ID_ARQUIVO]
      ,[ID_AUTOR_BENEFICIARIO]
      ,[NOME_ARQUIVO]
      ,[CAMINHO_ARQUIVO]
      ,[DT_CADASTRO]
  FROM [dbo].[TB_ARQUIVO_AUTOR_BENEFICIARIO]
  WHERE [ID_AUTOR_BENEFICIARIO] = @ID_AUTOR_BENEFICIARIO