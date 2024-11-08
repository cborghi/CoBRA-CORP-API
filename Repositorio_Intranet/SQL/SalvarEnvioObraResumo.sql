﻿IF(NOT EXISTS(SELECT 1 FROM ENVIO_OBRA_RESUMO WHERE OBRA = @OBRA))
BEGIN
    INSERT INTO [dbo].[ENVIO_OBRA_RESUMO]
           ([OBRA]
           ,[STATUS]
           ,[REVISAO_PROVA]
           ,[REVISAO_PROVA_DATA]
           ,[IMAGEM_ALTA]
           ,[IMAGEM_ALTA_DATA]
           ,[PAGTO]
           ,[PAGTO_DATA]
           ,[FATURAMENTO]
           ,[FATURAMENTO_OBS]
           ,[USO_IMAGEM]
           ,[USO_IMAGEM_OBS]
           ,[LICENCIAMENTO]
           ,[LICENCIAMENTO_OBS]
           ,[ICONOGRAFO]
           ,[RESPONSAVEL]
           ,[COORDENADOR]
           ,[E_STATUS]
           ,[E_REVISAO_PROVA]
           ,[E_REVISAO_PROVA_DATA]
           ,[E_IMAGEM_ALTA]
           ,[E_IMAGEM_ALTA_DATA]
           ,[E_PAGTO]
           ,[E_PAGTO_DATA]
           ,[E_FATURAMENTO]
           ,[E_FATURAMENTO_OBS]
           ,[E_USO_IMAGEM]
           ,[E_USO_IMAGEM_OBS]
           ,[E_LICENCIAMENTO]
           ,[E_LICENCIAMENTO_OBS]
           ,[E_ILUSTRADOR]
           ,[E_RESPONSAVEL]
           ,[E_COORDENADOR]
           ,[COD_AUTOR]
           ,[COD_EDITOR]
           ,[FECHAMENTO]
           ,[E_FECHAMENTO]
           ,[COD_ASSISTENTE]
           ,[COD_ASSISTENTE2]
           ,[G_FECHAMENTO]
           ,[G_OBS_FECHAMENTO]
           ,[COD_AUXILIAR]
           ,[VISUALIZAR]
           ,[COD_G_GI]
           ,[OBRA_ANTIGA])
     VALUES
           (@OBRA
           ,@STATUS
           ,@REVISAO_PROVA
           ,@REVISAO_PROVA_DATA
           ,@IMAGEM_ALTA
           ,@IMAGEM_ALTA_DATA
           ,@PAGTO
           ,@PAGTO_DATA
           ,@FATURAMENTO
           ,@FATURAMENTO_OBS
           ,@USO_IMAGEM
           ,@USO_IMAGEM_OBS
           ,@LICENCIAMENTO
           ,@LICENCIAMENTO_OBS
           ,@ICONOGRAFO
           ,@RESPONSAVEL
           ,@COORDENADOR
           ,@E_STATUS
           ,@E_REVISAO_PROVA
           ,@E_REVISAO_PROVA_DATA
           ,@E_IMAGEM_ALTA
           ,@E_IMAGEM_ALTA_DATA
           ,@E_PAGTO
           ,@E_PAGTO_DATA
           ,@E_FATURAMENTO
           ,@E_FATURAMENTO_OBS
           ,@E_USO_IMAGEM
           ,@E_USO_IMAGEM_OBS
           ,@E_LICENCIAMENTO
           ,@E_LICENCIAMENTO_OBS
           ,@E_ILUSTRADOR
           ,@E_RESPONSAVEL
           ,@E_COORDENADOR
           ,@COD_AUTOR
           ,@COD_EDITOR
           ,@FECHAMENTO
           ,@E_FECHAMENTO
           ,@COD_ASSISTENTE
           ,@COD_ASSISTENTE2
           ,@G_FECHAMENTO
           ,@G_OBS_FECHAMENTO
           ,@COD_AUXILIAR
           ,@VISUALIZAR
           ,@COD_G_GI
           ,@OBRA_ANTIGA)
END