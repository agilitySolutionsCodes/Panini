/****** Object:  StoredProcedure [dbo].[PR_INCLUIR_PDFS]    Script Date: 07/06/2012 10:36:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================        
-- Autor:   Juliana B. - Agility Solutions        
-- Data Criacao:  16/02/2012        
-- Descrição:  Importar dados na tabela de PDFS     
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO        
-- #001#  16/02/2012  Juliana   @Todas as variaveis vem com os itens separados por ; para que a procedure seja chamada apenas uma vez melhorando performance  
-- #002#  31/02/2012  Yule		  
-- =========================================================================        
  
ALTER PROCEDURE [dbo].[PR_INCLUIR_PDFS]        
(     --#001#     
 @P_CODPDFS VARCHAR(max),           
 @P_DESCRICAO VARCHAR(max),  
 @P_LICENCIANTE VARCHAR(max),  
 @P_DIVISAO VARCHAR(max),
 @P_CODDIVISAO VARCHAR(max),
 @P_COLECAO VARCHAR(max),
 @P_EDICAO VARCHAR(max),
 @P_CANAL VARCHAR(max),
 @P_PERSONAGEM VARCHAR(max),
 @P_CAPA VARCHAR(max),
 @P_LARGURA VARCHAR(max),
 @P_ALTURA VARCHAR(max),
 @P_PAGINAS VARCHAR(max),
 @P_ANO VARCHAR(max),
 @P_CODPANINI VARCHAR(max),
 @QTDREGISTROS INT = NULL OUTPUT  
)        
AS        
BEGIN        
SET NOCOUNT ON      
  
 DECLARE @CODPDFS VARCHAR(20)  
 DECLARE @DESCRICAO VARCHAR(100)  
 DECLARE @LICENCIANTE VARCHAR(50)  
 DECLARE @DIVISAO VARCHAR(20) 
 DECLARE @CODDIVISAO VARCHAR(20)
 DECLARE @COLECAO VARCHAR(8)
 DECLARE @EDICAO VARCHAR(3)
 DECLARE @CANAL VARCHAR(15)
 DECLARE @PERSONAGEM VARCHAR(40)
 DECLARE @CAPA VARCHAR(6)
 DECLARE @LARGURA VARCHAR(6)
 DECLARE @ALTURA VARCHAR(6)
 DECLARE @PAGINAS VARCHAR(6)
 DECLARE @ANO VARCHAR(4)
 DECLARE @CODPANINI VARCHAR(15)
 DECLARE @CONTADOR INT
 DECLARE @CODLICENCIANTE INT
 SET @CONTADOR = 0 
  
 WHILE charindex(';', @P_CODPDFS) > 0 BEGIN  
   
  SET @CODPDFS = LEFT(@P_CODPDFS, charindex(';', @P_CODPDFS)-1)  
  SET @P_CODPDFS = SUBSTRING(@P_CODPDFS, charindex(';', @P_CODPDFS)+1, LEN(@P_CODPDFS))  
  SET @DESCRICAO = LEFT(@P_DESCRICAO, charindex(';', @P_DESCRICAO)-1)  
  SET @P_DESCRICAO = SUBSTRING(@P_DESCRICAO, charindex(';', @P_DESCRICAO)+1, LEN(@P_DESCRICAO))  
  SET @LICENCIANTE = LEFT(@P_LICENCIANTE, charindex(';', @P_LICENCIANTE)-1)    
  SET @P_LICENCIANTE = SUBSTRING(@P_LICENCIANTE, charindex(';', @P_LICENCIANTE)+1, LEN(@P_LICENCIANTE))  
  SET @DIVISAO = LEFT(@P_DIVISAO, charindex(';', @P_DIVISAO)-1)  
  SET @P_DIVISAO = SUBSTRING(@P_DIVISAO, charindex(';', @P_DIVISAO)+1, LEN(@P_DIVISAO)) 
  SET @CODDIVISAO = LEFT(@P_CODDIVISAO, charindex(';', @P_CODDIVISAO)-1)  
  SET @P_CODDIVISAO = SUBSTRING(@P_CODDIVISAO, charindex(';', @P_CODDIVISAO)+1, LEN(@P_CODDIVISAO)) 
  SET @COLECAO = LEFT(@P_COLECAO, charindex(';', @P_COLECAO)-1)  
  SET @P_COLECAO = SUBSTRING(@P_COLECAO, charindex(';', @P_COLECAO)+1, LEN(@P_COLECAO))  
  SET @EDICAO = LEFT(@P_EDICAO, charindex(';', @P_EDICAO)-1)  
  SET @P_EDICAO = SUBSTRING(@P_EDICAO, charindex(';', @P_EDICAO)+1, LEN(@P_EDICAO))
  SET @CANAL = LEFT(@P_CANAL, charindex(';', @P_CANAL)-1)  
  SET @P_CANAL = SUBSTRING(@P_CANAL, charindex(';', @P_CANAL)+1, LEN(@P_CANAL)) 
  SET @PERSONAGEM = LEFT(@P_PERSONAGEM, charindex(';', @P_PERSONAGEM)-1)  
  SET @P_PERSONAGEM = SUBSTRING(@P_PERSONAGEM, charindex(';', @P_PERSONAGEM)+1, LEN(@P_PERSONAGEM)) 
  SET @CAPA = LEFT(@P_CAPA, charindex(';', @P_CAPA)-1)  
  SET @P_CAPA = SUBSTRING(@P_CAPA, charindex(';', @P_CAPA)+1, LEN(@P_CAPA)) 
  SET @LARGURA = LEFT(@P_LARGURA, charindex(';', @P_LARGURA)-1)  
  SET @P_LARGURA = SUBSTRING(@P_LARGURA, charindex(';', @P_LARGURA)+1, LEN(@P_LARGURA)) 
  SET @ALTURA = LEFT(@P_ALTURA, charindex(';', @P_ALTURA)-1)  
  SET @P_ALTURA = SUBSTRING(@P_ALTURA, charindex(';', @P_ALTURA)+1, LEN(@P_ALTURA)) 
  SET @PAGINAS = LEFT(@P_PAGINAS, charindex(';', @P_PAGINAS)-1)  
  SET @P_PAGINAS = SUBSTRING(@P_PAGINAS, charindex(';', @P_PAGINAS)+1, LEN(@P_PAGINAS))   
  SET @ANO = LEFT(@P_ANO, charindex(';', @P_ANO)-1)  
  SET @P_ANO = SUBSTRING(@P_ANO, charindex(';', @P_ANO)+1, LEN(@P_ANO))   
  SET @CODPANINI = LEFT(@P_CODPANINI, charindex(';', @P_CODPANINI)-1)  
  SET @P_CODPANINI = SUBSTRING(@P_CODPANINI, charindex(';', @P_CODPANINI)+1, LEN(@P_CODPANINI))   
    
  IF(SELECT COUNT(*) FROM PDFS (NOLOCK) WHERE ano = @ANO AND cod_pdfs = @CODPDFS AND edicao = @EDICAO) = 0 BEGIN  
	SELECT @CODLICENCIANTE = codigo FROM LICENCIANTES WHERE nome = @LICENCIANTE
	 IF @CODLICENCIANTE IS NULL BEGIN 
		  INSERT INTO LICENCIANTES(nome)	  
		  VALUES(@LICENCIANTE)
		  SELECT @CODLICENCIANTE = @@IDENTITY
		END
  INSERT INTO PDFS(cod_pdfs, descricao, cod_licenciante, licenciante, divisao, cod_divisao, colecao, edicao, canal, personagem, capa, largura, altura, paginas, ano, cod_panini) 
  VALUES(@CODPDFS, @DESCRICAO, @CODLICENCIANTE, @LICENCIANTE, @DIVISAO, @CODDIVISAO, @COLECAO, @EDICAO, @CANAL, @PERSONAGEM, @CAPA, @LARGURA, @ALTURA, @PAGINAS, @ANO, @CODPANINI)
	
	SET @CONTADOR = @CONTADOR + 1 
  END  
 END  
 SET @QTDREGISTROS = @CONTADOR
END        
SET NOCOUNT OFF 




GO


