CREATE TABLE [dbo].[TAB_Statistics_Weather]
(
	[WeatherDay] DATETIME NOT NULL PRIMARY KEY, 
    [DiDian] VARCHAR(50) NULL, 
    [ZhuangKuang] VARCHAR(50) NULL, 
    [WenDu] VARCHAR(50) NULL, 
    [FengLi] VARCHAR(50) NULL, 
    [ShiDu] VARCHAR(50) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'日期',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Weather',
    @level2type = N'COLUMN',
    @level2name = N'WeatherDay'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'地点',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Weather',
    @level2type = N'COLUMN',
    @level2name = N'DiDian'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'天气状况',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Weather',
    @level2type = N'COLUMN',
    @level2name = N'ZhuangKuang'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'温度',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Weather',
    @level2type = N'COLUMN',
    @level2name = N'WenDu'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'风力',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Weather',
    @level2type = N'COLUMN',
    @level2name = N'FengLi'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'湿度',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Statistics_Weather',
    @level2type = N'COLUMN',
    @level2name = N'ShiDu'