<?php
/**
 * Created by PhpStorm.
 * User: lenovo
 * Date: 2016/8/15
 * Time: 15:15
 */
namespace Student\Model;
use Think\Model\RelationModel;
class ClassSitutationModel extends RelationModel
{
    protected $pk = array('cid','sid');
    protected $_link = array(
       "Course" => array(
           'mapping_type' => self::BELONGS_TO,
           'class_name' => 'course',
           'foreign_key' => 'cid',
           'mapping_name' => 'course'
       ),

        "Student" => array(
            'mapping_type' => self::BELONGS_TO,
            'class_name' => 'student',
            'foreign_key' => 'sid',
            'mapping_name' => 'student'
        )
    );
}