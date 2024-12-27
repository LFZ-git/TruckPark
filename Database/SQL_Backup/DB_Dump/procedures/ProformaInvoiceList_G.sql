SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:EXEC ProformaInvoiceList_G 2, 8
-- =============================================
CREATE PROCEDURE [dbo].[ProformaInvoiceList_G] 
@RoleId int=null,
@UDID int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@RoleId=1 OR @RoleId=4)
	BEGIN
	SELECT PFI.ProformaInvoiceId
      ,PFI.InvoiceNo
      ,PFI.OrganizationId
	  ,O.CompanyShortName AS OrganizationName
      ,PFI.TotalTruckCount
      ,PFI.TotalInvoiceAmount
      ,PFI.Invoicedate
	  ,PFI.InvoiceFilePath

	  FROM			ProformaInvoice PFI
	  INNER JOIN	Organization O		ON PFI.OrganizationId=O.OrganizationID AND O.IsActive=1
	  WHERE			PFI.IsActive=1
	END

	IF(@RoleId=2)
	BEGIN
		SELECT PFI.ProformaInvoiceId
      ,PFI.InvoiceNo
      ,PFI.OrganizationId
	  ,O.CompanyShortName AS OrganizationName
      ,PFI.TotalTruckCount
      ,PFI.TotalInvoiceAmount
      ,PFI.Invoicedate
	  ,PFI.InvoiceFilePath

	  FROM			ProformaInvoice PFI
	  INNER JOIN	Organization O		ON PFI.OrganizationId=O.OrganizationID	AND O.IsActive=1
	  INNER JOIN	UserDetails UD		ON PFI.OrganizationId=UD.OrganizationID AND UD.IsActive=1
	  WHERE			PFI.IsActive=1		AND UD.UDID=@UDID
	END

	IF(@RoleId=3)
	BEGIN
		DECLARE @USER TABLE(UDID INT)
		INSERT INTO @USER
		SELECT * FROM GetGroupUsers(@UDID)

		
		DECLARE @GROUP_ORG TABLE(ORGID INT)
		INSERT INTO @GROUP_ORG
		SELECT UD.OrganizationID
		FROM @USER U
		INNER JOIN UserDetails UD ON U.UDID=UD.UDID

		SELECT PFI.ProformaInvoiceId
      ,PFI.InvoiceNo
      ,PFI.OrganizationId
	  ,O.CompanyShortName AS OrganizationName
      ,PFI.TotalTruckCount
      ,PFI.TotalInvoiceAmount
      ,PFI.Invoicedate
	  ,PFI.InvoiceFilePath

	  FROM			ProformaInvoice PFI
	  INNER JOIN	Organization O		ON PFI.OrganizationId=O.OrganizationID AND O.IsActive=1
	  LEFT JOIN		@GROUP_ORG GRO		ON PFI.OrganizationId=GRO.ORGID
	  WHERE			PFI.IsActive=1		AND PFI.OrganizationId IN (GRO.ORGID)
	END
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
