

-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  28/02/2012      
-- Descrição:  Alteração de Planejamento. Pode ser Revista, Livro ou Colecionaveis
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  24/02/2012  Juliana	Em Revista as edições sao gravadas
-- #002#  24/02/2012  Juliana	Em Livro as edições não existem
-- #003#  24/02/2012  Juliana	Em colecionaveis as edições não existem e existe uma tabela nova apenas com os dados especificos dos colecionaveis
-- =========================================================================      

ALTER PROCEDURE [dbo].[PR_ALTERAR_PLANEJAMENTO]      
(
@P_CODPLAN INT,  
@P_ASSINATURAS	BIT,	    
@P_FASE	VARCHAR(3),
@P_DISTRIBUICAO	VARCHAR(15),
@P_PRECO MONEY = null,
@P_PRECOPAGINA MONEY = NULL,
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
@P_CAPA	VARCHAR(10),
@P_MIOLO	VARCHAR(10),
@P_QTDE_CAPA	INTEGER = null,	
@P_QTDE_MIOLO	INTEGER = null,	
@P_EDICAO	VARCHAR	(7),
@P_BINDING VARCHAR(15),
@P_MANUSEIO VARCHAR(15),
@P_CODFORNECEDOR INT = null,
@P_CUSTO MONEY = null,
@P_CODPDFS_ALBUM VARCHAR(20) = null,
@P_CODPDFS_ENV VARCHAR(20) = null,
@P_CODPANINI_ALBUM VARCHAR(15) = null,
@P_CODPANINI_ENV VARCHAR(15) = null, 
@P_FORMATO_CROMO VARCHAR(15) = null,
@P_FORMATO_ENVELOPE VARCHAR(15) = null,
@P_QTDE_CROMO_NORMAL INT = null,
@P_QTDE_CROMO_ESPECIAL	INT = null,
@P_TOTAL_CROMOS INT = null,
@P_EDITORIAL VARCHAR(1) = null,
@P_ENV_MERCOSUL BIT = null,	
@P_PAIS VARCHAR(30) = null,
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
@P_DT_LCTO DATETIME = null
)     

AS      
BEGIN      
SET NOCOUNT ON   

	DECLARE @errornumber int,@errormessage varchar(max),
	@DATA_LCTO DATETIME, @QUERY AS VARCHAR(100), @MES INT

	BEGIN TRY
	
		UPDATE PLAN_CABECALHO SET fase = @P_FASE, distribuicao = @P_DISTRIBUICAO, cod_fornecedor = @P_CODFORNECEDOR,
		cod_pdfs_album = @P_CODPDFS_ALBUM, cod_pdfs_envelope = @P_CODPDFS_ENV, cod_panini_album = @P_CODPANINI_ALBUM, 
		cod_panini_envelope = @P_CODPANINI_ENV  
		WHERE codigo = @P_CODPLAN
			
		UPDATE PLAN_ITENS SET preco = @P_PRECO, preco_pagina = @P_PRECOPAGINA,  custo = @P_CUSTO, formato = @P_FORMATO, colecao = @P_COLECAO, 
		qtde_paginas = @P_QTDEPAGINAS, periodicidade = @P_PERIODICIDADE, categoria = @P_CATEGORIA, 
		acabamento = @P_ACABAMENTO, existe_brinde = @P_EXISTE_BRINDE, brinde = @P_BRINDE, imagem = @P_IMAGEM, 
		assinaturas = @P_ASSINATURAS, shrink = @P_SHRINK, capa = @P_CAPA, miolo = @P_MIOLO, qtde_capa = @P_QTDE_CAPA, 
		qtde_miolo = @P_QTDE_MIOLO, binding = @P_BINDING, manuseio = @P_MANUSEIO
		WHERE cod_plan = @P_CODPLAN AND edicao = @P_EDICAO
			
		IF @P_TIPO = 'C' BEGIN
			UPDATE PLAN_COLECIONAVEIS SET formato_cromo = @P_FORMATO_CROMO, formato_envelope = @P_FORMATO_ENVELOPE, 
			qtde_cromo_normal = @P_QTDE_CROMO_NORMAL, qtde_cromo_especial = @P_QTDE_CROMO_ESPECIAL, total_cromos = @P_TOTAL_CROMOS, 
			editorial = @P_EDITORIAL, env_mercosul = @P_ENV_MERCOSUL, pais = @P_PAIS, li_formato = @P_LI_FORMATO, 
			li_qtde_pag = @P_LI_QTDE_PAG, li_capa_venda = @P_LI_CAPA_VENDA, li_capa_cortesia = @P_LI_CAPA_CORTESIA, 
			li_papel_miolo = @P_LI_PAPEL_MIOLO, li_encarte_col = @P_LI_ENCARTE_COL, li_encarte_esp = @P_LI_ENCARTE_ESP, 
			li_poster = @P_LI_POSTER, preco_cromo = @P_PRECO_CROMO, preco_envelope = @P_PRECO_ENV, cromo_por_env = @P_CROMO_POR_ENV, 
			env_por_pacote = @P_ENV_POR_PACOTE, env_por_caixa = @P_ENV_POR_CAIXA, qtde_album_pacote = @P_QTDE_ALBUM_PACOTE
			WHERE cod_plan = @P_CODPLAN 
			
			SELECT @DATA_LCTO = data_lcto FROM PLAN_CABECALHO WHERE codigo = @P_CODPLAN
			IF @DATA_LCTO <> @P_DT_LCTO AND @P_DT_LCTO IS NOT NULL BEGIN
				--PERMITIR ALTERAR DATA DE LANCAMENTO
				SET @MES = MONTH(@P_DT_LCTO)
				UPDATE PLAN_CABECALHO SET edicao_jan = '', edicao_fev = '', edicao_mar = '', edicao_abr = '', edicao_mai = '', 
				edicao_jun = '', edicao_jul = '', edicao_ago = '', edicao_set = '', edicao_out = '', edicao_nov = '', 
				edicao_dez = '', data_lcto = @P_DT_LCTO WHERE codigo = @P_CODPLAN
				
				SET @QUERY = 'UPDATE PLAN_CABECALHO SET '
				IF @MES = 1 BEGIN
					SET @QUERY = @QUERY + ' edicao_jan = ''001'''
				END
				ELSE IF @MES = 2 BEGIN
					SET @QUERY = @QUERY +  ' edicao_fev = ''001'''
				END
				ELSE IF @MES = 3 BEGIN
					SET @QUERY = @QUERY +  ' edicao_mar = ''001'''
				END
				ELSE IF @MES = 4 BEGIN
					SET @QUERY = @QUERY +  ' edicao_abr = ''001'''
				END
				ELSE IF @MES = 5 BEGIN
					SET @QUERY = @QUERY +  ' edicao_mai = ''001'''
				END
				ELSE IF @MES = 6 BEGIN
					SET @QUERY = @QUERY +  ' edicao_jun = ''001'''
				END
				ELSE IF @MES = 7 BEGIN
					SET @QUERY = @QUERY +  ' edicao_jul = ''001'''
				END
				ELSE IF @MES = 8 BEGIN
					SET @QUERY = @QUERY +  ' edicao_ago = ''001'''
				END
				ELSE IF @MES = 9 BEGIN
					SET @QUERY = @QUERY +  ' edicao_set = ''001'''
				END
				ELSE IF @MES = 10 BEGIN
					SET @QUERY = @QUERY +  ' edicao_out = ''001'''
				END
				ELSE IF @MES = 11 BEGIN
					SET @QUERY = @QUERY +  ' edicao_nov = ''001'''
				END
				ELSE IF @MES = 12 BEGIN
					SET @QUERY = @QUERY +  ' edicao_dez = ''001'''
				END
				
				SET @QUERY = @QUERY +  ' WHERE codigo = ' + Convert(varchar(10),@P_CODPLAN)
				EXEC(@QUERY)
				UPDATE PLAN_ITENS SET mes = @MES WHERE cod_plan = @P_CODPLAN
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
SET NOCOUNT OFF 


GO


