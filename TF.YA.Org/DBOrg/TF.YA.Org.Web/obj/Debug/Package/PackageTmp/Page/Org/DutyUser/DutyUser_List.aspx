<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DutyUser_List.aspx.cs" Inherits="TF.YA.Org.Web.Page.Org.DutyUser.DutyUser_List" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" <%="href='"+TF.YA.Org.Web.FrameUtils.ResourceCss +"'" %>' />
	<script type="text/javascript" src="<%=TF.YA.Org.Web.FrameUtils.ResourceJs %>"></script>	
    
    <script type="text/javascript">
        function addUser(userID) {
            artDialogOpen('/Page/Org/DutyUser/DutyUser_Add.aspx?r=' + Math.random() + '&userNumber=' + userID, '添加值班员', 400, 300);
        }
        function editUser(userID) {
            artDialogOpen('/Page/Org/DutyUser/DutyUser_Add.aspx?r=' + Math.random() + '&userNumber=' + userID, '修改值班员', 400, 300);
        }

        function deleteUser(userID) {
            publicDeleteReload('/Page/Org/DutyUser/ashx/DutyUser_Delete.ashx?r=' + Math.random() + '&userNumber=' + userID, "确认删除该项吗？");
        }
        function clearPWD(userID) {
            publicDeleteReload('/Page/Org/DutyUser/ashx/DutyUser_ClearPWD.ashx?r=' + Math.random() + '&userNumber=' + userID, "确认清除该人员密码吗？");
        }
        //页面刷新
        function appdel_do() {
            window.location.reload();
        }
        function CheckQuery() {
            
            var userID = document.getElementById("tbUserNumber").value;
            var userName = document.getElementById("tbUserName").value;            
            QueryList(userID, userName);
        }
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="SerachDiv" style="margin: 10px; border: 1px #DDDDDD solid; background-color: #FBFBFB; width: 98%;">
            <input type="hidden" value ="" id="tbUserDeptID" />
                <div style="margin-left: 10px; height: 55px; line-height: 55px; vertical-align: middle; border-radius: 5px;">
                   工号：<input id="tbUserNumber"type="text" style="width:120px;" />
                   姓名：<input id="tbUserName" type="text" style="width:120px;" />                   
                   
                    <a href="#" class="btn btn_orange btn_search" onclick="CheckQuery()"><span>查询</span></a>   
                    <span style="float: right; margin-right:10px;">
                       <a href="#" onclick="addUser('')" class="btn btn_orange btn_export " style="margin-left: 10px; width: 100px;"><span>添加人员</span></a>
                    </span>
                </div>
                 
            </div>  
          
            <div class="ml10 mt10">
               <div id="list">
               </div>
            </div>
             <script type="text/javascript">
                 var width = $("#SerachDiv").width() + 2;
                 var height = $(window).height() - $("#SerachDiv").height() - 30;
                 function QueryList(userID, userName) {
                     $('#list').datagrid({
                         url: 'ashx/DutyUser_List.ashx?RandomID=' + Math.random() + '&userNumber=' + userID + '&userName=' + userName,
                         striped: true,
                         id: 'DutyUserNumber',
                         nowrap: true,
                         pagination: true,
                         rownumbers: true,
                         method: 'post',
                         singleSelect: true,
                         fitColumns: true,
                         height: height,
                         width: width,
                         pageSize: 30,
                         pageList: [30],
                         loadMsg: '数据加载中,请稍候..',
                         columns: [[
                                     { field: 'DutyUserNumber', title: '工号', width: 100, align: 'center' },
                                     { field: 'DutyUserName', title: '姓名', width: 100, align: 'center' },
                                     { field: 'RoleName', title: '角色', width: 60, align: 'center' },

                                     {
                                         field: 'id', title: '操作', width: 200, align: 'center', formatter: function (val, row) {
                                             return "&nbsp;&nbsp;" +
                                            "<a href='javacript:;' onclick=\"editUser('" + row.DutyUserNumber + "');\">修改权限</a>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                            "<a href='javacript:;' onclick=\"clearPWD('" + row.DutyUserNumber + "');\">清空密码</a>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                        "<a href='javacript:;' onclick=\"deleteUser('" + row.DutyUserNumber + "');\">删除帐号</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                                         }
                                     }
                         ]]
                     });
                 }
                 QueryList('', '', '');
        </script>
    </div>
    </form>
</body>
</html>
