﻿SELECT [PROD_ID]
      ,[PROD_CDIS]
      ,[PROD_TEMA]
      ,[PROD_DISC]
      ,[PROD_MERCADO]
      ,[PROD_PROGRAMA]
      ,[PROD_TIPO]
      ,[PROD_SELO]
      ,[PROD_SEGM]
      ,[PROD_AEDU]
      ,[PROD_COMP]
      ,[PROD_FAIXAETARIA]
      ,[PROD_ASSU]
      ,[PROD_GTEX]
      ,[PROD_PREMIACAO]
      ,[PROD_VERSAO]
      ,[PROD_COLE]
      ,[PROD_TITULO]
      ,[PROD_EDICAO]
      ,[PROD_MIDI]
      ,[PROD_PAGINAS]
      ,[PROD_PLAT]
      ,[PROD_FORALTURA]
      ,[PROD_FORLARGURA]
      ,[PROD_PESO]
      ,[PROD_PUBLICACAO]
      ,[PROD_ISBN]
      ,CASE PROD_STATUS  
         WHEN 0 THEN 1
         WHEN 1 THEN 2
         WHEN 2 THEN 3         
         ELSE NULL  
      END AS PROD_STATUS
      ,[PROD_CODBARRAS]
      ,[PROD_SINOPSE]
      ,[PROD_PRECO]
      ,[PROD_EBSA]
      ,[PROD_UNIDADE]
      ,[PROD_CODPROT]
      ,[PROD_PUBLICADO]
      ,[PROD_DATAPUBLICACAO]
      ,[PROD_ORIGEM]
      ,[EXCLUIDO]
      ,[PROD_TIPO_PRODUTO]
      ,[PROD_UNIDADE_MEDIDA]
      ,[ID_GRUPO]
      ,[ID_SEGMENTO]
      ,[PROD_VIDEO]
      ,[PROD_NOME_CAPA]
      ,[ID_SELO]
      ,[ANO_PROGRAMA]
      ,[PROD_DATAINTEGRACAO]
  FROM [dbo].[PRODUTO]
  WHERE PROD_TITULO = @Titulo
  AND EXCLUIDO = 0