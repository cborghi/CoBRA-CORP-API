﻿SELECT
	 PR.ID_PERMISSAO_USUARIO,
	 U.ID_USUARIO,
	 U.NOME,
	 U.EMAIL,
	 ISNULL(PR.APROVA_REQUISICAO_SUPERVISOR, 0) AS APROVA_REQUISICAO_SUPERVISOR
	,ISNULL(PR.REPROVA_REQUISICAO_SUPERVISOR, 0) AS REPROVA_REQUISICAO_SUPERVISOR
	,ISNULL(PR.CANCELA_REQUISICAO_SUPERVISOR, 0) AS CANCELA_REQUISICAO_SUPERVISOR
	,ISNULL(PR.APROVA_REQUISICAO_GERENTE, 0) AS APROVA_REQUISICAO_GERENTE
	,ISNULL(PR.REPROVA_REQUISICAO_GERENTE, 0) AS REPROVA_REQUISICAO_GERENTE
	,ISNULL(PR.CANCELA_REQUISICAO_GERENTE, 0) AS CANCELA_REQUISICAO_GERENTE
FROM 
	USUARIO AS U
LEFT JOIN
	TB_PERMISSAO_REQUISICAO AS PR
	ON U.ID_USUARIO = PR.ID_USUARIO
WHERE
	U.ATIVO = 1
AND
	ID_GRUPO = (SELECT ID_GRUPO FROM USUARIO WHERE ID_USUARIO = @ID_USUARIO)