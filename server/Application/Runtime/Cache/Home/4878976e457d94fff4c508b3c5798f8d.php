<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Lumino - Dashboard</title>

    <link href="<?php echo ($res_path); ?>/css/bootstrap.min.css" rel="stylesheet">
    <link href="<?php echo ($res_path); ?>/css/bootstrap-table.css" rel="stylesheet">
    <link href="<?php echo ($res_path); ?>/css/styles.css" rel="stylesheet">

    <!--[if lt IE 9]>
    <script src="<?php echo ($res_path); ?>/js/html5shiv.js"></script>
    <script src="<?php echo ($res_path); ?>/js/respond.min.js"></script>
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
        <li><a href="../Index/index"><span class="glyphicon glyphicon-dashboard"></span> 概况</a></li>
        <li><a href="../Teacher/index"><span class="glyphicon glyphicon-th"></span> 教师管理</a></li>
        <li><a href="../Student/index"><span class="glyphicon glyphicon-th"></span> 学生管理</a></li>
        <li><a href="../Major/index"><span class="glyphicon glyphicon-list-alt"></span> 专业管理</a></li>
        <li  class="active"><a href="../Class/index"><span class="glyphicon glyphicon-pencil"></span> 班级管理</a></li>
        <li><a href="../Course/index"><span class="glyphicon glyphicon-pencil"></span> 课程管理</a></li>
        <li role="presentation" class="divider"></li>
    </ul>
</div><!--/.sidebar-->

<div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><span class="glyphicon glyphicon-home"></span></a></li>
            <li>班级管理</li>
            <li>班级课程</li>
            <li class="active">成绩添加</li>
        </ol>
    </div><!--/.row-->

    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading"><?php echo ($name); echo ($class); ?> 的 <?php echo ($course_name); ?> 成 绩</div>
                <div class="panel-body">
                    <table id="table" data-toggle="table" data-url=""  data-show-refresh="true" data-show-toggle="true" data-show-columns="true" data-search="true" data-select-item-name="toolbar1" data-pagination="true" data-sort-name="name" data-sort-order="desc">
                        <thead>
                        <tr>
                            <th data-field="state" data-checkbox="true" >Item ID</th>
                            <th data-field="id" data-sortable="true">学 号</th>
                            <th data-field="name">姓 名</th>
                            <th data-field="grade">成 绩</th>
                            <th data-field="edit">编 辑</th>
                        </tr>
                        </thead>
                        <tbody>
                        <?php if(is_array($list)): $i = 0; $__LIST__ = $list;if( count($__LIST__)==0 ) : echo "" ;else: foreach($__LIST__ as $key=>$vo): $mod = ($i % 2 );++$i;?><tr>
                                <td></td>
                                <td><?php echo ($vo["zcy_sno"]); ?></td>
                                <td><?php echo ($vo["zcy_sname"]); ?></td>
                                <td><?php echo ($vo["zcy_ggrade"]); ?></td>
                                <td><button class="btn btn-primary btn-xs" data-toggle="modal"
                                            data-target="#changeModal" onclick="edit_before(<?php echo ($vo["zcy_sno"]); ?>,<?php echo ($course); ?>);">编 辑</button></td>
                            </tr><?php endforeach; endif; else: echo "" ;endif; ?>
                        </tbody>
                    </table>
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
                <h4 class="modal-title" id="myModalLabel">
                    增加班级课程
                </h4>
            </div>
            <div class="modal-body">
                <form class="form-horizontal" method="post" enctype="multipart/form-data">
                    <div class="form-group">
                        <div>
                            <label  class="col-lg-2 col-lg-offset-1 control-label">学 号</label>
                            <div class="col-lg-6">
                                <input type="text" class="form-control" placeholder="请输入学号"  id="student" name="student" disabled>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div>
                            <label  class="col-lg-2 col-lg-offset-1 control-label">课程号</label>
                            <div class="col-lg-6">
                                <input type="text" class="form-control" placeholder="请输入课程号"  id="course" name="course" disabled>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div>
                            <label  class="col-lg-2 col-lg-offset-1 control-label">成 绩</label>
                            <div class="col-lg-6">
                                <input type="text" class="form-control" placeholder="请输入成绩"  id="grade" name="grade">
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
    //编辑前
    function edit_before(sno,cno){
        $.get("../Class/editBefore",{
            "student":sno,
            "course":cno
        },function(data,status){
            $("#student").val(data[0]['zcy_sno']);
            $("#course").val(data[0]['zcy_cno']);
            $("#grade").val(data[0]['zcy_ggrade']);
        });
    }

    function edit(){
        var grade = $("#grade").val();
        var student = $("#student").val();
        var course = $("#course").val();
        $.get("../Class/update",{
            "course":course,
            "student":student,
            "grade":grade
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