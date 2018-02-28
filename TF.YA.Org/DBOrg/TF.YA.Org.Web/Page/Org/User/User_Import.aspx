<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Import.aspx.cs" Inherits="TF.YA.Org.Web.Page.Org.User.User_Import" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" <%="href='"+TF.YA.Org.Web.FrameUtils.ResourceCss +"'" %>' />
    <script type="text/javascript" src="<%=TF.YA.Org.Web.FrameUtils.ResourceJs %>"></script>	
    <script type="text/javascript">
        function uploadFile() {
            {
                var fileUpload = $("#fileupload").get(0);
                var files = fileUpload.files;
                if (files.length == 0) {
                    alert("请选择要导入的文件");
                    return;
                }
                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
                var recover = 0;                            
                if ($('#checkRecover').attr("checked") == "checked")
                {
                    recover = 1;
                }
                data.append("r", recover);
                $('#BtnSave').attr("disabled", "disabled");
                $('#btn_fb').attr("disabled", "disabled");
                $.ajax({
                    url: "/Page/Org/User/ashx/User_Import.ashx",
                    type: "POST",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        alert("导入完成");
                        art.dialog.open.origin.appdel_do();
                    },
                    error: function (err) {
                        alert(err.statusText)
                        $('#BtnSave').attr("disabled", true);
                        $('#btn_fb').attr("disabled", "true");
                    }
                });
            };
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div>                 
      <div style="">
        <div style="padding: 20px; clear: both;">
            <div style="clear: both; padding-top: 5px">
                <div style="float: left; width: 80px; text-align: right; padding-right: 5px;color:red">
                    *
                </div>
                <div style="float: left; color:red">
                    <ul>
                        <li>导入文件总共有六列</li>
                        <li>第一行为标题列不能放入数据，列名可自定义</li>
                        <li>第一列为工号</li>
                        <li>第二列为姓名</li>                      
                        <li>第三列为姓名简拼,可以为空白</li>
                        <li>第四列为姓名</li>
                        <li>第五列为部门全部名称从低到高以逗号隔开</li>
                        <li>第六列为职位，请参考人员职位信息中职位名称</li>
                    </ul>
                </div>
            </div>
            <div style="clear: both; padding-top: 5px">
                <div style="float: left; width: 80px; text-align: right; padding-right: 5px;">
                    导入文件:
                </div>
                <div style="float: left;">                    
                    <input type="file" id="fileupload"  style="width:200px;" />
                </div>                
            </div>       
            <div style="clear: both; padding-top: 5px">
                <div style="float: left; width: 80px; text-align: right; padding-right: 5px;">
                    选项:
                </div>
                <div style="float: left;">   
                    <input type="checkbox" id="checkRecover" />工号存在时覆盖该人员其他信息
                </div>                
            </div>                         
            <div style="clear: both; height: 30px; padding-top: 20px; padding-left: 85px;">                
                <input type="button" id="BtnSave" value="确认" title="取消" style="padding: 0; width: 60px; height: 30px;" onclick="uploadFile();" />
                <input type="button" id="btn_fb" value="取消" title="取消" style="width: 60px; height: 30px; margin-left: 40px;"
                    onclick="art.dialog.close();" />
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
