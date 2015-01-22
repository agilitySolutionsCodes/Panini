/****** Object:  StoredProcedure [dbo].[PR_IMPORTAR_PLAN]    Script Date: 07/06/2012 10:33:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =========================================================================        
-- Autor:   Juliana B. - Agility Solutions        
-- Data Criacao:  07/03/2012        
-- Descrição:  Importar dados no excel para tabelas de planejamento   
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO        
-- #001#  07/03/2012  Juliana @Todas as variaveis vem com os itens separados por ; para que a procedure seja chamada apenas uma vez melhorando performance  
-- =========================================================================        
  
ALTER PROCEDURE [dbo].[PR_IMPORTAR_PLAN]        
(     --#001#  
 @P_TITULO VARCHAR(max),   
 @P_FASE varchar(max), 
 @P_CODPDFS VARCHAR(max),
 @P_DISTR VARCHAR(MAX),
 @P_PRECO VARCHAR(MAX),
 @P_FORMATO VARCHAR(MAX),
 @P_BINDING VARCHAR(MAX),
 @P_PAGINAS VARCHAR(max),
 @P_QTDECAPA VARCHAR(max),
 @P_QTDEMIOLO VARCHAR(max),
 @P_PERIODICIDADE VARCHAR(MAX),
 @P_JAN VARCHAR(MAX),
 @P_FEV VARCHAR(MAX),
 @P_MAR VARCHAR(MAX),
 @P_ABR VARCHAR(MAX),
 @P_MAI VARCHAR(MAX),
 @P_JUN VARCHAR(MAX),
 @P_JUL VARCHAR(MAX),
 @P_AGO VARCHAR(MAX),
 @P_SET VARCHAR(MAX),
 @P_OUT VARCHAR(MAX),
 @P_NOV VARCHAR(MAX),
 @P_DEZ VARCHAR(MAX),
 @P_ANO VARCHAR(4),
 @P_TIPO VARCHAR(1),
 @QTDREGISTROS INT = NULL OUTPUT  
)        
AS        
BEGIN        
SET NOCOUNT ON      
  
 DECLARE @errornumber int,@errormessage varchar(max),
 @TITULO VARCHAR(100),
 @FASE varchar(3), 
 @CODPDFS VARCHAR(20),  
 @DISTR VARCHAR(15),
 @PRECO MONEY,
 @FORMATO VARCHAR(10),
 @BINDING VARCHAR(15),
 @PAGINAS VARCHAR(6),
 @QTDECAPA INT,
 @QTDEMIOLO INT,
 @PERIODICIDADE VARCHAR(1),
 @JAN VARCHAR(7),
 @FEV VARCHAR(7),
 @MAR VARCHAR(7),
 @ABR VARCHAR(7),
 @MAI VARCHAR(7),
 @JUN VARCHAR(7),
 @JUL VARCHAR(7),
 @AGO VARCHAR(7),
 @SET VARCHAR(7),
 @OUT VARCHAR(7),
 @NOV VARCHAR(7),
 @DEZ VARCHAR(7),
 @CONTADOR INT,
 @LICENCIANTE VARCHAR(50),
 @DIVISAO VARCHAR(30),
 @CODIGO int,
 @COLECAO VARCHAR(8),
 @CODPANINI VARCHAR(15) 
 SET @CODIGO = 0
 SET @CONTADOR = 0 
  
 WHILE charindex(';', @P_CODPDFS) > 0 BEGIN  
   
  SET @TITULO = LEFT(@P_TITULO, charindex(';', @P_TITULO)-1)  
  SET @P_TITULO = SUBSTRING(@P_TITULO, charindex(';', @P_TITULO)+1, LEN(@P_TITULO))  
  SET @FASE = LEFT(@P_FASE, charindex(';', @P_FASE)-1)  
  SET @P_FASE = SUBSTRING(@P_FASE, charindex(';', @P_FASE)+1, LEN(@P_FASE)) 
  SET @CODPDFS = LEFT(@P_CODPDFS, charindex(';', @P_CODPDFS)-1)  
  SET @P_CODPDFS = SUBSTRING(@P_CODPDFS, charindex(';', @P_CODPDFS)+1, LEN(@P_CODPDFS)) 
  
  SET @DISTR = LEFT(@P_DISTR, charindex(';', @P_DISTR)-1)  
  SET @P_DISTR = SUBSTRING(@P_DISTR, charindex(';', @P_DISTR)+1, LEN(@P_DISTR))
  SET @PRECO = LEFT(@P_PRECO, charindex(';', @P_PRECO)-1)  
  SET @P_PRECO = SUBSTRING(@P_PRECO, charindex(';', @P_PRECO)+1, LEN(@P_PRECO))
  SET @FORMATO = LEFT(@P_FORMATO, charindex(';', @P_FORMATO)-1)  
  SET @P_FORMATO = SUBSTRING(@P_FORMATO, charindex(';', @P_FORMATO)+1, LEN(@P_FORMATO))
  SET @BINDING = LEFT(@P_BINDING, charindex(';', @P_BINDING)-1)  
  SET @P_BINDING = SUBSTRING(@P_BINDING, charindex(';', @P_BINDING)+1, LEN(@P_BINDING))
  SET @PAGINAS = LEFT(@P_PAGINAS, charindex(';', @P_PAGINAS)-1)  
  SET @P_PAGINAS = SUBSTRING(@P_PAGINAS, charindex(';', @P_PAGINAS)+1, LEN(@P_PAGINAS))
  SET @QTDECAPA = LEFT(@P_QTDECAPA, charindex(';', @P_QTDECAPA)-1)  
  SET @P_QTDECAPA = SUBSTRING(@P_QTDECAPA, charindex(';', @P_QTDECAPA)+1, LEN(@P_QTDECAPA))
  SET @QTDEMIOLO = LEFT(@P_QTDEMIOLO, charindex(';', @P_QTDEMIOLO)-1)  
  SET @P_QTDEMIOLO = SUBSTRING(@P_QTDEMIOLO, charindex(';', @P_QTDEMIOLO)+1, LEN(@P_QTDEMIOLO))
  SET @PERIODICIDADE = LEFT(@P_PERIODICIDADE, charindex(';', @P_PERIODICIDADE)-1)  
  SET @P_PERIODICIDADE = SUBSTRING(@P_PERIODICIDADE, charindex(';', @P_PERIODICIDADE)+1, LEN(@P_PERIODICIDADE))
  SET @JAN = LEFT(@P_JAN, charindex(';', @P_JAN)-1)  
  SET @P_JAN = SUBSTRING(@P_JAN, charindex(';', @P_JAN)+1, LEN(@P_JAN))
  SET @FEV = LEFT(@P_FEV, charindex(';', @P_FEV)-1)  
  SET @P_FEV = SUBSTRING(@P_FEV, charindex(';', @P_FEV)+1, LEN(@P_FEV))
  SET @MAR = LEFT(@P_MAR, charindex(';', @P_MAR)-1)  
  SET @P_MAR = SUBSTRING(@P_MAR, charindex(';', @P_MAR)+1, LEN(@P_MAR))
  SET @ABR = LEFT(@P_ABR, charindex(';', @P_ABR)-1)  
  SET @P_ABR = SUBSTRING(@P_ABR, charindex(';', @P_ABR)+1, LEN(@P_ABR))
  SET @MAI = LEFT(@P_MAI, charindex(';', @P_MAI)-1)  
  SET @P_MAI = SUBSTRING(@P_MAI, charindex(';', @P_MAI)+1, LEN(@P_MAI))
  SET @JUN = LEFT(@P_JUN, charindex(';', @P_JUN)-1)  
  SET @P_JUN = SUBSTRING(@P_JUN, charindex(';', @P_JUN)+1, LEN(@P_JUN))
  SET @JUL = LEFT(@P_JUL, charindex(';', @P_JUL)-1)  
  SET @P_JUL = SUBSTRING(@P_JUL, charindex(';', @P_JUL)+1, LEN(@P_JUL))
  SET @AGO = LEFT(@P_AGO, charindex(';', @P_AGO)-1)  
  SET @P_AGO = SUBSTRING(@P_AGO, charindex(';', @P_AGO)+1, LEN(@P_AGO))
  SET @SET = LEFT(@P_SET, charindex(';', @P_SET)-1)  
  SET @P_SET = SUBSTRING(@P_SET, charindex(';', @P_SET)+1, LEN(@P_SET))
  SET @OUT = LEFT(@P_OUT, charindex(';', @P_OUT)-1)  
  SET @P_OUT = SUBSTRING(@P_OUT, charindex(';', @P_OUT)+1, LEN(@P_OUT))
  SET @NOV = LEFT(@P_NOV, charindex(';', @P_NOV)-1)  
  SET @P_NOV = SUBSTRING(@P_NOV, charindex(';', @P_NOV)+1, LEN(@P_NOV))
  SET @DEZ = LEFT(@P_DEZ, charindex(';', @P_DEZ)-1)  
  SET @P_DEZ = SUBSTRING(@P_DEZ, charindex(';', @P_DEZ)+1, LEN(@P_DEZ))
  
  SELECT TOP 1 @DIVISAO = divisao, @LICENCIANTE = licenciante, @COLECAO = colecao, @CODPANINI = cod_panini  FROM PDFS (NOLOCK) 
  WHERE cod_pdfs = @CODPDFS AND ano = @P_ANO

	BEGIN TRY
	
		IF @CODPDFS <> '' BEGIN
		
	
			SET @CONTADOR = @CONTADOR + 1
		
			INSERT INTO PLAN_CABECALHO(cod_pdfs, descricao_pdfs, licenciante, divisao, fase, distribuicao, ano, tipo, cod_panini, edicao_jan, edicao_fev, edicao_mar, edicao_abr, edicao_mai, edicao_jun, edicao_jul, edicao_ago, edicao_set, edicao_out, edicao_nov, edicao_dez)
			VALUES(@CODPDFS, @TITULO, @LICENCIANTE, @DIVISAO, @FASE, @DISTR, @P_ANO, @P_TIPO, @CODPANINI, @JAN, @FEV, @MAR, @ABR, @MAI, @JUN, @JUL, @AGO, @SET, @OUT, @NOV, @DEZ)
			SELECT @CODIGO = @@identity
			
			IF @JAN <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 1, @JAN, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
			IF @FEV <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 2, @FEV, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
			IF @MAR <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 3, @MAR, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
			IF @ABR <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 4, @ABR, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
			IF @MAI <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 5, @MAI, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
			IF @JUN <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 6, @JUN, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
			IF @JUL <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 7, @JUL, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
			IF @AGO <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 8, @AGO, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
			IF @SET <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 9, @SET, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
			IF @OUT <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 10, @OUT, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
			IF @NOV <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 11, @NOV, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
			IF @DEZ <> '' BEGIN		
				INSERT INTO PLAN_ITENS(cod_plan, cod_pdfs, mes, edicao, preco, formato, colecao, qtde_paginas, periodicidade, categoria, acabamento, existe_brinde, brinde, imagem, assinaturas, shrink, capa, miolo, qtde_capa, qtde_miolo, binding, manuseio )
				VALUES(@CODIGO, @CODPDFS, 12, @DEZ, @PRECO, @FORMATO, @COLECAO, @PAGINAS, @PERIODICIDADE, @P_TIPO, NULL, 'false', null, NULL, 'false', null, NULL, NULL, @QTDECAPA, @QTDEMIOLO, @BINDING, NULL)
			END
		END
		
	END TRY
		
	BEGIN CATCH
		select
			 @errornumber = ERROR_NUMBER()
			,@errormessage = ERROR_MESSAGE()

		RAISERROR(@errornumber, 16, 1, @errormessage)
	END CATCH
 END  
 SET @QTDREGISTROS = @CONTADOR
END        
SET NOCOUNT OFF 

GO


