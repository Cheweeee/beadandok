using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotyogosAmoba.Model {
    public class GameOverEventArgs : EventArgs{
        //0 döntetlen, 1 X nyert, 2 O nyert
        public string type { get; private set; }
        public string direction { get; private set; }
        public List<Coordinate> coords { get; private set; }

        public GameOverEventArgs(int t, string dir, List<Coordinate> c) {
            switch (t) {
                case 0:
                    type = "Tie!";
                    break;
                case 1:
                    type = "X won!";
                    break;
                case 2:
                    type = "O won!";
                    break;
            }
            direction = dir;
            coords = c;
        }
    }
}
