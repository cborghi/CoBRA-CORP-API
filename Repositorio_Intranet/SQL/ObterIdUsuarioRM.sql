﻿SELECT 
	RM.ID_USUARIO_RM
FROM
	EBSA.dbo.TB_USUARIO_RM AS RM
INNER JOIN
	__PROTHEUS__.dbo.SA2010 AS FORNECEDOR
	ON FORNECEDOR.A2_CGC = RM.CPF COLLATE Latin1_General_CI_AS
INNER JOIN
	EBSA.dbo.USUARIO AS USR
	ON USR.EMAIL = FORNECEDOR.A2_EMAIL COLLATE Latin1_General_CI_AS	
WHERE 
	LEN(A2_EMAIL) > 0
AND
	USR.ID_USUARIO = @ID_USUARIO
AND
	RM.[ATIVO] = 1