INSERT INTO [dbo].[DOCUMENTOSAUTORIA]
           ([AUTO_ID]
           ,[CAMINHOARQUIVO]
           ,[NOME]
           ,[CAMINHOLOCAL])
     VALUES
           (@AUTO_ID
           ,@CAMINHOARQUIVO
           ,@NOME
           ,@CAMINHOLOCAL)

SELECT @@IDENTITY