SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- EXEC OrganizationMailId_G '17219'
-- =============================================
CREATE PROCEDURE [dbo].[OrganizationMailId_G]
@TruckDetailsIdList VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	
	SELECT	
	
	 (SELECT EmailId from MGR_EmailDetails where OrganizationID=td.CalledByOrganizationId )AS EmailId
	,		T.TruckNo
	,		O.CompanyShortName
	,		TD.ActualArrivalDate
	,		TD.ActualDepatureDate AS ActualDepartureDate

	FROM		TruckDetails TD
	INNER JOIN	Truck T			ON TD.TruckId=T.TruckId							AND T.IsActive=1
	--INNER JOIN	UserDetails UD	ON TD.CalledByOrganizationId=UD.OrganizationID	AND UD.IsActive=1
	INNER JOIN	Organization O	ON TD.CalledByOrganizationId=O.OrganizationID			AND O.IsActive=1
	WHERE TD.TruckDetailsId IN (select ITEM from [dbo].[SPLIT](@TruckDetailsIdList,',')) 
	AND TD.IsActive=1
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
