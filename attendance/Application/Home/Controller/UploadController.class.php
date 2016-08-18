<?php
/**
 * Created by PhpStorm.
 * User: lenovo
 * Date: 2016/8/16
 * Time: 15:44
 */
namespace Home\Controller;
use Think\Controller;
use Think\Exception;
class UploadController extends Controller {
    public function index(){
        $res_path = C("RES_PATH");
        $this->assign("res_path",$res_path);
        $this->display();
    }

}