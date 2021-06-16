﻿SELECT
	 R.[ID_REGIAO_ATUACAO] AS ID_REGIAO
    ,R.[DESCRICAO]
    ,R.CODIGO_PROTHEUS AS UF
    ,GETDATE() AS DT_CADASTRO
FROM 
	TB_REGIAO_ATUACAO AS R
WHERE
	R.CODIGO_PROTHEUS NOT LIKE '0%'
    