SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Flag  CHECKOUT=1,CALLEDOUT = 2, CHECKIN=3, DELETE=4
-- =============================================
CREATE PROCEDURE [dbo].[TruckStatus_U] 
@TruckDetailsIdList VARCHAR(MAX) = NULL,
@Flag TINYINT = NULL,
@UDID int,
@OutError VARCHAR(100)='' OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@Flag=1)
	BEGIN
		BEGIN TRAN
		BEGIN TRY
			UPDATE TruckDetails
			SET ActualDepatureDate=DATEADD(hh,1,GETUTCDATE()),
			Modifiedby=@UDID,
			Modifieddate=DATEADD(hh,1,GETUTCDATE())
			WHERE TruckDetailsId IN (select ITEM from [dbo].[SPLIT](@TruckDetailsIdList,','))

			set @OutError = 'Truck Checked Out successfully '
		COMMIT TRAN
		END TRY
		BEGIN CATCH
			IF @@TRANCOUNT > 0
			ROLLBACK
			set @OutError = 'There is some problem in delete'
		END CATCH
	END

	IF(@Flag=2)
	BEGIN
		BEGIN TRAN
		BEGIN TRY
			UPDATE TruckDetails
			SET IsCalledOut=1,
			ActualArrivalDate=DATEADD(hh,1,GETUTCDATE())
			WHERE TruckDetailsId IN (select ITEM from [dbo].[SPLIT](@TruckDetailsIdList,','))

			set @OutError = 'Truck Called Out successfully '
		COMMIT TRAN
		END TRY
		BEGIN CATCH
			IF @@TRANCOUNT > 0
			ROLLBACK
			set @OutError = 'There is some problem in delete'

		END CATCH

	END

	IF(@Flag=3)
	BEGIN
		BEGIN TRAN
		BEGIN TRY
			UPDATE TruckDetails
			SET IsCheckedIn=1,
			ActualArrivalDate=DATEADD(hh,1,GETUTCDATE()),
			Modifiedby=@UDID,
			Modifieddate=DATEADD(hh,1,GETUTCDATE())
			WHERE TruckDetailsId IN (select ITEM from [dbo].[SPLIT](@TruckDetailsIdList,','))

			set @OutError = 'Truck Checked In successfully '
		COMMIT TRAN
		END TRY
		BEGIN CATCH
			IF @@TRANCOUNT > 0
			ROLLBACK
			set @OutError = 'There is some problem in delete'
		END CATCH
	END

	IF(@Flag=4)
	BEGIN
		BEGIN TRAN
		BEGIN TRY
			UPDATE TruckDetails
			SET IsActive=0
			WHERE TruckDetailsId IN (select ITEM from [dbo].[SPLIT](@TruckDetailsIdList,','))

			set @OutError = 'Truck details deleted successfully '
		COMMIT TRAN
		END TRY
		BEGIN CATCH
			IF @@TRANCOUNT > 0
			ROLLBACK
			set @OutError = 'There is some problem in delete'

		END CATCH
	END
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
