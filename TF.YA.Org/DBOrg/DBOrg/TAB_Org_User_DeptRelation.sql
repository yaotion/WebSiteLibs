CREATE TABLE [dbo].[TAB_Org_User_DeptRelation]
(
	[UserNumber] VARCHAR(50) NOT NULL , 
    [DeptID] VARCHAR(50) NULL, 
    [DeptLevel] INT NULL, 
    [DeptName] VARCHAR(50) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'人员工号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User_DeptRelation',
    @level2type = N'COLUMN',
    @level2name = 'UserNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上级部门编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User_DeptRelation',
    @level2type = N'COLUMN',
    @level2name = N'DeptID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上级部门序号(倒叙，从1开始)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User_DeptRelation',
    @level2type = N'COLUMN',
    @level2name = N'DeptLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上级部门名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User_DeptRelation',
    @level2type = N'COLUMN',
    @level2name = N'DeptName'