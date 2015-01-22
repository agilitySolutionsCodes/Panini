/****** Object:  StoredProcedure [dbo].[PR_AUTENTICAR_USUARIO_FORNECEDOR]    Script Date: 07/06/2012 10:26:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =========================================================================      
-- Autor:   Yule Souza. - Agility Solutions      
-- Data Criacao:  02/07/2012      
-- Descrição:  Lista Usuario Fornecedor   
-- Número Data        Usuário   Descrição      
-- #001#  
-- =========================================================================  
ALTER PROCEDURE [dbo].[PR_AUTENTICAR_USUARIO_FORNECEDOR] 

@P_CODUSUARIO INT, 
@F_EMAIL VARCHAR(50)= NULL OUTPUT

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT DISTINCT 
	
	@F_EMAIL = F.email
	
	FROM USUARIOS U
	INNER JOIN FORNECEDORES F	
	ON U.email = F.email
	WHERE U.codigo = @P_CODUSUARIO
END





GO


