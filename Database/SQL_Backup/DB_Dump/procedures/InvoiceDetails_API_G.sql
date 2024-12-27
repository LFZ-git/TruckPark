SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		ismail 
-- Create date: 25-02-2023   
-- modified date: 01-03-2023   
-- Description:  to get the invoice details with orion organization master integration
--  EXEC InvoiceDetails_API_G 1
-- =============================================
CREATE PROCEDURE [dbo].[InvoiceDetails_API_G] 
@ProformaInvoiceId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--IF(@RoleId=2)
	--BEGIN
	--	SELECT PFI.ProformaInvoiceId
 --     ,PFI.InvoiceNo as InvoiceReference,
 --     --,PFI.OrganizationId
	--  O.CompanyShortName AS CustomerCode,
 --     --,PFI.TotalTruckCount
 --     PFI.TotalInvoiceAmount as InvoiceAmount
 --    -- ,PFI.Invoicedate
	--  --,PFI.InvoiceFilePath

	--  FROM			ProformaInvoice PFI
	--  INNER JOIN	Organization O		ON PFI.OrganizationId=O.OrganizationID	AND O.IsActive=1
	-- -- INNER JOIN	UserDetails UD		ON PFI.OrganizationId=UD.OrganizationID AND UD.IsActive=1
	--  WHERE			PFI.IsActive=1	and PFI.ProformaInvoiceId=@ProformaInvoiceId	--AND UD.UDID=@UDID
	--END
	Begin

	SELECT PFI.ProformaInvoiceId
      ,PFI.InvoiceNo as InvoiceReference,
      --,PFI.OrganizationId
	 -- O.CompanyShortName AS CustomerCode,
	  mo.CompanyShortName as CustomerCode,
      --,PFI.TotalTruckCount
      PFI.TotalInvoiceAmount as InvoiceAmount
     -- ,PFI.Invoicedate
	  --,PFI.InvoiceFilePath

	  FROM			ProformaInvoice PFI
	  INNER JOIN	Organization O		ON PFI.OrganizationId=O.OrganizationID	AND O.IsActive=1
	  left outer join Map_Organization_Orion_Masters Mo on o.OrganizationID=mo.OrganizationIDMapped 
	 -- INNER JOIN	UserDetails UD		ON PFI.OrganizationId=UD.OrganizationID AND UD.IsActive=1
	  WHERE			PFI.IsActive=1	and PFI.ProformaInvoiceId=@ProformaInvoiceId
	END

	
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
