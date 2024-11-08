﻿SELECT 
     R.ID_RELATORIO
    ,ISNULL(L.[TITULO], 'TÍTULO NÃO LOCALIZADO') AS TITULO
    ,R.[PARTE_LIVRO]
    ,R.[QTD_TOTAL]
    ,R.[QTD_APROVADA]
    ,R.[QTD_FINALIZADA]
    ,R.[QTD_CAIU]
    ,R.[QTD_REPROVADA]
    ,R.[QTD_AGUARDANDO_APROVACAO]
    ,R.[QTD_EM_PESQUISA]
    ,R.[QTD_APROVACAO_ORCAMENTO]
    ,R.[DT_CADASTRO]
    ,R.[PORCENTAGEM_FINALIZADA]
    ,R.[QTD_OUTROS_STATUS]
    ,R.[DT_CADASTRO]
    ,R.[IMPRESSO]
    ,R.[QTD_PENDENTE]
    ,R.[QTD_TOTAL_MENOS_CAIU]
FROM 
    TB_RELATORIO_ELVIS R WITH(NOLOCK)
INNER JOIN
    TB_LIVRO_ELVIS L WITH(NOLOCK)
    ON R.ID_LIVRO = L.ID_LIVRO
ORDER BY	
	L.ID_LIVRO,
	L.TITULO,
	R.PARTE_LIVRO