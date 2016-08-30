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
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }
        $res_path = C("RES_PATH");
        $this->assign("res_path", $res_path);
        $stu = M("Student");
        $list = $stu->select();
        $this->assign("list",$list);

        $this->display();
    }

    public function add()
    {
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }
        //得到数据
        $id = I("get.id");
        $name = I("get.name");
        $sex = I("get.sex");
        $nationality = I("get.nationality");
        $college = I("get.college");
        $aclass = I("get.aclass");
        $mac = I("get.mac");
        $passwd = I("get.passwd");
        $major = I("get.major");
        $tel = I("get.tel");

        $stu = M('Student');
        $data["sid"] = $id;
        $data["sname"]  = $name;
        $data["ssex"]  = $sex;
        $data["snationality"]  = $nationality;
        $data["scollege"]  = $college;
        $data["sclass"]  = $aclass;
        $data["smac"]  = $mac;
        $data["spassword"]  = $passwd;
        $data['stel'] = $tel;
        $data['smajor'] = $major;

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
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }
        $ids = I("get.ids");
        $stu = M("Student");
        $result = Array();
        foreach ($ids as $value) {
            $result[$value] = $stu->delete($value);
        }

        $this->ajaxReturn($result);
    }

    public function findClass(){
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }
        $id = I("get.id");
        $model = new \Think\Model();
        $list = $model->query("select * from zhengcy_class where zcy_Mno='%s'",$id);
        $this->ajaxReturn($list);
    }

    public function student(){
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }
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
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }
        $id = I("get.id");

        $stu = M('Student');

        $data = $stu->where("sid=$id")->find();
        $this->ajaxReturn($data);
    }

    public function update(){
        if(!session('?admin'))
        {
            header('Location:'.U("Home/Index/index"));
        }
        //得到数据

        $data['sid'] = I("get.id");
        $data['sname'] = I("get.name");
        $data['ssex'] = I("get.sex");
        $data['snationality'] = I("get.nationality");
        $data['scollege'] = I("get.college");
        $data['sclass'] = I("get.class");
        $data['smac'] = I("get.mac");
        $data['spasswd'] = I("get.passwd");
        $data['smajor'] = I("get.major");
        $data['stel'] = I("get.tel");

        $stu = M("Student");
        $result = $stu->save($data);


        if($result == true)
            $res['success'] = true;
        else
            $res['success'] = false;

        $this->ajaxReturn($res);

    }

}