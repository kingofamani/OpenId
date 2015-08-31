using System;
using System.Collections.Generic;

namespace NTPCLibrary
{
    public class User
    {
        public User()
        {

        }
        public string Identity { get; set; }//帳號
        public string ID { get; set; }//身份證
        public string FullName { get; set; }//姓名
        public string NickName { get; set; }//暱稱
        public string Email { get; set; }//電子郵件
        public string  ClassRoom { get; set; }//年級班級座號
        public string SchoolName { get; set; }//學校名稱
        public DateTime? BirthDate { get; set; }//生日
        public List<Department> Departments { get; set; }//服務單位
        public string Gender { get; set; }//性別
        public string AXExtension { get; set; }//延伸資料
        //public string Message { get; set; }//訊息

    }
}
