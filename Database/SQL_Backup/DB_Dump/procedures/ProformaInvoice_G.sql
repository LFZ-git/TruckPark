SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Modified SP by Rucha to get orgname, fromdate and todate
-- exec ProformaInvoice_G 4
-- =============================================
CREATE PROCEDURE [dbo].[ProformaInvoice_G]
@proformainvoiceid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT PFI.ProformaInvoiceId
      ,PFI.InvoiceNo
      ,PFI.OrganizationId
	  ,O.CompanyShortName AS OrganizationShortName
	  ,O.CompanyName AS OrganizationName
	  ,replace(O.CompanyAddress, ',', ', </br>')OrgnaizationAddress
	 -- ,O.CompanyAddress AS OrgnaizationAddress
      ,PFI.TotalTruckCount
      ,PFI.TotalInvoiceAmount
      ,PFI.Invoicedate
	  ,PFI.InvoiceFilePath
	  , (select cast( Min(pd.CheckedInDate) as date) from ProformaInvoicedet pd where pd.ProformaInvoiceId =@proformainvoiceid ) as FromDate
	  , (select cast( Max(pd.CheckedOutDate) as date) from ProformaInvoicedet pd where pd.ProformaInvoiceId =@proformainvoiceid ) as Todate
	  ,PFI.TotalDiscount

	FROM		ProformaInvoice PFI
		INNER JOIN	Organization O		ON PFI.OrganizationId=O.OrganizationID		 AND O.IsActive=1
			--inner join ProformaInvoicedet pd on pd.ProformaInvoiceId = PFI.ProformaInvoiceId
	WHERE		PFI.IsActive=1		AND PFI.ProformaInvoiceId=@proformainvoiceid
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
