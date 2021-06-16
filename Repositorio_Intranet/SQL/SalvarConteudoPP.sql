INSERT INTO [dbo].[TB_CONTEUDO_PP]
           ([NOME]
           ,[CAMINHO]
           ,[NOME_GUID]
           ,[USUARIO]
           ,[DT_CADASTRO]
           ,[ATIVO]
           ,[PRIMEIRA_PAGINA])
     VALUES
           (@NOME
           ,@CAMINHO
           ,@NOME_GUID
           ,@USUARIO
           ,@DT_CADASTRO
           ,@ATIVO
           ,@PRIMEIRA_PAGINA)