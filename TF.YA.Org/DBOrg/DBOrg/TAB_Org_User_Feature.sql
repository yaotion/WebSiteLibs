CREATE TABLE [dbo].[TAB_Org_User_Feature]
(
	[UserNumber] VARCHAR(50) NOT NULL , 
    [FeatureType] INT NULL, 
    [FeatureContent] IMAGE NULL, 
    [FeatureIndex] INT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User_Feature',
    @level2type = N'COLUMN',
    @level2name = N'UserNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'特征类型',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User_Feature',
    @level2type = N'COLUMN',
    @level2name = N'FeatureType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'特征内容',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User_Feature',
    @level2type = N'COLUMN',
    @level2name = N'FeatureContent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'特征序号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User_Feature',
    @level2type = N'COLUMN',
    @level2name = N'FeatureIndex'