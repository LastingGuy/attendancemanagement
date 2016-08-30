using attendanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace attendanceManagement.XML
{

    //CurrentCourse类为单例,当前要考勤的课程
    class CurrentCourse
    {
        private static CurrentCourse instance = new CurrentCourse();

        private String courseId = null;
        private String courseName;
        private String teacherId;
        private String teacherName;
        private String week;
        private String startTime;
        private String endTime;
        private String firstTime; 
        private String secondTime;
        private String thirdTime;
        
        private int studentNr;
        public Student[] students;

        public List<Student> stuList = null;
        private CurrentCourse()
        { }  
        
        //获得唯一实例
        public static CurrentCourse getInstance()
        {
            return instance;
        }
        
        //设置实例的基本信息  该方法由ZXmlDocument调用
        public void setCourse(String courseId, String courseName, String teacherId, String teacherName, int number)
        {
            this.courseId = courseId;
            this.courseName = courseName;
            this.teacherId = teacherId;
            this.teacherName = teacherName;   
            this.studentNr = number;
            //直接设置students数组内学生的数据
            students = new Student[studentNr];
        }

        //设置实例的时间信息 该方法由ZXmlDocument调用
        public void setTime(String week, String start, String end)
        {
            this.startTime = start;
            this.endTime = end;
            this.week = week;

            //设置3次考勤时间，暂时省略
            //....
        }

        //获得表格填充的数据，一个二维string 数组。暂时不用！
        public String[,] getTableContent()
        {
            String[,] data = new String[studentNr,4];
            for(int i=0;i<studentNr;i++)
            {
                data[i,0] = students[0].name;
                data[i,1] = students[0].college;
                data[i,2] = students[0].major;
                data[i,3] = students[0].id;
            }
            return data;
        }

        //返回学生列表
        public List<Student> getStudentList()
        {
            if(stuList == null)
            {
                stuList = new List<Student>();
                foreach (Student stu in students)
                {
                    stuList.Add(stu);
                }
                
                return stuList;
            }
            else
            {
                return stuList;
            }
           
        }

        public String getCourseId()
        {
            return courseId;
        }

        public String getEndTime()
        {
            return endTime;
        }

        public String getStartTime()
        {
            return startTime;
        }
    }
}
