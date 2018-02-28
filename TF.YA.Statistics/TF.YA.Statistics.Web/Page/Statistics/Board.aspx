<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board.aspx.cs" Inherits="TF.YA.Statistics.Web.Page.Board" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" <%="href='"+TF.YA.Statistics.Web.FrameUtils.ResourceCss +"'" %>' />
	<script type="text/javascript" src="<%=TF.YA.Statistics.Web.FrameUtils.ResourceJs %>"></script>	        

    <script src="JS/highcharts.js"></script>
    <script src="JS/YAStatistics.js"></script>
    
    
    <script type="text/javascript">
        if (screen.width < 1920) {
            document.write('<link href="css/board1440_900.css" rel="stylesheet" />');
        } else {
            document.write('<link href="css/board1920_1080.css" rel="stylesheet" />');
        }

    </script>
    <script type="text/javascript">
        var globalInfo
        function loadGlobal()
        {
            $.ajax({
                type: "GET",
                url: "ashx/ConData.ashx",
                data: "sid=Global&iid=boardTitle&r=" + Math.random(),                              
                success: function (msg) {
                    var tmpString = JSON.parse(msg);
                    if (tmpString == globalInfo)
                        return;
                    if (tmpString == null)
                        return "";
                    
                    globalInfo = tmpString;
                    if (globalInfo.HasData > 0)
                        $("#board_title").html(globalInfo.Data.ItemData);
                    else
                        $("#board_title").html("数据看板");
                    
                }
            });
        }
        var weather;
        function currentTime() {
            var d = new Date(), str = '';
            str += d.getFullYear() + '-';
            str += d.getMonth() + 1 + '-';
            str += d.getDate() + ' ';
            str += d.getHours() + ':';
            str += d.getMinutes() + '';
            return str;
        }
        function loadWeather() {
            $.ajax({
                type: "GET",
                url: "ashx/Weather.ashx",
                data: "r=" + Math.random(),
                success: function (msg) {
                    var tmpString = JSON.parse(msg);
                    if (tmpString == weather)
                        return;
                    if (tmpString == null)
                        return "";
                    weather = tmpString;
                    $("#weather_zhuangkuang").html(weather.ZhuangKuang);
                    $("#weather_wendu").html(weather.WenDu + "℃");
                    $("#weather_fengli").html("风力" + weather.FengLi);
                    $("#weather_shidu").html("湿度" + weather.ShiDu);
                    $("#weather_didian").html(weather.DiDian);
                }
            });
            $("#div_timenow").html(currentTime());
        }
        var trainCount;
        function loadTrainCount() {
            $.ajax({
                type: "GET",
                url: "ashx/TrainCount.ashx",
                data: "r=" + Math.random(),
                success: function (msg) {

                    var tmpString = JSON.parse(msg);
                    if (tmpString == trainCount)
                        return;
                    if (tmpString == null)
                        return "";
                    trainCount = tmpString;

                    $("#jc_peishu").html(trainCount.PeiShuCount + "台");
                    $("#jc_zhipei").html(trainCount.ZhiPeiCount + "台");
                    $("#jc_yunyong").html(trainCount.YunYongCount + "台");
                    $("#jc_feiyong").html(trainCount.FeiYongCount + "台");
                }
            });
        }
        var jcData;
        function loadJC() {

            $.ajax({
                type: "GET",
                url: "ashx/JCTJ.ashx",
                data: "r=" + Math.random(),
                success: function (msg) {

                    if (msg == jcData)
                        return;
                    jcData = msg;
                    var d = JSON.parse(jcData);
                    if (d == null) {
                        return;
                    }

                    var trDates = d.tr.dates;
                    var trDatas = d.tr.datas;


                    InitLine('div_jctr',
                              trDates,
                              trDatas,
                              '#DCDDDF', '#E1CFB2', '#80BEEA', '#CCCCCC', '#EFA52A');
                    InitArea('div_jczx',
                                      d.zx.dates,
                                      d.zx.datas,
                                      '#DCDDDF', '#DCDDDF', '#CCCCCC', '#CCCCCC', '#80BEEA');
                    InitArea('div_jczz',
                                      d.zz.dates,
                                      d.zz.datas,
                                      '#DCDDDF', '#DCDDDF', '#CCCCCC', '#CCCCCC', '#D26857');
                    InitArea('div_jcsd',
                                      d.sd.dates,
                                      d.sd.datas,
                                      '#DCDDDF', '#DCDDDF', '#CCCCCC', '#CCCCCC', '#A2B971');
                    InitArea('div_jccl',
                                      d.cl.dates,
                                      d.cl.datas,
                                      '#DCDDDF', '#DCDDDF', '#CCCCCC', '#CCCCCC', '#AA9CC8');
                }
            });
        }
        var aqyjlist;
        function loadAQYJList() {
            $.ajax({
                type: "GET",
                url: "ashx/ConData.ashx",
                data: "sid=AQYJ&iid=zxyjList&r=" + Math.random(),
                success: function (msg) {
                    if (msg == aqyjlist)
                        return;
                    aqyjlist = msg;
                    var d = JSON.parse(aqyjlist);


                    if ((d == null) || (d == "")) {
                        return;
                    }
                    if (d.HasData > 0)
                        $("#ifrmyjgrid").attr("src", GetRelativeUrl(d.Data.ItemData));
                }
            });
        }
        var aqyj;
        function loadAQYJ() {
            $.ajax({
                type: "GET",
                url: "ashx/AQTJ.ashx",
                data: "r=" + Math.random(),
                success: function (msg) {
                    if (msg == aqyj)
                        return;
                    aqyj = msg;
                    var d = JSON.parse(aqyj);

                    if ((d == null) || (d == "")) {
                        InitPIE('div_yjpie',
                          [],
                          [["报警", 0], ["无报警", 1]],
                          '#DCDDDF', '#DCDDDF', '#CCCCCC', '#CCCCCC', '#AA9CC8');
                        return;
                    }

                    InitPIE('div_yjpie',
                          [],
                          d,
                          '#DCDDDF', '#DCDDDF', '#CCCCCC', '#CCCCCC', '#AA9CC8');
                }
            });




        }

        var ctq;
        function loadCTQ() {

            $.ajax({
                type: "GET",
                url: "ashx/CTQ.ashx",
                data: "r=" + Math.random(),
                success: function (msg) {
                    if (msg == ctq)
                        return;
                    ctq = msg;
                    var d = JSON.parse(ctq);
                    if (d == null) {
                        return;
                    }
                    InitCircle('div_cqpie', '出勤人数', [['出勤', d.CQ], ['其他', d.All]], '#A2B971', '#CDCDCD', d.CQ);

                    InitCircle('div_tqpie', '退勤人数', [['退勤', d.TQ], ['其他', d.All]], '#EFA52A', '#CDCDCD', d.TQ);

                    InitCircle('div_ztpie', '在途人数', [['在途', d.ZT], ['其他', d.All]], '#AA9CC8', '#CDCDCD', d.ZT);
                    var tmp = (220 * d.CQ / d.All);
                    if (tmp >= 220)
                        tmp = 220;
                    $("#div_line_cq").width(tmp);

                    tmp = (220 * d.TQ / d.All);
                    if (tmp >= 220)
                        tmp = 220;
                    $("#div_line_tq").width(tmp);

                    tmp = (220 * d.ZT / d.All);
                    if (tmp >= 220)
                        tmp = 220;
                    $("#div_line_zt").width(tmp);

                }
            });
        }
        var ctqcount;
        function loadCTQCount() {
            $.ajax({
                type: "GET",
                url: "ashx/CTQCount.ashx",
                data: "r=" + Math.random(),
                success: function (msg) {
                    if (msg == ctqcount)
                        return;
                    ctqcount = msg;
                    var d = JSON.parse(ctqcount);
                    if (d == null) {
                        return;
                    }
                    InitColumn('div_ctqtj', d.countDates, d.countDatas, '#DCDDDF', '#FFFFFF', '#80BEEA', ['#EFA52A', '#A2B971'], '#80BEEA');
                }
            });
        }

        function loadCQStep() {
            $.ajax({
                type: "GET",
                url: "ashx/ConData.ashx",
                data: "sid=CWZY&iid=newFlow&r=" + Math.random(),
                success: function (msg) {
                    var d = JSON.parse(msg);

                    if ((d == null) || (d == "")) {
                        return;
                    }
                    if (d.HasData > 0)
                        $("#ifrmCTQLC").attr("src", GetRelativeUrl(d.Data.ItemData));
                }
            });
        }
        function loadDayNow() {
            $.ajax({
                type: "GET",
                url: "ashx/Utils.ashx",
                data: "m=getlcnow",
                success: function (msg) {

                    $("#div_lcnow").html("统计日期:" + msg);
                }
            });
        }
        function loadData() {
            loadGlobal();
            loadWeather();
            loadTrainCount();
            loadJC();
            loadAQYJ();
            loadAQYJList();
            loadCTQ();
            loadCTQCount();
            loadCQStep();
            loadDayNow();
        }
        function bodyonload() {
            loadData();
            $(function () {
                setInterval(aa, 10000);
                function aa() {
                    loadData();
                }
            })
        }
    </script>

    
</head>
<body onload="bodyonload();">
    <form id="form1" runat="server">
     <%--整个区域--%>
    <div class="frameArea">
       <div class="frameTitle">
           <div style="width:54px;float:left;"><img src="/Page/Statistics/Images/Logo.png" /></div><div  class="frameTitleSub"><span id="board_title"></span></div>
       </div>
       <%--数据区域--%>
       <div class="dataArea">
            <%--第一列--%>
            <div class="firstCol">
                <%--天气预报--%>
                <div class="firstColRow firstColRowH1">
                    <div class="firstColTitle">天气预报</div>
                    <div style="">
                        <div id="div_timenow" class="wheatherTime"></div>                        
                        <div class="wheatherZhuangKuang">                                
                            <span id="weather_zhuangkuang" ></span>
                        </div>
                        <div class="wheatherWenDu">
                            <span id="weather_wendu"></span>
                        </div>
                        <div class="wheatherFengLi">                                
                            <span id="weather_fengli"></span>/<span id="weather_shidu"></span>
                        </div>
                        <div class="wheatherDiDian">                               
                            <span id="weather_didian"></span>
                        </div>
                        <div class="wheatherSpaceLine"></div>
                    </div>
                </div>
    
                <%--机车数据库--%>
                <div class="firstColRow firstColRowH2" >
                    <div class="firstColTitle">机车数据库</div>
                    <div class="jcsjCell">
                        <div class="jcsjText" style="background-color:#F05740;">
                            <span id="jc_peishu">0台</span>
                        </div>
                        <div class="jcsjName">配属机车</div>
                    </div>
                    <div class="jcsjCell">
                        <div class="jcsjText" style="background-color:#349c73;">
                            <span id="jc_zhipei">0台</span>
                        </div>
                        <div class="jcsjName">支配机车</div>                              
                    </div>
                   <div class="jcsjCell">
                        <div class="jcsjText" style="background-color:#FF9902;">
                            <span id="jc_yunyong">0台</span>
                        </div>
                        <div class="jcsjName">运转机车</div>                              
                    </div>
                    <div class="jcsjCell">
                        <div class="jcsjText" style="background-color:#349c73;">
                            <span id="jc_feiyong">0台</span>
                        </div>
                        <div class="jcsjName">非运机车</div>                              
                    </div>
                </div>
            
            </div>
            <%--第二列--%>
            <div class="secondColumn">
                <div class="jcyj_title">机车运用统计</div>
                <div class="jcyj_row">
                    <div class="jcyj_s_bg">
                        <div class="jcyj_s_title">机车台日（台日）</div>
                        <div id="div_jctr" class="jcyj_s_chart"></div>
                    </div>
               
                </div>
                <div class="jcyj_row">
                    <div class="jcyj_d_bg"><div class="jcyj_d_title">走行公里（公里）</div>
                        <div id="div_jczx" class="jcyj_d_chart"></div>
                    </div>
                    <div class="jcyj_d_bg">
                        <div class="jcyj_d_title">机车载重（万吨公里）</div>
                        <div id="div_jczz" class="jcyj_d_chart"></div>
                    </div>
                </div>

                <div class="jcyj_row">                
                    <div class="jcyj_d_bg">
                        <div class="jcyj_d_title">技术速度（公里/小时）</div>
                        <div id="div_jcsd" class="jcyj_d_chart"></div>                       
                    </div>
                    <div class="jcyj_d_bg">
                        <div class="jcyj_d_title">台日产量（万吨公里/台日）</div>
                        <div id="div_jccl" class="jcyj_d_chart"></div>
                     </div>
                </div>
            </div>
            <%--第三列--%>
            <div class="ThirdCol">
                <%--安全在线预警--%>
                <div class="ThirdColRow ThirdColRowH1">
                    <div class="aqyjTitle">
                        <div style="float:left">
                            安全在线预警
                        </div>
                        <div id="div_lcnow" style="float:right;">
                            
                        </div>
                    </div>
                    <div style="clear:both;">
                        <div id="div_yjpie" class="aqyjPie"></div>
      
                        <div class="aqyjGrid">
                            <div id="div_yjgrid" >
                                <iframe id="ifrmyjgrid" class="aqyjGridFrame" style="border:none ;overflow:hidden;width:100%;"  src="about:black"></iframe>    
                            </div>

                        </div>
                    </div>
                </div>

                <%--乘务作业--%>
                <div class="ThirdColRow ThirdColRowH2">
                    <div class="ctqTitle">乘务作业</div>
                    <div style="clear:both;">
                        <div id="div_cqpie" class="ctqPie"></div>                        
                        <div id="div_tqpie" class="ctqPie"></div>                        
                        <div id="div_ztpie" class="ctqPie"></div>

                        <div class="ctqLine">
                            <div class="ctqLineItemBg">
                                <div class="ctqLineItemName">出勤</div>
                                <div class="ctqLineItemMax">
                                    <div id="div_line_cq" class="ctqLineItemValue" style="background-color:#A2B971;"></div>
                                </div>
                            </div>
                            <div class="ctqLineItemBg">
                                <div class="ctqLineItemName">退勤</div>
                                <div class="ctqLineItemMax">
                                    <div id="div_line_tq" class="ctqLineItemValue" style="background-color:#EFA52A;"></div>
                                </div>
                            </div>
                            <div class="ctqLineItemBg">
                                <div class="ctqLineItemName">在途</div>
                                <div class="ctqLineItemMax">
                                    <div id="div_line_zt" class="ctqLineItemValue" style="background-color:#AA9CC8;"></div>
                                </div>
                            </div>
                        </div>
               
                    </div>
                    <div class="ThirdColRow ThirdColRowH3" >                                                                       
                        <div class="ctqGrid ctqGridHeight">
                            <div class="ctqGridTitle">
                                车间出退勤班数统计
                            </div>
                            <div id="div_ctqtj" class="ctqGridChart">
                            </div>
                        </div>                                                                                              
                    </div> 
                    <div  class="ThirdColRow ThirdColRowH4">       
                        <div class="ctqGrid ctqGridIFrameH">     
                            <iframe id="ifrmCTQLC" style="border:none ;width:100%;height:100%;overflow:hidden;"  src="about:black"></iframe>    
                        </div>   
                    </div>                                           
                </div>     
            </div>
        </div>

           
    </div>
    </form>
</body>
</html>
