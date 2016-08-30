<?php
/**
 * Created by PhpStorm.
 * User: lenovo
 * Date: 2016/8/13
 * Time: 15:25
 */
namespace Home\Controller;
use Think\Controller;
use Think\Exception;
class IndexController extends Controller
{
    public function index()
    {

        $res_path = C("RES_PATH");
        $this->assign("res_path", $res_path);
        $this->display();
    }

    public function login(){
        $username = I("post.username");
        $password = I("post.password");
        $type = I("post.type");
        if($type=="0"){
            //学生
            $model = M("student");
            $data = $model->where("sid='$username'")->find();
            if($data!=null and $data['spassword']==$password  ) {
                session('student',$username);
                $this->ajaxReturn("1");
            }
            else{
                $this->ajaxReturn("0");
            }
        }else if($type=="1"){
            //教师
            $model = M("teacher");
            $data = $model->where("tid='$username'")->find();
            if($data!=null and  $data['tpassword']==$password) {
                session('teacher',$username);
                $this->ajaxReturn("2");
            }
            else{
                $this->ajaxReturn("0");
            }
        }else if($type=='2'){
            //管理员
            if($username=="admin" and $password=="123456"){
                session('admin',$username);
                $this->ajaxReturn("3");
            }
            else{
                $this->ajaxReturn("0");
            }
        }
    }
}
