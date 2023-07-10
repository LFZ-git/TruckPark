SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proformaInvoiceDetails_G] 
@proformainvoiceid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		SELECT PFD.ProformaInvoiceDetId
			  ,PFD.ProformaInvoiceId
			  ,PFD.TruckNo
			  ,PFD.TruckCapacityId
			  ,MTC.TruckCapacity
			  ,PFD.CheckedInDate
			  ,PFD.CheckedOutDate
			  ,PFD.InvoiceRate
			  ,PFD.InvoiceAmount
			  ,PFD.Invoicedate
			  ,PFD.Discount
		  FROM			ProformaInvoiceDet PFD
		  INNER JOIN	M_TruckCapacity MTC							ON PFD.TruckCapacityId=MTC.TruckCapacityId
		  WHERE			PFD.ProformaInvoiceId=@proformainvoiceid	AND PFD.IsActive=1
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
