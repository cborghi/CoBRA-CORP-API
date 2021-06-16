
INSERT INTO [dbo].[TB_PRODUTO_EPUB]
           ([NOME_EPUB]
           ,[CAMINHO_ARQUIVO]
           ,[PROD_ID]
           ,[PROD_EBSA]
           ,[DT_CADASTRO])
     VALUES
           (@NOME_EPUB
           ,@CAMINHO_ARQUIVO
           ,@PROD_ID
           ,@PROD_EBSA
           ,@DT_CADASTRO)