<?php
/**
 * Created by PhpStorm.
 * User: lenovo
 * Date: 2016/7/3
 * Time: 10:35
 */
namespace Home\Controller;
use Think\Controller;
class ClassController extends Controller {
    public function index(){
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        $res_path = C("RES_PATH");
        $this->assign("res_path",$res_path);
        $model = D('ClassSitutation');
        $list = $model->relation(true)->select();
        $this->assign("list",$list);
        $this->display();
    }

    public function add(){
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        //得到数据
        $data['cid'] = I("get.course_id");
        $data['sid'] = I("get.stu_id");
        $data['absence_number'] = 0;
        $model = M('ClassSitutation');
        $result = false;
        try{
            $result = $model->add($data);
        }catch(Exception $e){
            echo $e;
        }

        if($result == true)
            $res['success'] = true;
        else
            $res['success'] = false;

        $this->ajaxReturn($res);

    }

    public function delete(){
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        $ids = I("get.ids");
        $model = M('ClassSitutation');
        $result = Array();
        foreach($ids as $value)
        {
            $arr = explode(",",$value);
            $result[$value] = $model->where("cid='$arr[0]' and sid='$arr[1]'")->delete();
        }

        $this->ajaxReturn($result);
    }

}