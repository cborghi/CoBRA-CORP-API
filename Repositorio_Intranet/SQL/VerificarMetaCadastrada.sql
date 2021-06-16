﻿DECLARE @ID_STATUS_RASCUNHO UNIQUEIDENTIFIER = (
	SELECT ID_STATUS FROM TB_STATUS_PAINEL WHERE DESCRICAO = 'Rascunho'
)

IF(EXISTS(SELECT 1 FROM TB_CABECALHO_PAINEL_META WHERE ID_GRUPO_PAINEL = @ID_GRUPO_PAINEL AND ID_STATUS <> @ID_STATUS_RASCUNHO))
	SELECT CAST(1 AS BIT) AS POSSUI_META
ELSE
	SELECT CAST(0 AS BIT) AS POSSUI_META