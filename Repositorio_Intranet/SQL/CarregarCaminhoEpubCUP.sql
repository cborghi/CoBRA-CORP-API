﻿SELECT [ID_PRODUTO_EPUB]
      ,[NOME_EPUB]
      ,[CAMINHO_ARQUIVO]
      ,[PROD_ID]
      ,[PROD_EBSA]
      ,[DT_CADASTRO]
  FROM [dbo].[TB_PRODUTO_EPUB]
WHERE PROD_ID = @PROD_ID