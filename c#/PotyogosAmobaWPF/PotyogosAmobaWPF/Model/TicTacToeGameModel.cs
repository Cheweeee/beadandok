using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;
using ELTE.TicTacToeGame.Persistence;
using PotyogosAmoba.Persistence;
using PotyogosAmobaWPF.Model;
using System.Windows.Threading;

namespace PotyogosAmoba.Model {
    public class TicTacToeGameModel : ITicTacToeModel {
        #region variables

        //0 nobody, 1 X, 2 O
        public int[,] _tableMatrix { get; set; }
        public int _tableSize { get; set; }
        public int _currentPlayer { get; set; }
        public int _placed { get; set; }
        private TextFilePersistence _persistence;
        public int _xTime { get; set; }
        public int _oTime { get; set; }
        public DispatcherTimer _timer { get; set; }
        public bool _timerOn { get; set; }
        public List<Coordinate> winningCoords { get; set; }

        public event EventHandler<FieldChangedEventArgs> FieldChanged;
        public event EventHandler<GameOverEventArgs> GameOver;
        public event EventHandler<GameLoadedEventArgs> GameLoaded;
        public event EventHandler<TimePassedEventArgs> TimePassed;
        public event EventHandler<GameStartedEventArgs> GameStarted;
        #endregion

        public TicTacToeGameModel(TextFilePersistence pers) {
            _persistence = pers;
            //timer
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(dispatherTimer_Tick);
            _timer.Interval += new TimeSpan(0,0,1);
            _timer.Start();
            _timerOn = true;
            _xTime = 0;
            _oTime = 0;
        }

        private void dispatherTimer_Tick(object sender, EventArgs e) {
            if (_timerOn) {
                if (_currentPlayer == 1) {
                    _xTime++;
                } else {
                    _oTime++;
                }
                TimePassed?.Invoke(this, new TimePassedEventArgs(_xTime, _oTime));
            }
        }

        public void NewGame() {
            _tableMatrix = new int[_tableSize, _tableSize];
            for (int i = 0; i < _tableSize; ++i) {
                for (int j = 0; j < _tableSize; ++j) {
                    _tableMatrix[i, j] = 0;
                }
            }
            _currentPlayer = 1;
            _placed = 0;
            _xTime = 0;
            _timer.Start();
            _oTime = 0;
            if (winningCoords != null) {
                winningCoords.Clear();
            }
            GameStarted?.Invoke(this, new GameStartedEventArgs());
        }
        public void StepGame(int x, int y) {
            if (_timerOn) {

                int place = _tableSize - 1;
                while (place >= 0 && _tableMatrix[place, y] != 0) {
                    --place;
                }
                if (place != -1) {
                    _tableMatrix[place, y] = _currentPlayer;
                    ++_placed;
                    CheckGame(place, y);
                    _currentPlayer = (_currentPlayer % 2) + 1;
                    FieldChanged?.Invoke(this, new FieldChangedEventArgs(place, y));
                }
            }
        }

        public int GetField(int x, int y) {
            return _tableMatrix[x, y];
        }
        public void CheckGame(int x, int y) {
            if (_placed == _tableSize * _tableSize) {
                FieldChanged?.Invoke(this, new FieldChangedEventArgs(x, y));
                GameOver?.Invoke(this, new GameOverEventArgs(0, "", new List<Coordinate>()));
            } else {
                List<Coordinate> c;
                string winDir = CheckFour(x, y, out c);  //hor, ltop, rtop
                c.Add(new Coordinate(x, y));
                if (winDir != "") {
                    winningCoords = c;
                    FieldChanged?.Invoke(this, new FieldChangedEventArgs(x, y));
                    GameOver?.Invoke(this, new GameOverEventArgs(_currentPlayer, winDir, winningCoords));
                }
            }
        }

        private string CheckFour(int x, int y, out List<Coordinate> c) {
            int count = 0;
            int i = x;
            int j = y;
            c = new List<Coordinate>();
            c.Clear();

            ////Vertical
            //while (i >= 0 && _tableMatrix[i, j] == _tableMatrix[x, y]) {
            //    if (i != x || j != y) {
            //        ++count;
            //    }
            //    --i;
            //}
            //i = x;
            //j = y;
            //while (i < _tableSize && _tableMatrix[i, j] == _tableMatrix[x, y]) {

            //    if (i != x || j != y) {
            //        ++count;
            //    }
            //    ++i;
            //}
            //if (count >= 3) {
            //    return "vert";
            //}
            //count = 0;
            //Horizontal
            i = x;
            j = y;
            while (j >= 0 && _tableMatrix[i, j] == _tableMatrix[x, y]) {
                if (i != x || j != y) {
                    c.Add(new Coordinate(i, j));
                    ++count;
                }
                --j;
            }
            i = x;
            j = y;
            while (j < _tableSize && _tableMatrix[i, j] == _tableMatrix[x, y]) {
                if (i != x || j != y) {
                    c.Add(new Coordinate(i, j));
                    ++count;
                }
                ++j;
            }
            if (count >= 3) {
                return "hor";
            }
            count = 0;
            c.Clear();
            //Diagonal left top to right bot
            i = x;
            j = y;
            while (j < _tableSize && i < _tableSize && _tableMatrix[i, j] == _tableMatrix[x, y]) {
                if (i != x || j != y) {
                    c.Add(new Coordinate(i, j));
                    ++count;
                }
                ++j;
                ++i;
            }
            i = x;
            j = y;
            while (j >= 0 && i >= 0 && _tableMatrix[i, j] == _tableMatrix[x, y]) {
                if (i != x || j != y) {
                    c.Add(new Coordinate(i, j));
                    ++count;
                }
                --j;
                --i;
            }
            if (count >= 3) {
                return "ltop";
            }
            count = 0;
            c.Clear();
            //Diagonal left bot to right top
            i = x;
            j = y;
            while (j < _tableSize && i >= 0 && _tableMatrix[i, j] == _tableMatrix[x, y]) {
                if (i != x || j != y) {
                    ++count;
                }
                ++j;
                --i;
            }
            i = x;
            j = y;
            while (j >= 0 && i < _tableSize && _tableMatrix[i, j] == _tableMatrix[x, y]) {
                if (i != x || j != y) {
                    c.Add(new Coordinate(i, j));
                    ++count;
                }
                --j;
                ++i;
            }
            if (count >= 3) {
                return "rop";
            }
            return "";
        }

        public bool LoadGame(String path) {
            if (_persistence == null)
                return false;

            // végrehajtjuk a betöltést
            TicTacToeDatas values = _persistence.Load(path);

            //if (values.tableSize != _tableSize)
            //    throw new Exception("Error occured during game loading.");
            GameLoaded?.Invoke(this, new GameLoadedEventArgs(values));
            return true;
        }

        public bool SaveGame(String path) {
            if (_persistence == null)
                return false;

            // az értékeket kimásoljuk egy új tömbbe
            TicTacToeDatas values = new TicTacToeDatas();
            values.tableSize = _tableSize;
            values.currentPlayer = _currentPlayer;
            values.xTime = _xTime;
            values.oTime = _oTime;
            values.placed = _placed;
            values.table = _tableMatrix;


            // végrehajtjuk a mentést
            _persistence.Save(path, values);
            return true;
        }
    }
}
