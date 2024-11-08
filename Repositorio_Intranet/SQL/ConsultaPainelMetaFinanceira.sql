﻿	DECLARE @ID_STATUS_META UNIQUEIDENTIFIER = (
		SELECT ID_STATUS FROM TB_STATUS_PAINEL WHERE DESCRICAO = @DESCRICAO_STATUS_META
	)

	DECLARE @ID_STATUS_CABECALHO_PAINEL UNIQUEIDENTIFIER = (
		SELECT ID_STATUS FROM TB_STATUS_PAINEL WHERE DESCRICAO = 'Aprovado'
	)


SELECT
	 [pma].[ID_META_FINANCEIRA]
	,[urm].[ID_USUARIO_RM]
	--,[lpm].[ID_LINHA_META]
	,[urm].[NOME]
	,[car].[DESCRICAO] AS CARGO
	,[gru].[DESCRICAO] AS GRUPO_PAINEL
	,[pma].[ID_STATUS]
	,[pma].[SITUACAO]
	,[pma].[META_RECEITA_LIQUIDA]
	,[pma].[VALOR_RECEBIMENTO]
	,[pma].[DATA_CRIACAO]
	,[pma].[DATA_ALTERACAO]
	,[pma].[OBSERVACAO]
FROM
	[TB_USUARIO_RM] [urm]
INNER JOIN
	[TB_CARGO] [car]
 ON [urm].[ID_CARGO] = [car].[ID_CARGO]
INNER JOIN
	[TB_GRUPO_PAINEL] [gru]
 ON [car].[ID_GRUPO_PAINEL] = [gru].[ID_GRUPO_PAINEL]
--INNER JOIN
--	[TB_CABECALHO_PAINEL_META] [cpm]
-- ON [gru].[ID_GRUPO_PAINEL] = [cpm].[ID_GRUPO_PAINEL]
--INNER JOIN
--	[TB_LINHA_PAINEL_META] [lpm]
-- ON [cpm].[ID_META] = [lpm].[ID_META]
LEFT JOIN
	(SELECT
		 [mF].[ID_META_FINANCEIRA]
		,[mF].[ID_USUARIO_RM]
		,[mF].[ID_LINHA_META]
		,[mF].[ID_STATUS]
		,[mF].[META_RECEITA_LIQUIDA]
		,[mF].[VALOR_RECEBIMENTO]
		,[stp].[DESCRICAO] AS SITUACAO
		,[mF].[DATA_CRIACAO]
		,[mF].[DATA_ALTERACAO]
		,[mF].[OBSERVACAO]
	 FROM
		 [TB_META_FINANCEIRA] [mF]
	 LEFT JOIN [TB_STATUS_PAINEL] [stp]
      ON [mF].[ID_STATUS] = [stp].[ID_STATUS]
	) AS [pma] ON [urm].[ID_USUARIO_RM] = [pma].[ID_USUARIO_RM]
			  --AND [lpm].[ID_LINHA_META] = [pma].[ID_LINHA_META]
WHERE
	[urm].[ATIVO] = 1
AND
--	([cpm].[ID_STATUS] = @ID_STATUS_CABECALHO_PAINEL)
--AND
	(
	--	(
	--		[pma].[ID_STATUS] IS NULL
	--	) 
	--OR 
		(
			[pma].[ID_STATUS] = @ID_STATUS_META
		)
	)