CREATE TABLE [dbo].[ProformaInvoiceDet] (
   [ProformaInvoiceDetId] [bigint] NOT NULL
      IDENTITY (1,1),
   [ProformaInvoiceId] [bigint] NOT NULL,
   [TruckNo] [varchar](50) NOT NULL,
   [TruckCapacityId] [int] NOT NULL,
   [CheckedInDate] [datetime] NOT NULL,
   [CheckedOutDate] [datetime] NOT NULL,
   [InvoiceRate] [numeric](21,2) NOT NULL,
   [InvoiceAmount] [numeric](21,2) NOT NULL,
   [Invoicedate] [datetime] NOT NULL,
   [Discount] [numeric](21,2) NULL,
   [Createdby] [int] NOT NULL,
   [Createddate] [datetime] NOT NULL,
   [Modifiedby] [int] NULL,
   [Modifieddate] [datetime] NULL,
   [IsActive] [bit] NOT NULL

   ,CONSTRAINT [PK_ProformaInvoiceDet] PRIMARY KEY CLUSTERED ([ProformaInvoiceDetId])
)


GO
