CREATE TABLE [dbo].[TAB_Sync_User_Data]
(
	[UserID] VARCHAR(50) NOT NULL, 
    [ObjectName] VARCHAR(50) NULL, 
    [[Key]]] VARCHAR(50) NULL, 
    [Op] VARCHAR(50) NULL, 
    [UpdateTime] DATETIME NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_User_Data',
    @level2type = N'COLUMN',
    @level2name = N'UserID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对象名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_User_Data',
    @level2type = N'COLUMN',
    @level2name = N'ObjectName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数据主键',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_User_Data',
    @level2type = N'COLUMN',
    @level2name = N'[Key]'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'操作(0不变、1增、2改、3删)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_User_Data',
    @level2type = N'COLUMN',
    @level2name = N'Op'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后更新时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Sync_User_Data',
    @level2type = N'COLUMN',
    @level2name = N'UpdateTime'