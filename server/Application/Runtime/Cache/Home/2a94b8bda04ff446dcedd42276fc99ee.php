<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Log in</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="<?php echo ($res_path); ?>/bootstrap/css/bootstrap.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="<?php echo ($res_path); ?>/css/AdminLTE.min.css">
    <!-- iCheck -->
    <link rel="stylesheet" href="<?php echo ($res_path); ?>/css/blue.css">

</head>
<body class="hold-transition login-page">
<div class="login-box">
    <div class="login-logo">
        <a href="#"><b>Education</b>System</a>
    </div>
    <!-- /.login-logo -->
    <div class="login-box-body">
        <p class="login-box-msg">Sign in to start your session</p>

        <div class="form-group has-feedback">
            <input type="text" class="form-control" placeholder="username" id="username" name='username'>
            <span class="glyphicon glyphicon-user form-control-feedback"></span>
        </div>
        <div class="form-group has-feedback">
            <input type="password" class="form-control" placeholder="Password" id="passwd" name='passwd'>
            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
        </div>
        <div class="row">
            <div class="col-xs-8">
                <div >
                    <!--<button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="true">
                      <span class="fa fa-caret-down"></span></button>-->
                    <select class="btn btn-default dropdown-toggle" id="type">
                        <option value="0">student</option>
                        <option value="1">teacher</option>
                        <option value="2">administrator</option>

                    </select>
                </div>
            </div>
            <!-- /.col -->
            <div class="col-xs-4">
                <button class="btn btn-primary btn-block btn-flat" onclick="return submit()">Sign In</button>
            </div>
            <!-- /.col -->
        </div>

        <!-- /.social-auth-links -->

        <a href="#">I forgot my password</a><br>

    </div>
    <!-- /.login-box-body -->
</div>
<!-- /.login-box -->

<!-- jQuery 2.2.3 -->
<script src="<?php echo ($res_path); ?>/plugins/jQuery/jquery-2.2.3.min.js"></script>
<!-- Bootstrap 3.3.6 -->
<script src="<?php echo ($res_path); ?>/bootstrap/js/bootstrap.min.js"></script>

<script>


    function submit()
    {
        user = $('#username').val();
        pass = $('#passwd').val();
        type = $('#type').val();
        $.post("<?php echo U('Home/Index/login');?>",{'username':user,'password':pass,'type':type},function(msg){

            if(msg=='1'){
                location.href="<?php echo U('Student/Attendance/index');?>";
            }
            else if(msg=='2'){
                location.href="<?php echo U('Teacher/Course/index');?>";
            }
            else if(msg=='3'){
                location.href="<?php echo U('Home/Attendance/index');?>";
            }else if(msg=='0'){
                alert("您输入的密码或账号错误！");
            }

        });
    }
</script>
</body>
</html>