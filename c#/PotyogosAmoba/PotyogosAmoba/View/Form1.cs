using PotyogosAmoba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;
using ELTE.TicTacToeGame.Persistence;

namespace PotyogosAmoba {
    public partial class Form1 : Form {
        //private ITicTacToeDataAccess _dataAccess; // adatelérés
        private TicTacToeGameModel _model;
        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _saveFileDialog;
        private bool isWinner = false;
        public int sajt;


        NewGameForm newgame;
        public int _size { get; set; }
        public Form1() {
            InitializeComponent();
            newgame = new NewGameForm();
            newgame.ShowDialog();
            _size = newgame.tableSize;
            _openFileDialog = new OpenFileDialog();
            _saveFileDialog = new SaveFileDialog();

            //model
            _model = new TicTacToeGameModel(new TextFilePersistence());
            _model._tableSize = _size;
            _model.FieldChanged += FieldChanged;
            _model.GameOver += GameOver;
            _model.TimePassed += TimePassed;
            _model.GameLoaded += GameLoaded;
            

            //first new game has to have a size
            _size = newgame.tableSize;
            _model._tableSize = _size;
            _model.NewGame();
            _model._timerOn = true;
            _model._timer.Start();
            xTimeText.Text = "0";
            oTimeText.Text = "0";

        }

        private void GameLoaded(object sender, GameLoadedEventArgs e) {
            //_model = new TicTacToeGameModel(new TextFilePersistence(), e.datas);
            _model._tableSize = e.datas.tableSize;
            _size = _model._tableSize;
            _model._tableMatrix = new int[_size, _size];
            _model._tableMatrix = e.datas.table;
            _model._currentPlayer = e.datas.currentPlayer;
            _model._placed = e.datas.placed;
            _model._xTime = e.datas.xTime;
            _model._oTime = e.datas.oTime;
            _model._timerOn = true;

            _panel.Refresh();
        }

        private void TimePassed(object sender, TimePassedEventArgs e) {
            changeTime(e.xTime, e.oTime);
        }
        public void changeTime(Int32 x, Int32 o) {
            if (InvokeRequired) {
                this.Invoke(new Action<Int32, Int32>(changeTime), new object[] {x, o });
                return;
            }

            xTimeText.Text = x.ToString();
            oTimeText.Text = o.ToString();
        }

        public void NewGame() {
            if(newgame.ShowDialog() == DialogResult.OK) {
                _size = newgame.tableSize;
                _model._tableSize = _size;
                _model.NewGame();
                _model._timerOn = true;
                _model._timer.Start();
                xTimeText.Text = "0";
                oTimeText.Text = "0";
                isWinner = false;
            }

            

            _panel.Refresh();
        }

        

        private void GameOver(object sender, GameOverEventArgs e) {
            _model._timerOn = false;
            isWinner = true;
            _panel.Refresh();
            MessageBox.Show(e.type, "Game over");
            NewGame();
        }

        private void FieldChanged(object sender, FieldChangedEventArgs e) {
            
            _panel.Refresh();
        }


        private void Panel_Paint(object sender, PaintEventArgs e) {
            Bitmap bitmap = new Bitmap(_panel.Width, _panel.Height); // kép a hatékony kirajzoláshoz

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White); // háttér fehérré festése

            // játéktábla rácsai
            float fieldWidth = (float)_panel.Width / _size;
            float fieldHeight = (float)_panel.Height / _size;
            for(int i = 0; i < _size; ++i) {
                //graphics.DrawLine(Pens.Black, 0, fieldHeight, _panel.Width, fieldHeight);
                graphics.DrawLine(Pens.Black, 0, i * fieldHeight, _panel.Width, i * fieldHeight);
                //graphics.DrawLine(Pens.Black, fieldWidth, 0, fieldWidth, _panel.Height);
                graphics.DrawLine(Pens.Black, i * fieldWidth, 0, i * fieldWidth, _panel.Height);
            }
            graphics.DrawLine(Pens.Black, 0, (_size * fieldHeight) - 1, _panel.Width, (_size * fieldHeight) - 1);
            graphics.DrawLine(Pens.Black, (_size * fieldWidth) - 1, 0, (_size * fieldWidth) - 1, _panel.Height);


            // a mezőtartalmak
            for (Int32 i = 0; i < _size; i++)
                for (Int32 j = 0; j < _size; j++) {
                    if (isWinner) {
                        bool winner = false;
                        if (_model.winningCoords != null) {
                            for (int k = 0; !winner && k < _model.winningCoords.Count; ++k) {
                                winner = (_model.winningCoords[k].x == j && _model.winningCoords[k].y == i);
                            }
                            switch (_model.GetField(j, i)) {
                                case 2:
                                    graphics.FillEllipse((winner ? Brushes.Yellow : Brushes.Blue), i * fieldWidth + fieldWidth / 10, (j * fieldHeight + fieldHeight / 10), 8 * fieldWidth / 10, (8 * fieldHeight / 10));
                                    break;
                                case 1:
                                    graphics.DrawLine(new Pen((winner ? Color.Yellow : Color.Red), _panel.Width / 100), i * fieldWidth + fieldWidth / 10, j * fieldHeight + fieldHeight / 10, i * fieldWidth + 9 * fieldWidth / 10, j * fieldHeight + 9 * fieldHeight / 10);
                                    graphics.DrawLine(new Pen((winner ? Color.Yellow : Color.Red), _panel.Width / 100), i * fieldWidth + 9 * fieldWidth / 10, j * fieldHeight + fieldHeight / 10, i * fieldWidth + fieldWidth / 10, j * fieldHeight + 9 * fieldHeight / 10);
                                    break;
                            }
                        } else {
                            switch (_model.GetField(j, i)) {
                                case 2:
                                    graphics.FillEllipse(Brushes.Blue, i * fieldWidth + fieldWidth / 10, (j * fieldHeight + fieldHeight / 10), 8 * fieldWidth / 10, (8 * fieldHeight / 10));
                                    break;
                                case 1:
                                    graphics.DrawLine(new Pen(Color.Red, _panel.Width / 100), i * fieldWidth + fieldWidth / 10, j * fieldHeight + fieldHeight / 10, i * fieldWidth + 9 * fieldWidth / 10, j * fieldHeight + 9 * fieldHeight / 10);
                                    graphics.DrawLine(new Pen(Color.Red, _panel.Width / 100), i * fieldWidth + 9 * fieldWidth / 10, j * fieldHeight + fieldHeight / 10, i * fieldWidth + fieldWidth / 10, j * fieldHeight + 9 * fieldHeight / 10);
                                    break;
                            }
                        }
                        } else {
                        switch (_model.GetField(j,i)) {
                            case 2:
                                graphics.FillEllipse(Brushes.Blue, i * fieldWidth + fieldWidth/ 10, (j * fieldHeight + fieldHeight / 10), 8 * fieldWidth / 10, (8 * fieldHeight / 10));
                                break;
                            case 1:
                                graphics.DrawLine(new Pen(Color.Red, _panel.Width / 100), i * fieldWidth + fieldWidth / 10, j * fieldHeight + fieldHeight / 10, i * fieldWidth + 9 * fieldWidth / 10, j * fieldHeight + 9 * fieldHeight / 10);
                                graphics.DrawLine(new Pen(Color.Red, _panel.Width / 100), i * fieldWidth + 9 * fieldWidth / 10, j * fieldHeight + fieldHeight / 10, i * fieldWidth + fieldWidth / 10, j * fieldHeight + 9 * fieldHeight / 10);
                                break;
                        }
                    }
                    
                }

            e.Graphics.DrawImage(bitmap, 0, 0);
            currentTextBox.Text = (_model._currentPlayer == 1) ? "X" : "O";
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e) {
            if (_model._timerOn) {
                Int32 x = _size * e.Y / _panel.Height;
                Int32 y = _size * e.X / _panel.Width;

                try {
                    _model.StepGame(x, y); // lépünk a játékban
                } catch { }

            }            
            
        }

        

        private void NewGameButton_Click(object sender, EventArgs e) {
            NewGame();
            _panel.Refresh();
        }

        private void PauseGameButton_Click(object sender, EventArgs e) {
            if (_model._timerOn) {
                _model._timer.Stop();
            } else {
               _model. _timer.Start();
            }
            _model._timerOn = !_model._timerOn;
        }

        private void SaveGameButton_Click(object sender, EventArgs e) {
            _model._timerOn = false;
            if (_saveFileDialog.ShowDialog() == DialogResult.OK) {
                try {
                    _model.SaveGame(_saveFileDialog.FileName);
                } catch (DataException) {
                    MessageBox.Show("An error has occured.", "Tic-Tac-Toe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            _model._timerOn = true;

        }

        private void LoadGameButton_Click(object sender, EventArgs e) {
            _model._timerOn = false;

            if (_openFileDialog.ShowDialog() == DialogResult.OK) {
                try {
                    _model.LoadGame(_openFileDialog.FileName);
                } catch (DataException) {
                    MessageBox.Show("An error has occured.", "Tic-Tac-Toe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            _model._timerOn = true;

        }
    }
}
