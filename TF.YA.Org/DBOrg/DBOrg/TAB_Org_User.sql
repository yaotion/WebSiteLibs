CREATE TABLE [dbo].[TAB_Org_User]
(
	[UserNumber] INT NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(50) NULL, 
    [NameJP] VARCHAR(50) NULL, 
    [TelNumber] VARCHAR(50) NULL, 
    [UserGUID] VARCHAR(50) NULL, 
    [DeptID] VARCHAR(50) NULL, 
    [DeptName] VARCHAR(50) NULL, 
    [PostID] VARCHAR(50) NULL, 
    [PostName] VARCHAR(50) NULL, 
    [DeptFullName] VARCHAR(50) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'工号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User',
    @level2type = N'COLUMN',
    @level2name = 'UserNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'姓名',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User',
    @level2type = N'COLUMN',
    @level2name = 'UserName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'姓名简拼',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User',
    @level2type = N'COLUMN',
    @level2name = N'NameJP'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'电话',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User',
    @level2type = N'COLUMN',
    @level2name = N'TelNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'GUID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User',
    @level2type = N'COLUMN',
    @level2name = 'UserGUID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所属部门编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User',
    @level2type = N'COLUMN',
    @level2name = N'DeptID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所属部门名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User',
    @level2type = N'COLUMN',
    @level2name = N'DeptName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所在职位编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User',
    @level2type = N'COLUMN',
    @level2name = N'PostID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所在职位名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User',
    @level2type = N'COLUMN',
    @level2name = N'PostName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所有上级部门名称(从小到大，以1开始)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User',
    @level2type = N'COLUMN',
    @level2name = N'DeptFullName'