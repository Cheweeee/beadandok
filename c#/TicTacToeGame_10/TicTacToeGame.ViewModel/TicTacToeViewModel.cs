using ELTE.TicTacToeGame.Model;
using ELTE.TicTacToeGame.Persistence;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ELTE.TicTacToeGame.ViewModel
{
    /// <summary>
    /// Tic-Tac-Toe nézetmodell típusa.
    /// </summary>
    public class TicTacToeViewModel : ViewModelBase
    {
        #region Private methods

        private ITicTacToeModel _model; // játék
        public int TableSize { get; set; }

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
            _model = model;
            _model.GameStarted += new EventHandler(Model_GameStarted);
            _model.FieldChanged += new EventHandler<FieldChangedEventArgs>(Model_FieldChanged);

            // parancsok kezelése
            NewGameCommand = new DelegateCommand(param => _model.NewGame());
            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            ExitGameCommand = new DelegateCommand(param => OnGameExit());

            // játéktábla létrehozása
            Fields = new ObservableCollection<TicTacToeField>();
            TableSize = 30;
            Refresh();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Nézetmodell frissítése.
        /// </summary>
        private void Refresh()
        {
            Fields.Clear();

            for (Int32 x = 0; x < TableSize; x++) // inicializáljuk a mezőket
            {
                for (Int32 y = 0; y < TableSize; y++)
                {
                    Fields.Add(new TicTacToeField
                    {
                        Player = PlayerToField(_model[0, 0]),
                        X = x,
                        Y = y,
                        FieldChangeCommand = new DelegateCommand(param =>
                        {
                            try
                            {
                                _model.StepGame((param as TicTacToeField).X, (param as TicTacToeField).Y);
                                // ha mezőre lépünk, akkor lépünk a játékban
                            }
                            catch { }
                        })
                    });
                }
            }
        }

        /// <summary>
        /// Játékos szöveggé alakítása.
        /// </summary>
        /// <param name="player">Játékos.</param>
        /// <returns>Játékos szöveges megfelelője.</returns>
        private String PlayerToField(Player player)
        {
            switch (player)
            { 
                case Player.PlayerX:
                    return "X";
                case Player.PlayerO:
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
        private void Model_GameStarted(object sender, EventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Modell mezőváltozásának eseménykezelése.
        /// </summary>
        private void Model_FieldChanged(object sender, FieldChangedEventArgs e)
        {
            Fields.FirstOrDefault(field => field.X == e.X && field.Y == e.Y).Player = PlayerToField(_model[e.X, e.Y]);
            // lineáris keresés a megadott sorra, oszlopra, majd a játékos átírása
        }

        #endregion

        #region Event methods

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

        /// <summary>
        /// Játékból való kilépés eseménykiváltása.
        /// </summary>
        private void OnGameExit()
        {
            if (GameExit != null)
                GameExit(this, EventArgs.Empty);
        }

        #endregion
    }
}
