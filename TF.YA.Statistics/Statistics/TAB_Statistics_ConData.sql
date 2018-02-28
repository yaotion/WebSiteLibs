CREATE TABLE [dbo].[TAB_Statistics_ConData]
(
	[SortID] VARCHAR(50) NOT NULL , 
    [ItemID] VARCHAR(50) NOT NULL, 
    [ItemName] VARCHAR(50) NULL, 
    [ItemData] VARCHAR(500) NULL, 
    PRIMARY KEY ([ItemID], [SortID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分类编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_ConData',
    @level2type = N'COLUMN',
    @level2name = N'SortID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'项编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_ConData',
    @level2type = N'COLUMN',
    @level2name = N'ItemID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'项名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_ConData',
    @level2type = N'COLUMN',
    @level2name = N'ItemName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'项数据',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_ConData',
    @level2type = N'COLUMN',
    @level2name = N'ItemData'