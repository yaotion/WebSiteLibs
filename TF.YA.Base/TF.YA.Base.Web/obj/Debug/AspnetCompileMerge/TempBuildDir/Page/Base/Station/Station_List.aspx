<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Station_List.aspx.cs" Inherits="TF.YA.Base.Web.Base.Station.Station_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link rel="stylesheet" type="text/css" <%="href='"+TF.YA.Org.Web.FrameUtils.ResourceCss +"'" %>' />
	<script type="text/javascript" src="<%=TF.YA.Org.Web.FrameUtils.ResourceJs %>"></script>	

       <script type="text/javascript">           
           function addStation(stationID) {
               artDialogOpen('/Page/Base/Station/Station_Add.aspx?r=' + Math.random() + '&stationID=' + stationID, '添加车站', 400, 200);
           }
           function updateStation(stationID) {
               artDialogOpen('/Page/Base/Station/Station_Add.aspx?r=' + Math.random() + '&stationID=' + stationID, '修改车站', 400, 200);
           }
           function deleteStation(stationID) {
               publicDeleteReload('/Page/Base/Station/ashx/Station_Delete.ashx?r=' + Math.random() + '&stationID=' + stationID, "确认删除该项吗？");
           }
           //页面刷新
           function appdel_do() {
               window.location.reload();
           }

           function CheckQuery() {

               var userID = document.getElementById("tbStationName").value;
               var userName = document.getElementById("tbNameJP").value;

               QueryList(userID, userName);
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
                   车站名称：<input id="tbStationName"type="text" style="width:120px;" />
                   车站简拼：<input id="tbNameJP" type="text" style="width:120px;" />                                      
                    <a href="#" class="btn btn_orange btn_search" onclick="CheckQuery()"><span>查询</span></a>   
                    <span style="float: right; margin-right:10px;">
                        <a href="#" onclick="addStation('0')" class="btn btn_orange btn_export " style="margin-left: 10px; width: 100px;"><span>添加车站</span></a>
                    </span>
                </div>
                                               

            </div>
        </div>    
        <div class="ml10 mt10">
           <div id="list">
           </div>
        </div>
         <script type="text/javascript">
             function QueryList(stationName, nameJP) {
                 var width = $("#SerachDiv").width() + 2;
                 var height = $(window).height() - $("#SerachDiv").height() - 30;
                 $('#list').datagrid({
                     url: 'ashx/Station_List.ashx?RandomID=' + Math.random() + '&stationName=' + stationName + '&nameJP=' + nameJP,
                     striped: true,
                     id: 'nid',
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
                                 { field: 'StationName', title: '车站名称', width: 100, align: 'center' },
                                 { field: 'NameJP', title: '名称简拼', width: 100, align: 'center' },
                                 { field: 'JLNumber', title: '交路号', width: 100, align: 'center' },
                                 { field: 'StationNumber', title: '车站号', width: 100, align: 'center' },
                                 { field: 'TMISNumber', title: 'TMIS号', width: 100, align: 'center' },
                                 {
                                     field: 'id', title: '操作', width: 200, align: 'center', formatter: function (val, row) {
                                         return "&nbsp;&nbsp;" +                                             
                                    "<a href='javacript:;' onclick=\"updateStation('" + row.nid + "');\">修改车站</a>&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                    "<a href='javacript:;' onclick=\"deleteStation('" + row.nid + "');\">删除车站</a>&nbsp;&nbsp;&nbsp;&nbsp;";
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
