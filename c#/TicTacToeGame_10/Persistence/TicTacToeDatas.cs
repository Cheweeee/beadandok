using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotyogosAmoba.Persistence {
    public class TicTacToeDatas {
        public int tableSize { get; set; }
        public int currentPlayer { get; set; }
        public int xTime { get; set; }
        public int oTime { get; set; }
        public int placed { get; set; }
        public int[,] table{ get; set; }

    }
}
