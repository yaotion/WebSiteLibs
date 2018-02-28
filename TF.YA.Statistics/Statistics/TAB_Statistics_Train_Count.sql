CREATE TABLE [dbo].[TAB_Statistics_Train_Count]
(
	[JWDCode] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [JWDName] VARCHAR(50) NULL, 
    [PeiShuCount] INT NULL, 
    [ZhiPeiCount] INT NULL, 
    [YunYongCount] INT NULL, 
    [FeiYongCount] INT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机务段编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_Count',
    @level2type = N'COLUMN',
    @level2name = N'JWDCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机务段名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_Count',
    @level2type = N'COLUMN',
    @level2name = N'JWDName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'配属机车数量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_Count',
    @level2type = N'COLUMN',
    @level2name = N'PeiShuCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'支配机车数量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_Count',
    @level2type = N'COLUMN',
    @level2name = N'ZhiPeiCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'运用机车数量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_Count',
    @level2type = N'COLUMN',
    @level2name = N'YunYongCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'非用机车数量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_Count',
    @level2type = N'COLUMN',
    @level2name = N'FeiYongCount'