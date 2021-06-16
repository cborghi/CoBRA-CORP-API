﻿IF(EXISTS(SELECT 1 FROM TB_CABECALHO_PAINEL_META WHERE ID_GRUPO_PAINEL = @ID_GRUPO_PAINEL))
	SELECT ID_META FROM TB_CABECALHO_PAINEL_META WHERE ID_GRUPO_PAINEL = @ID_GRUPO_PAINEL
ELSE
BEGIN

	DECLARE @ID_STATUS_RASCUNHO UNIQUEIDENTIFIER = (
		SELECT ID_STATUS FROM TB_STATUS_PAINEL WHERE DESCRICAO = 'Rascunho'
	)

	DECLARE @TB_ID_CABECALHO TABLE(ID_META UNIQUEIDENTIFIER)

	INSERT INTO TB_CABECALHO_PAINEL_META
	(
		ID_GRUPO_PAINEL,
		ID_STATUS
	)
	OUTPUT INSERTED.ID_META INTO @TB_ID_CABECALHO
	VALUES(
		@ID_GRUPO_PAINEL,
		@ID_STATUS_RASCUNHO
	)

	SELECT ID_META FROM @TB_ID_CABECALHO	
END
