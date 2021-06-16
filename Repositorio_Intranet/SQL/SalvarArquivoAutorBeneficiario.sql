INSERT INTO [dbo].[TB_ARQUIVO_AUTOR_BENEFICIARIO]
           ([ID_ARQUIVO]
           ,[NOME_ARQUIVO]
           ,[CAMINHO_ARQUIVO]
           ,[ID_AUTOR_BENEFICIARIO]
           ,[DT_CADASTRO])
     VALUES
           (@ID_ARQUIVO
           ,@NOME_ARQUIVO
           ,@CAMINHO_ARQUIVO
           ,@ID_AUTOR_BENEFICIARIO
           ,@DT_CADASTRO)