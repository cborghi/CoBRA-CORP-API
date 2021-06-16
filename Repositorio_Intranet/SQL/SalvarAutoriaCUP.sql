INSERT INTO [dbo].[AUTORIA]
           ([AUTO_NOMECON]
           ,[AUTO_DATALIBERA]
           ,[AUTO_DATAVCTO]
           ,[AUTO_RENOVAUTO]
           ,[AUTO_QTDEMESES]
           ,[AUTO_PCRECEBDA]
           ,[AUTO_DATALIMITE]
           ,[AUTO_PROD]
           ,[AUTO_NACIONALIDADE]
           ,[AUTO_NATURALIDADE]
           ,[AUTO_REPARTEIMP]
           ,[AUTO_REPARTEREIMP]
           ,[AUTO_COD_AUTOR])
     VALUES
           (@AUTO_NOMECON
           ,@AUTO_DATALIBERA
           ,@AUTO_DATAVCTO
           ,@AUTO_RENOVAUTO
           ,@AUTO_QTDEMESES
           ,@AUTO_PCRECEBDA
           ,@AUTO_DATALIMITE
           ,@AUTO_PROD
           ,@AUTO_NACIONALIDADE
           ,@AUTO_NATURALIDADE
           ,@AUTO_REPARTEIMP
           ,@AUTO_REPARTEREIMP
           ,@AUTO_COD_AUTOR)

SELECT @@IDENTITY;