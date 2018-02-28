<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Station_List.aspx.cs" Inherits="TF.YA.Base.Web.Base.Station.Station_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link rel="stylesheet" type="text/css" <%="href='"+TF.YA.Base.Web.FrameUtils.ResourceCss +"'" %>' />
	<script type="text/javascript" src="<%=TF.YA.Base.Web.FrameUtils.ResourceJs %>"></script>	
       <script type="text/javascript">           
    
           function addStation(sname) {
               artDialogOpen('/Page/Base/Station/Station_Add.aspx?r=' + Math.random() + '&sname=' + sname, '添加车站', 400, 300);
           }
           function updateStation(sname) {
               artDialogOpen('/Page/Base/Station/Station_Add.aspx?r=' + Math.random() + '&sname=' + sname, '修改车站', 400, 300);
           }
           function deleteStation(sname) {
               publicDeleteReload('/Page/Base/Station/ashx/Station_Delete.ashx?r=' + Math.random() + '&sname=' + sname, "确认删除该项吗？");
           }
           //页面刷新
           function appdel_do() {
               window.location.reload();
           }

           function CheckQuery() {

               var userID = document.getElementById("tbStationName").value;
               var userName = document.getElementById("tbNameJP").value;
               var jl = document.getElementById("tbJL").value;
               var tmis = document.getElementById("tbTMIS").value;
               QueryList(userID, userName,jl,tmis);
           }

           function exportTo()
           {               
               window.open('/Page/Base/Station/ashx/Station_Export.ashx');
           }
           function Import_S()
           {
               artDialogOpen('/Page/Base/Station/Station_Import_S.aspx?r=' + Math.random(), '导入车站_单行', 450, 300);
               return false;
           }
           function Import_M() {
               artDialogOpen('/Page/Base/Station/Station_Import_M.aspx?r=' + Math.random(), '导入车站_合并', 450, 300);
               return false;
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
                   车站名称：<input id="tbStationName"type="text" style="width:120px;"  value=""/>
                   车站简拼：<input id="tbNameJP" type="text" style="width:120px;"  value=""/>     
                   交路号：<input id="tbJL" type="text" style="width:120px;"  value=""/>   
                   TMIS号：<input id="tbTMIS" type="text" style="width:120px;" value=""/>                                    
                    <a href="#" class="btn btn_orange btn_search" onclick="CheckQuery()"><span>查询</span></a>   
                    <span style="margin-left:10px;">
                        <a href="#" onclick="addStation('')" class="btn btn_orange btn_export " style="margin-left: 10px; width: 100px;"><span>添加车站</span></a>
                    </span>
                    <span style="margin-left:10px;">
                        <a href="#" onclick="exportTo();" class="btn btn_orange btn_export " style="margin-left: 10px; width: 100px;"><span>导出</span></a>
                    </span>
                    <span  style="margin-top: 10px; margin-left:10px;">
                        <asp:Button ID="btnImport_S" runat="server" Text="数据导入(单行)" OnClientClick="return Import_S()" /></span>

                    <span  style="margin-top: 10px;margin-left:10px;">
                        <asp:Button ID="btInPut" runat="server" Text="数据导入(合并)" OnClientClick="return Import_M()"/></span>
                    
                </div>
                                               

            </div>
        </div>    
        <div class="ml10 mt10">

           <div id="list">
           </div>
        </div>
         <script type="text/javascript">
             function QueryList(stationName, nameJP, jl, tmis) {                 
                 var width = $("#SerachDiv").width() + 2;
                 var height = $(window).height() - $("#SerachDiv").height() - 30;
                 $('#list').datagrid({
                     url: 'ashx/Station_List.ashx?RandomID=' + Math.random() + '&stationName=' + stationName + '&nameJP=' + nameJP + '&jl=' + jl + '&tmis=' + tmis,
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
                     pageSize: 50,
                     pageList: [50],
                     loadMsg: '数据加载中,请稍候..',
                     columns: [[
                                 { field: 'StationName', title: '车站名称', width: 100, align: 'center' },
                                 { field: 'NameJP', title: '名称简拼', width: 100, align: 'center' },
                                 {
                                     field: 'StationNumber', title: '车站号(交路号-车站号-TMIS号)', width: 300, align: 'left', formatter: function (val, row)
                                     {
                                         return "&nbsp;&nbsp;&nbsp;&nbsp;" + row.StationNumber.replace(/,/g, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                                     }
                                 },
                                 {
                                     field: 'id', title: '操作', width: 200, align: 'center', formatter: function (val, row) {
                                         return "&nbsp;&nbsp;" +                                             
                                    "<a href='javacript:;' onclick=\"updateStation('" + row.StationName + "');\">修改车站</a>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                    "<a href='javacript:;' onclick=\"deleteStation('" + row.StationName + "');\">删除车站</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                                     }
                                 }
                     ]]
                 });

             }
             QueryList('', '','','');
    </script>
    </div>
    </form>
</body>
</html>
