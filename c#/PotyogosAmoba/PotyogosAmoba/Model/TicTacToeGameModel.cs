using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ELTE.TicTacToeGame.Persistence;
using PotyogosAmoba.Persistence;
using System.Timers;

namespace PotyogosAmoba.Model {
    class TicTacToeGameModel {
        #region variables

        //0 nobody, 1 X, 2 O
        public int[,] _tableMatrix { get; set; }
        public int _tableSize { get; set; }
        public int _currentPlayer { get; set; }
        public int _placed { get; set; }
        private TextFilePersistence _persistence;
        public int _xTime { get; set; }
        public int _oTime { get; set; }
        public Timer _timer { get; set; }
        public bool _timerOn { get; set; }

        public event EventHandler<FieldChangedEventArgs> FieldChanged;
        public event EventHandler<GameOverEventArgs> GameOver;
        public event EventHandler<GameLoadedEventArgs> GameLoaded;
        public event EventHandler<TimePassedEventArgs> TimePassed;
        #endregion

        public TicTacToeGameModel(TextFilePersistence pers) {
            _persistence = pers;
            //timer
            _timer = new Timer(1000);
            _timer.Elapsed += TimerEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timerOn = true;
            _xTime = 0;
            _oTime = 0;
        }

        
        private void TimerEvent(object sender, ElapsedEventArgs e) {
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
            _oTime = 0;
        }
        public void StepGame(int x, int y) {
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

        public int GetField(int x, int y) {
            return _tableMatrix[x, y];
        }
        public void CheckGame(int x, int y) {
            if (_placed == _tableSize * _tableSize) {
                FieldChanged?.Invoke(this, new FieldChangedEventArgs(x, y));
                GameOver?.Invoke(this, new GameOverEventArgs(0));
            } else if (CheckFour(x, y)) {
                FieldChanged?.Invoke(this, new FieldChangedEventArgs(x, y));
                GameOver?.Invoke(this, new GameOverEventArgs(_currentPlayer));
            }
        }

        private bool CheckFour(int x, int y) {
            int count = 0;
            int i = x;
            int j = y;

            //Vertical
            while (i >= 0 && _tableMatrix[i, j] == _tableMatrix[x, y]) {
                if (i != x || j != y) {
                    ++count;
                }
                --i;
            }
            i = x;
            j = y;
            while (i < _tableSize && _tableMatrix[i, j] == _tableMatrix[x, y]) {

                if (i != x || j != y) {
                    ++count;
                }
                ++i;
            }
            if (count >= 3) {
                return true;
            }
            count = 0;
            //Horizontal
            i = x;
            j = y;
            while (j >= 0 && _tableMatrix[i, j] == _tableMatrix[x, y]) {

                if (i != x || j != y) {
                    ++count;
                }
                --j;
            }
            i = x;
            j = y;
            while (j < _tableSize && _tableMatrix[i, j] == _tableMatrix[x, y]) {

                if (i != x || j != y) {
                    ++count;
                }
                ++j;
            }
            if (count >= 3) {
                return true;
            }
            count = 0;
            //Diagonal left top to right bot
            i = x;
            j = y;
            while (j < _tableSize && i < _tableSize && _tableMatrix[i, j] == _tableMatrix[x, y]) {
                if (i != x || j != y) {
                    ++count;
                }
                ++j;
                ++i;
            }
            i = x;
            j = y;
            while (j >= 0 && i >= 0 && _tableMatrix[i, j] == _tableMatrix[x, y]) {
                if (i != x || j != y) {
                    ++count;
                }
                --j;
                --i;
            }
            if (count >= 3) {
                return true;
            }
            count = 0;
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
                    ++count;
                }
                --j;
                ++i;
            }

            return count >= 3;
        }

        public void LoadGame(String path) {
            if (_persistence == null)
                return;

            // végrehajtjuk a betöltést
            TicTacToeDatas values = _persistence.Load(path);

            //if (values.tableSize != _tableSize)
            //    throw new Exception("Error occured during game loading.");
            GameLoaded?.Invoke(this, new GameLoadedEventArgs(values));
        }

        /// <summary>
        /// Játék mentése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        public void SaveGame(String path) {
            if (_persistence == null)
                return;

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
        }
    }
}
