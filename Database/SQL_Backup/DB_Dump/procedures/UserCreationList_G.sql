SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--exec UserCreationList_G 
CREATE procedure [dbo].[UserCreationList_G]
as

begin

			SELECT ud.UDID,ud.EmployeeName,ud.EmailId,ud.MobileNo,dpid.LOVName 'Department',rp.EmployeeName 'ReportingTo',ud.[Password],rm.RoleName 'Role'
			--rm.RoleName,dpid.LOVName 'Department',rprto.EmployeeName 'ReportingTo'
			from UserDetails ud
			INNER JOIN Map_UserRole mr 
				ON mr.UDID=ud.UDID
			inner join M_Role rm 
				on mr.RoleId = rm.RoleId
			inner join M_ListOfValues dpid 
				on ud.DepartmentID = dpid.LOVId and dpid.IsActive = 1
			left join UserDetails rp
				on rp.UDID = ud.ReportingToID
				where ud.IsActive=1
	   -- group by ud.UDID,ud.EmployeeName,ud.EmailId,ud.MobileNo,ud.DepartmentID,ud.ReportingToID,ud.[Password],ud.InitialPasswordReset,ud.OrganizationID,mr.RoleId
       order by ud.UDID desc

end
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
