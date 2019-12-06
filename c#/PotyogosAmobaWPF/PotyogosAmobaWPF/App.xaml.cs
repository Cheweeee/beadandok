using ELTE.TicTacToeGame.Persistence;
using ELTE.TicTacToeGame.ViewModel;
using Microsoft.Win32;
using PotyogosAmoba.Model;
using PotyogosAmobaWPF.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace PotyogosAmobaWPF {
        /// <summary>
    /// Alkalmazás típusa.
    /// </summary>
    public partial class App : Application {
        private TextFilePersistence _dataAccess;
        private ITicTacToeModel _model;
        private TicTacToeViewModel _viewModel;
        private MainGameWindow _window;
        private NewGameWindow _newGame;
        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _saveFileDialog;

        /// <summary>
        /// Alkalmazás példányosítása.
        /// </summary>
        public App() {
            Startup += new StartupEventHandler(App_Startup);
        }

        /// <summary>
        /// Alkalmazás indulásának eseménykezelője.
        /// </summary>
        private void App_Startup(object sender, StartupEventArgs e) {
            _dataAccess = new TextFilePersistence();

            _newGame = new NewGameWindow();

            _model = new TicTacToeGameModel(_dataAccess);
            //_model.GameWon += new EventHandler<GameWonEventArgs>(Model_GameWon);
            _model.NewGame();

            _viewModel = new TicTacToeViewModel(_model);
            _viewModel.LoadGame += new EventHandler(ViewModel_LoadGame); // kezeljük a nézetmodell eseményeit
            _viewModel.SaveGame += new EventHandler(ViewModel_SaveGame);
            _viewModel.GameExit += new EventHandler(ViewModel_GameExit);
            _viewModel.NewGame += new EventHandler(ViewModel_NewGame);
            _model.GameOver += new EventHandler<GameOverEventArgs>(Model_GameOver);

            _window = new MainGameWindow();
            _window.DataContext = _viewModel;
            _window.Show();

        }

        private void ViewModel_NewGame(object sender, EventArgs e) {
            _newGame = new NewGameWindow();
            _newGame.ShowDialog();
            if (_newGame.DialogResult.GetValueOrDefault(true)) {
                _viewModel.TableSize = _newGame.size;
                _model._tableSize = _newGame.size;
                _model.NewGame();
            }

        }

        #region Model event handlers


        /// <summary>
        /// Játék végének eseménykezelése.
        /// </summary>
        private void Model_GameOver(object sender, GameOverEventArgs e) {
            switch (e.type) {
                case "X won!":
                    MessageBox.Show("The X player won", "Game over", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    break;
                case "O won!":
                    MessageBox.Show("The O player won!", "Game over!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    break;
                default:
                    MessageBox.Show("Tie!", "Game over!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    break;
            }

            _model.NewGame();
        }

        #endregion

        #region ViewModel event handlers

        /// <summary>
        /// Játék betöltésének eseménykezelője.
        /// </summary>
        private async void ViewModel_LoadGame(object sender, System.EventArgs e) {
            if (_openFileDialog == null) {
                _openFileDialog = new OpenFileDialog();
                _openFileDialog.Title = "Tic-Tac-Toe - Load Game";
                //_openFileDialog.Filter = "Szövegfájlok|*.txt";
            }

            // nyithatunk új nézetet
            if (_openFileDialog.ShowDialog() == true) {
                try { 
                _model.LoadGame(_openFileDialog.FileName); // játék betöltése
                } catch (Exception) {
                    MessageBox.Show("An error occured during loading", "Tic-Tac-Toe", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        /// <summary>
        /// Játék mentésének eseménykezelője.
        /// </summary>
        private async void ViewModel_SaveGame(object sender, System.EventArgs e) {
            if (_saveFileDialog == null) {
                _saveFileDialog = new SaveFileDialog();
                _saveFileDialog.Title = "Tic-Tac-Toe - Save Game";
                //_saveFileDialog.Filter = "Szövegfájlok|*.txt";
            }

            if (_saveFileDialog.ShowDialog() == true) {
                try {
                    _model.SaveGame(_saveFileDialog.FileName); // játék mentése
                } catch (Exception) {
                    MessageBox.Show("An error occured during saving", "Tic-Tac-Toe", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        /// <summary>
        /// Kilépés eseménykezelője.
        /// </summary>
        private void ViewModel_GameExit(object sender, System.EventArgs e) {
            Shutdown(); // a teljes alkalmazás bezárása
        }

        #endregion
    }
}
