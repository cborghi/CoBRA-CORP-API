﻿IF(EXISTS(SELECT 1 FROM TB_META_FINANCEIRA WHERE ID_USUARIO_RM = @ID_USUARIO_RM))
BEGIN
	UPDATE TB_META_FINANCEIRA SET
		META_RECEITA_LIQUIDA = @META_RECEITA_LIQUIDA
		,VALOR_RECEBIMENTO = @VALOR_RECEBIMENTO
		,DATA_ALTERACAO = GETDATE()
		,ID_STATUS = @ID_STATUS
		,META_RECEITA_LIQUIDA_CALC = @META_RECEITA_LIQUIDA
	WHERE
		ID_USUARIO_RM = @ID_USUARIO_RM
END
ELSE
BEGIN
	INSERT INTO TB_META_FINANCEIRA 
		(ID_META_FINANCEIRA
		,ID_USUARIO_RM
		,META_RECEITA_LIQUIDA
		,VALOR_RECEBIMENTO
		,DATA_CRIACAO
		,ID_STATUS
		,ID_PERIODO
		,META_RECEITA_LIQUIDA_CALC)
	VALUES
		(NEWID()
		,@ID_USUARIO_RM
		,@META_RECEITA_LIQUIDA
		,@VALOR_RECEBIMENTO
		,GETDATE()
		,(SELECT ID_STATUS FROM TB_STATUS_PAINEL WHERE DESCRICAO = 'Rascunho')
		,(SELECT TOP 1 TP.ID_PERIODO FROM TB_PERIODO_META TP ORDER BY DATA_FIM DESC)
		,@META_RECEITA_LIQUIDA)
END

