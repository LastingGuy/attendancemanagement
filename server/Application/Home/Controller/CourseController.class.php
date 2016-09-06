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
class CourseController extends Controller
{
    public function index()
    {
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        $res_path = C("RES_PATH");
        $this->assign("res_path", $res_path);
        $model = D("Course");
        $list = $model->relation(true)->select();
        $this->assign("list", $list);
        $this->display();
    }

    public function add()
    {
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        //得到数据
        $data['cid'] = I("get.id");
        $data['cname'] = I("get.name");
        $data['tid'] = I("get.teacher");

        $model = M("Course");

        try {
            $result = $model->add($data);
        } catch (Exception $e) {
            $result = false;
        }

        if ($result == true)
            $data['success'] = true;
        else
            $data['success'] = false;

        $this->ajaxReturn($data);

    }

    public function delete()
    {
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        $ids = I("get.ids");
        $model = M("Course");
        $result = Array();
        foreach ($ids as $value) {
            $result[$value] = $model->delete($value);
        }

        $this->ajaxReturn($result);
    }

    public function findTeachers()
    {
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        $model = M('Teacher');
        $list = $model->select();
        $this->ajaxReturn($list);

    }

    public function editBefore()
    {
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }
        $id = I("get.id");

        $model = M("Course");
        $data = $model->where("cid='$id'")->find();
        $this->ajaxReturn($data);
    }

    public function update()
    {
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        //得到数据
        $data['cid'] = I("get.id");
        $data['cname'] = I("get.name");
        $data['tid'] = I("get.teacher");

        $model = M("Course");

        try {
            $result = $model->save($data);
        } catch (Exception $e) {
            echo $e;
            $result = false;
        }

        if ($result == true)
            $res['success'] = true;
        else
            $res['success'] = false;

        $this->ajaxReturn($res);

    }


    //课程考勤表
    public function courseCondition()
    {
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        $course_id = I('get.course_id');
        $course_name = I('get.course_name');

        $res_path = C("RES_PATH");
        $this->assign("res_path", $res_path);
        $this->assign("course_id", $course_id);
        $this->assign("course_name", $course_name);

        $filelist = Array();
        $dir = C("SAVE_ROOT");
        $files = scandir($dir . '/' . $course_id);
        foreach ($files as $file) {
            if (!is_dir($file)) {
                array_push($filelist,str_replace(".xml","",$file));
            }
        }
        $this->assign("list", $filelist);
        $this->display();
    }

    //考勤显示
    public function result()
    {
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }

        $res_path = C("RES_PATH");
        $this->assign("res_path", $res_path);
        $course_id = I('get.course_id');
        $course_name = I("get.course_name");
        $date = I('get.date');

        $filename = $date . ".xml";
        $dir = C("SAVE_ROOT");
        $filepath = $dir . '/' . $course_id . '/' . $filename;

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
        foreach ($students as $stu) {
            $data = array();
            $data['id'] = $stu->getElementsByTagName("id")[0]->nodeValue;
            $data['name'] = $stu->getElementsByTagName("name")[0]->nodeValue;
            $data['college'] = $stu->getElementsByTagName("college")[0]->nodeValue;
            $data['major'] = $stu->getElementsByTagName("major")[0]->nodeValue;
            $data['class'] = $stu->getElementsByTagName("sclass")[0]->nodeValue;
            $data['sex'] = $stu->getElementsByTagName("sex")[0]->nodeValue;
            $data['check'] = $stu->getElementsByTagName("check")[0]->nodeValue;
            $data['arrive_time'] = $stu->getElementsByTagName("arrive_time")[0]->nodeValue;
            array_push($stu_data, $data);
        }

        $this->assign("course_id", $course_id);
        $this->assign("course_name", $course_name);
        $this->assign("stu_data",$stu_data);
        $this->assign("course_data",$course_data);
        $this->display();

    }

}