ALTER TABLE [dbo].[Truck] ADD CONSTRAINT [DF_Truck_Createddate] DEFAULT (dateadd(hour,(1),getutcdate())) FOR [Createddate]
GO
ALTER TABLE [dbo].[Truck] ADD CONSTRAINT [DF_Truck_Modifieddate] DEFAULT (dateadd(hour,(1),getutcdate())) FOR [Modifieddate]
GO
