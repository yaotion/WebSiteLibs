CREATE TABLE [dbo].[TAB_Org_Dept]
(
	[DeptID] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [DeptName] VARCHAR(50) NULL, 
    [DeptType] INT NULL, 
    [ParentDeptID] VARCHAR(50) NULL, 
    [ParentDeptName] VARCHAR(50) NULL, 
    [FullParentID] VARCHAR(500) NULL, 
    [FullParentName] VARCHAR(500) NULL, 
    [DeptOrder] INT NULL, 
    [DeptLevel] INT NULL, 
    [DeptData] VARCHAR(200) NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'部门编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept',
    @level2type = N'COLUMN',
    @level2name = N'DeptID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'部门名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept',
    @level2type = N'COLUMN',
    @level2name = N'DeptName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'部门类型',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept',
    @level2type = N'COLUMN',
    @level2name = N'DeptType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上级部门编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept',
    @level2type = N'COLUMN',
    @level2name = N'ParentDeptID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上级部门名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept',
    @level2type = N'COLUMN',
    @level2name = N'ParentDeptName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'全部上级部门编号，逗号分割',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept',
    @level2type = N'COLUMN',
    @level2name = N'FullParentID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'全部上级部门名称，逗号分割',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept',
    @level2type = N'COLUMN',
    @level2name = N'FullParentName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排序序号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept',
    @level2type = N'COLUMN',
    @level2name = N'DeptOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'部门附加数据(格式参照部门类型DeptTypeDS)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept',
    @level2type = N'COLUMN',
    @level2name = N'DeptData'