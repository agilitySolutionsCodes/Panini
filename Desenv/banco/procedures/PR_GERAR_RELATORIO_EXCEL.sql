/****** Object:  StoredProcedure [dbo].[PR_GERAR_RELATORIO_EXCEL]    Script Date: 07/06/2012 10:32:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  07/02/2012      
-- Descrição:  Consultar dados de edição   
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================  

ALTER PROCEDURE [dbo].[PR_GERAR_RELATORIO_EXCEL]      
(     
 @P_ANO INTEGER = NULL,
 @P_DIVISAO VARCHAR(30) = NULL,
 @P_TIPO VARCHAR(1) = NULL,
 @P_CODPLAN INT = NULL,
 @P_NOMEARQUIVO VARCHAR(50) = NULL,
 @P_VERSAO VARCHAR(2) = NULL
)      
AS      
BEGIN      
SET NOCOUNT ON 

	create table #RESULT_RELATORIO
	(
		codigo int null,
		ano varchar(4) null,
		divisao varchar(30) null, 
		descricao_pdfs varchar(100) null,
		fase varchar(3) null,
		cod_pdfs varchar(20) null,
		distribuicao varchar(15) null,
		preco money null,
		formato varchar(10) null,
		binding varchar(15) null,
		qtde_paginas varchar(10) null,
		periodicidade varchar(1) null,
		mes int null,
		edicao varchar(7) null
	)

	Insert into #RESULT_RELATORIO ( codigo, ano, divisao, descricao_pdfs, fase, cod_pdfs, distribuicao, preco, formato, binding, qtde_paginas, periodicidade, mes, edicao ) 
	(
		SELECT PC.codigo, PC.ano, PC.divisao, PC.descricao_pdfs, PC.fase, PC.cod_pdfs, PC.distribuicao,
		 PI.preco, PI.formato, PI.binding, PI.qtde_paginas, PI.periodicidade, PI.mes, PI.edicao
		FROM PLAN_CABECALHO(NOLOCK) PC
		INNER JOIN PLAN_ITENS (NOLOCK) PI 
		ON PI.cod_plan = PC.codigo
		WHERE (ano = @P_ANO  OR @P_ANO IS NULL) 
		AND (divisao = @P_DIVISAO  OR @P_DIVISAO IS NULL) 
		AND (tipo = @P_TIPO  OR @P_TIPO IS NULL)
		AND (PC.codigo = @P_CODPLAN  OR @P_CODPLAN IS NULL)
	)
	
	create table #RELATORIO
	(
		nome_arquivo varchar(50),
		versao varchar(2),
		cod_plan int,
		cod_pdfs varchar(20),
		descricao_pdfs varchar(100),
		fase varchar(3),
		divisao varchar(30),
		distribuicao varchar(15),
		ano integer,
		preco money,	
		formato	varchar(10),
		qtde_paginas varchar(10),
		periodicidade varchar(1),
		binding varchar(15),
		edicao_jan	varchar(7),
		edicao_fev	varchar(7),
		edicao_mar	varchar(7),
		edicao_abr	varchar(7),
		edicao_mai	varchar(7),
		edicao_jun	varchar(7),
		edicao_jul	varchar(7),
		edicao_ago	varchar(7),
		edicao_set	varchar(7),
		edicao_out	varchar(7),
		edicao_nov	varchar(7),
		edicao_dez	varchar(7)
	)


DECLARE @codigo int,
		@ano varchar(4),
		@divisao varchar(30), 
		@descricao_pdfs varchar(100),
		@fase varchar(3),
		@cod_pdfs varchar(20),
		@distribuicao varchar(15),
		@preco money,
		@formato varchar(10),
		@binding varchar(15),
		@qtde_paginas varchar(10),
		@periodicidade varchar(1),
		@mes int,
		@edicao varchar(7)
		

DECLARE TempCursor CURSOR
FOR

SELECT codigo, ano, edicao, divisao, descricao_pdfs, fase, cod_pdfs, distribuicao, preco, formato, binding, qtde_paginas, periodicidade, mes, edicao FROM #RESULT_RELATORIO ORDER BY codigo, mes

OPEN TempCursor

FETCH NEXT FROM TempCursor INTO @codigo, @ano, @edicao, @divisao, @descricao_pdfs, @fase, @cod_pdfs, @distribuicao, @preco, @formato, @binding, @qtde_paginas, @periodicidade, @mes, @edicao
WHILE @@FETCH_STATUS = 0 BEGIN

	IF (SELECT COUNT(*) FROM #RELATORIO WHERE cod_plan = @codigo) = 0 BEGIN
		INSERT INTO #RELATORIO(nome_arquivo,versao,cod_plan,cod_pdfs,descricao_pdfs,divisao, fase,
		distribuicao,ano, preco, formato,qtde_paginas,periodicidade,binding) 
		VALUES(@P_NOMEARQUIVO, @P_VERSAO, @codigo, @cod_pdfs, @descricao_pdfs, @divisao, @fase,   
		@distribuicao, @ano, @preco, @formato, @qtde_paginas, @periodicidade, @binding) 
	END
	
	IF @mes = 1 BEGIN
		UPDATE #RELATORIO SET edicao_jan = @edicao WHERE  cod_plan = @codigo
	END
	ELSE IF @mes = 2 BEGIN
		UPDATE #RELATORIO SET edicao_fev = @edicao WHERE  cod_plan = @codigo
	END
	ELSE IF @mes = 3 BEGIN
		UPDATE #RELATORIO SET edicao_mar = @edicao WHERE  cod_plan = @codigo
	END
	ELSE IF @mes = 4 BEGIN
		UPDATE #RELATORIO SET edicao_abr = @edicao WHERE  cod_plan = @codigo
	END
	ELSE IF @mes = 5 BEGIN
		UPDATE #RELATORIO SET edicao_mai = @edicao WHERE  cod_plan = @codigo
	END
	ELSE IF @mes = 6 BEGIN
		UPDATE #RELATORIO SET edicao_jun = @edicao WHERE  cod_plan = @codigo
	END
	ELSE IF @mes = 7 BEGIN
		UPDATE #RELATORIO SET edicao_jul = @edicao WHERE  cod_plan = @codigo
	END
	ELSE IF @mes = 8 BEGIN
		UPDATE #RELATORIO SET edicao_ago = @edicao WHERE  cod_plan = @codigo
	END
	ELSE IF @mes = 9 BEGIN
		UPDATE #RELATORIO SET edicao_set = @edicao WHERE  cod_plan = @codigo
	END
	ELSE IF @mes = 10 BEGIN
		UPDATE #RELATORIO SET edicao_out = @edicao WHERE  cod_plan = @codigo
	END
	ELSE IF @mes = 11 BEGIN
		UPDATE #RELATORIO SET edicao_nov = @edicao WHERE  cod_plan = @codigo
	END
	ELSE IF @mes = 12 BEGIN
		UPDATE #RELATORIO SET edicao_dez = @edicao WHERE  cod_plan = @codigo
	END
	
	FETCH NEXT FROM TempCursor INTO @codigo, @ano, @edicao, @divisao, @descricao_pdfs, @fase, @cod_pdfs, @distribuicao, @preco, @formato, @binding, @qtde_paginas, @periodicidade, @mes, @edicao
END
	
CLOSE TempCursor
DEALLOCATE TempCursor
DROP TABLE #RESULT_RELATORIO

-- INSERIR TODOS OS DADOS GERADOS NA TABELA TEMPORARIA PARA  A TABELA RELATORIO
INSERT INTO RELATORIO(nome_arquivo,versao,cod_plan,cod_pdfs,descricao_pdfs,divisao, fase,
	distribuicao,ano, preco, formato,qtde_paginas,periodicidade,binding, edicao_jan, edicao_fev, edicao_mar,
	edicao_abr, edicao_mai, edicao_jun, edicao_jul, edicao_ago, edicao_set, edicao_out, edicao_nov, edicao_dez) 
	(
		SELECT nome_arquivo,versao,cod_plan,cod_pdfs,descricao_pdfs,divisao, fase,
		distribuicao,ano, preco, formato,qtde_paginas,periodicidade,binding, edicao_jan, edicao_fev, edicao_mar,
		edicao_abr, edicao_mai, edicao_jun, edicao_jul, edicao_ago, edicao_set, edicao_out, edicao_nov, edicao_dez FROM #RELATORIO
	)

SELECT cod_pdfs,descricao_pdfs,divisao, fase, CASE distribuicao WHEN 'S' THEN 'Setorizada' WHEN 'N' THEN 'NACIONAL' ELSE '' END distribuicao, ano, preco, formato,qtde_paginas,periodicidade,
	binding, edicao_jan, edicao_fev, edicao_mar, edicao_abr, edicao_mai, edicao_jun, edicao_jul, edicao_ago,
	edicao_set, edicao_out, edicao_nov, edicao_dez
FROM #RELATORIO ORDER BY descricao_pdfs, fase

DROP TABLE #RELATORIO

     
SET NOCOUNT OFF 
END 

GO


