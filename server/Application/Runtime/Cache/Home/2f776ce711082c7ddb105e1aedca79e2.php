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
            <li><a href="../Attendance/index"><span class="glyphicon glyphicon-th"></span> 考勤管理</a></li>
            <li><a href="../Class/index"><span class="glyphicon glyphicon-th"></span> 课程情况管理</a></li>
            <li><a href="../Course/index"><span class="glyphicon glyphicon-list-alt"></span> 课程管理</a></li>
            <li><a href="../Student/index"><span class="glyphicon glyphicon-pencil"></span> 学生管理</a></li>
            <li class="active"><a href="../Teacher/index"><span class="glyphicon glyphicon-pencil"></span> 教师管理</a></li>
            <li role="presentation" class="divider"></li>
        </ul>
    </div><!--/.sidebar-->

    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
        <div class="row">
            <ol class="breadcrumb">
                <li><a href="#"><span class="glyphicon glyphicon-home"></span></a></li>
                <li class="active">教师管理</li>
            </ol>
        </div><!--/.row-->


        <div class="row">
            <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">教 师 情 况</div>
                <div class="panel-body">
                    <div id="toolbar" >
                        <button id="remove" class="btn btn-danger" disabled>
                            <i class="glyphicon glyphicon-remove"></i> 删除
                        </button>
                        <button id="add" class="btn btn-success" data-toggle="modal"
                                data-target="#myModal">
                            <i class="glyphicon glyphicon-plus"></i> 增加
                        </button>
                    </div>
                    <table id="table" data-toggle="table" data-url=""  data-show-refresh="true" data-show-toggle="true" data-show-columns="true" data-search="true" data-select-item-name="toolbar1" data-pagination="true" data-sort-name="name" data-sort-order="desc">
                        <thead>
                        <tr>
                            <th data-field="state" data-checkbox="true" >Item ID</th>
                            <th data-field="id" data-sortable="true">编 号</th>
                            <th data-field="name">姓 名</th>
                            <th data-field="sex">性 别</th>
                            <th data-field="passwd">密 码</th>
                            <th data-field="edit">编 辑</th>
                        </tr>
                        </thead>
                        <tbody>
                            <?php if(is_array($list)): $i = 0; $__LIST__ = $list;if( count($__LIST__)==0 ) : echo "" ;else: foreach($__LIST__ as $key=>$vo): $mod = ($i % 2 );++$i;?><tr>
                                <td></td>
                                <td><?php echo ($vo["tid"]); ?></td>
                                <td><?php echo ($vo["tname"]); ?></td>
                                <td><?php if((vo.tsex == true)): ?>男
                                    <?php else: ?>女<?php endif; ?>
                                </td>
                                <td><?php echo ($vo["tpassword"]); ?></td>
                                <td><button class="btn btn-primary btn-xs" data-toggle="modal"
                                            data-target="#changeModal" onclick="edit_before(<?php echo ($vo["tid"]); ?>);">编 辑</button></td>
                            </tr><?php endforeach; endif; else: echo "" ;endif; ?>
                        </tbody>
                    </table>
                </div>
            </div>
            </div>
        </div>

    </div>

    <!-- 模态框（Modal） 增加-->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog"
         aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close"
                            data-dismiss="modal" aria-hidden="true">
                        &times;
                    </button>
                    <h4 class="modal-title" id="myModalLabel">
                        增加教师
                    </h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" method="post" action="../Teacher/add" enctype="multipart/form-data">
                        <div class="form-group">
                            <div id="check1">
                                <label  class="col-lg-2 col-lg-offset-1 control-label">编 号</label>
                                <div class="col-lg-6">
                                    <input type="text" class="form-control" placeholder="请输入编号"  id="id" name="id">
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div id="check2">
                                <label  class="col-lg-2 col-lg-offset-1 control-label">姓 名</label>
                                <div class="col-lg-6">
                                    <input type="text" class="form-control" placeholder="请输入姓名"  id="name" name="name" >
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div id="check3">
                                <label  class="col-lg-2 col-lg-offset-1 control-label">性 别</label>
                                <div class="col-lg-5">
                                    <select class="form-control" name="sex" id="sex">
                                        <option value='1'>男</option>
                                        <option value='0'>女</option>
                                    </select>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div id="check4">
                                <label  class="col-lg-2 col-lg-offset-1 control-label">密 码</label>
                                <div class="col-lg-6">
                                    <input type="text" class="form-control" placeholder="请输入密码"  id="mac" name="mac">
                                </div>
                            </div>
                        </div>



                    </form>
                </div>
                <div class="modal-footer">
                    <button id="close" type="button" class="btn btn-default"
                            data-dismiss="modal">关闭
                    </button>
                    <button type="button" class="btn btn-primary" onclick="add()">
                        提交更改
                    </button>
                </div>
            </div><!-- /.modal-content -->
        </div><!-- /.modal -->
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
                        修改教师
                    </h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" method="post" enctype="multipart/form-data">
                        <div class="form-group">
                            <div>
                                <label  class="col-lg-2 col-lg-offset-1 control-label">编 号</label>
                                <div class="col-lg-6">
                                    <input type="text" class="form-control" placeholder="请输入编号"  id="eid" name="eid">
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div>
                                <label  class="col-lg-2 col-lg-offset-1 control-label">姓 名</label>
                                <div class="col-lg-6">
                                    <input type="text" class="form-control" placeholder="请输入姓名"  id="ename" name="ename" >
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div>
                                <label  class="col-lg-2 col-lg-offset-1 control-label">性 别</label>
                                <div class="col-lg-5">
                                    <select class="form-control" name="esex" id="esex">
                                        <option value='1'>男</option>
                                        <option value='0'>女</option>
                                    </select>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div>
                                <label  class="col-lg-2 col-lg-offset-1 control-label">密 码</label>
                                <div class="col-lg-6">
                                    <input type="text" class="form-control" placeholder="请输入密码"  id="epasswd" name="epasswd">
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
        var $remove = $('#remove');
        var $table = $('#table');
        $table.on('check.bs.table uncheck.bs.table ' +
                'check-all.bs.table uncheck-all.bs.table', function () {
            $remove.prop('disabled', !$table.bootstrapTable('getSelections').length);
            // save your data, here just save the current page
            selections = getIdSelections();
            // push or splice the selections if you want to save all data selections
        });

        function getIdSelections() {
            return $.map($table.bootstrapTable('getSelections'), function (row) {
                return row.id
            });
        }

        $remove.click(function () {
            var ids = getIdSelections();
            $.get("../Teacher/delete",{
                'ids[]':ids
            },function(data,status){
                var str = "";
                for(var o in data)
                {
                    if(data[o]==1)
                        str += "编号："+o+" 删除成功! \n";
                    else
                        str += "编号："+o+" 删除失败！\n";
                }
                alert(str)
            });
            $remove.prop('disabled', true);
            window.location.reload();
        });

        //使用Ajax,增加教师
        function add() {

            var id = $("#id").val();
            var name = $("#name").val();
            var sex = $("#sex").val();
            var passwd= $("#passwd").val();


           $.get("../Teacher/add",{
               "id":id,
               "name":name,
               "sex":sex,
               "passwd":passwd
           },function(data,status){
               if(data["success"]){
                   $("#close").click();
                   alert("添加成功！");
                   setTimeout("",300);
                   window.location.reload();
               }
               else{
                   $("#close").click();
                   alert("添加失败！");
                   setTimeout("",300);
               }
           });

        }

        //编辑前
        function edit_before(tid){
            $.get("../Teacher/editBefore",{
                "id":tid
            },function(data,status){
                $("#eid").val(data['tid']);
                $("#ename").val(data['tname']);
                $("#esex").val(data['tsex']);
                $("#epasswd").val(data['tpassword']);
                $("#eid").attr("disabled","disabled");
            });
        }

        function edit(){
            var eid = $("#eid").val();
            var ename = $("#ename").val();
            var esex = $("#esex").val();
            var epasswd = $("#epasswd").val();

            $.get("../Teacher/update",{
                "id":eid,
                "name":ename,
                "sex":esex,
                "passwd":epasswd,
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