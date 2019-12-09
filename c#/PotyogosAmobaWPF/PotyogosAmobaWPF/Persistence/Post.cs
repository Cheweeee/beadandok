using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotyogosAmobaWPF.Persistence {
    public class SaveDb {
        public int Id { get; set; }
        public string SaveName { get; set; }
        public int TableSize { get; set; }
        public int CurrentPlayer { get; set; }
        public int XTime { get; set; }
        public int OTime { get; set; }
        public int Placed { get; set; }
        public string Table { get; set; }
    }
    public class TicTacToeDbContext : DbContext {
        public DbSet<SaveDb> Saves { get; set; }
    }
}
