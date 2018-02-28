<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DutyPlace_List.aspx.cs" Inherits="TF.YA.Base.Web.Base.DutyPlace.DutyPlace_List1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" <%="href='"+TF.YA.Org.Web.FrameUtils.ResourceCss +"'" %>' />
	<script type="text/javascript" src="<%=TF.YA.Org.Web.FrameUtils.ResourceJs %>"></script>	

       <script type="text/javascript">
           
           function addPlace(placeID) {
               artDialogOpen('/Page/Base/DutyPlace/DutyPlace_Add.aspx?r=' + Math.random() + '&placeID=' + placeID, '添加地点', 400, 200);
           }
           function editPlace(placeID) {
               artDialogOpen('/Page/Base/DutyPlace/DutyPlace_Add.aspx?r=' + Math.random() + '&placeID=' + placeID, '修改地点', 400, 200);
           }
           function deletePlace(placeID) {
               publicDeleteReload('/Page/Base/DutyPlace/ashx/DutyPlace_Delete.ashx?r=' + Math.random() + '&placeID=' + placeID, "确认删除该项吗？");
           }
           //页面刷新
           function appdel_do() {
               window.location.reload();
           }

           function CheckQuery() {

               QueryList();
           }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="SerachDiv" style="margin: 10px; border: 1px #DDDDDD solid; background-color: #FBFBFB; width: 98%;">
            <div style="width: 98%; margin-left: 10px; height: 55px; line-height: 55px; vertical-align: middle; border-radius: 5px;">
                 <input type="hidden" value ="" id="tbUserDeptID" />
                <div style="margin-left: 10px; height: 55px; line-height: 55px; vertical-align: middle; border-radius: 5px;">                  
                    <span style="float: right; margin-right:10px;">
                        <a href="#" onclick="addPlace('')" class="btn btn_orange btn_export " style="margin-left: 10px; width: 100px;"><span>添加地点</span></a>
                    </span>
                </div>
                                               

            </div>
        </div>    
        <div class="ml10 mt10">
           <div id="list">
           </div>
        </div>
         <script type="text/javascript">
             function QueryList() {
                 var width = $("#SerachDiv").width() + 2;
                 var height = $(window).height() - $("#SerachDiv").height() - 30;
                 $('#list').datagrid({
                     url: 'ashx/DutyPlace_List.ashx?RandomID=' + Math.random(),
                     striped: true,
                     id: 'PlaceID',
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
                                 { field: 'PlaceID', title: '地点编号', width: 476, align: 'center' },
                                 { field: 'PlaceName', title: '地点名称', width: 476, align: 'center' },
                                 {
                                     field: 'id', title: '操作', width: 303, align: 'center', formatter: function (val, row) {
                                         return "&nbsp;&nbsp;" +
                                          "<a href='javacript:;' onclick=\"editPlace('" + row.PlaceID + "');\">修改地点</a>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                    "<a href='javacript:;' onclick=\"deletePlace('" + row.PlaceID + "');\">删除地点</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                                     }
                                 }
                     ]]
                 });

             }
             QueryList('', '');
    </script>
    </div>
    </form>
</body>
</html>
