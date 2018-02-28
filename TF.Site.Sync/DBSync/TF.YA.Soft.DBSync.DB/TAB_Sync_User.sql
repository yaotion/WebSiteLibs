CREATE TABLE [dbo].[TAB_Sync_User]
(
	[ObjectName] VARCHAR(50) NOT NULL, 
    [UserID] VARCHAR(50) NULL, 
    [UserName] VARCHAR(50) NULL, 
    [CreateTime] DATETIME NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对象名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_User',
    @level2type = N'COLUMN',
    @level2name = N'ObjectName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_User',
    @level2type = N'COLUMN',
    @level2name = N'UserID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_User',
    @level2type = N'COLUMN',
    @level2name = N'UserName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_User',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'