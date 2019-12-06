using PotyogosAmobaWPF.Model;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Threading;

namespace PotyogosAmoba.Model {
    public interface ITicTacToeModel {
        int _currentPlayer { get; set; }
        int _oTime { get; set; }
        int _placed { get; set; }
        int[,] _tableMatrix { get; set; }
        int _tableSize { get; set; }
        DispatcherTimer _timer { get; set; }
        bool _timerOn { get; set; }
        int _xTime { get; set; }
        List<Coordinate> winningCoords { get; set; }

        event EventHandler<FieldChangedEventArgs> FieldChanged;
        event EventHandler<GameLoadedEventArgs> GameLoaded;
        event EventHandler<GameOverEventArgs> GameOver;
        event EventHandler<GameStartedEventArgs> GameStarted;
        event EventHandler<TimePassedEventArgs> TimePassed;

        void CheckGame(int x, int y);
        int GetField(int x, int y);
        bool LoadGame(string path);
        void NewGame();
        bool SaveGame(string path);
        void StepGame(int x, int y);
    }
}