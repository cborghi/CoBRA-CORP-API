﻿INSERT INTO [dbo].[PRODUTO]
           ([PROD_CDIS]
           ,[PROD_TEMA]
           ,[PROD_DISC]
           ,[PROD_MERCADO]
           ,[PROD_PROGRAMA]
           ,[PROD_TIPO]
           ,[PROD_SELO]
           ,[ID_SELO]
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
           ,[PROD_STATUS]
           ,[PROD_CODBARRAS]
           ,[PROD_SINOPSE]
           ,[PROD_PRECO]
           ,[PROD_EBSA]
           ,[PROD_UNIDADE]
           ,[PROD_CODPROT]
           ,[PROD_PUBLICADO]
           ,[PROD_ORIGEM]
           ,[EXCLUIDO]
           ,[PROD_TIPO_PRODUTO]
           ,[PROD_UNIDADE_MEDIDA]
           ,[ID_GRUPO]
           ,[ID_SEGMENTO]
           ,[PROD_VIDEO]
           ,[PROD_NOME_CAPA]
           ,[ANO_PROGRAMA]
           ,[PROD_INTEGRADO])
     VALUES
           (@PROD_CDIS
           ,@PROD_TEMA
           ,@PROD_DISC
           ,@PROD_MERCADO
           ,@PROD_PROGRAMA
           ,@PROD_TIPO
           ,@PROD_SELO
           ,(SELECT ID_SELO FROM TB_SELO_PROTHEUS WHERE DESCRICAO = @PROD_SELO)
           ,@PROD_SEGM
           ,@PROD_AEDU
           ,@PROD_COMP
           ,@PROD_FAIXAETARIA
           ,NULL
           ,@PROD_GTEX
           ,@PROD_PREMIACAO
           ,@PROD_VERSAO
           ,@PROD_COLE
           ,@PROD_TITULO
           ,@PROD_EDICAO
           ,@PROD_MIDI
           ,NULL
           ,@PROD_PLAT
           ,NULL
           ,NULL
           ,NULL
           ,@PROD_PUBLICACAO
           ,@PROD_ISBN
           ,@PROD_STATUS
           ,@PROD_CODBARRAS
           ,@PROD_SINOPSE
           ,NULL
           ,@PROD_EBSA
           ,NULL
           ,NULL
           ,NULL
           ,@PROD_ORIGEM
           ,@EXCLUIDO
           ,@PROD_TIPO_PRODUTO
           ,@PROD_UNIDADE_MEDIDA
           ,NULL
           ,@ID_SEGMENTO
           ,NULL
           ,@PROD_NOME_CAPA
           ,@ANO_PROGRAMA
           ,@PROD_INTEGRADO)

SELECT @@IDENTITY;