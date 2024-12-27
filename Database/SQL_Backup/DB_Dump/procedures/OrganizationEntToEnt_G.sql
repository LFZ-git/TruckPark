SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec OrganizationEntToEnt_G 3
-- =============================================
CREATE PROCEDURE [dbo].[OrganizationEntToEnt_G]
@UDID INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @CompanyId int

		select @CompanyId = ud.OrganizationID  from UserDetails ud where UDID =@UDID

		DECLARE @OrganizationT Table(OrganizationId int)

		INSERT INTO @OrganizationT
		select OrganizationID
			from Organization where IsActive=1 and OrganizationID = @CompanyId
		union all 
		select OrganizationID
			from Organization where IsActive=1 --and OrganizationID = @CompanyId
			and OrganizationID in (select EnterpriseId from  Map_Ent2Ent where GroupEntId= @CompanyId)
		--SELECT		UD1.OrganizationID 
		--FROM		UserDetails UD
		--INNER JOIN	Map_Ent2Ent MEE	ON UD.UDID=MEE.GroupEntId			AND MEE.IsActive=1
		--INNER JOIN	UserDetails UD1 ON MEE.EnterpriseId=UD1.UDID		AND UD1.IsActive=1
		--WHERE		UD.UDID=@UDID		AND UD.IsActive=1
		--UNION ALL
		--SELECT		OrganizationID
		--FROM		UserDetails 
		--WHERE		UDID=@UDID

		SELECT		O.OrganizationID
				  , O.CompanyName 
				  , O.CompanyShortName
				  , O.OrganizationCreatedDate
				  , O.IsActive
				  , O.OrganizationTypeId
				  , '' AS OrganizationType
		FROM		@OrganizationT OT
		INNER JOIN	Organization O	ON OT.OrganizationId=O.OrganizationID AND O.IsActive=1
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
