<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Lumino - Dashboard</title>

    <link href="<?php echo ($res_path); ?>/css/bootstrap.min.css" rel="stylesheet">
    <link href="<?php echo ($res_path); ?>/css/datepicker3.css" rel="stylesheet">
    <link href="<?php echo ($res_path); ?>/css/bootstrap-table.css" rel="stylesheet">
    <link href="<?php echo ($res_path); ?>/css/styles.css" rel="stylesheet">

    <!--[if lt IE 9]>
    <script src="<?php echo ($res_path); ?>/js/html5shiv.js"></script>
    <script src="<?php echo ($res_path); ?>//respond.min.js"></script>
    <![endif]-->

</head>
<body>
<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
    <div class="container-fluid">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#sidebar-collapse">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="#"><span>Lumino</span>Admin</a>
            <ul class="user-menu">
                <li class="dropdown pull-right">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-user"></span> User <span class="caret"></span></a>
                    <ul class="dropdown-menu" role="menu">
                        <li><a href="#"><span class="glyphicon glyphicon-user"></span> Profile</a></li>
                        <li><a href="#"><span class="glyphicon glyphicon-cog"></span> Settings</a></li>
                        <li><a href="#"><span class="glyphicon glyphicon-log-out"></span> Logout</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    </div><!-- /.container-fluid -->
</nav>

<div id="sidebar-collapse" class="col-sm-3 col-lg-2 sidebar">
    <form role="search">
        <div class="form-group">
            <input type="text" class="form-control" placeholder="Search">
        </div>
    </form>
    <ul class="nav menu">
        <li class="active"><a href="../Information/index"><span class="glyphicon glyphicon-th"></span> 个人信息</a></li>
        <li><a href="../Attendance/index"><span class="glyphicon glyphicon-th"></span> 个人考勤</a></li>

        <li role="presentation" class="divider"></li>
    </ul>
</div><!--/.sidebar-->

<div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><span class="glyphicon glyphicon-home"></span></a></li>
            <li class="active">个人信息</li>
        </ol>
    </div><!--/.row-->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">个 人 信 息</div>
                <div class="panel-body">
                    <form class="form-horizontal" enctype="multipart/form-data">
                        <div class="form-group">
                            <div id="check1">
                                <label  class="col-lg-2  control-label">学 号</label>
                                <div class="col-lg-3">
                                    <input type="text" class="form-control" value="<?php echo ($stu_data["sid"]); ?>"  disabled>
                                </div>
                            </div>

                            <div id="check2">
                                <label  class="col-lg-2  control-label">姓 名</label>
                                <div class="col-lg-3">
                                    <input type="text" class="form-control" value="<?php echo ($stu_data["sname"]); ?>" disabled >
                                </div>
                            </div>
                        </div>


                        <div class="form-group">
                            <div id="check3">
                                <label  class="col-lg-2 control-label">学 院</label>
                                <div class="col-lg-3">
                                    <input type="text" class="form-control" value="<?php echo ($stu_data["scollege"]); ?>"  disabled>
                                </div>
                            </div>

                            <div id="check4">
                                <label  class="col-lg-2  control-label">专 业</label>
                                <div class="col-lg-3">
                                    <input type="text" class="form-control" value="<?php echo ($stu_data["smajor"]); ?>"  disabled>
                                </div>
                            </div>
                        </div>


                        <div class="form-group">
                            <div id="check5">
                                <label  class="col-lg-2  control-label">班 级</label>
                                <div class="col-lg-3">
                                    <input type="text" class="form-control" value="<?php echo ($stu_data["sclass"]); ?>"  disabled>
                                </div>
                            </div>

                            <div id="check6">
                                <label  class="col-lg-2  control-label">Mac地址</label>
                                <div class="col-lg-3">
                                    <input type="text" class="form-control" value="<?php echo ($stu_data["smac"]); ?>"  disabled>
                                </div>
                            </div>
                        </div>



                        <div class="form-group">
                            <div id="check7">
                                <label  class="col-lg-2 control-label">电 话</label>
                                <div class="col-lg-3">
                                    <input type="text" class="form-control" value="<?php echo ($stu_data["stel"]); ?>" disabled>
                                </div>
                            </div>
                        </div>

                        <div class="form-group"></div>
                        <div class="form-group"></div>


                    </form>
                    <div class="col-lg-2 col-lg-offset-5">
                        <button  class="btn btn-success" data-toggle="modal"
                                 data-target="#changeModal" onclick="edit_before()">
                            <i class="glyphicon glyphicon-plus"></i> 编辑
                        </button>
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>

<!-- 模态框（Modal） 修改-->
<div class="modal fade" id="changeModal" tabindex="-1" role="dialog"
     aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close"
                        data-dismiss="modal" aria-hidden="true">
                    &times;
                </button>
                <h4 class="modal-title">
                    修改信息
                </h4>
            </div>
            <div class="modal-body">
                <form class="form-horizontal" method="post" enctype="multipart/form-data">
                    <div class="form-group">
                        <div>
                            <label  class="col-lg-2 col-lg-offset-1 control-label">Mac地址</label>
                            <div class="col-lg-6">
                                <input type="text" class="form-control" placeholder="请输入Mac"  id="mac" name="mac">
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div>
                            <label  class="col-lg-2 col-lg-offset-1 control-label">电 话</label>
                            <div class="col-lg-6">
                                <input type="text" class="form-control" placeholder="请输入电话"  id="tel" name="tel">
                            </div>
                        </div>
                    </div>

                </form>
            </div>
            <div class="modal-footer">
                <button id="close2" type="button" class="btn btn-default"
                        data-dismiss="modal">关闭
                </button>
                <button type="button" class="btn btn-primary" onclick="edit()">
                    提交更改
                </button>
            </div>
        </div><!-- /.modal-content -->
    </div><!-- /.modal -->
</div>

<script src="<?php echo ($res_path); ?>/js/jquery-1.11.1.min.js"></script>
<script src="<?php echo ($res_path); ?>/js/bootstrap.min.js"></script>
<script src="<?php echo ($res_path); ?>/js/bootstrap-table.js"></script>
<script>

    function edit_before(){
        $("#mac").val('<?php echo ($stu_data["smac"]); ?>');
        $("#tel").val('<?php echo ($stu_data["stel"]); ?>');
    }

    function edit(){
        var mac = $("#mac").val();
        var tel = $("#tel").val();

        $.get("../Information/update",{
            "mac":mac,
            "tel":tel
        },function(data,status){
            if(data["success"]){
                $("#close2").click();
                alert("修改成功！");
                setTimeout("",300);
                window.location.reload();
            }
            else{
                $("#close2").click();
                alert("修改失败！");
                setTimeout("",300);
            }
        });
    }

</script>
</body>
</html>