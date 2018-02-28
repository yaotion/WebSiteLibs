<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Org_Dept_Dialog.aspx.cs" Inherits="TF.YA.Org.Web.Org.Org_Dept_Dialog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" <%="href='"+TF.YA.Org.Web.FrameUtils.ResourceCss +"'" %>' />
	<script type="text/javascript" src="<%=TF.YA.Org.Web.FrameUtils.ResourceJs %>"></script>	
    <script type="text/javascript">
        function selectDept()
        {
            var idCtrl = document.getElementById("selectedID");;
            var nameCtrl = document.getElementById("selectedName");
            if (idCtrl.value == "")
            {
                alert("请选择部门");
                return;
            } 
            art.dialog.open.origin.selectDept(idCtrl.value, nameCtrl.value);
            art.dialog.close();
        }
        function clearDept() {           
            art.dialog.open.origin.selectDept("", "");
            art.dialog.close();
        }

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div style="margin-left:10px; width:420px;height:240px;">
            <table id="list" style="height:240px;">
            </table>
         <input type="hidden" id="selectedID" value="" />
         <input type="hidden" id="selectedName" value="" />
        </div>      
        <div style="clear: both; height: 25px; padding-top: 10px; padding-right: 20px;float:right;">
                <input type="button" id="btn_Clear" value="清除" title="清除" style="padding: 0; width: 60px; height: 25px;" 
                        onclick="clearDept();" />        
                <input type="button" id="btn_Save" value="确认" title="确认" style="padding: 0; width: 60px; height: 25px;" 
                        onclick="selectDept();" />        
                    <input type="button" id="btn_fb" value="取消" title="取消"  style="width: 60px;
                        height: 25px; margin-left: 15px;" 
                        onclick="art.dialog.close();" />
            </div> 
        <script type="text/javascript">
                
                function showProductTree(rowIndex,rowData) {

                    document.getElementById("selectedID").value = "";
                    document.getElementById("selectedName").value = "";
                    //加载完毕后获取所有的checkbox遍历
                    var radio = document.getElementsByName("deptCheck");
                    
                  
                    for (var i = 0; i < radio.length; i++) {
                        
                        //如果当前的单选框不可选，则不让其选中
                        if (radio[i].id != rowIndex) {
                            //让点击的行单选按钮选中
                            radio[i].checked = false;
                        }
                        else {
                            radio[i].checked = true;
                            document.getElementById("selectedID").value = rowIndex;
                            document.getElementById("selectedName").value = rowData;                            
                        }
                    }
                    
                }
        
                function LoadTree() {
                    $(function () {
                        $("#list").treegrid({
                            url: 'ashx/Org_List.ashx?r=' + Math.random(),
                            idField: 'id',
                            treeField: 'name',                          
                            checkBox: true,
                            columns: [[
                                { title: '部门名称', field: 'name', width: 180, align: 'left' },
                                { field: 'id', title: '部门编号', width: 100, align: 'center' },
                                { field: 'DeptTypeName', title: '部门类型', width: 80, align: 'center' },
                                {
                                    field: 'idField', title: '选择', width: 40,
                                    formatter: function (val,row) {
                                                                     
                                        var d = '<input type="checkbox" name="deptCheck"  onclick="showProductTree(\'' + row.id + '\',\'' + row.name + '\')"  id="' + row.id + '"  />&nbsp;&nbsp;';
                                                                     return d;
                                                                 }
                                }
                            ]]
                            
                        });
                    });
                }
                LoadTree();
        </script> 
    </div>
    </form>
</body>
</html>
