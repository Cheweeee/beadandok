using System;

namespace ELTE.TicTacToeGame.ViewModel
{
    /// <summary>
    /// Mező típusa.
    /// </summary>
    public class TicTacToeField : ViewModelBase
    {
        private string _player;

        /// <summary>
        /// Játékos lekérdezése, vagy beállítása.
        /// </summary>
        public string Player 
        { 
            get { return _player; }
            set
            {
                if (_player != value)
                {
                    _player = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Oszlop lekérdezése, vagy beállítása.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Sor lekérdezése, vagy beállítása.
        /// </summary>
        public int Y { get; set; }

        private string _BackColor;

        public string BackColor {
            get { return _BackColor; }
            set {
                _BackColor = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Mezőváltoztató parancs lekérdezése, vagy beállítása.
        /// </summary>
        public DelegateCommand FieldChangeCommand { get; set; }
    }
}
