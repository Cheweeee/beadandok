#include "newgame.h"
#include <QHBoxLayout>
#include <QMessageBox>

newGame::newGame(QWidget *parent) : QDialog(parent)
{
    setFixedSize(300,200);
    setWindowTitle("New game");
    setModal(true);



    QLabel* givenLabel = new QLabel("Choose a table size!");
    givenLabel->setStyleSheet("QLabel { font-weight: bold; font-style: italic; text-decoration: underline;}");

    QPushButton *given1 = new QPushButton("3x7");
    connect(given1, SIGNAL(clicked()), this, SLOT(choice1()));
    QPushButton *given2 = new QPushButton("5x10");
    connect(given2, SIGNAL(clicked()), this, SLOT(choice2()));
    QPushButton *given3 = new QPushButton("7x13");
    connect(given3, SIGNAL(clicked()), this, SLOT(choice3()));


    QHBoxLayout *givens = new QHBoxLayout();
    givens->addWidget(given1);
    givens->addWidget(given2);
    givens->addWidget(given3);

    QVBoxLayout *mainLayout = new QVBoxLayout();
    mainLayout->addLayout(givens);

    setLayout(mainLayout);
}

void newGame::choice1(){
   _row=7;
   _col=3;
   close();
}
void newGame::choice2(){
    _row=10;
    _col=5;
    close();
}
void newGame::choice3(){
    _row=13;
    _col=7;
    close();
}

