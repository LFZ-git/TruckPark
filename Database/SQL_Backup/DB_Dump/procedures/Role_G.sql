SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Created By:	Ismail B
-- Create date: 23-12-2019
-- Description:	For Retreiving Role mater data for Dropdown or any other use
-- =============================================
-- EXEC Role_G 
CREATE PROCEDURE [dbo].[Role_G]    
AS
BEGIN
	
	SELECT r.RoleId,r.RoleName
		from M_Role r	
			where r.IsActive=1 
				order by RoleName

END


/****** Object:  StoredProcedure [dbo].[Role_D]    Script Date: 30-10-2018 15:16:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
