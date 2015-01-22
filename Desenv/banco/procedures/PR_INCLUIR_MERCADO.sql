/****** Object:  StoredProcedure [dbo].[PR_INCLUIR_MERCADO]    Script Date: 07/06/2012 10:35:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================        
-- Autor:   Juliana B. - Agility Solutions        
-- Data Criacao:  14/02/2012        
-- Descrição:  Incluir datas na tabela de mercado e itens de planejamento de acordo com a edicao    
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO        
-- #001#  09/02/2012  Juliana @Todas as variaveis vem com os itens separados por ; para que a procedure seja chamada apenas uma vez melhorando performance  
-- =========================================================================        
  
ALTER PROCEDURE [dbo].[PR_INCLUIR_MERCADO]        
(     --#001#   
 @P_CODPLAN INT,
 @P_EDICAO VARCHAR(7) = null,
 @P_DTPREVISTA VARCHAR(max),           
 @P_DTREAL VARCHAR(max),  
 @P_DIFERENCA VARCHAR(max),
 @P_TIPODATA VARCHAR(65),
 @P_CODDATA INT = NULL,
 @P_TPOCORRENCIAMKT VARCHAR(1) = null,
 @P_OCORRENCIAMKT VARCHAR(255) = null,
 @P_TPOCORRENCIAEDIT VARCHAR(1) = null,
 @P_OCORRENCIAEDIT VARCHAR(255) = null
)        
AS        
BEGIN        
SET NOCOUNT ON      
  
 DECLARE @DTPREVISTA VARCHAR(30)  
 DECLARE @DTREAL VARCHAR(30)  
 DECLARE @DIFERENCA INT 
 DECLARE @TIPODATA VARCHAR(5)  
 
  
 WHILE charindex(';', @P_TIPODATA) > 0 BEGIN  
   
	SET @DTPREVISTA = LEFT(@P_DTPREVISTA, charindex(';', @P_DTPREVISTA)-1)
	SET @DTPREVISTA = case @DTPREVISTA when '' then null else convert(datetime, @DTPREVISTA, 103) end
	SET @P_DTPREVISTA = SUBSTRING(@P_DTPREVISTA, charindex(';', @P_DTPREVISTA)+1, LEN(@P_DTPREVISTA))  
	SET @DTREAL = LEFT(@P_DTREAL, charindex(';', @P_DTREAL)-1)
	SET @DTREAL = case @DTREAL when '' then null else convert(datetime, @DTREAL, 103) end
	SET @P_DTREAL = SUBSTRING(@P_DTREAL, charindex(';', @P_DTREAL)+1, LEN(@P_DTREAL))  
	SET @DIFERENCA = LEFT(@P_DIFERENCA, charindex(';', @P_DIFERENCA)-1)  
	SET @P_DIFERENCA = SUBSTRING(@P_DIFERENCA, charindex(';', @P_DIFERENCA)+1, LEN(@P_DIFERENCA))  
	SET @TIPODATA = LEFT(@P_TIPODATA, charindex(';', @P_TIPODATA)-1)  
	SET @P_TIPODATA = SUBSTRING(@P_TIPODATA, charindex(';', @P_TIPODATA)+1, LEN(@P_TIPODATA))  

	IF(SELECT COUNT(*) FROM MERCADO (NOLOCK) WHERE cod_plan = @P_CODPLAN AND edicao = @P_EDICAO AND tipo_data = @TIPODATA) = 0 BEGIN
		INSERT INTO MERCADO (cod_plan, edicao, dt_prevista, dt_real, diferenca, tipo_data)
		VALUES(@P_CODPLAN, @P_EDICAO, @DTPREVISTA, @DTREAL, @DIFERENCA, @TIPODATA)
	END
	ELSE BEGIN
		UPDATE MERCADO SET dt_prevista = @DTPREVISTA, dt_real = @DTREAL, diferenca = @DIFERENCA
		WHERE cod_plan = @P_CODPLAN AND edicao = @P_EDICAO AND tipo_data = @TIPODATA
	END  
 END  
 
 UPDATE PLAN_ITENS SET cod_data = @P_CODDATA, tipo_ocorrencia_mkt = @P_TPOCORRENCIAMKT, obs_ocorrencia_mkt = @P_OCORRENCIAMKT,
 tipo_ocorrencia_edit = @P_TPOCORRENCIAEDIT, obs_ocorrencia_edit = @P_OCORRENCIAEDIT
 WHERE cod_plan = @P_CODPLAN AND edicao = @P_EDICAO
    
       
SET NOCOUNT OFF 
END 

GO


