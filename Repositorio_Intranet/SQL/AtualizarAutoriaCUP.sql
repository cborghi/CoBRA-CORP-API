﻿UPDATE [dbo].[AUTORIA]
   SET [AUTO_NOMECON] = @AUTO_NOMECON
      ,[AUTO_DATALIBERA] = @AUTO_DATALIBERA
      ,[AUTO_DATAVCTO] = @AUTO_DATAVCTO
      ,[AUTO_RENOVAUTO] = @AUTO_RENOVAUTO
      ,[AUTO_QTDEMESES] = @AUTO_QTDEMESES
      ,[AUTO_PCRECEBDA] = @AUTO_PCRECEBDA
      ,[AUTO_DATALIMITE] = @AUTO_DATALIMITE
      ,[AUTO_PROD] = @AUTO_PROD
      ,[AUTO_NACIONALIDADE] = @AUTO_NACIONALIDADE
      ,[AUTO_NATURALIDADE] = @AUTO_NATURALIDADE
      ,[AUTO_REPARTEIMP] = @AUTO_REPARTEIMP
      ,[AUTO_REPARTEREIMP] = @AUTO_REPARTEREIMP
      ,[AUTO_COD_AUTOR] = @AUTO_COD_AUTOR
 WHERE [AUTO_ID] = @AUTO_ID