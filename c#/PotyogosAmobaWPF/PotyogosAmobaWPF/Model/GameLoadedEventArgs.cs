using PotyogosAmoba.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotyogosAmoba.Model {
    public class GameLoadedEventArgs {
        public TicTacToeDatas datas { get; set; }

        public GameLoadedEventArgs(TicTacToeDatas data) {
            datas = data;
        }
    }
}
