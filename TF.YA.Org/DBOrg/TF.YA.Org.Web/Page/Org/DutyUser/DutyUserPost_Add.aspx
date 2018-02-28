<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DutyUserPost_Add.aspx.cs" Inherits="TF.YA.Org.Web.Page.Org.DutyUser.DutyUserPost_Add" %>

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
        <asp:HiddenField ID="HDD_ParentID" runat="server" />    
    <div style="width: 380px; height: 180px;">
        <div style="padding: 20px; clear: both;">   
            <div  style="clear: both; padding-top: 5px;">
                <div style="float: left; width: 80px; text-align: right; padding-right: 5px;">
                    类型名称:
                </div>
                <div style="float: left;">                    
                    <asp:DropDownList ID="DDLPostType" Width="205px" runat="server"></asp:DropDownList>
                </div>
            </div>
                       
            <div style="clear: both; height: 30px; padding-top: 20px; padding-left: 85px;">
                <asp:Button ID="BtnSave" runat="server" Text="确认" OnClientClick="return beforeSave();"
                    OnClick="BtnSave_Click" Style="padding: 0; width: 60px; height: 30px;" />
                <input type="button" id="btn_fb" value="取消" title="取消" style="width: 60px; height: 30px; margin-left: 40px;"
                    onclick="art.dialog.close();" />
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>