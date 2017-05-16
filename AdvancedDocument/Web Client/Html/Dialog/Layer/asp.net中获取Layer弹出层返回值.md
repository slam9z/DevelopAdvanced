[asp.net中获取Layer弹出层返回值](http://www.aichengxu.com/view/2392171)


1. MainPage.aspx中点击按钮利用Layer弹出层，代码如下：

```aspx
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="demo.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../Scripts/Jquery-1.8.0.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            layer.config({
                extend: ['skin/espresso/style.css'], //加载新皮肤
                skin: 'layer-ext-espresso' //一旦设定，所有弹层风格都采用此主题。
            });
        });

        //弹出框
        function popUp() {
            var UnitCode = "110000";
            layer.open({
                title: ['高级查询'],
                type: 2,
                content: "SubPage.aspx?UnitCode=" + UnitCode,
                area: ['500px', '520px'],
                shade: 0.3,
                btn: ['确定', '关闭'],
                yes: function (index) {
                    var res = window["layui-layer-iframe" + index].saveFunc();
                    if (res != false) {
                        $("#hidSelectUnit").val(res.SelectUnit);
                        $("#hidCompareDate").val(res.CompareDate);
                        $("#hidCompareTips").val(res.CompareTips);
                        $("#form1").submit();
                    }
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" action="?FormSubmit=true">
        <input type="button" id="btnSelect" onclick='popUp();' style="margin-left: 10px; font-family: 微软雅黑; font-size: 14px;" value="高级查询" />
        <asp:HiddenField ID="hidSelectUnit" runat="server" />
        <asp:HiddenField ID="hidCompareDate" runat="server" />
        <asp:HiddenField ID="hidCompareTips" runat="server" />
        <div id="divMsg" runat="server">
        </div>
    </form>
</body>
</html>


protected void Page_Load(object sender, EventArgs e)
{
      if (!string.IsNullOrEmpty(Request.QueryString["FormSubmit"]))
      {
          this.divMsg.InnerHtml = "单位编码：" + hidSelectUnit.Value + "</br>对比日期：" + hidCompareDate.Value.Split(',')[0] + "|" + hidCompareDate.Value.Split(',')[1] + "</br>对比提示：" + hidCompareTips.Value.Split(',')[0] + "|" + hidCompareTips.Value.Split(',')[1];
      }
 }
```

重点代码：

```js
layer.open({
title: ['高级查询'],
type: 2,
content: "SubPage.aspx?UnitCode=" + UnitCode,
area: ['500px', '520px'],
shade: 0.3,
btn: ['确定', '关闭'],
yes: function (index) {
var res = window["layui-layer-iframe" + index].saveFunc();
if (res != false) {
$("#hidSelectUnit").val(res.SelectUnit);
$("#hidCompareDate").val(res.CompareDate);
$("#hidCompareTips").val(res.CompareTips);
$("#form1").submit();
}
}
});
```
2. SubPage.aspx是弹出框内容区域，代码如下：

```aspx
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubPage.aspx.cs" Inherits="demo.SubPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>弹出页面</title>
    <script src="../Scripts/Jquery-1.8.0.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            layer.config({
                extend: ['skin/espresso/style.css'], //加载新皮肤
                skin: 'layer-ext-espresso' //一旦设定，所有弹层风格都采用此主题。
            });
        });

        function saveFunc() {
            var selectedUnit = $("#unitcode").val();
            var compareDate = new Array();
            compareDate[0] = "2015-12-01";
            compareDate[1] = "2015-12-23";
            var compareTips = new Array();
            compareTips[0] = "合肥";
            compareTips[1] = "六安";
            var returnJson = {
                "SelectUnit": selectedUnit,
                "CompareDate": compareDate,
                "CompareTips": compareTips
            }
            return returnJson;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div>
          <input id="unitcode" type="text" />
      </div>
    </form>
</body>
</html>
```