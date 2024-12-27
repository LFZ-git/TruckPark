SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	select [dbo].[GenerateInvoiceNumber] (2)
-- =============================================
CREATE FUNCTION [dbo].[GenerateInvoiceNumber]
(
	 @OrganizationId INT
)
RETURNS VARCHAR(50)
AS
BEGIN
	DECLARE @OrganizationName varchar(10),@invoiceNumber varchar(50),@invoiceCount varchar(10);
	DECLARE @PreviousInvoiceDate DATETIME

	SELECT @OrganizationName = CAST(CompanyShortName AS VARCHAR(10)) FROM Organization WHERE OrganizationID = @OrganizationId
	SELECT TOP 1 @PreviousInvoiceDate= Invoicedate FROM ProformaInvoice WHERE OrganizationId = @OrganizationId 
	ORDER BY Invoicedate DESC

	IF(MONTH(@PreviousInvoiceDate)<MONTH(DATEADD(hh,1,GETUTCDATE())))
	BEGIN
		SET @invoiceCount=1
	END
	ELSE
	BEGIN
		SELECT @invoiceCount = (COUNT(*)+1) FROM ProformaInvoice WHERE OrganizationId = @OrganizationId 
		AND MONTH(Invoicedate)=MONTH(DATEADD(hh,1,GETUTCDATE()))
	END

	SET @invoiceNumber = CONCAT('INV/',@OrganizationName,'/',year(getdate()),RIGHT('0' + RTRIM(MONTH(GETDATE())), 2),@invoiceCount);

		--SET @invoiceNumber = CONCAT('INV/',@OrganizationName,'/',year(getdate()),('05'),1);

	Return @invoiceNumber;
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
