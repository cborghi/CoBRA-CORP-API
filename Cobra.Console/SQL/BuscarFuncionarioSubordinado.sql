﻿SELECT
	SC.ID_CONSULTOR AS ID_USUARIO
FROM 
	TB_USUARIO_RM AS U
LEFT JOIN
	TB_GERENTE_SUPERVISOR AS GS
	ON U.ID_USUARIO_RM = GS.ID_GERENTE
LEFT JOIN
	TB_SUPERVISOR_CONSULTOR AS SC
	ON (GS.ID_SUPERVISOR = SC.ID_SUPERVISOR OR U.ID_USUARIO_RM = SC.ID_SUPERVISOR)
WHERE
	U.ID_USUARIO_RM = @ID_USUARIO
AND
	U.ATIVO = 1
AND
	SC.ID_CONSULTOR IS NOT NULL