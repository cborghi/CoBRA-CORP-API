﻿SELECT 
	SUM(PESO) AS PESO_TOTAL
FROM 
	TB_LINHA_PAINEL_META AS L
INNER JOIN
	TB_CABECALHO_PAINEL_META AS CAB
	ON L.ID_META = CAB.ID_META
WHERE
	((@ID_LINHA_META IS NULL OR @ID_LINHA_META = '00000000-0000-0000-0000-000000000000') OR (CAB.ID_META = (
		SELECT C.ID_META FROM TB_CABECALHO_PAINEL_META AS C JOIN  TB_LINHA_PAINEL_META AS L ON C.ID_META = L.ID_META
		WHERE L.ID_LINHA_META = @ID_LINHA_META)  
	 ))
AND
	((@ID_GRUPO_PAINEL IS NULL) OR (CAB.ID_GRUPO_PAINEL = @ID_GRUPO_PAINEL))
