CREATE TABLE [dbo].[TAB_Statistics_Train_TJ]
(
	[TJDay] DATETIME NOT NULL , 
    [JWDCode] VARCHAR(50) NULL, 
    [JWDName] VARCHAR(50) NULL, 
    [TR] VARCHAR(50) NULL, 
    [ZX] VARCHAR(50) NULL, 
    [ZZ] VARCHAR(50) NULL, 
    [SD] VARCHAR(50) NULL, 
    [CL] VARCHAR(50) NULL, 
    CONSTRAINT [PK_TAB_Statistics_Train_TJ] PRIMARY KEY ([TJDay]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'统计日期',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_TJ',
    @level2type = N'COLUMN',
    @level2name = N'TJDay'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机务段编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_TJ',
    @level2type = N'COLUMN',
    @level2name = N'JWDCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机务段名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_TJ',
    @level2type = N'COLUMN',
    @level2name = N'JWDName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'台日',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_TJ',
    @level2type = N'COLUMN',
    @level2name = N'TR'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'走行',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_TJ',
    @level2type = N'COLUMN',
    @level2name = N'ZX'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'载重',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_TJ',
    @level2type = N'COLUMN',
    @level2name = N'ZZ'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'速度',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_TJ',
    @level2type = N'COLUMN',
    @level2name = N'SD'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Train_TJ',
    @level2type = N'COLUMN',
    @level2name = N'CL'