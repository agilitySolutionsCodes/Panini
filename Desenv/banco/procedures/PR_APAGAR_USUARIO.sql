/****** Object:  StoredProcedure [dbo].[PR_APAGAR_USUARIO]    Script Date: 07/06/2012 10:25:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  23/05/2012      
-- Descrição:  Verifica se e-mail eh valido e retorna senha do usuario  
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================      

ALTER PROCEDURE [dbo].[PR_APAGAR_USUARIO]      
(     
 @P_CODUSUARIO INT,
 @RETORNO BIT = NULL OUTPUT,
 @MSGRETORNO VARCHAR(50) = NULL OUTPUT
)      
AS      
BEGIN      
SET NOCOUNT ON   

	SET @RETORNO = 'TRUE'
	SET @MSGRETORNO = ''
	
	DELETE FROM PERMISSOES_USUARIOS WHERE @P_CODUSUARIO = cod_usuario
	IF (SELECT COUNT(*) FROM PERMISSOES_USUARIOS WHERE cod_usuario = @P_CODUSUARIO) = 0 BEGIN
		DELETE FROM USUARIOS WHERE codigo =  @P_CODUSUARIO
	END
	ELSE BEGIN
		SET @RETORNO = 'FALSE'
		SET @MSGRETORNO = 'Não foi possível apagar o registro na tabela de PERMISSOES_USUARIOS'
	END
END      
SET NOCOUNT OFF 


GO


