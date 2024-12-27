CREATE TABLE [dbo].[ProformaInvoiceB4Clean] (
   [ProformaInvoiceId] [bigint] NOT NULL
      IDENTITY (1,1),
   [InvoiceNo] [varchar](50) NOT NULL,
   [OrganizationId] [int] NOT NULL,
   [TotalTruckCount] [int] NULL,
   [TotalInvoiceAmount] [numeric](21,2) NULL,
   [TotalDiscount] [numeric](21,2) NULL,
   [Invoicedate] [datetime] NOT NULL,
   [InvoiceFilePath] [varchar](2000) NULL,
   [Createdby] [int] NOT NULL,
   [Createddate] [datetime] NOT NULL,
   [Modifiedby] [int] NULL,
   [Modifieddate] [datetime] NULL,
   [IsActive] [bit] NOT NULL
)


GO
