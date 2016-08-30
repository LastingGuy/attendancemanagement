<?php
/**
 * Created by PhpStorm.
 * User: lenovo
 * Date: 2016/8/15
 * Time: 10:01
 */
namespace Teacher\Model;
use Think\Model\RelationModel;
class CourseModel extends RelationModel{
    protected $pk  = 'cid';

    protected $_link = array(
        'Teacher' =>array(
            'mapping_type' => self::BELONGS_TO,
            'class_name'  => 'Teacher',
            'mapping_name' => 'teacher',
            'foreign_key' => 'tid'
        )


    );
}
