CREATE TABLE [dbo].[TAB_Org_User_FeatureType]
(
	[FeatureTypeID] INT NOT NULL PRIMARY KEY, 
    [FeatureTypeName] VARCHAR(50) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'0工号,1指纹,2人脸,3虹膜,4照片',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User_FeatureType',
    @level2type = N'COLUMN',
    @level2name = N'FeatureTypeID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'特征类型名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_User_FeatureType',
    @level2type = N'COLUMN',
    @level2name = N'FeatureTypeName'