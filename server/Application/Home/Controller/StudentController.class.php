<?php
/**
 * Created by PhpStorm.
 * User: lenovo
 * Date: 2016/7/3
 * Time: 10:36
 */
namespace Home\Controller;
use Think\Controller;
class StudentController extends Controller
{
    public function index()
    {
        $res_path = C("RES_PATH");
        $this->assign("res_path", $res_path);
        $stu = M("Student");
        $list = $stu->select();
        $this->assign("list",$list);

        $this->display();
    }

    public function add()
    {
        //得到数据
        $id = I("get.id");
        $name = I("get.name");
        $sex = I("get.sex");
        $nationality = I("get.nationality");
        $college = I("get.college");
        $aclass = I("get.aclass");
        $mac = I("get.mac");
        $passwd = I("get.passwd");

        $stu = M('Student');
        $data["sid"] = $id;
        $data["sname"]  = $name;
        $data["ssex"]  = $sex;
        $data["snationality"]  = $nationality;
        $data["scollege"]  = $college;
        $data["sclass"]  = $aclass;
        $data["smac"]  = $mac;
        $data["spassword"]  = $passwd;
        try{
            $result = $stu->add($data);
        }
        catch( \Think\Exception  $e)
        {
            $res['success'] = false;
        }

        if($result == true)
            $res['success'] = true;
        else
            $res['success'] = false;


        $this->ajaxReturn($res);

    }

    public function delete()
    {
        $ids = I("get.ids");
        $stu = M("Student");
        $result = Array();
        foreach ($ids as $value) {
            $result[$value] = $stu->delete($value);
        }

        $this->ajaxReturn($result);
    }

    public function findClass(){
        $id = I("get.id");
        $model = new \Think\Model();
        $list = $model->query("select * from zhengcy_class where zcy_Mno='%s'",$id);
        $this->ajaxReturn($list);
    }

    public function student(){
        $res_path = C("RES_PATH");
        $student = I("get.id");
        $model = new \Think\Model();
        $list = $model->query("select * from student_course_view where zcy_Sno='%s'",$student);
        $this->assign("list",$list);
        $this->assign("res_path",$res_path);
        $this->assign("student",$student);
        $this->display();
    }

    public function editBefore(){
        $id = I("get.id");

        $stu = M('Student');

        $data = $stu->where("sid=$id")->find();
        $this->ajaxReturn($data);
    }

    public function update(){
        //得到数据

        $data['sid'] = I("get.id");
        $data['sname'] = I("get.name");
        $data['ssex'] = I("get.sex");
        $data['snationality'] = I("get.nationality");
        $data['scollege'] = I("get.college");
        $data['sclass'] = I("get.class");
        $data['smac'] = I("get.mac");
        $data['spasswd'] = I("get.passwd");

        $stu = M("Student");
        $result = $stu->save($data);


        if($result == true)
            $data['success'] = true;
        else
            $data['success'] = false;

        $this->ajaxReturn($data);

    }

}