CREATE TABLE [dbo].[TAB_Base_Station]
(
	[StationName] VARCHAR(50) NOT NULL , 
    [NameJP] VARCHAR(50) NULL, 
    [StationNumber] VARCHAR(50) NULL, 
    CONSTRAINT [PK_TAB_Base_Station] PRIMARY KEY ([StationName]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'车站名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_Station',
    @level2type = N'COLUMN',
    @level2name = N'StationName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'名称简拼',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_Station',
    @level2type = N'COLUMN',
    @level2name = N'NameJP'
GO

GO

GO

GO


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'运行记录编号列表(1-101,100001,1-102,10002)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_Station',
    @level2type = N'COLUMN',
    @level2name = 'StationNumber'
GO
