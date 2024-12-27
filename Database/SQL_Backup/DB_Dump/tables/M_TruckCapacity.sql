CREATE TABLE [dbo].[M_TruckCapacity] (
   [TruckCapacityId] [int] NOT NULL
      IDENTITY (1,1),
   [TruckCapacity] [varchar](10) NOT NULL,
   [Createdby] [int] NOT NULL,
   [Createddate] [datetime] NOT NULL,
   [Modifiedby] [int] NULL,
   [Modifieddate] [datetime] NULL,
   [IsActive] [bit] NOT NULL

   ,CONSTRAINT [PK_M_TruckCapacity] PRIMARY KEY CLUSTERED ([TruckCapacityId])
)


GO
