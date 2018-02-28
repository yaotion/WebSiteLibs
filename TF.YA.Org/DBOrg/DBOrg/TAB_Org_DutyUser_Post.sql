CREATE TABLE [dbo].[TAB_Org_DutyUser_Post]
(
	[PostTypeID] BIGINT NOT NULL PRIMARY KEY, 
    [PostTypeName] VARCHAR(50) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'职位类型编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DutyUser_Post',
    @level2type = N'COLUMN',
    @level2name = N'PostTypeID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'职位类型名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DutyUser_Post',
    @level2type = N'COLUMN',
    @level2name = N'PostTypeName'