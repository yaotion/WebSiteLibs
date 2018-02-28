CREATE TABLE [dbo].[TAB_Org_Post]
(
	[PostID] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [PostName] VARCHAR(50) NULL, 
    [PostType] INT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Post',
    @level2type = N'COLUMN',
    @level2name = N'PostID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Post',
    @level2type = N'COLUMN',
    @level2name = N'PostName'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位类型',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_Post',
    @level2type = N'COLUMN',
    @level2name = N'PostType'