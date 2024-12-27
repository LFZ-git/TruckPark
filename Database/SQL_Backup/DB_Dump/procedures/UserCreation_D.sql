SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Created By:	Ismail 
-- Create date: 05-06-2020
-- Description:	For DELETE user details
-- =============================================

--EXEC UserCreation_D 5,''
CREATE PROCEDURE [dbo].[UserCreation_D] 
     @UDID int,
	 --@UDID int,
	 --@RoleId int,
	 @OutError VARCHAR(50) OUTPUT
AS
BEGIN
	--IF EXISTS(select 1  from Map_UserRole where UDID=@UDID and RoleId=1 and IsActive=1 )

	UPDATE UserDetails 
	SET IsActive=0,
	UpdatedBy=1,
	UpdatedDate=GETDATE()
	WHERE UDID=@UDID

	SET @OutError='User deleted successfully'
	
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
