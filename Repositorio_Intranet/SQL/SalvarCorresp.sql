INSERT INTO [dbo].[TB_CORRESPONDENCIA_AUTOR_BENEFICIARIO]
           ([AGENDA]
           ,[ASSUNTO]
           ,[OBS]
           ,[CODIGO_INTERNO]
           ,[ATIVO]
           ,[ID_AUTOR_BENEFICIARIO])
     VALUES
           (@AGENDA
           ,@ASSUNTO
           ,@OBS
           ,@CODIGO_INTERNO
           ,@ATIVO
           ,@ID_AUTOR_BENEFICIARIO)

    SELECT @@IDENTITY