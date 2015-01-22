/****** Object:  StoredProcedure [dbo].[PR_APROVA_PLANEJAMENTO]    Script Date: 07/06/2012 10:26:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  08/03/2012      
-- Descrição:  Aprova ou reprova planejamento 
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================  
    
ALTER PROCEDURE [dbo].[PR_APROVA_PLANEJAMENTO]      
(     
 @P_CODPLAN VARCHAR(MAX),
 @P_EDICAO VARCHAR(MAX),
 @P_APROVA BIT,
 @P_FORNECEDOR BIT
)      
AS      
BEGIN      
SET NOCOUNT ON  
	
	DECLARE @errornumber int, @errormessage varchar(max),
	@CODPLAN INT,
	@EDICAO VARCHAR(7),
	@MES INT,
	@APROVEDIT BIT,
	@APROVMKT BIT,
	@QUERY VARCHAR(100),
	@QUERY_ADD VARCHAR(20)
	
	WHILE charindex(';', @P_CODPLAN) > 0 BEGIN 

		SET @CODPLAN = LEFT(@P_CODPLAN, charindex(';', @P_CODPLAN)-1)
		SET @P_CODPLAN = SUBSTRING(@P_CODPLAN, charindex(';', @P_CODPLAN)+1, LEN(@P_CODPLAN))  
		SET @EDICAO = LEFT(@P_EDICAO, charindex(';', @P_EDICAO)-1)
		SET @P_EDICAO = SUBSTRING(@P_EDICAO, charindex(';', @P_EDICAO)+1, LEN(@P_EDICAO))
		
		--BEGIN TRY
		IF @P_FORNECEDOR = 1 BEGIN
			UPDATE PLAN_ITENS SET aprovacao_forn = @P_APROVA
			WHERE cod_plan = @CODPLAN AND edicao = @EDICAO
		END
		ELSE BEGIN
			UPDATE PLAN_ITENS SET aprovacao_edit = @P_APROVA
			WHERE cod_plan = @CODPLAN AND edicao = @EDICAO
		
		
			SELECT @MES = mes, @APROVEDIT = aprovacao_edit, @APROVMKT = aprovacao_mkt FROM PLAN_ITENS WHERE cod_plan = @CODPLAN AND edicao = @EDICAO
			IF @MES = 1 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_jan = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_jan = 1'
				END
			END
			ELSE IF @MES = 2 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_fev = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_fev = 1'
				END
			END
			ELSE IF @MES = 3 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_mar = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_mar = 1'
				END
			END
			ELSE IF @MES = 4 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_abr = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_abr = 1'
				END
			END
			ELSE IF @MES = 5 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_mai = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_mai = 1'
				END
			END
			ELSE IF @MES = 6 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_jun = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_jun = 1'
				END
			END
			ELSE IF @MES = 7 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_jul = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_jul = 1'
				END
			END
			ELSE IF @MES = 8 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_ago = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_ago = 1'
				END
			END
			ELSE IF @MES = 9 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_set = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_set = 1'
				END
			END
			ELSE IF @MES = 10 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_out = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_out = 1'
				END
			END
			ELSE IF @MES = 11 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_nov = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_nov = 1'
				END
			END
			ELSE IF @MES = 12 BEGIN
				IF @APROVEDIT = '0' OR @APROVMKT = '0' BEGIN
					SET @QUERY_ADD = ' aprovado_dez = 0'
				END
				ELSE BEGIN
					SET @QUERY_ADD = ' aprovado_dez = 1'
				END
			END
			SET @QUERY = 'UPDATE PLAN_CABECALHO SET ' + @QUERY_ADD + ' WHERE codigo = ' + CONVERT(VARCHAR(10),@CODPLAN)
			EXEC(@QUERY)
	    END
		--END TRY
		--BEGIN CATCH
		--	select
		--		 @errornumber = ERROR_NUMBER()
		--		,@errormessage = ERROR_MESSAGE()

		--	RAISERROR(@errornumber, 16, 1, @errormessage)
		--END CATCH
	END
	
     
SET NOCOUNT OFF 
END 

GO


