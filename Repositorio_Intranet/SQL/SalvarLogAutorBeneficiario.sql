INSERT INTO [dbo].[TB_LOG_AUTOR_BENEFICIARIO]
           ([ID_AUTOR]
           ,[DATA_LOG]
           ,[DESCRICAO_LOG]
           ,[ID_USUARIO])
     VALUES
           (@ID_AUTOR
           ,@DATA_LOG
           ,@DESCRICAO_LOG
           ,@ID_USUARIO)