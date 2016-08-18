-- 创建教师表
create table zhengcy_Teacher
(
	zcy_Tno varchar(20),
	zcy_Tname	varchar(10),
	zcy_Tsex	bit,
	zcy_Tage	int,
	zcy_Tposition	varchar(20),
	zcy_Ttel	varchar(20),
	constraint pk_teacher primary key(zcy_Tno)
);

--创建课程表
create table zhengcy_Course
(
	zcy_Cno		varchar(20),
	zcy_Cname	varchar(10),
	zcy_Cterm	bit,
	zcy_Cperiod	tinyint,
	zcy_Cexam	bit,
	zcy_Ccredit	tinyint,
	constraint pk_course primary key(zcy_Cno)
);

--创建专业表
create table zhengcy_Major
(
	zcy_Mno	varchar(20),
	zcy_Mname	varchar(20),
	constraint pk_major primary key(zcy_Mno)
)

--创建班级表
create table zhengcy_Class
(
	zcy_Mno	varchar(20),
	zcy_CLno	varchar(20),
	constraint pk_class primary key(zcy_Mno,zcy_CLno),
	constraint fk_class foreign key(zcy_Mno) references zhengcy_Major(zcy_Mno)
)

--创建学生表
create table zhengcy_Student
(
	zcy_Sno		varchar(20),
	zcy_Sname	varchar(10),
	zcy_Ssex	bit,
	zcy_Sage	tinyint,
	zcy_Scredit tinyint,
	zcy_Sarea	varchar(20),
	zcy_Mno		varchar(20),
	zcy_CLno	varchar(20),
	constraint pk_student primary key(zcy_Sno),
	constraint fk_sutdent_mno foreign key(zcy_Mno,zcy_CLno) references zhengcy_Class(zcy_Mno,zcy_CLno)
);

--创建上课情况
create table zhengcy_Class_Situtation
(
	zcy_Cno		varchar(20),
	zcy_Tno		varchar(20),
	zcy_CLno	varchar(20),
	zcy_Mno		varchar(20),
	zcy_CSyear	varchar(4),
	constraint pk_class_situtaion primary key(zcy_Cno,zcy_CLno,zcy_Mno),
	constraint fk_class_situtation_tno  foreign key(zcy_Tno) references zhengcy_Teacher(zcy_Tno) ON DELETE CASCADE,
	constraint fk_class_situtation_cno	foreign key(zcy_Cno) references zhengcy_Course(zcy_Cno) ON DELETE CASCADE,
	constraint fk_class_situtation_class foreign key(zcy_Mno,zcy_CLno) references zhengcy_Class(zcy_Mno,zcy_CLno) ON DELETE CASCADE
)
 
--创建成绩情况
create table zhengcy_Grade_Situtation
(
	zcy_Sno		varchar(20),
	zcy_Cno		varchar(20),
	zcy_Ggrade	tinyint,
	constraint pk_grade_situtation primary key(zcy_Sno,zcy_Cno),
	constraint fk_grade_situtaion_course foreign key(zcy_Cno) references zhengcy_Course(zcy_Cno) ON DELETE CASCADE,
	constraint fk_grade_situtaion_student foreign key(zcy_Sno) references zhengcy_Student(zcy_Sno) ON DELETE CASCADE
)

--创建班级人数存储过程 
create procedure get_num_class
@major varchar(20),
@class varchar(20),
@num	int output
as
	select @num=count(*) from zhengcy_Student where zcy_Mno=@major and zcy_CLno=@class;

--地区人数统计存储过程
create procedure get_area_num
@area varchar(20),
@num	int output
as
	select @num=count(*) from zhengcy_Student where zcy_Sarea like '%'+@area+'%';

--课程平均分存储过程
create procedure get_averge_grade
@major varchar(20),
@class varchar(20),
@course varchar(20),
@average float output
as
	select @average = AVG(zcy_Ggrade*1.0) from zhengcy_Grade_Situtation, zhengcy_Student
	where zhengcy_Grade_Situtation.zcy_Sno=zhengcy_Student.zcy_Sno and zcy_Mno=@major
	 and zcy_CLno=@class and zcy_Cno=@course


--创建班级视图
create view class_view 
as
	select zhengcy_Major.zcy_Mno, zcy_Mname, zcy_CLno from zhengcy_Major, zhengcy_Class where zhengcy_Major.zcy_Mno = zhengcy_Class.zcy_Mno;


--创建学生课程视图
create view student_course_view 
as
	select zhengcy_Course.zcy_Cno, zcy_Cname, zcy_Ggrade, zcy_Sno 
	from zhengcy_Course, zhengcy_Grade_Situtation
	where zhengcy_Course.zcy_Cno = zhengcy_Grade_Situtation.zcy_Cno



--创建教师课程查询视图
create view teacher_view
as
	select zhengcy_Class_Situtation.zcy_Cno, zcy_Cname, zcy_CSyear, zcy_Cterm, zcy_Ccredit, zcy_Cperiod, zcy_Cexam, zcy_Mname, zcy_CLno, zcy_Tno
	from zhengcy_Class_Situtation, zhengcy_Course, zhengcy_Major 
	where zhengcy_Class_Situtation.zcy_Cno = zhengcy_Course.zcy_Cno and  zhengcy_Class_Situtation.zcy_Mno = zhengcy_Major.zcy_Mno

--创建班级课程查询视图
create view class_course_view
as
	select zhengcy_Course.zcy_Cno, zhengcy_Course.zcy_Cname, zhengcy_Class_Situtation.zcy_CSyear, zhengcy_Course.zcy_Cterm, zhengcy_Course.zcy_Ccredit,
	 zhengcy_Teacher.zcy_Tno, zhengcy_Teacher.zcy_Tname, zhengcy_Class_Situtation.zcy_CLno, zhengcy_Class_Situtation.zcy_Mno
	from zhengcy_Class_Situtation, zhengcy_Teacher, zhengcy_Course, zhengcy_Class 
	where zhengcy_Class_Situtation.zcy_Cno = zhengcy_Course.zcy_Cno and 
		  zhengcy_Class_Situtation.zcy_Tno = zhengcy_Teacher.zcy_Tno and
		  zhengcy_Class_Situtation.zcy_Mno = zhengcy_Class.zcy_Mno and
		  zhengcy_Class_Situtation.zcy_CLno = zhengcy_Class.zcy_CLno

--班级成绩视图
create view class_grade_view
as 
	select zhengcy_Student.zcy_Sno, zhengcy_Student.zcy_Sname, zcy_Ggrade,zcy_Mno,zcy_CLno,zcy_Cno
	from zhengcy_Student left join zhengcy_Grade_Situtation
	on zhengcy_Student.zcy_Sno  = zhengcy_Grade_Situtation.zcy_Sno


--班级添加课程时 触发器添加成绩记录到成绩表中
create trigger add_course_trigger
on zhengcy_Class_Situtation
after insert 
as
	DECLARE @stu_id as varchar(20);
	DECLARE @mno as varchar(20), @clno varchar(20), @cno varchar(20);
	select @mno = zcy_Mno, @clno = zcy_CLno, @cno=zcy_Cno  from inserted;
	DECLARE each_student CURSOR FAST_FORWARD FOR
		SELECT zcy_Sno
		FROM zhengcy_Student
		WHERE @mno = zhengcy_Student.zcy_Mno and @clno = zhengcy_Student.zcy_CLno;
	OPEN each_student;
	FETCH NEXT FROM each_student INTO @stu_id;

	WHILE @@FETCH_STATUS=0
	BEGIN
		insert into zhengcy_Grade_Situtation values(@stu_id,@cno,0);
		FETCH NEXT FROM each_student INTO @stu_id;
	END
	CLOSE each_student;

	-- 释放游标
	DEALLOCATE each_student;

--修改成绩后 学生学分变化
create trigger update_gradee_trigger
on zhengcy_Grade_Situtation 
after update 
as
	DECLARE @stu_id as varchar(20),@course as varchar(20), @stu_grade as tinyint,@credit as tinyint;
	IF update(zcy_Ggrade)
	begin
		select @stu_id=zcy_Sno, @stu_grade=zcy_Ggrade, @course=zcy_Cno from inserted;
		select @credit=zcy_Ccredit from zhengcy_Course where zcy_Cno = @course;
		IF @stu_grade>=60
		begin
			update zhengcy_Student set zcy_Scredit = zcy_Scredit+@credit
			where  zcy_Sno = @stu_id;
		end 
	end 

