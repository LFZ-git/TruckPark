SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--exec UserLogin 'admin@lftz.com','1234'
CREATE PROCEDURE [dbo].[CheckUser]
 
    @EmailId VARCHAR(100)
	--,
	 --@password VARCHAR(50) null,
	 --@OutError VARCHAR(50)='' OUTPUT

AS
BEGIN

	SET NOCOUNT ON

	IF EXISTS(SELECT EmailId FROM UserDetails WHERE EmailId = @EmailId AND IsActive=1)
	BEGIN
	   SELECT Password,saltKey FROM UserDetails WHERE EmailId = @EmailId 	   	   
	END
	
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
