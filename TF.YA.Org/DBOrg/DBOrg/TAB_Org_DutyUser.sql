CREATE TABLE [dbo].[TAB_Org_DutyUser]
(
	[DutyUserNumber] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [DutyUserName] VARCHAR(50) NULL, 
    [RoleID] VARCHAR(50) NULL, 
    [Password] VARCHAR(50) NULL, 
    [TokenID] VARCHAR(50) NULL, 
    [TokenTime] DATETIME NULL, 
    [RoleName] VARCHAR(50) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'值班员工号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DutyUser',
    @level2type = N'COLUMN',
    @level2name = N'DutyUserNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'值班员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DutyUser',
    @level2type = N'COLUMN',
    @level2name = N'DutyUserName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'担任角色',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DutyUser',
    @level2type = N'COLUMN',
    @level2name = N'RoleID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'密码',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DutyUser',
    @level2type = N'COLUMN',
    @level2name = N'Password'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'登录凭证',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DutyUser',
    @level2type = N'COLUMN',
    @level2name = N'TokenID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'凭证日期',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DutyUser',
    @level2type = N'COLUMN',
    @level2name = N'TokenTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'角色名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TAB_Org_DutyUser',
    @level2type = N'COLUMN',
    @level2name = N'RoleName'