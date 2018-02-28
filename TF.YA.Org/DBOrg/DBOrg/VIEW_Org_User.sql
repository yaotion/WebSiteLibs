SELECT     dbo.TAB_Org_User.DeptID, dbo.TAB_Org_User.PostID, dbo.TAB_Org_User.PostName, dbo.TAB_Org_Dept.FullParentName AS DeptFullName, 
                      dbo.TAB_Org_User.UserNumber, dbo.TAB_Org_User.UserName, dbo.TAB_Org_User.NameJP, dbo.TAB_Org_User.TelNumber, dbo.TAB_Org_User.UserGUID, 
                      dbo.TAB_Org_Dept.DeptName, dbo.TAB_Org_Dept.DeptType, dbo.TAB_Org_Post.PostType
FROM         dbo.TAB_Org_User LEFT OUTER JOIN
                      dbo.TAB_Org_Post ON dbo.TAB_Org_User.PostID = dbo.TAB_Org_Post.PostID LEFT OUTER JOIN
                      dbo.TAB_Org_Dept ON dbo.TAB_Org_User.DeptID = dbo.TAB_Org_Dept.DeptID