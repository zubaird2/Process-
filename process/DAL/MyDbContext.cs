using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class MyDbContext:DbContext
    {

        public MyDbContext()
        { }
        public DbSet<ProcessObject> ProcessDatas { get; set; }
        public DbSet<ImgObject> ImgDatas { get; set; }

        public DbSet<ApplicationObject> Applications { get; set; }
        public DbSet<UserObjects> Users { get; set; }
    }

}
