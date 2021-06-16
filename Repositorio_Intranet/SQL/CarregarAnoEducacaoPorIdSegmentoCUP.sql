SELECT aedu_id
	, aedu_descricao
	, aedu_segm
	, ABREVIACAO 
FROM ano_educacao
WHERE aedu_segm = @ID_SEGMENTO