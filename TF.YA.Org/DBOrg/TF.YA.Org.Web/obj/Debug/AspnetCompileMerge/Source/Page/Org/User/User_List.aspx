<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_List.aspx.cs" Inherits="TF.YA.Org.Web.User.User_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" <%="href='"+TF.YA.Org.Web.FrameUtils.ResourceCss +"'" %>' />
	<script type="text/javascript" src="<%=TF.YA.Org.Web.FrameUtils.ResourceJs %>"></script>	
    
    <script type="text/javascript">      
        function addUser(userID) {
            artDialogOpen('/Page/Org/User/User_Add.aspx?r=' + Math.random() + '&userNumber=' + userID, '添加人员', 400, 300);
        }
        function editUser(userID) {
            artDialogOpen('/Page/Org/User/User_Add.aspx?r=' + Math.random() + '&userNumber=' + userID, '修改人员', 400, 300);
        }
        function deleteUser(userID) {
            publicDeleteReload('/Page/Org/User/ashx/User_Delete.ashx?r=' + Math.random() + '&userNumber=' + userID, "确认删除该项吗？");
        }
        //页面刷新
        function appdel_do() {
            window.location.reload();
        }
        function CheckQuery()
        {
            
            var userID = document.getElementById("tbUserNumber").value;
            var userName = document.getElementById("tbUserName").value;
            var deptID = document.getElementById("tbUserDeptID").value;
            
            QueryList(userID,userName,deptID);
        }
        function selectDept(id, data) {
            document.getElementById("tbUserDept").value = data;
            document.getElementById("tbUserDeptID").value = id;
        }
        function openSelectedDepartDialog() {
            artDialogOpen('/Page/Org/Dept/Org_Dept_Dialog.aspx?r=' + Math.random(), '选择部门', 460, 300);
        }
        function Import() {
            artDialogOpen('/Page/Org/User/User_Import.aspx?r=' + Math.random(), '导入人员', 450, 350);
            return false;
        }
        function Export() {
            window.open('/Page/Org/User/ashx/User_Export.ashx?r=' + Math.random());
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
                   部门：<input id="tbUserDept" type="text" style="width:120px;"  onclick="openSelectedDepartDialog();"/>
                   
                    <a href="#" class="btn btn_orange btn_search" onclick="CheckQuery()"><span>查询</span></a>   
                    <span style="margin-right:10px;">
                       <a href="#" onclick="addUser('')" class="btn btn_orange btn_export " style="margin-left: 10px; width: 100px;"><span>添加人员</span></a>
                    </span>
                     <span style="margin-right:10px;">
                       <a href="#" onclick="Import()" class="btn btn_orange btn_export " style="margin-left: 10px; width: 100px;"><span>导入人员</span></a>
                    </span>
                    <span style="margin-right:10px;">
                       <a href="#" onclick="Export()" class="btn btn_orange btn_export " style="margin-left: 10px; width: 100px;"><span>导出人员</span></a>
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
                 function QueryList(userID, userName, deptid) {                     
                     $('#list').datagrid({
                         url: 'ashx/User_List.ashx?RandomID=' + Math.random() + '&userNumber=' + userID + '&userName=' + userName + '&deptID=' + deptid,
                         striped: true,
                         id: 'UserNumber',
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
                                     { field: 'UserNumber', title: '人员工号', width: 100, align: 'center' },
                                     { field: 'UserName', title: '人员姓名', width: 100, align: 'center' },
                                     { field: 'NameJP', title: '姓名简拼', width: 60, align: 'center' },
                                     { field: 'TelNumber', title: '联系电话', width: 100, align: 'center' },
                                     { field: 'DeptFullName', title: '所属部门', width: 200, align: 'center' },
                                     { field: 'PostName', title: '所在岗位', width: 60, align: 'center' },
                                     {
                                         field: 'id', title: '操作', width: 200, align: 'center', formatter: function (val, row) {
                                             return "&nbsp;&nbsp;" +
                                               
                                            "<a href='javacript:;' onclick=\"editUser('" + row.UserNumber + "');\">修改人员</a>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                        "<a href='javacript:;' onclick=\"deleteUser('" + row.UserNumber + "');\">删除人员</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                                         }
                                     }
                         ]]
                     });
                 }
                 QueryList('','','');
        </script>
    </div>
    </form>
</body>
</html>
