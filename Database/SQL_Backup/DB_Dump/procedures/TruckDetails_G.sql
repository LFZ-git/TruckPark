SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TruckDetails_G] 
@TruckDetailsId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

			SELECT  TD.TruckDetailsId
			  ,TD.TruckId
			  ,TD.CalledByOrganizationId
			  ,TD.TruckCapacityId
			  ,TD.ExpectedArrivalDate
			  ,TD.ExpectedDepatureDate
			  ,TD.LocalTransferTypeId
			  ,MLV1.LOVName AS LocalTransferType
			  ,TD.TransportName
			  ,TD.TransportNo
			  ,TD.DriverName
			  ,TD.DriverNo
			  ,TD.MaterialTypeId
			  ,MLV2.LOVName AS MaterialType
			  ,TD.MaterialGoods
			  ,TD.ActualArrivalDate
			  ,TD.ActualDepatureDate
			  ,TD.IsForecasted
			  ,TD.IsCheckedIn
			  ,TD.IsCalledOut
			  ,TD.Createdby
			  ,TD.IsActive
			  ,T.TruckNo
			  ,MTC.TruckCapacity
			  ,O.CompanyShortName AS OwnedByOrganization 
			  ,TD.IsBilled
			  ,UR.RoleId 
		  FROM  [dbo].[TruckDetails] TD
		  INNER JOIN Truck T ON TD.TruckId=T.TruckId AND T.IsActive=1
		  INNER JOIN M_TruckCapacity MTC ON TD.TruckCapacityId=MTC.TruckCapacityId AND MTC.IsActive=1
		  INNER JOIN Organization O ON TD.CalledByOrganizationId=O.OrganizationID AND O.IsActive=1
		  LEFT JOIN M_ListOfValues MLV1 ON TD.LocalTransferTypeId=MLV1.LOVId AND MLV1.IsActive=1
		  LEFT JOIN M_ListOfValues MLV2 ON TD.MaterialTypeId=MLV2.LOVId AND MLV2.IsActive=1
		  INNER JOIN UserDetails UD ON TD.Createdby=UD.UDID AND UD.IsActive=1
		  INNER JOIN Map_UserRole UR ON UD.UDID=UR.UDID AND UR.IsActive=1
		  WHERE TD.IsActive=1 AND TD.TruckDetailsId=@TruckDetailsId 
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
