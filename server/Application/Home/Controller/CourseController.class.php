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
class CourseController extends Controller {
    public function index(){
        $res_path = C("RES_PATH");
        $this->assign("res_path",$res_path);
        $model = D("Course");
        $list = $model->relation(true)->select();
        $this->assign("list",$list);
        $this->display();
    }

    public function add(){
        //得到数据
        $data['cid'] = I("get.id");
        $data['cname'] = I("get.name");
        $data['tid'] = I("get.teacher");

        $model = M("Course");

        try{
            $result = $model->add($data);
        }catch(Exception $e){
            $result = false;
        }

        if($result == true)
            $data['success'] = true;
        else
            $data['success'] = false;

        $this->ajaxReturn($data);

    }

    public function delete(){
        $ids = I("get.ids");
        $model = M("Course");
        $result = Array();
        foreach($ids as $value)
        {
            $result[$value] = $model->delete($value);
        }

        $this->ajaxReturn($result);
    }

    public function findTeachers(){
        $model = M('Teacher');
        $list = $model->select();
        $this->ajaxReturn($list);

    }

    public function editBefore(){
        $id = I("get.id");

        $model = M("Course");
        $data = $model->where("cid='$id'")->find();
        $this->ajaxReturn($data);
    }

    public function update(){
        //得到数据
        $data['cid'] = I("get.id");
        $data['cname'] = I("get.name");
        $data['tid'] = I("get.teacher");

        $model = M("Course");

        try{
            $result = $model->save($data);
        }catch(Exception $e){
            echo $e;
            $result = false;
        }

        if($result == true)
            $res['success'] = true;
        else
            $res['success'] = false;

        $this->ajaxReturn($res);

    }

    //课程考勤表
    public function courseCondition(){
        //
        $course_id = "1234";
        $filelist = Array();
        $dir = "d:/new";
        $files = scandir($dir.'/'.$course_id);
        foreach($files as $file){
            if(!is_dir($file)){
                array_push($filelist,$file);
            }
        }
        var_dump($filelist);
        $this->display();
    }

    //考勤显示
    public function attendanceResult(){
        $course_id = "1234";
        $course_name = "";
        $filename = "20160423.xml";
        $dir = "d:/new";
        $filepath = $dir.'/'.$course_id.'/'.$filename;

        $xml = new \DOMDocument();
        $xml->load($filepath);
        //从xml文件中读取的数据存放在此处
        $stu_data = array();
        $course_data = array();

        $root = $xml->documentElement;
        $course_data['course_id'] = $root->getElementsByTagName("courseid")[0]->nodeValue;
        $course_data['date'] = $root->getElementsByTagName("date")[0]->nodeValue;
        $course_data['begin_time'] = $root->getElementsByTagName("ts")[0]->nodeValue;
        $course_data['end_time'] = $root->getElementsByTagName("te")[0]->nodeValue;

        $students = $root->getElementsByTagName("stu");
        foreach($students as $stu) {
            $data = array();
            $data['id'] = $stu->getElementsByTagName("id")[0]->nodeValue;
            $data['name'] = $stu->getElementsByTagName("name")[0]->nodeValue;
            $data['college'] = $stu->getElementsByTagName("college")[0]->nodeValue;
            $data['major'] = $stu->getElementsByTagName("major")[0]->nodeValue;
            $data['class'] = $stu->getElementsByTagName("sclass")[0]->nodeValue;
            $data['sex'] = $stu->getElementsByTagName("sex")[0]->nodeValue;
            $data['check'] = $stu->getElementsByTagName("check")[0]->nodeValue;
            $data['arrive_time'] = $stu->getElementsByTagName("arrive_time")[0]->nodeValue;
            array_push($stu_data,$data);
        }

    }

}