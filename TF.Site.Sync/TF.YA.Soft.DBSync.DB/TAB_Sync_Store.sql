CREATE TABLE [dbo].[TAB_Sync_Store]
(
	[ObjectName] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [ObjectVersion] VARCHAR(200) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对象名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_Store',
    @level2type = N'COLUMN',
    @level2name = N'ObjectName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对象当前版本',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_Store',
    @level2type = N'COLUMN',
    @level2name = N'ObjectVersion'