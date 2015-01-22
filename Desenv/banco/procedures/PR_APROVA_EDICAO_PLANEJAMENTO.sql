/****** Object:  StoredProcedure [dbo].[PR_APROVA_EDICAO_PLANEJAMENTO]    Script Date: 07/06/2012 10:25:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  15/03/2012      
-- Descrição:  Aprova ou reprova edicao do planejamento 
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================  
    
ALTER PROCEDURE [dbo].[PR_APROVA_EDICAO_PLANEJAMENTO]      
(     
 @P_CODPLAN int,
 @P_EDICAO VARCHAR(7),
 @P_APROVADOR VARCHAR(1),
 @P_APROVA BIT
)      
AS      
BEGIN      
SET NOCOUNT ON  
	
	DECLARE @MES INT,
	@APROVEDIT BIT,
	@APROVMKT BIT,
	@QUERY VARCHAR(100),
	@QUERY_ADD VARCHAR(20)
		
	IF @P_APROVADOR = 'M' BEGIN
		UPDATE PLAN_ITENS SET aprovacao_mkt = @P_APROVA WHERE cod_plan = @P_CODPLAN AND edicao = @P_EDICAO
	END
	ELSE BEGIN
		UPDATE PLAN_ITENS SET aprovacao_edit = @P_APROVA WHERE cod_plan = @P_CODPLAN AND edicao = @P_EDICAO
	END
		
	SELECT @MES = mes, @APROVEDIT = aprovacao_edit, @APROVMKT = aprovacao_mkt FROM PLAN_ITENS WHERE cod_plan = @P_CODPLAN AND edicao = @P_EDICAO
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
	SET @QUERY = 'UPDATE PLAN_CABECALHO SET ' + @QUERY_ADD + ' WHERE codigo = ' + CONVERT(VARCHAR(10),@P_CODPLAN)
	EXEC(@QUERY)
    
SET NOCOUNT OFF 
END 

GO


