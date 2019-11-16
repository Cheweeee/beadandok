using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace PotyogosAmoba.Model {
    class FieldChangedEventArgs : EventArgs {
        public int changedX { get; private set; }
        public int changedY { get; private set; }

        public FieldChangedEventArgs(int x, int y) {
            changedX = x;
            changedY = y;
        }
    }
}
