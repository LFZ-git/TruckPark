SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ProfomaInvoice_U] 
@InvoiceId int,
@path VARCHAR(500)=''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION;
		BEGIN TRY

			UPDATE ProformaInvoice
			SET InvoiceFilePath=@path
			WHERE ProformaInvoiceId=@InvoiceId

	COMMIT TRANSACTION;
		END TRY

		BEGIN CATCH
	ROLLBACK TRANSACTION
		END CATCH
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
