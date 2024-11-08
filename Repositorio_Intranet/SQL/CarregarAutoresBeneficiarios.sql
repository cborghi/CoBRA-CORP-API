﻿SELECT ID_AUTOR_BENEFICIARIO
	 , NOME
	 , CODIGO
	 , ID_TIPO_CADASTRO
	 , TIPO_PESSOA
	 , ID_ESTADO
	 , ATIVO
	 , TELEFONE_RES
	 , TELEFONE_CEL
	 , (SELECT COUNT(*) 	
			FROM TB_AUTOR_BENEFICIARIO
			WHERE ((@ATIVO IS NULL) OR (ATIVO = @ATIVO))
			AND	((@TIPO_PESSOA IS NULL) OR (TIPO_PESSOA = @TIPO_PESSOA))
			AND	((@ID_ESTADO IS NULL) OR (ID_ESTADO = @ID_ESTADO))
			AND	((@ID_TIPO_CADASTRO IS NULL) OR (ID_TIPO_CADASTRO = @ID_TIPO_CADASTRO))) AS CONTAGEM
FROM TB_AUTOR_BENEFICIARIO
WHERE ((@ATIVO IS NULL) OR (ATIVO = @ATIVO))
AND	((@TIPO_PESSOA IS NULL) OR (TIPO_PESSOA = @TIPO_PESSOA))
AND	((@ID_ESTADO IS NULL) OR (ID_ESTADO = @ID_ESTADO))
AND	((@ID_TIPO_CADASTRO IS NULL) OR (ID_TIPO_CADASTRO = @ID_TIPO_CADASTRO))
ORDER BY [NOME]
OFFSET ((@NumeroPagina) * @RegistrosPagina) ROWS
FETCH NEXT @RegistrosPagina ROWS ONLY;