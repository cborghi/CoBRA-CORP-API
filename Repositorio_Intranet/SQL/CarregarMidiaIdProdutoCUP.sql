﻿SELECT [ID_MIDIA]
      ,[ID_PRODUTO]
      ,[MIDIA_TIPO]
      ,[MIDIA_OUTROS]
  FROM [dbo].[TB_MIDIA_PRODUTO]
  WHERE [ID_PRODUTO] = @IdProduto