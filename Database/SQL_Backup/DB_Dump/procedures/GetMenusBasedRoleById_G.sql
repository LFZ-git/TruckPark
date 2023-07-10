SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Created By:	
-- Create date: 
-- Description:	
-- =============================================

--EXEC GetMenusBasedRoleById_G 1
CREATE PROCEDURE [dbo].[GetMenusBasedRoleById_G] 
    @udid INT
AS
BEGIN

    Select distinct m.ModuleId,m.ModuleName,isnull(m.Parentid,0) as Parentid,m.ControllerName,m.ActionName,isnull(m.DisplayOrder,0) as DisplayOrder,m.IconImage,m.IsActive
	, STUFF((SELECT ', ' + convert(varchar,rm.RoleId) 
        FROM M_Modules mr1
		 join  Map_RoleModule rm on rm.ModuleId =m.ModuleId
		 WHERE mr1.ModuleId = m.ModuleId
        FOR XML PATH('')), 1, 1, '') RoleIds

		from  M_Modules m
			join Map_RoleModule rm on rm.ModuleId =m.ModuleId
		where m.IsActive =1 and rm.RoleId in 
		(select r.roleid from M_Role r
			 join Map_UserRole ur on ur.RoleId =r.RoleId
		  where ur.UDID =@udid)

		  order by DisplayOrder
	--END

END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
