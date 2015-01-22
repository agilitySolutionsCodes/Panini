/****** Object:  StoredProcedure [dbo].[PR_REDIRECIONA_USUARIO]    Script Date: 07/06/2012 10:39:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Yule Souza. - Agility Solutions      
-- Data Criacao:  31/05/2012     
-- Descrição:  Verifica permissões de tela de acesso na tabela Permissoes_Usuarios
--			   e retorna o nome da tela e o codigo para redirecionamento do respectivo usuário
-- Número	   Data		    Usuário      Descrição
-- #001#	   15/06/2012	Yule Souza	 Primeira Versão
-- =========================================================================  

ALTER PROCEDURE [dbo].[PR_REDIRECIONA_USUARIO]
 
	@P_CodUsuario INT,
	@P_CodTela INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

    -- Lista o Código da Tela de acesso de acordo com o parametro @P_CodUsuario

	SELECT TOP 1
	
	@P_CodTela = PU.cod_tela
	
	FROM  PERMISSOES_USUARIOS PU
	INNER JOIN TELAS_PERMISSOES TP (NOLOCK)
	ON TP.codigo = PU.cod_tela 
	
	WHERE 
	PU.cod_usuario = @P_CodUsuario AND 
	PU.permissao != '0;0;0;0;0;0;0;0;0'
	ORDER BY TP.codigo
	
	END



GO


