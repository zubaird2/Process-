using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Table("ImgData")]
   public class ImgObject
    {
        public int Id { get; set; }
        public string img { get; set; }
        public Nullable<int> UId { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
    }
}
