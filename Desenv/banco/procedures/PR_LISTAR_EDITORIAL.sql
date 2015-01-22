/****** Object:  StoredProcedure [dbo].[PR_LISTAR_EDITORIAL]    Script Date: 07/06/2012 10:38:35 ******/
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

ALTER PROCEDURE [dbo].[PR_LISTAR_EDITORIAL]      

(     

 @P_DIVISAO VARCHAR(30) = NULL,
 @P_ANO VARCHAR(4) = NULL,
 @P_CODPLAN INT = NULL,
 @P_EDICAO VARCHAR(7) = NULL,
 @P_FORNECEDOR BIT = NULL, 
 @P_EMAILUSUARIO VARCHAR(50) = NULL
 
)      

	AS      
	BEGIN      
	SET NOCOUNT ON 

	create table #RESULT_EDITORIAL
	(
		cod_plan int null,
		cod_forn int null,
		email varchar(40),
		descricao_pdfs varchar(100) null,
		divisao varchar(30) null,
		edicao varchar(7) null, 
		mes varchar(2) null,
		data datetime null,
		tipo_data varchar(6) null,
		aprovacao_mkt bit null,
		aprovacao_edit bit null,
		aprovacao_forn bit null
	)

	Insert into #RESULT_EDITORIAL 
		( cod_plan,
		  cod_forn, 
		  email, 
		  descricao_pdfs, 
		  divisao, 
		  edicao, 
		  mes, 
		  data, 
		  tipo_data, 
		  aprovacao_mkt, 
		  aprovacao_edit, 
		  aprovacao_forn ) 
	(
		SELECT DISTINCT 
			M.cod_plan, 
			PC.cod_fornecedor, 
			F.email, 
			PC.descricao_pdfs, 
			PC.divisao,  
			M.edicao, 
			PI.mes, 
			dt_real, 
			M.tipo_data, 
			PI.aprovacao_mkt, 
			PI.aprovacao_edit , 
			PI.aprovacao_forn
			
			FROM PLAN_CABECALHO PC (NOLOCK)
			INNER JOIN MERCADO M (NOLOCK)
			ON M.cod_plan = PC.codigo 
			INNER JOIN PLAN_ITENS PI (NOLOCK)
			ON PI.cod_plan = M.cod_plan AND PI.edicao = M.edicao
			INNER JOIN FORNECEDORES F (NOLOCK)
			ON F.codigo = PC.cod_fornecedor
			WHERE (PC.divisao = @P_DIVISAO  OR @P_DIVISAO IS NULL)
			AND (PC.ano = @P_ANO  OR @P_ANO IS NULL)
			AND (PC.codigo = @P_CODPLAN  OR @P_CODPLAN IS NULL)
			AND (PI.edicao = @P_EDICAO  OR @P_EDICAO IS NULL)
			AND M.tipo_data in ('DL', 'DPE', 'DAL', 'DAP')
			AND aprovacao_mkt = 1
			AND (PI.liberado_forn = @P_FORNECEDOR  OR @P_FORNECEDOR IS NULL)
	)
	
		create table #EDITORIAL
		(
			cod_plan int null,
			cod_forn int null,
			email varchar(40),
			descricao_pdfs varchar(100) null,
			divisao varchar(30) null,
			edicao varchar(7) null, 
			mes varchar(2) null,
			dt_lancamento datetime null,
			dt_producao datetime null,
			dt_aprov_lic datetime null,
			dt_aprov_plotter datetime null,
			aprovacao_mkt bit null,
			aprovacao_edit bit null,
			aprovacao_forn bit null
		) 


	DECLARE @cod_plan int,
			@cod_forn int,
			@email varchar(40),
			@descricao_pdfs varchar(100),
			@divisao varchar(30),
			@edicao varchar(7), 
			@mes varchar(2),
			@data datetime,
			@tipo_data varchar(6),
			@aprovacao_mkt bit,
			@aprovacao_edit bit,
			@aprovacao_forn bit

	DECLARE TempCursor CURSOR
	FOR

			SELECT 
			cod_plan, 
			cod_forn, 
			email, 
			descricao_pdfs, 
			divisao, 
			edicao, 
			mes, 
			data, 
			tipo_data, 
			aprovacao_mkt, 
			aprovacao_edit, 
			aprovacao_forn 

	FROM #RESULT_EDITORIAL ORDER BY descricao_pdfs, edicao

	OPEN TempCursor

	FETCH NEXT FROM TempCursor 

			INTO 
			@cod_plan, 
			@cod_forn, 
			@email, 
			@descricao_pdfs, 
			@divisao, @edicao, 
			@mes, 
			@data, 
			@tipo_data, 
			@aprovacao_mkt, 
			@aprovacao_edit, 
			@aprovacao_forn
		 
	WHILE @@FETCH_STATUS = 0 BEGIN
		IF ((@P_FORNECEDOR = 1 AND (SELECT COUNT(cod_plan) FROM #RESULT_EDITORIAL WHERE cod_plan = @cod_plan AND edicao = @edicao AND email = @P_EMAILUSUARIO) > 0) OR @P_FORNECEDOR  IS NULL ) BEGIN
			IF (SELECT COUNT(cod_plan) FROM #EDITORIAL WHERE cod_plan = @cod_plan AND edicao = @edicao) = 0 BEGIN
				
				INSERT INTO #EDITORIAL
				(cod_plan, 
				 cod_forn, 
				 email, 
				 descricao_pdfs, 
				 divisao, 
				 edicao, 
				 mes, 
				 aprovacao_mkt, 
				 aprovacao_edit, 
				 aprovacao_forn) 
				
				VALUES
				(@cod_plan, 
				 @cod_forn, 
				 @email, 
				 @descricao_pdfs, 
				 @divisao, 
				 @edicao, 
				 @mes, 
				 @aprovacao_mkt, 
				 @aprovacao_edit, 
				 @aprovacao_forn) 
			END
		
			IF @tipo_data = 'DAP' BEGIN
					UPDATE #EDITORIAL SET dt_aprov_plotter = @data WHERE  cod_plan = @cod_plan AND edicao = @edicao
			END
			ELSE IF  @tipo_data = 'DAL' BEGIN
				UPDATE #EDITORIAL SET dt_aprov_lic = @data WHERE  cod_plan = @cod_plan AND edicao = @edicao
			END
			ELSE IF  @tipo_data = 'DPE' BEGIN
				UPDATE #EDITORIAL SET dt_producao = @data WHERE  cod_plan = @cod_plan AND edicao = @edicao
			END
			ELSE IF  @tipo_data = 'DL' BEGIN
				UPDATE #EDITORIAL SET dt_lancamento = @data WHERE  cod_plan = @cod_plan AND edicao = @edicao
			END
		END
		
		FETCH NEXT FROM TempCursor 
		INTO @cod_plan, 
			 @cod_forn, 
			 @email, 
			 @descricao_pdfs, 
			 @divisao, 
			 @edicao, 
			 @mes, 
			 @data, 
			 @tipo_data, 
			 @aprovacao_mkt, 
			 @aprovacao_edit, 
			 @aprovacao_forn
	END
		
			CLOSE TempCursor
			DEALLOCATE TempCursor
			DROP TABLE #RESULT_EDITORIAL

			SELECT 
				cod_plan, 
				cod_forn, 
				email, 
				edicao, 
				descricao_pdfs, 
				divisao,  
	
			CASE WHEN LEN(descricao_pdfs) < 13 THEN LEFT(descricao_pdfs,13) 
			ELSE LEFT(descricao_pdfs,13) + '...' END descricao_pdfs_red,
			CASE mes WHEN '1' THEN 'JAN' WHEN '2' THEN 'FEV' WHEN '3' THEN 'MAR' 
			WHEN '4' THEN 'ABR' WHEN '5' THEN 'MAI' WHEN '6' THEN 'JUN' WHEN '7' THEN 'JUL' WHEN '8' THEN 'AGO' 
			WHEN '9' THEN 'SET' WHEN '10' THEN 'OUT' WHEN '11' THEN 'NOV' WHEN '12' THEN 'DEZ' END mes, 
			convert(varchar(10), dt_lancamento, 103) dt_lancamento, 
			convert(varchar(10), dt_producao, 103) dt_producao, 
			convert(varchar(10), dt_aprov_lic, 103) dt_aprov_lic,
			convert(varchar(10), dt_aprov_plotter, 103) dt_aprov_plotter,
			CASE aprovacao_mkt WHEN '1' THEN 'imagens/mkt_aprova.gif' WHEN '0' THEN 'imagens/mkt_n_aprova.gif' 
			ELSE '' END img_aprovacao_mkt,
			CASE aprovacao_edit WHEN '1' THEN 'imagens/edt_aprova.gif' WHEN '0' THEN 'imagens/edt_n_aprova.gif' 
			ELSE '' END img_aprovacao_edit,
			ISNULL(aprovacao_forn, '0') aprovacao_forn,
			CASE WHEN aprovacao_mkt is null THEN '0' ELSE '1' END aprovacao_mkt,
			CASE WHEN aprovacao_edit is null THEN '0' ELSE '1' END aprovacao_edit
			FROM #EDITORIAL

			DROP TABLE #EDITORIAL

			SET NOCOUNT OFF 
			END 

GO


