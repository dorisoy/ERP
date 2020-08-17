<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mytest.aspx.cs" Inherits="PaiXie.Erp.mytest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

 <script src="Scripts/jquery-1.8.2.min.js"></script>
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
  
    <script src="Scripts/showTip.js"></script>
 <%--  <script>
       function ss()
       {   alert("hello");
           func();}
    
    
       function func() {
           window.location.href = "https://www.baidu.com/";
        
       }

   </script>--%>
</head>
<body onload="ss();">


    <div id="a1111" style="display:none;">ewewewewew</div>
      <input type="button" id="bb" value="我是hambert，哈哈哈哈哈哈~~~" />
       
       <div id="main">
        <input type="text" id="txtMsg" value="我是hambert，哈哈哈哈哈哈~~~" class="text-info" />
        <input type="button" id="btnSuccess" value="显示成功信息" class="btn btn-success" />
        <input type="button" id="btnFailure" value="显示失败信息" class="btn btn-danger" />
        <input type="button" id="btnInfo" value="显示普通信息" class="btn btn-info" />
        <input type="button" id="btnWarn" value="显示警告信息" class="btn btn-warning" />
    </div>
   
      <script type="text/javascript">
          $('#btnSuccess').on('click', function () {
              alert("11");
              ShowSuccess($('#txtMsg').val());
          });
          $('#btnFailure').on('click', function () {
              ShowFailure($('#txtMsg').val());
          });
          $('#btnInfo').on('click', function () {
              ShowMsg($('#txtMsg').val());
          });
          $('#btnWarn').on('click', function () {
              ShowWarn($('#txtMsg').val());
          });
    </script>
    <form id="form1" runat="server">
    <div>
    添加：
        <asp:Button ID="Button1" runat="server" OnClick="add_Click" Text="add" />
        <br />
        删除
         <asp:Button ID="Button3" runat="server" OnClick="del_Click" Text="del" />
         <br />
        更新
           <asp:Button ID="Button4" runat="server" OnClick="update_Click" Text="update" />
         <br />
         获取单条记录

           <asp:Button ID="Button5" runat="server" OnClick="getmodel_Click" Text="getmodel" />
         <br />
       获取列表 外键
          <asp:Button ID="Button6" runat="server" OnClick="getlist_Click" Text="getlist" />
         <br />
       获取记录数
          <asp:Button ID="Button7" runat="server" OnClick="getcount_Click" Text="getcount" />


         <br />
        分页

        page:
        <asp:TextBox ID="TextBox1" runat="server">1</asp:TextBox>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="getpage" />
    

        <br /><asp:Button ID="Button8" runat="server" Text="获取拍鞋网订单" OnClick="Button8_Click" />
    </div>
    </form>


       <script>


           //刷新
           $("#bb").click(function () {
               //   $("#a1").attr("display", "block");
               // $("#a1111").css("display", "block");
               $("#a1111").show();


           });
    </script>
</body>
</html>
