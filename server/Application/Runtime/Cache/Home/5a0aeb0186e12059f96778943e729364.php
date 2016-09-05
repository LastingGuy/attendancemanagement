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
        <li><a href="../Index/index"><span class="glyphicon glyphicon-dashboard"></span> 概况</a></li>
        <li class="active"><a href="../Teacher/index"><span class="glyphicon glyphicon-th"></span> 教师管理</a></li>
        <li><a href="../Student/index"><span class="glyphicon glyphicon-th"></span> 学生管理</a></li>
        <li><a href="../Major/index"><span class="glyphicon glyphicon-list-alt"></span> 专业管理</a></li>
        <li><a href="../Class/index"><span class="glyphicon glyphicon-pencil"></span> 班级管理</a></li>
        <li><a href="../Course/index"><span class="glyphicon glyphicon-pencil"></span> 课程管理</a></li>
        <li role="presentation" class="divider"></li>
    </ul>
</div><!--/.sidebar-->

<div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><span class="glyphicon glyphicon-home"></span></a></li>
            <li>教师管理</li>
            <li class="active">教师课程</li>
        </ol>
    </div><!--/.row-->


    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">教 师 <?php echo ($teacher); ?> <?php echo ($name); ?> 的 课 程</div>
                <div class="panel-body">
                    <table data-toggle="table" data-url=""  data-show-refresh="true" data-show-toggle="true" data-show-columns="true" data-search="true" data-select-item-name="toolbar1" data-pagination="true" data-sort-name="name" data-sort-order="desc">
                        <thead>
                        <tr>
                            <th data-field="state" data-checkbox="true" >Item ID</th>
                            <th data-field="id" data-sortable="true">课程号</th>
                            <th data-field="name">课程名</th>
                            <th data-field="year">年 度</th>
                            <th data-field="term">开课学期</th>
                            <th data-field="credit">学 分</th>
                            <th data-field="period">学 时</th>
                            <th data-field="exam">考试或考察</th>
                            <th data-field="major">专 业</th>
                            <th data-field="class"  data-sortable="true">班 级</th>

                        </tr>
                        </thead>
                        <tbody>
                        <?php if(is_array($list)): $i = 0; $__LIST__ = $list;if( count($__LIST__)==0 ) : echo "" ;else: foreach($__LIST__ as $key=>$vo): $mod = ($i % 2 );++$i;?><tr>
                                <td></td>
                                <td><?php echo ($vo["zcy_cno"]); ?></td>
                                <td><?php echo ($vo["zcy_cname"]); ?></td>
                                <td><?php echo ($vo["zcy_csyear"]); ?></td>
                                <td>
                                    <?php if($vo["zcy_cterm"] == 0): ?>第一学期
                                        <?php else: ?>第二学期<?php endif; ?>
                                </td>
                                <td><?php echo ($vo["zcy_ccredit"]); ?></td>
                                <td><?php echo ($vo["zcy_cperiod"]); ?></td>
                                <td>
                                    <?php if($vo["zcy_cexam"] == 1): ?>考试
                                        <?php else: ?>考查<?php endif; ?>
                                </td>
                                <td><?php echo ($vo["zcy_mname"]); ?></td>
                                <td><?php echo ($vo["zcy_clno"]); ?></td>
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