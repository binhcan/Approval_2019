<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Approval.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
  <title>Approval | Đăng nhập</title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
  <!-- Bootstrap 3.3.7 -->
  <link rel="stylesheet" href="../../bower_components/bootstrap/dist/css/bootstrap.min.css"/>
  <!-- Font Awesome -->
  <link rel="stylesheet" href="../../bower_components/font-awesome/css/font-awesome.min.css"/>
  <!-- Ionicons -->
  <link rel="stylesheet" href="../../bower_components/Ionicons/css/ionicons.min.css"/>
  <!-- Theme style -->
  <link rel="stylesheet" href="../../dist/css/AdminLTE.min.css"/>
  <!-- iCheck -->
  <link rel="stylesheet" href="../../plugins/iCheck/square/blue.css"/>



</head>
    
<body class="hold-transition login-page">
<form id="formlogin" runat="server">
<div class="login-box">
  <div class="login-logo">
    <a href="../../index2.html"><b>Hệ thống xin cấp</b></a>
  </div>
  <!-- /.login-logo -->
  <div class="login-box-body">
    <p class="login-box-msg">Đăng nhập hệ thống</p>

   <%-- <form action="../../index2.html" method="post">--%>
      <div class="form-group has-feedback">
        <asp:TextBox ID="txtusername" class="form-control" runat="server" placeholder="Tên đăng nhập" autofocus=""></asp:TextBox>
        <%--<input type="email" class="form-control" placeholder="Tên đăng nhập"/>--%>
        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
          <asp:TextBox ID="txtpassword" class="form-control" runat="server" type="password" placeholder="Mật khẩu" value="" autofocus=""></asp:TextBox>
        <%--<input type="password" class="form-control" placeholder="Mật khẩu"/>--%>
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
      <div class="row">
        <div class="col-xs-8">
          <div class="checkbox icheck">
            <label>
              <input type="checkbox"/> Lưu thông tin
            </label>
          </div>
        </div>
        <!-- /.col -->
        <div class="col-xs-4">
            <asp:Button ID="bttLogin" class="btn btn-primary" runat="server" Text="Đăng nhập" OnClick="bttLogin_Click" />
          <%--<button type="submit" class="btn btn-primary btn-block btn-flat">Đăng nhập</button>--%>
        </div>
        <!-- /.col -->
      </div>



  </div>
  <!-- /.login-box-body -->
</div>
<!-- /.login-box -->

<!-- jQuery 3 -->
<script src="../../bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src="../../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- iCheck -->
<script src="../../plugins/iCheck/icheck.min.js"></script>
<script>
  $(function () {
    $('input').iCheck({
      checkboxClass: 'icheckbox_square-blue',
      radioClass: 'iradio_square-blue',
      increaseArea: '20%' /* optional */
    });
  });
</script>
    </form>
</body>
</html>
