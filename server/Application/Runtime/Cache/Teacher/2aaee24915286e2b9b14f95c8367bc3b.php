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
        <li class="active"><a href="../Course/index"><span class="glyphicon glyphicon-th"></span> 个人课程</a></li>
        <li role="presentation" class="divider"></li>
    </ul>
</div><!--/.sidebar-->

<div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><span class="glyphicon glyphicon-home"></span></a></li>
            <li class="active">课程管理</li>
        </ol>
    </div><!--/.row-->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">课 程 &nbsp;<?php echo ($course_id); ?> <?php echo ($course_name); ?>&nbsp; 的 考 勤 结 果</div>
                <div class="panel-body">
                    <form class="form-horizontal" method="post" enctype="multipart/form-data">

                        <div class="form-group">
                            <div id="check1">
                                <label  class="col-lg-2 col-lg-offset-1 control-label">考勤日期</label>
                                <div class="col-lg-3">
                                    <input type="text" class="form-control" placeholder="<?php echo ($course_data["date"]); ?>" disabled>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div id="check2">
                                <label  class="col-lg-2 col-lg-offset-1 control-label">开始时间</label>
                                <div class="col-lg-3">
                                    <input type="text" class="form-control" placeholder="<?php echo ($course_data["begin_time"]); ?>" disabled>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div id="check3">
                                <label  class="col-lg-2 col-lg-offset-1 control-label">结束时间</label>
                                <div class="col-lg-3">
                                    <input type="text" class="form-control" placeholder="<?php echo ($course_data["end_time"]); ?>"  disabled>
                                </div>
                            </div>
                        </div>
                    </form>

                        <table id="table" data-toggle="table" data-url=""  data-show-refresh="true" data-show-toggle="true" data-show-columns="true" data-search="true" data-select-item-name="toolbar1" data-pagination="true" data-sort-name="name" data-sort-order="desc">
                        <thead>
                        <tr>
                            <th data-field="state" data-checkbox="true" >Item ID</th>
                            <th data-field="name" > 姓 名</th>
                            <th data-field="id"> 学 号</th>
                            <th data-field="sex" > 性 别</th>
                            <th data-field="college" > 学 院</th>
                            <th data-field="major" > 专 业</th>
                            <th data-field="class" > 班 级</th>
                            <th data-field="attendance" > 是否出勤</th>
                            <th data-field="time" > 出勤时间</th>
                        </tr>
                        </thead>
                        <tbody>
                        <?php if(is_array($stu_data)): $i = 0; $__LIST__ = $stu_data;if( count($__LIST__)==0 ) : echo "" ;else: foreach($__LIST__ as $key=>$vo): $mod = ($i % 2 );++$i;?><tr>
                                <td></td>
                                <td><?php echo ($vo["name"]); ?></td>
                                <td><?php echo ($vo["id"]); ?></td>
                                <td><?php echo ($vo["sex"]); ?></td>
                                <td><?php echo ($vo["college"]); ?></td>
                                <td><?php echo ($vo["major"]); ?></td>
                                <td><?php echo ($vo["class"]); ?></td>

                                    <?php if($vo["attendance"] == 1): ?><td style="color:green">是</td>
                                        <?php else: ?><td style="color:red;">否</td><?php endif; ?>
                                <td><?php echo ($vo["arrive_time"]); ?></td>
                            </tr><?php endforeach; endif; else: echo "" ;endif; ?>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>


<script src="<?php echo ($res_path); ?>/js/jquery-1.11.1.min.js"></script>
<script src="<?php echo ($res_path); ?>/js/bootstrap.min.js"></script>
<script src="<?php echo ($res_path); ?>/js/bootstrap-table.js"></script>

</body>
</html>