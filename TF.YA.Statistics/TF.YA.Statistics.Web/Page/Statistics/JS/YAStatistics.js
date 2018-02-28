
function InitLine(containID,categories,data,bgColor,areaBgColor,borderColor,gridColor,lineColor) {
    $(document).ready(function () {
        var title = { text: '' };
        var subtitle = { text: '' };
        var xAxis = {
            categories: categories,//['11.25', '11.26', '11.27', '11.28', '11.29', '11.30', '12.01', '12.02', '12.03', '12.04'],
            lineColor: borderColor,
            gridLineColor: gridColor,
            gridLineWidth: 1
        };
        var yAxis = {
            title: { text: '', enabled: false },
            plotLines: [{ value: 0, width: 1, color: lineColor }],
            gridLineColor: gridColor,
            gridLineWidth: 1
        };
        var tooltip = { enabled: false }
        var legend = { enabled: false };
        var series = [{
            name: ' ', data: data //[9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6] 
        }];
        var credits = [{ enabled: false }];
        var chart = { type: 'line', backgroundColor: bgColor, plotBackgroundColor: areaBgColor, plotBorderColor: borderColor, plotBorderWidth: 1 };
        var colors = [lineColor];
        var json = {};
        var plotOptions = {
            line: {
                dataLabels: {
                    enabled: true,
                    useHTML: true,//标签作为HTML
                    style: {
                        color: '#777777',
                        fontSize: '9px',                        
                        fontFamily: 'Arial'
                    }
                },
                enableMouseTracking: false
            }
        }
        json.plotOptions = plotOptions;
        json.title = title;
        json.subtitle = subtitle;
        json.xAxis = xAxis;
        json.yAxis = yAxis;
        json.tooltip = tooltip;
        json.legend = legend;
        json.series = series;
        json.credits = credits;
        json.chart = chart;
        json.colors = colors;
        $('#' + containID).highcharts(json);
    });
}


function InitArea(containID, categories, data, bgColor, areaBgColor, borderColor, gridColor, lineColor) {

    $(document).ready(function () {
        var chart = {
            backgroundColor: bgColor,
            plotBackgroundColor: areaBgColor,
            plotBorderColor: gridColor,
            plotBorderWidth: 1
        };
        var title = {
            text: '',
            enabled: false

        };
        var subtitle = {
            text: '',
            enabled: false
        };
        var xAxis = {
            categories: categories,
            gridLineColor: gridColor,
            gridLineWidth: 1,
            lineColor: borderColor
        };
        var yAxis = {
            title: {
                text: ''
            },
            gridLineColor: gridColor,
            gridLineWidth: 1
        };
        var legend = {
            enabled: false
        };
        var plotOptions = {
            area: {
                dataLabels: {
                    enabled: true,
                    useHTML: true,//标签作为HTML
                    style: {
                        color: '#777777',
                        fontSize: '9px',                        
                        fontFamily: 'Arial'
                    }
                },
                enableMouseTracking: false,
                fillColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                       [0, lineColor],
                       [1, lineColor]
                    ]

                },
                marker: {
                    radius: 2
                },
                lineWidth: 1,
                states: {
                    hover: {
                        lineWidth: 1
                    }
                },
                threshold: null
            }
        };
        var series = [{
            type: 'area',
            data: data
        }
        ];
        var credits = [{ enabled: false }];
        var json = {};
        json.chart = chart;
        json.title = title;
        json.subtitle = subtitle;
        json.legend = legend;
        json.xAxis = xAxis;
        json.yAxis = yAxis;
        json.series = series;
        json.plotOptions = plotOptions;
        json.credits = credits;
        json.colors = [];
        
        $('#' + containID).highcharts(json);

    });
}

function InitPIE(containID, categories, data, bgColor, areaBgColor, borderColor, gridColor, lineColor) {

    $(document).ready(function () {
        var chart = {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        };
        var title = {
            text: ''
        };
        var tooltip = {
            pointFormat: ''
        };
        var plotOptions = {
            pie: {
                allowPointSelect: false,
                showInLegend: true,
                cursor: 'pointer',
                size: 120,
                innerSize: 60,
                dataLabels: {
                    enabled: false,
                    format: '<b>{point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black',
                        fontSize: 9
                    }
                }
            }
        };
        var series = [{
            type: 'pie',
            name: '',
            data: data
        }];
        var credits = [{ enabled: false }];
        var legend = {
            enabled: true,
            labelFormatter: function () { return this.name + ":" + Highcharts.numberFormat(this.percentage, 1) + '%' }

        };
        var json = {};
        json.chart = chart;
        json.title = title;
        json.tooltip = tooltip;
        json.series = series;
        json.plotOptions = plotOptions;
        json.credits = credits;
        json.legend = legend;
        $('#' + containID).highcharts(json);
    });
}

function InitCircle(containID, circleName,data,dataColor,NullColor,circleTxt)
{
    $(document).ready(function () {
        var chart = {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        };
        var title = {
            floating: true,
            text: circleTxt,
            style: {
                fontSize: "16px",
                lineHeight: "80px"
            }
        };
        var tooltip = {
            pointFormat: ''
        };
        var plotOptions = {
            pie: {
                allowPointSelect: false,
                showInLegend: false,
                cursor: 'pointer',
                size: 70,
                innerSize: 50,
                dataLabels: {
                    enabled: false
                }               
            }
        };
        var series = [{
            type: 'pie',
            name: circleName,
            data: data
        }];
        var credits = [
        {
            enabled: false
        }];
        var legend = {
            enabled: false
        };
        var colors = [dataColor, NullColor];
        var json = {};
        json.chart = chart;
        json.title = title;
        json.tooltip = tooltip;
        json.series = series;
        json.plotOptions = plotOptions;
        json.credits = [];
        json.legend = legend;
        json.colors = colors;
        $('#' + containID).highcharts(json, function (c) {

            // 环形图圆心
            var centerX = c.series[0].center[0];
            var centerY = c.series[0].center[1];
            var txt = circleTxt;
            // 标题字体大小，返回类似 16px ，所以需要 parseInt 处理
            var titleHeight = parseInt(c.title.styles.fontSize);

            c.setTitle({
                text: txt,
                y: centerY + titleHeight / 2
            });

            chart = c;
        });
    });
}


function InitColumn(containID, categories, data, bgColor, areaBgColor, borderColor, columnColors, lineColor)
{
    $(document).ready(function () {

        var title = {
            text: ''
        };
        var subtitle = {
            text: ''
        };
        var xAxis = {
            categories: categories,
            lineColor: lineColor
        };
        var yAxis = {
            title: {
                text: '',
                enabled: false
            }
        };

        var tooltip = {
            enabled: false
        }

        var legend = {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0,
            enabled: false
        };

        var series = data;
        var credits = [
            {
                enabled: false
            }
        ];
        var chart =
                        {
                            type: 'column',
                            backgroundColor: bgColor,
                            plotBackgroundColor: areaBgColor,
                            plotBorderColor: borderColor,
                            plotBorderWidth: 1
                        }
        ;
        var plotOptions = {
            column: {
                dataLabels: {
                    enabled: true,
                    useHTML: true,//标签作为HTML
                    style: {
                        color: '#777777',
                        fontSize: '6px',
                        fontFamily: 'Arial'
                    }
                },
                enableMouseTracking: false
            }
        }
        
        var colors = columnColors;

        var json = {};
        json.plotOptions = plotOptions;
        json.title = title;
        json.subtitle = subtitle;
        json.xAxis = xAxis;
        json.yAxis = yAxis;
        json.tooltip = tooltip;
        json.legend = legend;
        json.series = series;
        json.credits = credits;
        json.chart = chart;
        json.colors = colors;
        $('#' + containID).highcharts(json);
    });
}