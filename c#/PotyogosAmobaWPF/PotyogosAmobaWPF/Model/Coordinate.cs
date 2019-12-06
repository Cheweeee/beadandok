using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotyogosAmoba.Model {
    public class Coordinate {
        public int x { get; set; }
        public int y { get; set; }
        public Coordinate(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
}
