SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- exec GetMappingModulesByRoleId 1
create PROCEDURE [dbo].[GetMappingModulesByRoleId]
    -- Add the parameters for the stored procedure here
   @Roleid int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SELECT a.roleid,c.rolename,a.moduleid,b.ModuleName , a.isactive
	FROM map_rolemodule a
	INNER JOIN m_modules b 
		ON a.moduleid =b.ModuleId
			INNER JOIN m_role c
				ON c.roleid = a.roleid
	WHERE a.roleid =@Roleid
    
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
