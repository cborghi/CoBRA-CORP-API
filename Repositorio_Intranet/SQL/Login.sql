﻿SELECT 
    U.ID_USUARIO,
    U.NOME,
    U.EMAIL,
    U.COD_USUARIO,
    U.CONTA_AD,
    G.ID_GRUPO,
    G.ID_GRUPO_AD,
    G.DESCRICAO GRUPODESC,
    G.DEPARTAMENTO,
    N.ID_NIVEL,
    N.DESCRICAO NIVELDESC,
    C.ID_CARGO,
    C.DESCRICAO CARGODESC,
    CT.CTT_CUSTO 
FROM 
    USUARIO U
INNER JOIN 
    GRUPO G ON G.ID_GRUPO = U.ID_GRUPO
LEFT JOIN 
    NIVEL N ON N.ID_NIVEL = U.ID_NIVEL
LEFT JOIN 
    CARGO C ON C.ID_CARGO = U.ID_CARGO
INNER JOIN 
    __protheusAmbiente__.dbo.CTT010 as CT 
    on CT.CTT_DESC01 COLLATE Latin1_General_CI_AS = G.DEPARTAMENTO COLLATE Latin1_General_CI_AS
WHERE 
    __queryEmail__ 
AND 
    U.ATIVO = 1