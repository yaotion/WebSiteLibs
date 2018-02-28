CREATE TABLE [dbo].[TAB_Org_Post_Type]
(
	[PostType] INT NOT NULL PRIMARY KEY, 
    [PostTypeName] VARCHAR(50) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位类型',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Post_Type',
    @level2type = N'COLUMN',
    @level2name = N'PostType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位类型名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Post_Type',
    @level2type = N'COLUMN',
    @level2name = N'PostTypeName'