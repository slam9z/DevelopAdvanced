[Master Page ��̽ ](http://blog.csdn.net/zhangleixp/article/details/753151)

Master Page ��ĸ��ҳ����VS.NET2005�е������ԣ��ṩ��ҳ��ģ��Ĺ��ܡ���ģ���Ƕ�̬�ģ�����������ҳ������ѡ��

##1 Master Page�����
Master Page��Ҫ����������ɣ�Master Page��ĸ��ҳ�������һ������Content Page������ҳ����

###1.1ĸ��ҳ��Master Page��

ĸ��ҳ���û��ؼ���User Control�����ƣ���Ҫ�Ĳ�ͬ���У�

1. ĸ��ҳ����չ��Ϊ.master,��Default.master������չ����System.Web.HttpForbiddenHandler ���������˿ͻ������������ֱ�ӷ��ʵ�ĸ��ҳ��
2. ĸ��ҳ��@Masterָ���ǣ�������@Page��@Controlָ�@Master�а�����ָ���@Control�а�����ָ�������ͬ��

	[����ʾ��1]@Masterָ��
	```aspx
	<%@ Master Language="C#" CodeFile="MasterPage.master.cs" Inherits="MasterPage" %>
	```
	
3. ĸ��ҳ���԰������ɸ�ContentPlaceHolder�ؼ�����Щռλ���ؼ�����������ҳ��Content Page����λ�ã���������ҳ���ǡ�
	ͨ��VS.NET�򵼿���ֱ�ӽ���һ��ĸ��ҳ���ͽ����û��ؼ����ơ����ҿ�����ĸ��ҳ�����������Ҫ�������ؼ�����SiteMapPath�ȵȡ�
	```aspx
	[����ʾ��2]һ����ĸ��ҳ��Example.master��
	<%@ Master Language="C#" AutoEventWireup="true" CodeFile="Example.master.cs" Inherits="Example" %>
	 
	<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
	 
	<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<title>ĸ��ҳ����</title>
	</head>
	<body>
		<form id="form1" runat="server">
			<table width=��100%��>
				<tr>
					<td>
						<asp:contentplaceholder id="HeaderHolder" runat="server">
						</asp:contentplaceholder>
					</td>
				</tr>
				<tr>
					<td>
						<asp:ContentPlaceHolder ID="MainHolder" runat="server">
						</asp:ContentPlaceHolder>
					</td>
				</tr>
				<tr>
					<td>
						<asp:ContentPlaceHolder ID="FooterHolder" runat="server">
						</asp:ContentPlaceHolder>
					</td>
				</tr>  
			</table>
		</form>
	</body>
	</html>
	```
	�������ʾ���п��Կ�����ĸ��ҳ�к���<HTML>��<HEAD>��<BODY> �� <FORM> ��Щ����HTMLԪ�أ�������ҳ����û�еġ�



	��Ҫע����ǣ�ĸ��ҳ������ҳ�Ĺ�ϵ���Ǽ̳й�ϵ����Ȼ�ͼ̳е��ص�������MasterPage��Ļ�����UserControl��MasterPage�������Ϊ��public class MasterPage : UserControl ��
	�����Ҫע����ǣ�ĸ��ҳ��֧�����⣨Theme����
	
##1.2����ҳ��Content Page��

����ҳ���������滻ĸ��ҳ�е�ContentPlaceHolder��ASP.NETҳ�棬.aspx��չ������VS.NET�¼�ҳ������ѡ��WebForm����ѡ��Select master page����Ȼ��ѡ�������ҳ��Ӧ��ĸ��ҳ���ɡ�

����ҳͨ��@Pageָ��MasterPageFile��ָ����Ҫʹ�õ�ĸ��ҳ��������ʾ��

```aspx
<%@ Page Language="C#" MasterPageFile="~/Example.master" AutoEventWireup="true" CodeFile="A.aspx.cs" Inherits="A" Title="����ҳA����" %>
```

������ҳ�����Content�ؼ���������ӳ�䵽ĸ��ҳ�ϵ�ContentPlaceHolder��Ȼ����Content�ؼ������������Ҫ���ı��������ؼ���

[����ʾ��3]
```aspx
<%@ Page Language="C#" MasterPageFile="~/Example.master" AutoEventWireup="true" CodeFile="A.aspx.cs" Inherits="A" Title="����ҳA����" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderHolder" Runat="Server">
    Head Style 1.
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainHolder" Runat="Server">
    <p>
        ʹ�� ASP.NET ĸ��ҳ����ΪӦ�ó����е�ҳ����һ�µĲ��֡�����ĸ��ҳ����ΪӦ�ó����е�����ҳ����һ��ҳ�������������ۺͱ�׼��Ϊ��Ȼ����Դ�������Ҫ��ʾ�����ݵĸ�������ҳ�����û���������ҳʱ����Щ����ҳ��ĸ��ҳ�ϲ��Խ�ĸ��ҳ�Ĳ���������ҳ�����������һ�������
    </p>
</asp:Content>
<asp:Content ID="FooterContent" ContentPlaceHolderID="FooterHolder" Runat="Server">
    Powered by <a href="http://zhangleixp.itpub.net/" title="����������ҳ��zhangleixp.itpub.net">zhangleixp</a>
</asp:Content>
```

##2 ĸ��ҳ������ҳ����ϼ�������Ϊ

###2.1 URL

ǰ���Ѿ�˵��������ֱ�ӻ�ȡĸ��ҳ���磺http://202.119.192.211/Example.master �����������ǲ���ȷ�ġ�Ӧ��ʹ������ҳ��URL������ĳ��ҳ�档

###2.2 �ϲ�

���������ĸ��ҳ�����ϲ�������ҳ�Ŀؼ����У�Content�ؼ��е����ݺϲ�����Ӧ��ContentPlaceHolder�ؼ��С���ͼ��

���Կ�����ĸ��ҳ������ҳ��һ���֣��ͺ��û��ؼ�����Ϊ��ͬ�����ǵĹ�ϵ�������ģ�����ҳ��ĸ��ҳ��������ĸ��ҳ����һ��������������������ҳ����ӦContent�еĿؼ���


###2.3 ��ʼ������

* ĸ��ҳ-Init
* ����ҳ-Init
* ����ҳ-Load
* ĸ��ҳ-Load
* ����ҳ-PreRender
* ĸ��ҳ-PreRender

###2.4 ҳ��ִ�л�����URLת��

ĸ��ҳ������ҳ�ϲ���ҳ���ִ�л���Ϊ����ҳ�Ļ����������᲻�ᵼ��ĸ��ҳ����Դ���û����URL���ִ����أ����ڷ������ؼ���ASP.NET�����Զ����������⣬����ĸ��ҳ�ϵ�һ��Image�ؼ�����ImageUrlΪ���URL����images/banner.gif������ĸ��ҳ������ҳ���ʱ��ASP.NET����ת��Ϊ���ʵ�URL�����ڷǷ������ؼ����ǣ���<IMG>��ASP.NET�������κ�ת������ˣ���ĸ��ҳ�У�Ӧ�þ���ʹ�÷������ؼ���

##3 ��̬����ĸ��ҳ

����ҳ�п��Զ�̬������ĸ��ҳ��ͨ����PreInit��������Ҫʹ�õ�ĸ��ҳ�����£�

```cs
protected void Page_PreInit(object sender, EventArgs e)
{
    this.MasterPageFile = "~/Example.master";
}
```

##4 ��ȡĸ��ҳ�ϵĿؼ�

����ʱ��ĸ��ҳ������ҳ�ϲ��ˣ���˿��Է���ĸ��ҳ�ϵĿؼ�������ʹ��FindControl��������λĸ��ҳ�ϵĿؼ������Ҫ����ContentPlaceHolder�еĿؼ�����Ҫ�Ȼ�ȡContentPlaceHolder�ؼ������ã�Ȼ���ٵ�����FindControl������Ҳ����˵��Ҫ���ε���FindControl������
����һ�ַ�������ĸ��ҳ�й������������з��ʡ�

##5 Ƕ��ĸ��ҳ
