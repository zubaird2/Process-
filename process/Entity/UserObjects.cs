using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Table("User_Table")]
   public class UserObjects
    {
        [Key]
        public int UID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
