﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Org_Dept_Data.aspx.cs" Inherits="TF.YA.Org.Web.Page.Org.Dept.Org_Dept_Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" <%="href='"+TF.YA.Org.Web.FrameUtils.ResourceCss +"'" %>' />
	<script type="text/javascript" src="<%=TF.YA.Org.Web.FrameUtils.ResourceJs %>"></script>	
</head>
<body>
    <form id="form1" runat="server">
    <div>         
            <div  style="clear: both; padding-top: 5px;">
                <div style="float: left; width: 80px; text-align: right; padding-right: 5px;">
                    部门名称:
                </div>
                <div style="float: left;">
                    
                    <asp:Label ID="Lbl_DeptName" runat="server" style='width: 200px; height: 13px;'></asp:Label>
                </div>
            </div>
            <div runat="server" id="Div_DeptData"  style="clear: both; padding-top: 5px;">
                
            </div>
        <div style="clear: both; height: 30px; padding-top: 20px; padding-left: 85px;">
                <asp:Button ID="BtnSave" runat="server" Text="确认" OnClientClick="return beforeSave();"
                    OnClick="BtnSave_Click" Style="padding: 0; width: 60px; height: 30px;" />
                <input type="button" id="btn_fb" value="取消" title="取消" style="width: 60px; height: 30px; margin-left: 40px;"
                    onclick="art.dialog.close();" />
            </div>
    </div>
    </form>
</body>
</html>
