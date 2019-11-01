#include "spaceview.h"
#include <QPainter>
#include <QDebug>
#include <QKeyEvent>
#include <QGridLayout>
#include <QVBoxLayout>
#include <QPushButton>
#include "spacemodel.h"

SpaceView::SpaceView(QWidget *parent)
    : QWidget(parent)
{

    setFixedSize(300,400);
    setWindowTitle("Space Game");
    _mainGrid = new QGridLayout();
    _newGame = new newGame();
    qDebug() << "asd";
    _newGame->exec();
    qDebug() << "ASDASD";
    _lcdEnemy = new QLCDNumber();
    _lcdTime = new QLCDNumber();
    _amaz = new QHBoxLayout();
    _amaz->addWidget(_lcdEnemy);
    _amaz->addWidget(_lcdTime);
    _az = new QVBoxLayout();
    _az->addLayout(_amaz);

    _rowCount = _newGame->getRow();
    _colCount = _newGame->getCol();
    _model.setRow(_rowCount);
    _model.setCol(_colCount);
    _model.newGame();
    drawTable();
    _az->addLayout(_mainGrid);
    setLayout(_az);
    connect(&_model, SIGNAL(fieldChanged()), this, SLOT(model_fieldChanged()));
    connect(&_model, SIGNAL(pointEarned()), this, SLOT(model_pointEarned()));
    connect(&_model, SIGNAL(timePassed()), this, SLOT(model_timePassed()));

    //connect(&_model, SIGNAL(fieldChanged()), this, SLOT(model_fieldChanged()));
}
void SpaceView::drawTable(){
    _buttonVec.resize(_rowCount);
    for (int i = 0; i < _rowCount; ++i) {
        _buttonVec[i].resize(_colCount);
        for (int j = 0; j < _colCount; ++j){
            _buttonVec[i][j] = new QPushButton("", this); // gomb létrehozása
            _buttonVec[i][j]->setEnabled(false);
            if(_model.getField(i, j) == SpaceModel::Characters::Enemy){
                _buttonVec[i][j]->setStyleSheet("background-color: red;");
            }else if(_model.getField(i, j) == SpaceModel::Characters::Player){
                _buttonVec[i][j]->setStyleSheet("background-color: green;");
            }else{
                _buttonVec[i][j]->setStyleSheet("background-color: gray;");
            }
            _mainGrid->addWidget(_buttonVec[i][j], i, j); // gomb felvétele az elrendezésre
            //QObject::connect(button, SIGNAL(clicked()), this, SLOT(setNumber())); // eseménykezelő kapcsolat
        }
    }
}

SpaceView::~SpaceView()
{

}

void SpaceView::keyPressEvent(QKeyEvent *event){
    qDebug() << "keyPress";
    if (event->key() == Qt::Key_N)
    {
        model_newGame();
    }
    if (event->key() == Qt::Key_P)
    {
        _model.pause();
    }
    if (event->key() == Qt::Key_R)
    {
        _model.resume();
    }
    if (event->key() == Qt::Key_Space)
    {
        if(_model.untilFire==0){
            _model.fire();
        }
    }
    if(event->key() == Qt::Key_A || event->key() == Qt::Key_D){
        qDebug() << QString(event->key());
        _model.stepGame(QString(event->key()));
    }
}

void SpaceView::model_fieldChanged(){
    //qDebug() << "modelFieldCh";
    for (int i = 0; i < _rowCount; ++i) {
        for (int j = 0; j < _colCount; ++j){
            if(_model.getField(i, j) == SpaceModel::Characters::Enemy){
                _buttonVec[i][j]->setStyleSheet("background-color: red;");
            }else if(_model.getField(i, j) == SpaceModel::Characters::Player){
                _buttonVec[i][j]->setStyleSheet("background-color: green;");
            }else{
                _buttonVec[i][j]->setStyleSheet("background-color: gray;");
            }
        }
    }
}
void SpaceView::model_newGame(){
    _newGame->exec();
    _rowCount = _newGame->getRow();
    _colCount = _newGame->getCol();
    _model.setRow(_rowCount);
    _model.setCol(_colCount);
    _model.newGame();
    drawTable();
    _lcdEnemy->display(0);
}
void SpaceView::model_pointEarned(){
    double num = _lcdEnemy->value();
    num+=1;
    qDebug() << num;
    _lcdEnemy->display(num);

}
void SpaceView::model_timePassed(){
    double num = _lcdTime->value();
    num+=1;
    qDebug() << num;
    _lcdTime->display(num);
}
