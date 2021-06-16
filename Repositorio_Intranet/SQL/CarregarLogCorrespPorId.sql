﻿SELECT T.[ID_LOG_CORRESP_AUTOR]
      ,T.[DATA_LOG]
      ,T.[DESCRICAO_LOG]
      ,T.[ID_USUARIO]
      ,U.[NOME]
      ,T.[ID_CORRESPONDENCIA]
  FROM [dbo].[TB_LOG_CORRESP_AUTOR_BENEFICIARIO] T
  JOIN USUARIO U ON U.ID_USUARIO = T.ID_USUARIO
  WHERE [ID_CORRESPONDENCIA] = @ID_CORRESPONDENCIA