/* 学生表 */
create table kaoqin_student
(
	sid varchar(20),
    sname varchar(20),
    ssex  bit,
    snationality varchar(20),
    scollege varchar(20),
    smajor varchar(20),
    sclass  varchar(20),
    smac varchar(12),
    spassword varchar(20),
    stel varchar(20),
    constraint pk_kaoqin_student primary key(sid)
);

/* 外国学生护照表 */
create table kaoqin_foreign_student
(
	sid varchar(20),
    spassport varchar(20),
	constraint pk_kaoqin_student primary key(sid),
	constraint fk_kaoqin_student foreign key(sid) references kaoqin_student(sid)
);


/* 教师表 */
create table kaoqin_teacher
(
	tid varchar(20),
    tname varchar(20),
    tsex bit,
    tpassword varchar(20),
    tchange varchar(35),
    constraint pk_kaoqin_teacher primary key(tid)
);

/* 课程表 */
create table kaoqin_course
(
	cid varchar(20),
    cname varchar(20),
    tid varchar(20),
    constraint pk_kaoqin_course primary key(cid)
);

/* 课程情况表 */
create table kaoqin_class_situtation
(
	cid varchar(20),
    sid varchar(20),
    absence_number int,
    constraint pk_kaoqin_class_situtation primary key(cid,sid),
    constraint fk_kaoqin_class_situtation_cid foreign key(cid) references kaoqin_course(cid),
    constraint fk_kaoqin_class_situtation_sid foreign key(sid) references kaoqin_student(sid)
);

/* 缺勤表 */
create table kaoqin_absence
(
	cid varchar(20),
    sid varchar(20),
    date date,
    constraint pk_kaoqin_absence primary key(cid,sid),
    constraint fk_kaoqin_absence foreign key(cid,sid) references kaoqin_class_situtation(cid,sid)
);

DELIMITER $
create trigger absence_insert_trigger
after insert on kaoqin_absence
for each row 
begin 
	declare absence_sid,absence_cid varchar(20);
    set absence_sid = new.sid;
    set absence_cid = new.cid;
	update kaoqin_class_situtation set absence_number = absence_number+1 
    where sid=absence_sid and cid=absence_cid;
end

DELIMITER $
create trigger absence_delete_trigger
after delete on kaoqin_absence
for each row 
begin 
	declare absence_sid,absence_cid varchar(20);
    set absence_sid = old.sid;
    set absence_cid = old.cid;
	update kaoqin_class_situtation set absence_number = absence_number-1 
    where sid=absence_sid and cid=absence_cid;
end

DELIMITER $
CREATE DEFINER=`root`@`localhost` TRIGGER `attendance`.`kaoqin_teacher_BEFORE_INSERT` BEFORE INSERT ON `kaoqin_teacher` FOR EACH ROW
BEGIN
	set new.tchange = md5(unix_timestamp());
END

DELIMITER $
CREATE DEFINER=`root`@`localhost` TRIGGER `attendance`.`kaoqin_course_BEFORE_INSERT` BEFORE INSERT ON `kaoqin_course` FOR EACH ROW
BEGIN
	declare tea_id varchar(20);
    set tea_id = new.tid;
	update kaoqin_teacher set tchange = md5(unix_timestamp()) where tid = tea_id;
END

DELIMITER $
CREATE DEFINER=`root`@`localhost` TRIGGER `attendance`.`kaoqin_class_situtation_BEFORE_UPDATE` BEFORE UPDATE ON `kaoqin_class_situtation` FOR EACH ROW
BEGIN
	declare tea_id varchar(20);
    set tea_id = (select tid from kaoqin_course where cid = new.cid);
	update kaoqin_teacher set tchange = md5(unix_timestamp()) where tid = tea_id;
END

DELIMITER $
CREATE DEFINER=`root`@`localhost` TRIGGER `attendance`.`kaoqin_class_situtation_BEFORE_DELETE` BEFORE DELETE ON `kaoqin_class_situtation` FOR EACH ROW
BEGIN
	declare tea_id varchar(20);
    set tea_id = (select tid from kaoqin_course where cid = old.cid);
	update kaoqin_teacher set tchange = md5(unix_timestamp()) where tid = tea_id;
END

DELIMITER $
CREATE DEFINER=`root`@`localhost` TRIGGER `attendance`.`kaoqin_class_situtation_BEFORE_INSERT` BEFORE INSERT ON `kaoqin_class_situtation` FOR EACH ROW
BEGIN
	declare tea_id varchar(20);
    set tea_id = (select tid from kaoqin_course where cid = new.cid);
	update kaoqin_teacher set tchange = md5(unix_timestamp()) where tid = tea_id;
END

DELIMITER $
CREATE DEFINER=`root`@`localhost` TRIGGER `attendance`.`kaoqin_class_situtation_BEFORE_UPDATE` BEFORE UPDATE ON `kaoqin_class_situtation` FOR EACH ROW
BEGIN
	declare tea_id varchar(20);
    set tea_id = (select tid from kaoqin_course where cid = new.cid);
	update kaoqin_teacher set tchange = md5(unix_timestamp()) where tid = tea_id;
END

DELIMITER $
CREATE DEFINER=`root`@`localhost` TRIGGER `attendance`.`kaoqin_class_situtation_BEFORE_DELETE` BEFORE DELETE ON `kaoqin_class_situtation` FOR EACH ROW
BEGIN
	declare tea_id varchar(20);
    set tea_id = (select tid from kaoqin_course where cid = old.cid);
	update kaoqin_teacher set tchange = md5(unix_timestamp()) where tid = tea_id;
END