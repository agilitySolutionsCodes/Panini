


-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  31/01/2012      
-- Descrição:  Incluir Planejamento. Pode ser Revista, Livro ou Colecionaveis
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  24/02/2012  Juliana	Em Revista as edições sao gravadas
-- #002#  24/02/2012  Juliana	Em Livro as edições não existem
-- #003#  24/02/2012  Juliana	Em colecionaveis as edições não existem e existe uma tabela nova apenas com os dados especificos dos colecionaveis
-- =========================================================================      

ALTER PROCEDURE [dbo].[PR_INCLUIR_PLANEJAMENTO]      
(     
@P_CODPDFS	VARCHAR(20),
@P_DESCPDFS	VARCHAR(100),
@P_ASSINATURAS	BIT,	
@P_LICENCIANTE	VARCHAR(50),
@P_FASE	VARCHAR(3),
@P_DISTRIBUICAO	VARCHAR(15),
@P_PRECO MONEY = null,	
@P_PRECOPAGINA MONEY = NULL,
@P_CUSTO MONEY = null,
@P_FORMATO	VARCHAR(10),
@P_COLECAO	VARCHAR(7),
@P_QTDEPAGINAS	VARCHAR(10),
@P_PERIODICIDADE	VARCHAR(1),
@P_TIPO	VARCHAR(1),
@P_CATEGORIA	VARCHAR(30),
@P_ACABAMENTO	VARCHAR(70),
@P_EXISTE_BRINDE	BIT,	
@P_BRINDE	VARCHAR(30),
@P_IMAGEM	VARCHAR(100),
@P_SHRINK	VARCHAR(80),
@P_CAPA	VARCHAR(100),
@P_MIOLO	VARCHAR(80),
@P_QTDE_CAPA	INTEGER = null,	
@P_QTDE_MIOLO	INTEGER = null,	
@P_EDICAO	VARCHAR	(70),
@P_ANO	VARCHAR(4), 
@P_BINDING VARCHAR(15),
@P_MANUSEIO VARCHAR(90),
@P_FORMATO_CROMO VARCHAR(15) = null,
@P_FORMATO_ENVELOPE VARCHAR(15) = null,
@P_QTDE_CROMO_NORMAL INT = null,
@P_QTDE_CROMO_ESPECIAL	INT = null,
@P_TOTAL_CROMOS INT = null,
@P_EDITORIAL VARCHAR(1) = null,
@P_ENV_MERCOSUL BIT = null,	
@P_PAIS VARCHAR(40) = null,
@P_LI_FORMATO VARCHAR(15) = null,
@P_LI_QTDE_PAG VARCHAR(15) = null,
@P_LI_CAPA_VENDA VARCHAR(20) = null,
@P_LI_CAPA_CORTESIA VARCHAR(20) = null,
@P_LI_PAPEL_MIOLO VARCHAR(20) = null,
@P_LI_ENCARTE_COL BIT = null,
@P_LI_ENCARTE_ESP BIT = null,
@P_LI_POSTER BIT = null,
@P_PRECO_CROMO MONEY = null,
@P_PRECO_ENV MONEY = null,
@P_CROMO_POR_ENV INT = null,
@P_ENV_POR_PACOTE INT = null,
@P_ENV_POR_CAIXA INT = null,
@P_QTDE_ALBUM_PACOTE INT = null,
@P_DT_LCTO DATETIME = null,
@P_CODPANINI VARCHAR(15) = null,
@P_CODFORNECEDOR INT = null,
@P_CODPDFS_ALBUM VARCHAR(20) = null,
@P_CODPDFS_ENV VARCHAR(20) = null,
@P_CODPANINI_ALBUM VARCHAR(15) = null,
@P_CODPANINI_ENV VARCHAR(15) = null
)     

AS      
BEGIN      
SET NOCOUNT ON   

	DECLARE @errornumber int,@errormessage varchar(max)
	DECLARE @EDICAO VARCHAR(3)
	DECLARE @DIVISAO VARCHAR(30)
	DECLARE @CODIGO INTEGER
	DECLARE @MES INTEGER
	DECLARE @EDICOES VARCHAR(70)
	DECLARE @JAN VARCHAR(7), @FEV VARCHAR(7), @MAR VARCHAR(7), @ABR VARCHAR(7), @MAI VARCHAR(7), 
	@JUN VARCHAR(7), @JUL VARCHAR(7), @AGO VARCHAR(7), @SET VARCHAR(7), @OUT VARCHAR(7), @NOV VARCHAR(7), @DEZ VARCHAR(7)
	SET @MES = 1
	SET @EDICOES = @P_EDICAO
	
	--SEPARAR OS MESES
	SET @JAN = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	SET @FEV = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	SET @MAR = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	SET @ABR = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	SET @MAI = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	SET @JUN = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	SET @JUL = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	SET @AGO = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	SET @SET = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	SET @OUT = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	SET @NOV = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	SET @DEZ = LEFT(@EDICOES, charindex(';', @EDICOES)-1)
	SET @EDICOES = SUBSTRING(@EDICOES, charindex(';', @EDICOES)+1, LEN(@EDICOES))
	
	SELECT @DIVISAO = divisao FROM PDFS NOLOCK WHERE cod_pdfs = @P_CODPDFS

	BEGIN TRY
	
		IF @P_TIPO = 'L' BEGIN
			SET @MES = 1
			SET @JAN = '001'
		END
		ELSE IF @P_TIPO = 'C' BEGIN
			SET @MES = MONTH(@P_DT_LCTO)
		END
	
		INSERT INTO PLAN_CABECALHO
		(cod_pdfs, 
		descricao_pdfs, 
		licenciante, 
		divisao, 
		fase, 
		distribuicao, 
		ano, 
		tipo, 
		data_lcto, 
		cod_panini, 
		cod_fornecedor, 
		edicao_jan, 
		edicao_fev, 
		edicao_mar, 
		edicao_abr, 
		edicao_mai, 
		edicao_jun, 
		edicao_jul, 
		edicao_ago, 
		edicao_set, 
		edicao_out, 
		edicao_nov, 
		edicao_dez, 
		cod_pdfs_album, 
		cod_pdfs_envelope, 
		cod_panini_album, 
		cod_panini_envelope)
		
		VALUES
		(@P_CODPDFS, 
		@P_DESCPDFS, 
		@P_LICENCIANTE, 
		@DIVISAO, 
		@P_FASE, 
		@P_DISTRIBUICAO, 
		@P_ANO, 
		@P_TIPO, 
		@P_DT_LCTO, 
		@P_CODPANINI, 
		@P_CODFORNECEDOR, 
		@JAN, 
		@FEV, 
		@MAR, 
		@ABR, 
		@MAI, 
		@JUN, 
		@JUL, 
		@AGO, 
		@SET, 
		@OUT, 
		@NOV, 
		@DEZ, 
		@P_CODPDFS_ALBUM, 
		@P_CODPDFS_ENV, 
		@P_CODPANINI_ALBUM, 
		@P_CODPANINI_ENV)
		
		SELECT @CODIGO = @@identity
		
		IF @P_TIPO = 'R' BEGIN
		
			WHILE charindex(';', @P_EDICAO) > 0 BEGIN
			
				SET @EDICAO = LEFT(@P_EDICAO, charindex(';', @P_EDICAO)-1)
				SET @P_EDICAO = SUBSTRING(@P_EDICAO, charindex(';', @P_EDICAO)+1, LEN(@P_EDICAO))
				
				--APENAS INCLUI O ITEM NA TABELA SE A EDICAO ESTIVER PREENCHIDA
				IF @EDICAO <> '' BEGIN
					
					INSERT INTO PLAN_ITENS
					(cod_plan, 
					 cod_pdfs, 
					 mes, 
					 edicao, 
					 preco,
					 preco_pagina, 
					 custo, 
					 formato, 
					 colecao, 
					 qtde_paginas, 
					 periodicidade, 
					 categoria, 
					 acabamento, 
					 existe_brinde, 
					 brinde, 
					 imagem, 
					 assinaturas, 
					 shrink, 
					 capa, 
					 miolo, 
					 qtde_capa, 
					 qtde_miolo, 
					 binding, manuseio)
					 
					VALUES
					(@CODIGO, 
					@P_CODPDFS, 
					@MES, 
					@EDICAO, 
					@P_PRECO, 
					@P_PRECOPAGINA,
					@P_CUSTO, 
					@P_FORMATO, 
					@P_COLECAO, 
					@P_QTDEPAGINAS, 
					@P_PERIODICIDADE, 
					@P_CATEGORIA, 
					@P_ACABAMENTO, 
					@P_EXISTE_BRINDE, 
					@P_BRINDE, 
					@P_IMAGEM, 
					@P_ASSINATURAS, 
					@P_SHRINK, 
					@P_CAPA, 
					@P_MIOLO, 
					@P_QTDE_CAPA, 
					@P_QTDE_MIOLO, 
					@P_BINDING, 
					@P_MANUSEIO)
				END
				
				SET @MES = @MES + 1
			END
		END
		
		ELSE BEGIN
			INSERT INTO PLAN_ITENS
			(cod_plan, 
			 cod_pdfs, 
			 mes, 
			 edicao, 
			 preco,
			 preco_pagina, 
			 custo, 
			 formato, 
			 colecao, 
			 qtde_paginas, 
			 periodicidade, 
			 categoria, 
			 acabamento, 
			 existe_brinde, 
			 brinde, 
			 imagem, 
			 /*assinaturas,*/ 
			 shrink, 
			 capa, 
			 miolo, 
			 qtde_capa, 
			 qtde_miolo, 
			 binding, manuseio )
			
			VALUES
			(@CODIGO, 
			 @P_CODPDFS, 
			 @MES, 
			 '001', 
			 @P_PRECO, 
			 @P_PRECOPAGINA,
			 @P_CUSTO, 
			 @P_FORMATO, 
			 @P_COLECAO, 
			 @P_QTDEPAGINAS, 
			 @P_PERIODICIDADE, 
			 @P_CATEGORIA, 
			 @P_ACABAMENTO, 
			 @P_EXISTE_BRINDE, 
			 @P_BRINDE, 
			 @P_IMAGEM, 
			 /*@P_ASSINATURAS,*/ 
			 @P_SHRINK, 
			 @P_CAPA, 
			 @P_MIOLO, 
			 @P_QTDE_CAPA, 
			 @P_QTDE_MIOLO, 
			 @P_BINDING, 
			 @P_MANUSEIO)
			
			IF @P_TIPO = 'C' BEGIN
				INSERT INTO PLAN_COLECIONAVEIS
				(cod_plan, 
				 formato_cromo, 
				 formato_envelope, 
				 qtde_cromo_normal, 
				 qtde_cromo_especial, 
				 total_cromos, 
				 editorial, 
				 env_mercosul, 
				 pais, 
				 li_formato, 
				 li_qtde_pag, 
				 li_capa_venda, 
				 li_capa_cortesia, 
				 li_papel_miolo, 
				 li_encarte_col, 
				 li_encarte_esp, 
				 li_poster,
				 preco_cromo, 
				 preco_envelope, 
				 cromo_por_env, 
				 env_por_pacote, 
				 env_por_caixa, 
				 qtde_album_pacote)
				
				VALUES
				(@CODIGO, 
				 @P_FORMATO_CROMO, 
				 @P_FORMATO_ENVELOPE, 
				 @P_QTDE_CROMO_NORMAL, 
				 @P_QTDE_CROMO_ESPECIAL, 
				 @P_TOTAL_CROMOS, 
				 @P_EDITORIAL, 
				 @P_ENV_MERCOSUL, 
				 @P_PAIS, 
				 @P_LI_FORMATO, 
				 @P_LI_QTDE_PAG, 
				 @P_LI_CAPA_VENDA, 
				 @P_LI_CAPA_CORTESIA, 
				 @P_LI_PAPEL_MIOLO, 
				 @P_LI_ENCARTE_COL, 
				 @P_LI_ENCARTE_ESP, 
				 @P_LI_POSTER,
				 @P_PRECO_CROMO, 
				 @P_PRECO_ENV, 
				 @P_CROMO_POR_ENV, 
				 @P_ENV_POR_PACOTE, 
				 @P_ENV_POR_CAIXA, 
				 @P_QTDE_ALBUM_PACOTE)
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


