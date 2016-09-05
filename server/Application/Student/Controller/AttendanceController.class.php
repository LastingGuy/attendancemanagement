<?php
/**
 * Created by PhpStorm.
 * User: lenovo
 * Date: 2016/8/21
 * Time: 10:12
 */
namespace Student\Controller;
use Think\Controller;
class AttendanceController extends Controller{
    public function index()
    {
        if(!session('?student'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        $stu_id = session("student");
        $res_path = C("RES_PATH");
        $this->assign("res_path",$res_path);
        $model = D("ClassSitutation");
        $list = $model->relation(true)->where("sid='$stu_id'")->select();
        $this->assign("list",$list);
        $this->display();
    }
}