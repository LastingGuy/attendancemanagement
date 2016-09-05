<?php
/**
 * Created by PhpStorm.
 * User: lenovo
 * Date: 2016/7/3
 * Time: 10:36
 */
namespace Home\Controller;
use Think\Controller;
use Think\Exception;
class AttendanceController extends Controller {
    public function index(){
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        $res_path = C("RES_PATH");
        $this->assign("res_path",$res_path);
        $model = D('Absence');
        $list = $model->select();
        $this->assign("list",$list);
        $this->display();
    }
    
}