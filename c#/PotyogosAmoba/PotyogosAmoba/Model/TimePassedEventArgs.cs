using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotyogosAmoba.Model {
    class TimePassedEventArgs {
        public int xTime { get; set; }
        public int oTime { get; set; }

        public TimePassedEventArgs(int x, int o) {
            xTime = x;
            oTime = o;
        }
    }
}
