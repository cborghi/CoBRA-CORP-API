﻿UPDATE [dbo].[PRODUTO]
   SET [EXCLUIDO] = 1,
   [PROD_INTEGRADO] = 'deletado'
 WHERE [PROD_ID] = @ID_PRODUTO