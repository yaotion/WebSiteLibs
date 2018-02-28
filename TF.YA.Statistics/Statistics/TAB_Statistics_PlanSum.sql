CREATE TABLE [dbo].[TAB_Statistics_PlanSum]
(
	[PlanDay] INT NOT NULL PRIMARY KEY, 
    [CQ] INT NULL, 
    [TQ] INT NULL, 
    [ZT] INT NULL, 
    [All] INT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'日期',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_PlanSum',
    @level2type = N'COLUMN',
    @level2name = N'PlanDay'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'出勤数量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_PlanSum',
    @level2type = N'COLUMN',
    @level2name = N'CQ'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退勤数量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_PlanSum',
    @level2type = N'COLUMN',
    @level2name = N'TQ'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'在途数量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_PlanSum',
    @level2type = N'COLUMN',
    @level2name = N'ZT'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所有机班数量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_PlanSum',
    @level2type = N'COLUMN',
    @level2name = N'All'