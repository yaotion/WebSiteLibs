CREATE TABLE [dbo].[TAB_Org_Dept_Relation]
(
	[DeptID] VARCHAR(50) NOT NULL , 
    [DeptName] VARCHAR(50) NULL, 
    [HigherDepartID] VARCHAR(50) NULL, 
    [HigherLevel] INT NULL, 
    [HigherDepartName] VARCHAR(50) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'部门编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept_Relation',
    @level2type = N'COLUMN',
    @level2name = N'DeptID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'部门名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept_Relation',
    @level2type = N'COLUMN',
    @level2name = N'DeptName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上级部门编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept_Relation',
    @level2type = N'COLUMN',
    @level2name = N'HigherDepartID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上级部门名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept_Relation',
    @level2type = N'COLUMN',
    @level2name = N'HigherDepartName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上级部门相对级别',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Dept_Relation',
    @level2type = N'COLUMN',
    @level2name = N'HigherLevel'