﻿SELECT 
     [gp].[ID_GRUPO_PAINEL]
    ,[gp].[DESCRICAO]
    ,[gp].[DT_CADASTRO]
    ,[gp].[ATIVO]
FROM 
    [TB_GRUPO_PAINEL] [gp]
LEFT JOIN
	[TB_CABECALHO_PAINEL_META] [cpm]
 ON [gp].[ID_GRUPO_PAINEL] = [cpm].[ID_GRUPO_PAINEL]
WHERE
    ((@ID_STATUS = '00000000-0000-0000-0000-000000000000' OR @ID_STATUS IS NULL) OR ([cpm].[ID_STATUS] = @ID_STATUS))
