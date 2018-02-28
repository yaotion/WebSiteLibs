CREATE TABLE [dbo].[TAB_Base_Station_YJ]
(
	[StationName] VARCHAR(50) NOT NULL , 
    [JLH] INT NOT NULL, 
    [CZH] INT NOT NULL, 
    [TMIS] INT NOT NULL, 
    CONSTRAINT [PK_TAB_Base_Station_YJ] PRIMARY KEY ([JLH], [CZH])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'车站名',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_Station_YJ',
    @level2type = N'COLUMN',
    @level2name = N'StationName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'交路号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_Station_YJ',
    @level2type = N'COLUMN',
    @level2name = N'JLH'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'车站号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_Station_YJ',
    @level2type = N'COLUMN',
    @level2name = N'CZH'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'TMIS号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_Station_YJ',
    @level2type = N'COLUMN',
    @level2name = N'TMIS'