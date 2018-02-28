CREATE TABLE [dbo].[TAB_Org_DeptType]
(
	[DeptType] INT NOT NULL , 
    [DeptTypeName] VARCHAR(50) NULL, 
    [DeptTypeDS] VARCHAR(50) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'部门类型编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DeptType',
    @level2type = N'COLUMN',
    @level2name = N'DeptType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'部门类型名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DeptType',
    @level2type = N'COLUMN',
    @level2name = N'DeptTypeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'部门类型数据格式',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DeptType',
    @level2type = N'COLUMN',
    @level2name = N'DeptTypeDS'