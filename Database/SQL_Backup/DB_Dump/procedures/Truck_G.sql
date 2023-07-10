SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
--exec Truck_G '85'
-- =============================================
CREATE PROCEDURE [dbo].[Truck_G] 
@TruckNo varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SELECT [TruckId]
			  ,[TruckNo]
			  ,[OwnedByOrganizationId]
			  ,[TruckCapacityId]
			  ,[IsActive]
		  FROM [dbo].[Truck]
		  WHERE TruckNo like '%'+@TruckNo+ '%' AND 
		  IsActive=1
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
