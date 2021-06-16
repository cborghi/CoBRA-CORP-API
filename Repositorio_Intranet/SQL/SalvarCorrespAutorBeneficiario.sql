INSERT INTO [dbo].[TB_CORRESPONDENCIA_AUTOR_BENEFICIARIO]
           ([ID_AUTOR_BENEFICIARIO]
           ,[AGENDA]
           ,[ASSUNTO]
           ,[OBS]
           ,[CODIGO_INTERNO]
           ,[ATIVO])
     VALUES
           (@ID_AUTOR_BENEFICIARIO
           ,@AGENDA
           ,@ASSUNTO
           ,@OBS
           ,@CODIGO_INTERNO
           ,@ATIVO)

