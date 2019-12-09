using PotyogosAmoba.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PotyogosAmobaWPF.Persistence {
    public class DBPersistence {
        TicTacToeDbContext _context = new TicTacToeDbContext();

        /// <summary>
        /// Fájl betöltése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        /// <returns>A beolvasott mezőértékek.</returns>
        public TicTacToeDatas Load(String path) {
            Debug.WriteLine("LOADDDDDDDDDDDDDD");
            SaveDb loaded = _context.Saves.SingleOrDefault(p => p.SaveName == path);
            if(loaded != null) {
                TicTacToeDatas datas = new TicTacToeDatas();
                datas.currentPlayer = loaded.CurrentPlayer;
                datas.oTime = loaded.OTime;
                datas.placed = loaded.Placed;
                string[] tableSplit = loaded.Table.Split(' ');
                datas.table = new int[loaded.TableSize, loaded.TableSize];
                for (int i = 0; i < loaded.TableSize; i++) {
                    for (int j = 0; j < loaded.TableSize; j++) {
                        datas.table[i, j] = Convert.ToInt32(tableSplit[i * loaded.TableSize + j]);
                    }
                }
                datas.tableSize = loaded.TableSize;
                datas.xTime = loaded.XTime;
                return datas;
            }
            return null;
        }

        /// <summary>
        /// Fájl mentése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        /// <param name="values">A mezőértékek.</param>
        public void Save(String name, TicTacToeDatas values) {
            SaveDb toSave = new SaveDb();
            toSave.SaveName = name;
            toSave.CurrentPlayer = values.currentPlayer;
            toSave.OTime = values.oTime;
            toSave.XTime = values.xTime;
            toSave.Placed = values.placed;
            toSave.TableSize = values.tableSize;
            StringBuilder myTable = new StringBuilder();
            for(int i = 0; i < values.tableSize; ++i) {
                for(int j = 0; j < values.tableSize; ++j) {
                    myTable.Append(values.table[i, j] + " ");
                }
            }
            toSave.Table = myTable.ToString();
            Debug.WriteLine("works");
            _context.Saves.Add(toSave);
            _context.SaveChanges();
        }
        public List<string> GetList() {
            return _context.Saves.Select(p => p.SaveName).ToList();
        }
    }

}