using PotyogosAmoba.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;
using PotyogosAmobaWPF.Model;
using System.Collections.Generic;

namespace ELTE.TicTacToeGame.ViewModel
{
    /// <summary>
    /// Tic-Tac-Toe nézetmodell típusa.
    /// </summary>
    public class TicTacToeViewModel : ViewModelBase
    {
        #region Private methods

        private ITicTacToeModel _model; // játék
        private int _TableSize;

        public int TableSize {
            get { return _TableSize; }
            set {
                _TableSize = value;
                OnPropertyChanged();
            }
        }
        private int _xTime;

        public int xTime {
            get { return _xTime; }
            set {
                _xTime = value;
                OnPropertyChanged();
            }
        }
        private int _oTime;

        public int oTime {
            get { return _oTime; }
            set {
                _oTime = value;
                OnPropertyChanged();
            }
        }



        private string _CurrentPlayer;

        public string CurrentPlayer {
            get { return _CurrentPlayer; }
            set {
                _CurrentPlayer = value;
                OnPropertyChanged();
            }
        }



        #endregion

        #region Public properties

        /// <summary>
        /// Új játék kezdése parancs lekérdezése.
        /// </summary>
        public DelegateCommand NewGameCommand { get; private set; }


        /// <summary>
        /// Játék betöltése parancs lekérdezése.
        /// </summary>
        public DelegateCommand LoadGameCommand { get; private set; }

        /// <summary>
        /// Játék mentése parancs lekérdezése.
        /// </summary>
        public DelegateCommand SaveGameCommand { get; private set; }
        public DelegateCommand PauseGameCommand { get; private set; }
        

        /// <summary>
        /// Kilépés parancs lekérdezése.
        /// </summary>
        public DelegateCommand ExitGameCommand { get; private set; }

        /// <summary>
        /// Játékmező gyűjtemény lekérdezése.
        /// </summary>
        public ObservableCollection<TicTacToeField> Fields { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Játékból való kilépés eseménye.
        /// </summary>
        public event EventHandler GameExit;

        /// <summary>
        /// Játék betöltésének eseménye.
        /// </summary>
        public event EventHandler LoadGame;
        public event EventHandler PauseGame;
        public event EventHandler NewGame;

        /// <summary>
        /// Játék mentésének eseménye.
        /// </summary>
        public event EventHandler SaveGame;

        #endregion

        #region Constructors

        /// <summary>
        /// Tic-Tac-Toe játékmodell példányosítása.
        /// </summary>
        public TicTacToeViewModel(ITicTacToeModel model)
        {
            TableSize = 10;
            _model = model;
            _model._tableSize = TableSize;
            _model.NewGame();
            _model.GameStarted += new EventHandler<GameStartedEventArgs>(Model_GameStarted);
            _model.GameLoaded += Model_GameLoaded;
            _model.FieldChanged += new EventHandler<FieldChangedEventArgs>(Model_FieldChanged);
            _model.GameOver += new EventHandler<GameOverEventArgs>(Model_GameOver);
            _model.TimePassed += new EventHandler<TimePassedEventArgs>(Model_TimePassed);
            CurrentPlayer = PlayerToField(_model._currentPlayer);

            // parancsok kezelése
            //NewGameCommand = new DelegateCommand(param => _model.NewGame());
            NewGameCommand = new DelegateCommand(param => onNewGame());
            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            ExitGameCommand = new DelegateCommand(param => OnGameExit());
            PauseGameCommand = new DelegateCommand(param => OnGamePause());

            // játéktábla létrehozása
            Fields = new ObservableCollection<TicTacToeField>();
            Refresh();
        }

        private void Model_GameLoaded(object sender, GameLoadedEventArgs e) {
            _model._tableSize = e.datas.tableSize;
            _model._currentPlayer = e.datas.currentPlayer;
            _model._oTime = e.datas.oTime;
            _model._xTime = e.datas.xTime;
            _model._placed = e.datas.placed;
            _model._tableMatrix = new int[_model._tableSize, _model._tableSize];
            for(int i = 0; i < _model._tableSize; ++i) {
                for (int j = 0; j < _model._tableSize; j++) {
                    _model._tableMatrix[i, j] = e.datas.table[i, j];
                }
            }
            Refresh();
        }

        private void OnGamePause() {
            _model._timerOn = !_model._timerOn;
        }

        private void Model_TimePassed(object sender, TimePassedEventArgs e) {
            xTime = _model._xTime;
            oTime = _model._oTime;
        }

        private void Model_GameOver(object sender, GameOverEventArgs e) {
            _model._timer.Stop();
            for(int i = 0; i < _model.winningCoords.Count; ++i) {
                Fields.FirstOrDefault(field => field.X == _model.winningCoords[i].x && field.Y == _model.winningCoords[i].y).BackColor = "blue";
            }

        }

        private void OnGameExit() {
            if(GameExit != null) {
                GameExit(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Nézetmodell frissítése.
        /// </summary>
        private void Refresh()
        {
            Fields.Clear();
            //Debug.WriteLine("kaktusz");
            for (int x = 0; x < TableSize; x++) // inicializáljuk a mezőket
            {
                for (int y = 0; y < TableSize; y++)
                {
                    Fields.Add(new TicTacToeField
                    {
                        Player = PlayerToField(_model.GetField(x,y)),
                        X = x,
                        Y = y,
                        FieldChangeCommand = new DelegateCommand(param =>
                        {
                            try
                            {
                                _model.StepGame((param as TicTacToeField).X, (param as TicTacToeField).Y);
                                // ha mezőre lépünk, akkor lépünk a játékban
                            } catch { }
                        }),
                        BackColor = "white"
                    });;
                }
            }
        }

        /// <summary>
        /// Játékos szöveggé alakítása.
        /// </summary>
        /// <param name="player">Játékos.</param>
        /// <returns>Játékos szöveges megfelelője.</returns>
        private string PlayerToField(int player)
        {
            switch (player)
            { 
                case 1:
                    return "X";
                case 2:
                    return "O";
                default:
                    return String.Empty;
            }
        }

        #endregion

        #region Model event handlers

        /// <summary>
        /// Játék indításának eseménykezelése.
        /// </summary>
        private void Model_GameStarted(object sender, GameStartedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Modell mezőváltozásának eseménykezelése.
        /// </summary>
        private void Model_FieldChanged(object sender, FieldChangedEventArgs e)
        {
            CurrentPlayer = PlayerToField(_model._currentPlayer);
            //Debug.WriteLine(PlayerToField(_model.GetField(e.changedX, e.changedY)));
            Fields.FirstOrDefault(field => field.X == e.changedX && field.Y == e.changedY).Player = PlayerToField(_model.GetField(e.changedX, e.changedY));
            // lineáris keresés a megadott sorra, oszlopra, majd a játékos átírása
        }

        #endregion

        #region Event methods

        private void onNewGame() {
            if(NewGame != null) {
                NewGame(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Játék betöltése eseménykiváltása.
        /// </summary>
        private void OnLoadGame()
        {
            if (LoadGame != null)
                LoadGame(this, EventArgs.Empty);
        }

        /// <summary>
        /// Játék mentése eseménykiváltása.
        /// </summary>
        private void OnSaveGame()
        {
            if (SaveGame != null)
                SaveGame(this, EventArgs.Empty);
        }

        

        #endregion
    }
}
