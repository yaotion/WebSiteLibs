<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Org_List.aspx.cs" Inherits="TF.YA.Org.Web.Page.Org.Dept.Org_List" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   
    <link rel="stylesheet" type="text/css" <%="href='"+TF.YA.Org.Web.FrameUtils.ResourceCss +"'" %>' />
	<script type="text/javascript" src="<%=TF.YA.Org.Web.FrameUtils.ResourceJs %>"></script>	
    
    
    <script type="text/javascript">
        function addDept(parentID) {
            artDialogOpen('/Page/Org/Dept/Org_Dept_Add.aspx?r=' + Math.random() + '&parentID=' + parentID, '添加部门', 400, 240);
        }
        function editDept(deptID) {
            artDialogOpen('/Page/Org/Dept/Org_Dept_Add.aspx?r=' + Math.random() + '&deptID=' + deptID, '修改部门', 400, 240);
        }
        function editData(deptID)
        {
            artDialogOpen('/Page/Org/Dept/Org_Dept_Data.aspx?r=' + Math.random() + '&deptID=' + deptID, '完善数据', 360, 320);
        }
        function deleteDept(deptID) {
            publicDeleteReload('/Page/Org/Dept/ashx/Org_Dept_Del.ashx?r=' + Math.random() + '&deptID=' + deptID, "确认删除该项吗？");
        }
        //页面刷新
        function appdel_do() {
            window.location.reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>   
        <div id="SerachDiv" style="margin: 10px; border: 1px #DDDDDD solid; background-color: #FBFBFB; width: 98%;">
            <div style="width: 98%; margin-left: 10px; height: 55px; line-height: 55px; vertical-align: middle; border-radius: 5px;">            
                <span style="float: right; margin-top: 10px;">
                   <a href="#" onclick="addDept('',0)" class="btn btn_orange btn_export " style="margin-left: 10px; width: 100px;"><span>添加部门</span></a>
                </span>

            </div>
        </div>    
        <div style="margin-left:10px;">
            <table id="list">
            </table>
        </div>       
            <script type="text/javascript">
                function LoadTree() {
                    $(function () {
                        $("#list").treegrid({

                            url: 'ashx/Org_List.ashx?r=' + Math.random(),
                            idField: 'id',
                            treeField: 'name',
                            columns: [[

                                { title: '部门名称', field: 'name', width: 180, align: 'left' },
                                { field: 'id', title: '部门编号', width: 100, align: 'center' },
                                { field: 'DeptTypeName', title: '部门类型', width: 80, align: 'center' },
                                {
                                    field: 'DeptData', title: '部门信息', width: 500, align: 'left',                                   
                                }
                                ,
                                
                                {
                                    field: '_parentId', title: '操作', align: 'left', width: 250,
                                    //添加超级链 
                                    formatter: function (val, row) {
                                        return "&nbsp;&nbsp;<a href='javacript:;' onclick=\"addDept('" + row.id + "');\">添加下属部门</a>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                             "<a href='javacript:;' onclick=\"editData('" + row.id + "');\">完善数据</a>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                            "<a href='javacript:;' onclick=\"editDept('" + row.id + "');\">修改</a>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                        "<a href='javacript:;' onclick=\"deleteDept('" + row.id + "');\">删除</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                                    }
                                }
                            ]],
                            onLoadSuccess: function (row, data) {

                            }
                        });
                    });
                }
                LoadTree();
        </script> 
    </div>
    </form>
</body>
</html>

