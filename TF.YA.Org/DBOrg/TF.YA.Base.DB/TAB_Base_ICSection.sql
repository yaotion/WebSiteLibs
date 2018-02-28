CREATE TABLE [dbo].[TAB_Base_ICSection]
(
	[JWDNumber] VARCHAR(50) NOT NULL , 
    [JWDName] VARCHAR(50) NULL, 
    [ICSectionNumber] INT NULL, 
    [ICSectionName] VARCHAR(50) NOT NULL, 
    PRIMARY KEY ([JWDNumber], [ICSectionName])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机务段号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_ICSection',
    @level2type = N'COLUMN',
    @level2name = N'JWDNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机务段名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_ICSection',
    @level2type = N'COLUMN',
    @level2name = N'JWDName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'写卡区段号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_ICSection',
    @level2type = N'COLUMN',
    @level2name = N'ICSectionNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'写卡区段名',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_ICSection',
    @level2type = N'COLUMN',
    @level2name = N'ICSectionName'