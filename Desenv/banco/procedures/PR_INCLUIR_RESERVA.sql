
/****** Object:  StoredProcedure [dbo].[PR_INCLUIR_RESERVA]    Script Date: 07/06/2012 10:36:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================        
-- Autor:   Juliana B. - Agility Solutions        
-- Data Criacao:  07/02/2012        
-- Descrição:  Inclusao de reserva por edicao     
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO        
-- #001#    
-- =========================================================================        
  
ALTER PROCEDURE [dbo].[PR_INCLUIR_RESERVA]        
(        
 @P_CODPLAN INT,           
 @P_EDICAO VARCHAR(7),  
 @P_VAREJO INT,  
 @P_ASSINATURAS INT,  
 @P_EXPORTACAO INT,  
 @P_BIENAL INT,  
 @P_DOACAO INT,  
 @P_PACOTE INT,  
 @P_OUTROS1 INT,  
 @P_OUTROS2 INT,  
 @P_OUTROS3 INT,  
 @P_OUTROS4 INT,  
 @P_TOTAL INT  
)        
AS        
BEGIN        
SET NOCOUNT ON   
  
 DECLARE @errornumber int,@errormessage varchar(max)     
  
 BEGIN TRY  
 
 -- A PRIMEIRA VEZ QUE SERA INCLUIDA UMA RESERVA DE UM PLANEJAMENTO, GRAVAR PARA TODAS AS EDICOES
  IF(SELECT COUNT(*) FROM RESERVA (NOLOCK) WHERE cod_plan = @P_CODPLAN) = 0 BEGIN
	DECLARE @EDICAO varchar(7)
	DECLARE Edicoes_Cursor CURSOR FOR
	SELECT edicao FROM PLAN_ITENS WHERE cod_plan = @P_CODPLAN
	OPEN Edicoes_Cursor
	FETCH NEXT FROM Edicoes_Cursor INTO @EDICAO
	WHILE @@FETCH_STATUS = 0 BEGIN
		INSERT INTO RESERVA(cod_plan, edicao, varejo, assinaturas, exportacao, bienal, doacao, pacote, outros1, outros2, outros3, outros4, total)   
		VALUES(@P_CODPLAN, @EDICAO,@P_VAREJO, @P_ASSINATURAS, @P_EXPORTACAO, @P_BIENAL, @P_DOACAO, @P_PACOTE, @P_OUTROS1, @P_OUTROS2, @P_OUTROS3, @P_OUTROS4, @P_TOTAL)    
		FETCH NEXT FROM Edicoes_Cursor INTO @EDICAO
	END
	CLOSE Edicoes_Cursor
	DEALLOCATE Edicoes_Cursor
  END
  ELSE BEGIN   
	  IF(SELECT COUNT(*) FROM RESERVA (NOLOCK) WHERE cod_plan = @P_CODPLAN AND edicao = @P_EDICAO) = 0 BEGIN  
	   INSERT INTO RESERVA(cod_plan, edicao, varejo, assinaturas, exportacao, bienal, doacao, pacote, outros1, outros2, outros3, outros4, total)   
	   VALUES(@P_CODPLAN, @P_EDICAO,@P_VAREJO, @P_ASSINATURAS, @P_EXPORTACAO, @P_BIENAL, @P_DOACAO, @P_PACOTE, @P_OUTROS1, @P_OUTROS2, @P_OUTROS3, @P_OUTROS4, @P_TOTAL)  
	  END  
	  ELSE BEGIN  
	   UPDATE RESERVA SET varejo = @P_VAREJO, assinaturas = @P_ASSINATURAS, exportacao = @P_EXPORTACAO,   
	   bienal = @P_BIENAL, doacao = @P_DOACAO, pacote = @P_PACOTE, outros1 = @P_OUTROS1,   
	   outros2 = @P_OUTROS2, outros3 = @P_OUTROS3, outros4 = @P_OUTROS4, total = @P_TOTAL  
	   WHERE cod_plan = @P_CODPLAN AND edicao = @P_EDICAO  
	  END  
   END 
 END TRY  
   
 BEGIN CATCH  
  SELECT  
    @errornumber = ERROR_NUMBER()  
   ,@errormessage = ERROR_MESSAGE()  
  
  RAISERROR(@errornumber, 16, 1, @errormessage)  
 END CATCH  
    
END        
SET NOCOUNT OFF 

GO


