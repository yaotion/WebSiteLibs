CREATE TABLE [dbo].[TAB_Base_DutyPlace]
(
	[PlaceID] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [PlaceName] VARCHAR(50) NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'地点编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_DutyPlace',
    @level2type = N'COLUMN',
    @level2name = N'PlaceID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'地点名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Base_DutyPlace',
    @level2type = N'COLUMN',
    @level2name = N'PlaceName'