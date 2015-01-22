/****** Object:  StoredProcedure [dbo].[PR_AUTENTICAR_USUARIO]    Script Date: 07/06/2012 10:26:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  18/01/2012      
-- Descrição:  Autenticar login do usuario   
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================  
    
ALTER PROCEDURE [dbo].[PR_AUTENTICAR_USUARIO]      
(     
 @P_EMAIL VARCHAR(40),
 @P_SENHA VARCHAR(15),
 @OK BIT OUTPUT,
 @RETORNO VARCHAR(20) OUTPUT,
 @CODUSUARIO INTEGER OUTPUT,
 @NOMEUSUARIO VARCHAR(80) OUTPUT
)      
AS      
BEGIN      
SET NOCOUNT ON    

	DECLARE @SENHA VARCHAR(15)

	SET @CODUSUARIO = 0
	SET @RETORNO = ''
	SET @NOMEUSUARIO = ''
	
	IF(SELECT COUNT(*) FROM USUARIOS (NOLOCK) WHERE email = @P_EMAIL) > 0 BEGIN
		SELECT @SENHA = senha, @CODUSUARIO = codigo, @NOMEUSUARIO = nome FROM USUARIOS (NOLOCK) WHERE email = @P_EMAIL
		IF @SENHA = @P_SENHA BEGIN
			SET @OK = 'TRUE'
		END
		ELSE BEGIN
			SET @OK = 'FALSE'
			SET @RETORNO = 'Senha inválida.' 
		END
	END
	ELSE
	BEGIN
		SET @OK = 'FALSE'
		SET @RETORNO = 'Usuário inválido.' 
	END
END      
SET NOCOUNT OFF 

GO


