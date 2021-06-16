INSERT INTO [dbo].[TB_CONTROLE_IMPRESSAO_PRODUTO]
           ([ID_PRODUTO]
           ,[CONT_IMP_EDICAO]
           ,[CONT_IMP_GRAFICA]
           ,[CONT_IMP_IMPRESSAO]
           ,[CONT_IMP_DATA]
           ,[CONT_IMP_TIRAGEM]
           ,[CONT_IMP_OBS])
     VALUES
           (@ID_PRODUTO
           ,@CONT_IMP_EDICAO
           ,@CONT_IMP_GRAFICA
           ,@CONT_IMP_IMPRESSAO
           ,@CONT_IMP_DATA
           ,@CONT_IMP_TIRAGEM
           ,@CONT_IMP_OBS)