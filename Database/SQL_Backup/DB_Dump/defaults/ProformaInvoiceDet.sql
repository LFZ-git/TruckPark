ALTER TABLE [dbo].[ProformaInvoiceDet] ADD CONSTRAINT [DF_ProformaInvoiceDet_Createddate] DEFAULT (dateadd(hour,(1),getutcdate())) FOR [Createddate]
GO
ALTER TABLE [dbo].[ProformaInvoiceDet] ADD CONSTRAINT [DF_ProformaInvoiceDet_Modifieddate] DEFAULT (dateadd(hour,(1),getutcdate())) FOR [Modifieddate]
GO
