<?php
/**
 * Created by PhpStorm.
 * User: lenovo
 * Date: 2016/8/16
 * Time: 15:20
 */
namespace Background\Controller;
use Think\Controller;
class UploadController extends Controller {

    public function upload(){
        if(!session('?teacher'))
        {
            $this->show("error");
            return ;
        }

        $upload = new \Think\Upload();
        $upload->maxSize = 3145728;
        $upload->etxs = array('xml');
        $upload->rootPath = C("UPLOAD_ROOT");
        $upload->savePath = '';
        $upload->autoSub = false;

        $info = $upload->upload();
        if(!$info){ //上传错误提示错误信息
            $this->error($upload->getError());
        }else{ //上传成功
           //$this->success('上传成功！');
        }
        $oldfile = C("UPLOAD_ROOT").'/'.$info['file']['savename'];

        //判断文件是否已经存在并处理
        $course_id = $this->handleXml($oldfile,$info['file']['name']);


        $newpath = C("SAVE_ROOT").'/'.$course_id;
        $newname = $info['file']['name'];
        $newfile = $newpath.'/'.$newname;

        mkdir($newpath,0777,true);
        rename($oldfile,$newfile);
        unlink($oldfile);

        //对新文件进行处理
        $this->handleNewXml($newpath,$newname);

        $this->show("success");
    }

    private function handleXml($oldfile,$filename){

        $xml = new \DOMDocument();
        $course_id = null;
        //找到课程id
        if($xml->load($oldfile)){
            $root = $xml->documentElement;
            $elm = $root->getElementsByTagName("courseid");
            $course_id = $elm[0]->nodeValue;

            //查看文件是否存在
            if(file_exists(C("SAVE_ROOT").'/'.$course_id.'/'.$filename)) {
                //文件存在，删除数据库中数据
                $xml = new \DOMDocument();
                if($xml->load(C("SAVE_ROOT").'/'.$course_id.'/'.$filename)){
                    $root = $xml->documentElement;
                    $elm = $root->getElementsByTagName("stu");
                    $model = M("Absence");
                    foreach($elm as $node){
                        if($node->getElementsByTagName("check")->item(0)->nodeValue==0){
                            $stu_id = $node->getElementsByTagName("id")->item(0)->nodeValue;
                            $reuslt = $model->where("sid='$stu_id' and cid='$course_id'")->delete();
                        }
                    }
                }
            }
        }
        return $course_id;
    }

    private function handleNewXml($filepath,$filename)
    {
        $xml = new \DOMDocument();
        if($xml->load($filepath.'/'.$filename)) {
            $root = $xml->documentElement;
            $elm = $root->getElementsByTagName("courseid");
            $course_id = $elm[0]->nodeValue;
            $elm = $root->getElementsByTagName("stu");
            $model = D("Absence");
            foreach($elm as $node) {
                if($node->getElementsByTagName("check")->item(0)->nodeValue==0) {
                    $stu_id = $node->getElementsByTagName("id")->item(0)->nodeValue;
                    $data['cid'] = $course_id;
                    $data['sid'] = $stu_id;
                    $data['date'] = $filename;
                    $this->success($course_id." ".$stu_id." ".$filename);
                    $model->add($data);
                }
            }
        }
    }
}