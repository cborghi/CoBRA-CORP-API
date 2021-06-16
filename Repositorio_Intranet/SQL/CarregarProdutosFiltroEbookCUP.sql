﻿SELECT p.PROD_ID
	, p.PROD_TITULO
	, p.PROD_EBSA
	, p.PROD_ISBN
	, p.PROD_PUBLICADO
	, p.PROD_INTEGRADO
	, p.PROD_VIDEO
	, p.PROD_PRECO
	, A.AEDU_DESCRICAO
	, (SELECT COUNT(*) 	FROM PRODUTO p
	LEFT JOIN ANO_EDUCACAO AS A 
	ON p.PROD_AEDU = A.AEDU_ID WHERE (p.PROD_ID LIKE '%' + @Filtro + '%'
   OR PROD_TITULO LIKE '%' + @Filtro + '%'
   OR PROD_EBSA LIKE '%' + @Filtro + '%'
   OR PROD_ISBN LIKE '%' + @Filtro + '%'
   OR AEDU_DESCRICAO LIKE '%' + @Filtro + '%')
   AND EXCLUIDO = 0  AND p.PROD_MIDI = 2) AS CONTAGEM
FROM PRODUTO p
LEFT JOIN ANO_EDUCACAO AS A 
	ON p.PROD_AEDU = A.AEDU_ID 
WHERE (p.PROD_ID LIKE '%' + @Filtro + '%'
   OR p.PROD_TITULO LIKE '%' + @Filtro + '%'
   OR p.PROD_EBSA LIKE '%' + @Filtro + '%'
   OR p.PROD_ISBN LIKE '%' + @Filtro + '%'
   OR A.AEDU_DESCRICAO LIKE '%' + @Filtro + '%')
AND EXCLUIDO = 0  AND p.PROD_MIDI = 2
ORDER BY p.PROD_TITULO
OFFSET ((@NumeroPagina) * @RegistrosPagina) ROWS
FETCH NEXT @RegistrosPagina ROWS ONLY;