using PotyogosAmoba.Persistence;
using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace ELTE.TicTacToeGame.Persistence {
    /// <summary>
    /// Tic-Tac-Toe szöveges fájl alapú adatelérés típusa.
    /// </summary>
    public class TextFilePersistence {
        /// <summary>
        /// Fájl betöltése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        /// <returns>A beolvasott mezőértékek.</returns>
        public TicTacToeDatas Load(String path) {
            if (path == null)
                throw new ArgumentNullException("path");

            try {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása olvasásra
                {
                    String[] numbers = reader.ReadToEnd().Split(); // fájl tartalmának feldarabolása a whitespace karakterek mentén

                    // a szöveget számmá, majd játékossá konvertáljuk, és ezzel a tömbbel visszatérünk
                    //return numbers.Select(number => (Player)Int32.Parse(number)).ToArray();

                    // ugyanez ciklussal:

                    TicTacToeDatas values = new TicTacToeDatas();
                    values.tableSize = Convert.ToInt32(numbers[0]);
                    values.currentPlayer = Convert.ToInt32(numbers[1]);
                    values.xTime = Convert.ToInt32(numbers[2]);
                    values.oTime = Convert.ToInt32(numbers[3]);
                    values.placed = Convert.ToInt32(numbers[4]);
                    values.table = new int[values.tableSize, values.tableSize];
                    Debug.WriteLine(numbers.Length);
                    for (int i = 5; i < numbers.Length - 1; i++)
                        values.table[(i - 5) / values.tableSize, (i - 5) % values.tableSize] = Convert.ToInt32(numbers[i]);
                    return values;

                } // bezárul a fájl
            } catch // ha bármi hiba történt
              {
                throw new Exception("Error occured during reading.");
            }
        }

        /// <summary>
        /// Fájl mentése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        /// <param name="values">A mezőértékek.</param>
        public void Save(String path, TicTacToeDatas values) {
            if (path == null)
                throw new ArgumentNullException("path");
            if (values == null)
                throw new ArgumentNullException("values");

            try {
                using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása írásra
                {
                    // a mezőket számmá, majd szöveggé konvertáljuk, végül aggregáljuk őket szóközökkel közrezárva
                    //writer.Write(values.Select(value => ((Int32)value).ToString()).Aggregate((value1, value2) => value1 + " " + value2));

                    // ugyanez ciklussal:
                    writer.Write(values.tableSize + " " + values.currentPlayer + " " + values.xTime + " " + values.oTime + " " + values.placed + " ");
                    for (int i = 0; i < values.tableSize; i++) {
                        for (int j = 0; j < values.tableSize; j++) {
                            writer.Write(values.table[i, j] + " "); // kiírjuk a mezőket
                        }
                    }

                }
            } catch // ha bármi hiba történt
              {
                throw new Exception("Error occured during writing.");
            }
        }
    }
}
