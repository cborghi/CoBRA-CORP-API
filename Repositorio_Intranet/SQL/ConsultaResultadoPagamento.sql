﻿SELECT
     [ID_RESULTADO_PAGAMENTO]
    ,[PORCENTAGEM_RESULTADO]
    ,[PORCENTAGEM_PAGAMENTO]
    ,[DT_CADASTRO]
FROM
     [TB_RESULTADO_PAGAMENTO_META]
WHERE
     [PORCENTAGEM_RESULTADO] = @PORCENTAGEM_RESULTADO