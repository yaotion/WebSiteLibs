CREATE TABLE [dbo].[TAB_Sync_Store_Data]
(
	[ObjectName] VARCHAR(50) NULL , 
    [[Key]]] VARCHAR(50) NULL, 
    [Version] VARCHAR(50) NULL, 
    [Json] VARCHAR(5000) NULL, 
    [UpdateTime] DATETIME NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对象名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_Store_Data',
    @level2type = N'COLUMN',
    @level2name = N'ObjectName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数据主键',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_Store_Data',
    @level2type = N'COLUMN',
    @level2name = N'[Key]'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数据版本',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_Store_Data',
    @level2type = N'COLUMN',
    @level2name = N'Version'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数据内容的JSON形势',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_Store_Data',
    @level2type = N'COLUMN',
    @level2name = N'Json'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后更新时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_Store_Data',
    @level2type = N'COLUMN',
    @level2name = N'UpdateTime'