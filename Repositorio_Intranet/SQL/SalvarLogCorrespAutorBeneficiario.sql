INSERT INTO [dbo].[TB_LOG_CORRESP_AUTOR_BENEFICIARIO]
           ([DATA_LOG]
           ,[DESCRICAO_LOG]
           ,[ID_USUARIO]
           ,[ID_CORRESPONDENCIA])
     VALUES
           (@DATA_LOG
           ,@DESCRICAO_LOG
           ,@ID_USUARIO
           ,@ID_CORRESPONDENCIA)