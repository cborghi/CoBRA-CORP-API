INSERT INTO [dbo].[TB_PRODUTO_IMAGEM_CAPA]
           ([NOME_CAPA]
           ,[CAMINHO_ARQUIVO]
           ,[PROD_ID]
           ,[PROD_EBSA]
           ,[DT_CADASTRO])
     VALUES
           (@NOME_CAPA
           ,@CAMINHO_ARQUIVO
           ,@PROD_ID
           ,@PROD_EBSA
           ,@DT_CADASTRO)