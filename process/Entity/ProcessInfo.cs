using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ProcessInfo
    {
        string pn;
        string pt;
        double pks;
        double pmc;
        double perProTime;
        double perMT;
        int i;
        int ui;
        public int UserId
        {
            get { return ui; }
            set { ui = value; }
        }
        public int Id
        {
            get { return i; }
            set { i = value; }
        }
        public string ProcessName
        {
            get { return pn; }
            set { pn = value; }
        }


        public string ProcessTitle
        {
            get { return pt; }
            set { pt = value; }
        }

        public double PercentageMouseTime
        {
            get { return perMT; }
            set { perMT = value; }
        }
        public double PerKeyStroke
        {
            get { return pks; }
            set { pks = value; }
        }


        public double PerMouseCount
        {
            get { return pmc; }
            set { pmc = value; }
        }


        public double PercentageProcessActiveTime
        {
            get { return perProTime; }
            set { perProTime = value; }
        }

    }
}
