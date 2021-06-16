INSERT INTO [dbo].[TB_NOME_CAPA]
           ([ID_AUTOR_BENEFICIARIO]
           ,[NOME_CAPA]
           ,[ID_SEGMENTO]
           ,[ID_DISCIPLINA]
           ,[ID_USUARIO]
           ,[DATA_INCLUSAO]
           ,[ATIVO])
     VALUES
           (@ID_AUTOR_BENEFICIARIO
           ,@NOME_CAPA
           ,@ID_SEGMENTO
           ,@ID_DISCIPLINA
           ,@ID_USUARIO
           ,@DATA_INCLUSAO
           ,@ATIVO)

SELECT @@IDENTITY