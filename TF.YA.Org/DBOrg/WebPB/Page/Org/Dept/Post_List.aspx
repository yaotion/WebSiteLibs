<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Post_List.aspx.cs" Inherits="TF.YA.Org.Web.Org.Post_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" <%="href='"+TF.YA.Org.Web.FrameUtils.ResourceCss +"'" %>' />
	<script type="text/javascript" src="<%=TF.YA.Org.Web.FrameUtils.ResourceJs %>"></script>	

       <script type="text/javascript">
        
           function addPost(postID) {               
               artDialogOpen('/Page/Org/Dept/Post_Add.aspx?r=' + Math.random() + '&postID=' + postID, '添加岗位', 400, 200);
           }
           function editPost(postID) {
               artDialogOpen('/Page/Org/Dept/Post_Add.aspx?r=' + Math.random() + '&postID=' + postID, '修改岗位', 400, 200);
           }
           function deletePost(postID) {
               publicDeleteReload('/Page/Org/Dept/ashx/Org_Post_Del.ashx?r=' + Math.random() + '&postID=' + postID, "确认删除该项吗？");
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
                   <a href="#" onclick="addPost('')" class="btn btn_orange btn_export " style="margin-left: 10px; width: 100px;"><span>添加岗位</span></a>
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
             $('#list').datagrid({
                 url:'ashx/Post_List.ashx?RandomID=' + Math.random(),
                 striped: true,
                 id:'PostID',
                 nowrap: true,
                 pagination: true,
                 rownumbers: true,
                 method: 'post',
                 singleSelect: true,
                 fitColumns: true,
                 height: height,
                 width: width,
                 pageSize: 15,
                 pageList: [10, 15, 20],
                 loadMsg: '数据加载中,请稍候..',
                 columns: [[
                             { field: 'PostID', title: '岗位编号', width: 476, align: 'center' },
                             { field: 'PostName', title: '岗位名称', width: 476, align: 'center' },
                             { field: 'PostType', title: '岗位类型', width: 476, align: 'center' },                             
                             {
                                 field: 'id', title: '操作', width: 303, align: 'center', formatter: function (val, row) {
                                     return "&nbsp;&nbsp;" + 
                                    "<a href='javacript:;' onclick=\"editPost('" + row.PostID + "');\">修改岗位</a>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                "<a href='javacript:;' onclick=\"deletePost('" + row.PostID + "');\">删除岗位</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                           }
                        }
            ]]
        });

       
                  
    </script>

    </div>    
    </form>
</body>
</html>
