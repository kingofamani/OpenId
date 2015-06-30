using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPCLibrary
{
    public class Department
    {
        public string ID { get; set; }//學校代碼
        public string Name { get; set; }//學校名稱
        public string Role { get; set; }//身份別
        public string Title { get; set; }//職務別
        public List<string> Groups { get; set; }//職稱別
    }
}
