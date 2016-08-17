using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entity
{
    [Table("ProcessData")]
    public class ProcessObject
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public string ProcessTitle { get; set; }
        public int UId { get; set; }
        public System.DateTime StartActiveTime { get; set; }
        public Nullable<System.DateTime> EndActiveTime { get; set; }
        public Nullable<int> keyStroke { get; set; }
        public Nullable<int> P_Hieght { get; set; }
        public Nullable<int> P_Width { get; set; }
        public Nullable<int> MouseUseCount { get; set; }
        public Nullable<System.TimeSpan> MouseTime { get; set; }
    }
}