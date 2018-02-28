/*
TF.YA.Org.DB 的部署脚本

此代码由工具生成。
如果重新生成此代码，则对此文件的更改可能导致
不正确的行为并将丢失。
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "TF.YA.Org.DB"
:setvar DefaultFilePrefix "TF.YA.Org.DB"
:setvar DefaultDataPath "C:\Users\Administrator\AppData\Local\Microsoft\VisualStudio\SSDT\DBOrg\"
:setvar DefaultLogPath "C:\Users\Administrator\AppData\Local\Microsoft\VisualStudio\SSDT\DBOrg\"

GO
:on error exit
GO
/*
请检测 SQLCMD 模式，如果不支持 SQLCMD 模式，请禁用脚本执行。
要在启用 SQLCMD 模式后重新启用脚本，请执行:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'要成功执行此脚本，必须启用 SQLCMD 模式。';
        SET NOEXEC ON;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE,
                DISABLE_BROKER 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'disable';


GO
PRINT N'已跳过具有键 438326e6-545f-4a35-b3b8-04b2bc480577, e2b11a10-561c-47dd-b382-a2ef635ee08e 的重命名重构操作，不会将元素 [dbo].[TAB_Org_Dept].[Id] (SqlSimpleColumn) 重命名为 DeptID';


GO
PRINT N'已跳过具有键 ac871fae-539a-40c8-8a7e-d3843c16d67f 的重命名重构操作，不会将元素 [dbo].[TAB_Org_Dept].[DepartName] (SqlSimpleColumn) 重命名为 DeptName';


GO
PRINT N'已跳过具有键 9c01bd74-9bb7-42f2-b987-5d32b8b69c1b, 774b8d18-9d12-49b8-8703-a6bb2a1ccc98 的重命名重构操作，不会将元素 [dbo].[TAB_Org_Dept].[DepartType] (SqlSimpleColumn) 重命名为 DeptType';


GO
PRINT N'已跳过具有键 6ad9652b-beb3-4f3f-ae87-2dfffaeb2ba2 的重命名重构操作，不会将元素 [dbo].[TAB_Org_Dept_Relation].[Id] (SqlSimpleColumn) 重命名为 DeptID';


GO
PRINT N'已跳过具有键 5161a20e-c382-4939-9076-94f80790c8cd 的重命名重构操作，不会将元素 [dbo].[TAB_Org_Dept_Relation].[Hig] (SqlSimpleColumn) 重命名为 HigherDepartID';


GO
PRINT N'已跳过具有键 9c9e7534-715f-4559-bb9b-b208b31e6e50 的重命名重构操作，不会将元素 [dbo].[TAB_Org_Post].[Id] (SqlSimpleColumn) 重命名为 PostID';


GO
PRINT N'已跳过具有键 27775a5c-ffb8-494a-a74a-0fdcbd6440c8 的重命名重构操作，不会将元素 [dbo].[TAB_Org_Post_Type].[Id] (SqlSimpleColumn) 重命名为 PostType';


GO
PRINT N'已跳过具有键 d026bdc6-9d05-4756-9d57-270bb8cf4179 的重命名重构操作，不会将元素 [dbo].[TAB_Org_DeptType].[Id] (SqlSimpleColumn) 重命名为 DeptType';


GO
PRINT N'已跳过具有键 04243ef1-599f-4ec2-91ae-9373cbcb1996, 38e1fc20-bd6a-4c32-94ef-e8cea5ef00d5 的重命名重构操作，不会将元素 [dbo].[TAB_Org_User].[Id] (SqlSimpleColumn) 重命名为 UserNumber';


GO
PRINT N'已跳过具有键 160c9d90-1add-4529-b94c-f4ced22278f3, 5f57485a-556c-44a6-a1a5-9aed8fd0c249 的重命名重构操作，不会将元素 [dbo].[TAB_Org_User_DeptRelation].[Id] (SqlSimpleColumn) 重命名为 UserNumber';


GO
PRINT N'已跳过具有键 1ff33890-7be6-4f7d-9f42-69d3b5735df0 的重命名重构操作，不会将元素 [dbo].[TAB_Org_User].[TMName] (SqlSimpleColumn) 重命名为 UserName';


GO
PRINT N'已跳过具有键 8b363f81-183b-49ad-82d4-a081151b15e9 的重命名重构操作，不会将元素 [dbo].[TAB_Org_User].[TMGUID] (SqlSimpleColumn) 重命名为 UserGUID';


GO
PRINT N'已跳过具有键 ea1a7c53-99b7-4244-be6c-9e32cc82ee48 的重命名重构操作，不会将元素 [dbo].[TAB_Org_User_Feature].[Id] (SqlSimpleColumn) 重命名为 UserNumber';


GO
PRINT N'已跳过具有键 ed7fec5e-f1cf-46da-8382-8cd530478293 的重命名重构操作，不会将元素 [dbo].[TAB_Org_User_FeatureType].[Id] (SqlSimpleColumn) 重命名为 FeatureTypeID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept]...';


GO
CREATE TABLE [dbo].[TAB_Org_Dept] (
    [DeptID]         VARCHAR (50)  NOT NULL,
    [DeptName]       VARCHAR (50)  NULL,
    [DeptType]       INT           NULL,
    [ParentDeptID]   VARCHAR (50)  NULL,
    [ParentDeptName] VARCHAR (50)  NULL,
    [FullParentID]   VARCHAR (500) NULL,
    [FullParentName] VARCHAR (500) NULL,
    [DeptOrder]      INT           NULL,
    PRIMARY KEY CLUSTERED ([DeptID] ASC)
);


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept_Relation]...';


GO
CREATE TABLE [dbo].[TAB_Org_Dept_Relation] (
    [DeptID]           VARCHAR (50) NOT NULL,
    [DeptName]         VARCHAR (50) NULL,
    [HigherDepartID]   VARCHAR (50) NULL,
    [HigherLevel]      INT          NULL,
    [HigherDepartName] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([DeptID] ASC)
);


GO
PRINT N'正在创建 [dbo].[TAB_Org_DeptType]...';


GO
CREATE TABLE [dbo].[TAB_Org_DeptType] (
    [DeptType]     INT          NOT NULL,
    [DeptTypeName] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([DeptType] ASC)
);


GO
PRINT N'正在创建 [dbo].[TAB_Org_Post]...';


GO
CREATE TABLE [dbo].[TAB_Org_Post] (
    [PostID]   VARCHAR (50) NOT NULL,
    [PostName] VARCHAR (50) NULL,
    [DeptID]   VARCHAR (50) NULL,
    [PostType] INT          NULL,
    PRIMARY KEY CLUSTERED ([PostID] ASC)
);


GO
PRINT N'正在创建 [dbo].[TAB_Org_Post_Type]...';


GO
CREATE TABLE [dbo].[TAB_Org_Post_Type] (
    [PostType]     INT          NOT NULL,
    [PostTypeName] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([PostType] ASC)
);


GO
PRINT N'正在创建 [dbo].[TAB_Org_User]...';


GO
CREATE TABLE [dbo].[TAB_Org_User] (
    [UserNumber]   INT          NOT NULL,
    [UserName]     VARCHAR (50) NULL,
    [NameJP]       VARCHAR (50) NULL,
    [TelNumber]    VARCHAR (50) NULL,
    [UserGUID]     VARCHAR (50) NULL,
    [DeptID]       VARCHAR (50) NULL,
    [DeptName]     VARCHAR (50) NULL,
    [PostID]       VARCHAR (50) NULL,
    [PostName]     VARCHAR (50) NULL,
    [DeptFullName] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([UserNumber] ASC)
);


GO
PRINT N'正在创建 [dbo].[TAB_Org_User_DeptRelation]...';


GO
CREATE TABLE [dbo].[TAB_Org_User_DeptRelation] (
    [UserNumber] VARCHAR (50) NOT NULL,
    [DeptID]     VARCHAR (50) NULL,
    [DeptLevel]  INT          NULL,
    [DeptName]   VARCHAR (50) NULL
);


GO
PRINT N'正在创建 [dbo].[TAB_Org_User_Feature]...';


GO
CREATE TABLE [dbo].[TAB_Org_User_Feature] (
    [UserNumber]     VARCHAR (50) NOT NULL,
    [FeatureType]    INT          NULL,
    [FeatureContent] IMAGE        NULL,
    [FeatureIndex]   INT          NULL
);


GO
PRINT N'正在创建 [dbo].[TAB_Org_User_FeatureType]...';


GO
CREATE TABLE [dbo].[TAB_Org_User_FeatureType] (
    [FeatureTypeID]   INT          NOT NULL,
    [FeatureTypeName] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([FeatureTypeID] ASC)
);


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept].[DeptID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'部门编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept', @level2type = N'COLUMN', @level2name = N'DeptID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept].[DeptName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'部门名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept', @level2type = N'COLUMN', @level2name = N'DeptName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept].[DeptType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'部门类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept', @level2type = N'COLUMN', @level2name = N'DeptType';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept].[ParentDeptID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上级部门编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept', @level2type = N'COLUMN', @level2name = N'ParentDeptID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept].[ParentDeptName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上级部门名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept', @level2type = N'COLUMN', @level2name = N'ParentDeptName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept].[FullParentID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'全部上级部门编号，逗号分割', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept', @level2type = N'COLUMN', @level2name = N'FullParentID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept].[FullParentName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'全部上级部门名称，逗号分割', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept', @level2type = N'COLUMN', @level2name = N'FullParentName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept].[DeptOrder].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept', @level2type = N'COLUMN', @level2name = N'DeptOrder';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept_Relation].[DeptID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'部门编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept_Relation', @level2type = N'COLUMN', @level2name = N'DeptID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept_Relation].[DeptName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'部门名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept_Relation', @level2type = N'COLUMN', @level2name = N'DeptName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept_Relation].[HigherDepartID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上级部门编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept_Relation', @level2type = N'COLUMN', @level2name = N'HigherDepartID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept_Relation].[HigherLevel].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上级部门相对级别', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept_Relation', @level2type = N'COLUMN', @level2name = N'HigherLevel';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Dept_Relation].[HigherDepartName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上级部门名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Dept_Relation', @level2type = N'COLUMN', @level2name = N'HigherDepartName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_DeptType].[DeptType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'部门类型编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_DeptType', @level2type = N'COLUMN', @level2name = N'DeptType';


GO
PRINT N'正在创建 [dbo].[TAB_Org_DeptType].[DeptTypeName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'部门类型名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_DeptType', @level2type = N'COLUMN', @level2name = N'DeptTypeName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Post].[PostID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'岗位编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Post', @level2type = N'COLUMN', @level2name = N'PostID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Post].[PostName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'岗位名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Post', @level2type = N'COLUMN', @level2name = N'PostName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Post].[DeptID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属部门', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Post', @level2type = N'COLUMN', @level2name = N'DeptID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Post].[PostType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'岗位类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Post', @level2type = N'COLUMN', @level2name = N'PostType';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Post_Type].[PostType].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'岗位类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Post_Type', @level2type = N'COLUMN', @level2name = N'PostType';


GO
PRINT N'正在创建 [dbo].[TAB_Org_Post_Type].[PostTypeName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'岗位类型名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_Post_Type', @level2type = N'COLUMN', @level2name = N'PostTypeName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User].[UserNumber].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User', @level2type = N'COLUMN', @level2name = N'UserNumber';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User].[UserName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User', @level2type = N'COLUMN', @level2name = N'UserName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User].[NameJP].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'姓名简拼', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User', @level2type = N'COLUMN', @level2name = N'NameJP';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User].[TelNumber].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User', @level2type = N'COLUMN', @level2name = N'TelNumber';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User].[UserGUID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'GUID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User', @level2type = N'COLUMN', @level2name = N'UserGUID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User].[DeptID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属部门编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User', @level2type = N'COLUMN', @level2name = N'DeptID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User].[DeptName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属部门名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User', @level2type = N'COLUMN', @level2name = N'DeptName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User].[PostID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所在职位编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User', @level2type = N'COLUMN', @level2name = N'PostID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User].[PostName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所在职位名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User', @level2type = N'COLUMN', @level2name = N'PostName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User].[DeptFullName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所有上级部门名称(从小到大，以1开始)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User', @level2type = N'COLUMN', @level2name = N'DeptFullName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User_DeptRelation].[UserNumber].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'人员工号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User_DeptRelation', @level2type = N'COLUMN', @level2name = N'UserNumber';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User_DeptRelation].[DeptID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上级部门编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User_DeptRelation', @level2type = N'COLUMN', @level2name = N'DeptID';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User_DeptRelation].[DeptLevel].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上级部门序号(倒叙，从1开始)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User_DeptRelation', @level2type = N'COLUMN', @level2name = N'DeptLevel';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User_DeptRelation].[DeptName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上级部门名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User_DeptRelation', @level2type = N'COLUMN', @level2name = N'DeptName';


GO
PRINT N'正在创建 [dbo].[TAB_Org_User_FeatureType].[FeatureTypeID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0工号,1指纹,2人脸,3虹膜,4照片', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TAB_Org_User_FeatureType', @level2type = N'COLUMN', @level2name = N'FeatureTypeID';


GO
-- 正在重构步骤以使用已部署的事务日志更新目标服务器

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '438326e6-545f-4a35-b3b8-04b2bc480577')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('438326e6-545f-4a35-b3b8-04b2bc480577')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'e2b11a10-561c-47dd-b382-a2ef635ee08e')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('e2b11a10-561c-47dd-b382-a2ef635ee08e')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'ac871fae-539a-40c8-8a7e-d3843c16d67f')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('ac871fae-539a-40c8-8a7e-d3843c16d67f')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '9c01bd74-9bb7-42f2-b987-5d32b8b69c1b')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('9c01bd74-9bb7-42f2-b987-5d32b8b69c1b')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '774b8d18-9d12-49b8-8703-a6bb2a1ccc98')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('774b8d18-9d12-49b8-8703-a6bb2a1ccc98')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '6ad9652b-beb3-4f3f-ae87-2dfffaeb2ba2')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('6ad9652b-beb3-4f3f-ae87-2dfffaeb2ba2')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '5161a20e-c382-4939-9076-94f80790c8cd')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('5161a20e-c382-4939-9076-94f80790c8cd')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '9c9e7534-715f-4559-bb9b-b208b31e6e50')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('9c9e7534-715f-4559-bb9b-b208b31e6e50')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '27775a5c-ffb8-494a-a74a-0fdcbd6440c8')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('27775a5c-ffb8-494a-a74a-0fdcbd6440c8')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'd026bdc6-9d05-4756-9d57-270bb8cf4179')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('d026bdc6-9d05-4756-9d57-270bb8cf4179')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '04243ef1-599f-4ec2-91ae-9373cbcb1996')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('04243ef1-599f-4ec2-91ae-9373cbcb1996')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '160c9d90-1add-4529-b94c-f4ced22278f3')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('160c9d90-1add-4529-b94c-f4ced22278f3')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '38e1fc20-bd6a-4c32-94ef-e8cea5ef00d5')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('38e1fc20-bd6a-4c32-94ef-e8cea5ef00d5')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '1ff33890-7be6-4f7d-9f42-69d3b5735df0')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('1ff33890-7be6-4f7d-9f42-69d3b5735df0')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '8b363f81-183b-49ad-82d4-a081151b15e9')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('8b363f81-183b-49ad-82d4-a081151b15e9')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '5f57485a-556c-44a6-a1a5-9aed8fd0c249')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('5f57485a-556c-44a6-a1a5-9aed8fd0c249')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'ea1a7c53-99b7-4244-be6c-9e32cc82ee48')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('ea1a7c53-99b7-4244-be6c-9e32cc82ee48')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'ed7fec5e-f1cf-46da-8382-8cd530478293')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('ed7fec5e-f1cf-46da-8382-8cd530478293')

GO

GO
PRINT N'更新完成。';


GO
