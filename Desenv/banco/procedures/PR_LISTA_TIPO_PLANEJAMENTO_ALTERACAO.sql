
-- =========================================================================      
-- Autor:   Yule Souza. - Agility Solutions      
-- Data Criacao:  31/05/2012     
-- Descrição:  Lista Tipo de Publicações Campo Canal Tabela PDFS
-- Número	   Data		    Usuário      Descrição
-- #001#	   28/06/2012	Yule Souza	 Primeira Versão
-- =========================================================================     
ALTER PROCEDURE [dbo].[PR_LISTA_TIPO_PLANEJAMENTO_ALTERACAO]  
(
  @P_Ano VARCHAR(4),
  @P_Divisao VARCHAR(30) = NULL
)
AS
BEGIN

	SET NOCOUNT ON;

    SELECT DISTINCT tipo FROM PLAN_CABECALHO
	WHERE 
	ano = @P_Ano 
	AND	(divisao = @P_Divisao OR @P_Divisao IS NULL)
	
	SET NOCOUNT OFF 
END







