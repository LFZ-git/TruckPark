CREATE TABLE [dbo].[Truck] (
   [TruckId] [bigint] NOT NULL
      IDENTITY (1,1),
   [TruckNo] [varchar](50) NOT NULL,
   [OwnedByOrganizationId] [int] NOT NULL,
   [TruckCapacityId] [int] NOT NULL,
   [Createdby] [int] NOT NULL,
   [Createddate] [datetime] NOT NULL,
   [Modifiedby] [int] NULL,
   [Modifieddate] [datetime] NULL,
   [IsActive] [bit] NOT NULL

   ,CONSTRAINT [PK_Truck] PRIMARY KEY CLUSTERED ([TruckId])
)


GO
