CREATE TABLE [dbo].[TruckDetailsB4Cleanup] (
   [TruckDetailsId] [bigint] NOT NULL
      IDENTITY (1,1),
   [TruckId] [bigint] NOT NULL,
   [CalledByOrganizationId] [int] NOT NULL,
   [TruckCapacityId] [int] NOT NULL,
   [ExpectedArrivalDate] [datetime] NULL,
   [ExpectedDepatureDate] [datetime] NULL,
   [LocalTransferTypeId] [int] NOT NULL,
   [TransportName] [varchar](200) NULL,
   [TransportNo] [varchar](50) NULL,
   [DriverName] [varchar](200) NULL,
   [DriverNo] [varchar](50) NULL,
   [MaterialTypeId] [int] NULL,
   [MaterialGoods] [varchar](200) NULL,
   [ActualArrivalDate] [datetime] NULL,
   [ActualDepatureDate] [datetime] NULL,
   [IsForecasted] [bit] NOT NULL,
   [IsCheckedIn] [bit] NOT NULL,
   [IsCalledOut] [bit] NOT NULL,
   [IsBilled] [bit] NULL,
   [Createdby] [int] NOT NULL,
   [Createddate] [datetime] NOT NULL,
   [Modifiedby] [int] NULL,
   [Modifieddate] [datetime] NULL,
   [IsActive] [bit] NOT NULL
)


GO
