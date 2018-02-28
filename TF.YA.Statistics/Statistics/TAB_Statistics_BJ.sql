CREATE TABLE [dbo].[TAB_Statistics_BJ]
(
	[BJDay] DATETIME NOT NULL , 
    [BJItemID] VARCHAR(50) NOT NULL, 
    [BJItemName] VARCHAR(50) NULL, 
    [BJCount] INT NULL, 
    PRIMARY KEY ([BJItemID], [BJDay])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'报警日期',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_BJ',
    @level2type = N'COLUMN',
    @level2name = N'BJDay'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'报警分类编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_BJ',
    @level2type = N'COLUMN',
    @level2name = N'BJItemID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'报警分类名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_BJ',
    @level2type = N'COLUMN',
    @level2name = N'BJItemName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'报警数量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_BJ',
    @level2type = N'COLUMN',
    @level2name = N'BJCount'