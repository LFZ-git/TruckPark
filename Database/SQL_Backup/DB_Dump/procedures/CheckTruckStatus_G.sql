SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CheckTruckStatus_G]
@TruckNo VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		SELECT	T.TruckNo
			,	T.TruckCapacityId
			,	T.OwnedByOrganizationId
			,	T.IsActive
			,	TD.IsForecasted
			,	TD.IsCheckedIn 
		FROM		Truck T
		LEFT JOIN	TruckDetails TD		ON T.TruckId=TD.TruckId AND TD.ActualDepatureDate IS NULL AND TD.IsActive=1
		WHERE UPPER(T.TruckNo)			LIKE '%'+UPPER(@TruckNo)+ '%'	
		AND		TD.ActualDepatureDate IS NULL	AND TD.IsBilled=0
		ORDER BY TD.TruckDetailsId DESC
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
