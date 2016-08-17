using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entity
{
    [Table("Application")]
   public class ApplicationObject
    {

        public int id { get; set; }
        public string App_Name { get; set; }
        public string Exe_Name { get; set; }
        public string Productivity { get; set; }
    }
}
