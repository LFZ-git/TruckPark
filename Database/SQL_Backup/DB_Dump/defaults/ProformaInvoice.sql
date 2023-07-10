ALTER TABLE [dbo].[ProformaInvoice] ADD CONSTRAINT [DF_ProformaInvoice_Createddate] DEFAULT (dateadd(hour,(1),getutcdate())) FOR [Createddate]
GO
ALTER TABLE [dbo].[ProformaInvoice] ADD CONSTRAINT [DF_ProformaInvoice_Modifieddate] DEFAULT (dateadd(hour,(1),getutcdate())) FOR [Modifieddate]
GO
