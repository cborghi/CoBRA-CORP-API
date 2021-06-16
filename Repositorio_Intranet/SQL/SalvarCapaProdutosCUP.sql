INSERT INTO [dbo].[TB_CAPA_PRODUTO]
           ([ID_PRODUTO]
           ,[CAPA_CORES]
           ,[CAPA_TIPO_PAPEL]
           ,[CAPA_GRAMATURA]
           ,[CAPA_ORELHA]
           ,[CAPA_ACABAMENTO]
           ,[CAPA_OBS]
           ,[CAPA_ACABAMENTO_LOMBADA])
     VALUES
           (@ID_PRODUTO
           ,@CAPA_CORES
           ,@CAPA_TIPO_PAPEL
           ,@CAPA_GRAMATURA
           ,@CAPA_ORELHA
           ,@CAPA_ACABAMENTO
           ,@CAPA_OBS
           ,@CAPA_ACABAMENTO_LOMBADA)