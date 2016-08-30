<?php
/**
 * Created by PhpStorm.
 * User: lenovo
 * Date: 2016/8/21
 * Time: 9:55
 */
namespace Student\Controller;
use Think\Controller;
class InformationController extends Controller
{
    public function index(){
        if(!session('?student'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        $stu_id = session("student");
        $res_path = C("RES_PATH");
        $this->assign("res_path",$res_path);

        $model = M("Student");
        $data = $model->where("sid = '$stu_id'")->find();
        $this->assign("stu_data",$data);
        $this->display();
    }

    public function update()
    {
        if(!session('?student'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        $stu_id = session("student");
        $data['smac'] = I("get.mac");
        $data['stel'] = I("get.tel");
        $model = M("Student");
        $result = $model->where("sid='$stu_id'")->save($data);

        if($result == true)
            $res['success'] = true;
        else
            $res['success'] = false;

        $this->ajaxReturn($res);
    }
}