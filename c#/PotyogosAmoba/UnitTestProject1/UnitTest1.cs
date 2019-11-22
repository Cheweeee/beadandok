using System;
using System.Windows.Forms;
using ELTE.TicTacToeGame.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotyogosAmoba.Model;

namespace UnitTestProject1 {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
        }
        private TicTacToeGameModel model;
        [TestMethod]
        public void InitTest() {
            model = new TicTacToeGameModel(new TextFilePersistence());
        }

        [TestMethod]
        public void NewGameTest() {
            model = new TicTacToeGameModel(new TextFilePersistence());
            model._tableSize = 10;
            model.NewGame();
            for (int i = 0; i < model._tableSize; i++) {
                for (int j = 0; j < model._tableSize; j++) {
                    Assert.AreEqual(model._tableMatrix[i, j], 0);
                }
            }
        }
        [TestMethod]
        public void StepTest() {
            model = new TicTacToeGameModel(new TextFilePersistence());
            model._tableSize = 10;
            model.NewGame();
            model.StepGame(0, 0);
            for (int i = 0; i < model._tableSize; i++) {
                for (int j = 0; j < model._tableSize; j++) {
                    if(i==model._tableSize-1 && j == 0) {
                        Assert.AreEqual(model._tableMatrix[i, j], 1);
                    } else {
                        Assert.AreEqual(model._tableMatrix[i, j], 0);
                    }
                }
            }
            Assert.AreEqual(model._currentPlayer, 2);
            model.StepGame(0, 0);
            for (int i = 0; i < model._tableSize; i++) {
                for (int j = 0; j < model._tableSize; j++) {
                    if (i == model._tableSize - 1 && j == 0) {
                        Assert.AreEqual(model._tableMatrix[i, j], 1);
                    } else if (i == model._tableSize - 2 && j == 0) {
                        Assert.AreEqual(model._tableMatrix[i, j], 2);
                    } else {
                        Assert.AreEqual(model._tableMatrix[i, j], 0);
                    }
                }
            }
            Assert.AreEqual(model._currentPlayer, 1);
        }

        [TestMethod]
        public void WinTest() {
            model = new TicTacToeGameModel(new TextFilePersistence());
            model._tableSize = 10;
            model.NewGame();
            Assert.IsTrue(model.winningCoords == null); //noone has won yet
            model.StepGame(0, 0);
            Assert.IsTrue(model.winningCoords == null);
            model.StepGame(0, 0);
            Assert.IsTrue(model.winningCoords == null);
            model.StepGame(0, 1);
            Assert.IsTrue(model.winningCoords == null);
            model.StepGame(0, 1);
            Assert.IsTrue(model.winningCoords == null);
            model.StepGame(0, 2);
            Assert.IsTrue(model.winningCoords == null);
            model.StepGame(0, 2);
            Assert.IsTrue(model.winningCoords == null); //still noone
            model.StepGame(0, 3);
            Assert.IsTrue(model.winningCoords != null); //X won
            Assert.IsTrue(model.winningCoords.Count != 0); //with these coordinates
        }

        [TestMethod]
        public void SaveTest() {
            model = new TicTacToeGameModel(new TextFilePersistence());
            model._tableSize = 10;
            model.NewGame();
            model.StepGame(0, 0);
            model.StepGame(0, 0);
            model.StepGame(0, 1);
            model.StepGame(0, 1);
            model.StepGame(0, 2);
            model.StepGame(0, 2);
            Assert.IsTrue(model.SaveGame(".'\'PotyogosAmoba"));
        }
        [TestMethod]
        public void LoadTest() {
            model = new TicTacToeGameModel(new TextFilePersistence());                                                                                                Assert.IsTrue(model.SaveGame("D:'\'beadandok'\'c#'\'PotyogosAmoba'\'PotyogosAmoba"));
            //here you need to select a save file to test the load
            //uncomment, if this has to be tested too
            OpenFileDialog open = new OpenFileDialog();
            //if (open.ShowDialog() == DialogResult.OK) {
            //    Assert.IsTrue(model.LoadGame(open.FileName)); 
            //}
        }

        [TestMethod]
         public void NoMoreSpaceInColumnTest() {
            model = new TicTacToeGameModel(new TextFilePersistence());
            model._tableSize = 10;
            model.NewGame();
          
            for (int i = 0;  i < 10;  i++) {
                model.StepGame(0, 0);
            }

            Assert.AreEqual(model._placed, 10);
            Assert.AreEqual(model._currentPlayer, 1);

            model.StepGame(0, 0);
            Assert.AreEqual(model._placed, 10); //did not count as a step
            Assert.AreEqual(model._currentPlayer, 1); //current player stays the same

        }
    }
}
