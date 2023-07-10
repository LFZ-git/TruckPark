ALTER TABLE [dbo].[TruckDetails] ADD CONSTRAINT [DF_TruckDetails_Createddate] DEFAULT (dateadd(hour,(1),getutcdate())) FOR [Createddate]
GO
ALTER TABLE [dbo].[TruckDetails] ADD CONSTRAINT [DF_TruckDetails_Modifieddate] DEFAULT (dateadd(hour,(1),getutcdate())) FOR [Modifieddate]
GO
