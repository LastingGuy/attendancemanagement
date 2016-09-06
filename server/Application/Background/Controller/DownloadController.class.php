<?php
/**
 * Created by PhpStorm.
 * User: lenovo
 * Date: 2016/8/17
 * Time: 17:29
 */
namespace Background\Controller;
use Think\Controller;
class DownloadController extends Controller{
    //获取课程xml
    public function getCourse(){

        if(!session('?teacher'))
        {
            $this->show("error");
            return ;
        }
        $tea_id = session("teacher");

        $xml = new \DOMDocument('1.0','utf-8');
        $courses = $xml->createElement("courses");
        $model = D('Course');
        $model1 = M('classSitutation');
        $list = $model->relation(true)->where("tid='$tea_id'")->select();

        foreach($list as $data){
            $course = $xml->createElement("course");

            $id =  $xml->createElement("id");
            $id->nodeValue = $data['cid'];

            $name =  $xml->createElement("name");
            $name->nodeValue = $data['cname'];

            $teacher =  $xml->createElement("teacher");
            $teacher->nodeValue = $data['teacher']['tname'];

            $teacherid =  $xml->createElement("teacherid");
            $teacherid->nodeValue = $data['teacher']['tid'];

            $nrofstu =  $xml->createElement("nrofstu");
            $nrofstu->nodeValue = $model1->where("cid='".$data['cid']."'")->count('sid');

            $date =  $xml->createElement("date");

            $course->appendChild($id);
            $course->appendChild($name);
            $course->appendChild($teacher);
            $course->appendChild($teacherid);
            $course->appendChild($nrofstu);
            $course->appendChild($date);

            $courses->appendChild($course);
        }

        $xml->appendChild($courses);

    /*       header("Content-Type:text/xml");
        header("Content-Disposition:attachment;filename='courses.xml'");*/
        echo $xml->saveXML();
    }

    //获取考勤名单xml
    public function getAttendanceList()
    {
        if(!session('?teacher'))
        {
            $this->show("error");
            return ;
        }

        $course_id = I("get.course_id",'123');
        $xml = new \DOMDocument('1.0', 'utf-8');
        $students = $xml->createElement("students");
        $model = D('Course');
        $list = $model->relation(true)->where("cid='$course_id'")->find()['Student'];

        foreach ($list as $data) {
            $stu = $xml->createElement('stu');

            $id = $xml->createElement('id');
            $id->nodeValue = $data['sid'];
            $name = $xml->createElement('name');
            $name->nodeValue = $data['sname'];
            $mac = $xml->createElement('mac');
            $mac->nodeValue = $data['smac'];
            $college = $xml->createElement('college');
            $college->nodeValue = $data['scollege'];
            $major = $xml->createElement('major');
            $major->nodeValue = $data['smajor'];
            $sclass = $xml->createElement('sclass');
            $sclass->nodeValue = $data['sclass'];
            $sex = $xml->createElement('sex');
            $sex->nodeValue = $data['ssex'];
            $tel = $xml->createElement('tel');
            $tel->nodeValue = $data['stel'];

            $stu->appendChild($id);
            $stu->appendChild($name);
            $stu->appendChild($mac);
            $stu->appendChild($college);
            $stu->appendChild($major);
            $stu->appendChild($sclass);
            $stu->appendChild($sex);
            $stu->appendChild($tel);

            $students->appendChild($stu);
        }

        $xml->appendChild($students);
       /*
        header("Content-Type:text/xml");
        header("Content-Disposition:attachment;filename='$course_id".".xml"."'");*/
        echo($xml->saveXML());
    }

    //获取md5
    public function getMD5(){
        if(!session('?teacher'))
        {
            $this->show("error");
            return ;
        }
        $tea_id = session("teacher");

        $model = M("Teacher");
        $data = $model->where("tid='$tea_id'")->find();
        echo $data['tchange'];
    }
}