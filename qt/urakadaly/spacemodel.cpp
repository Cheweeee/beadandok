#include "spacemodel.h"
#include <QDebug>
#include <QTimer>

SpaceModel::SpaceModel(){
    _timer = new QTimer(this);
    _timer->start(1000);
    connect(_timer, SIGNAL(timeout()), this, SLOT(timerFoo()));
}

void SpaceModel::newGame(){
    _table.clear();

    _enemyPosVec.resize(_colCount);
    for(int i=0;i<_colCount;++i){

        if(i==1){
            _enemyPosVec[i] = -1;
        }else{
            _enemyPosVec[i] = i;
        }
    }

    _playerCol = 1;
    _table.resize(_rowCount);
    for(int i=0;i<_rowCount;++i){
        _table[i].resize(_colCount);
        for(int j=0;j<_colCount;++j){
            if(i == _enemyPosVec[j] && _enemyPosVec[j] != -1){
                _table[i][j] = Enemy;
            }else if(i==_rowCount-1 && j==_playerCol){
                _table[i][j] = Player;
            }else{
                _table[i][j] = Nothing;
            }
        }
    }
}
SpaceModel::Characters SpaceModel::getField(int row, int col){
    return _table[row][col];
}

void SpaceModel::stepGame(QString direction){
    //qDebug() << "stepGame";
    if(direction == "A"){
        if(_playerCol > 0 && _table[_rowCount-1][_playerCol-1] == Nothing){
            _table[_rowCount-1][_playerCol] = Nothing;
            _table[_rowCount-1][--_playerCol] = Player;
            //qDebug() << _playerCol;
        }
    }else if(direction == "D"){
        if(_playerCol < _colCount-1 && _table[_rowCount-1][_playerCol+1] == Nothing){
            _table[_rowCount-1][_playerCol] = Nothing;
            _table[_rowCount-1][++_playerCol] = Player;

            //qDebug() << _playerCol;
        }
    }
    fieldChanged();
}

void SpaceModel::timerFoo(){
    for(int i=0;i<_colCount;++i){
        if(_enemyPosVec[i] != -1){
            if(_enemyPosVec[i] < _rowCount-1){
                _table[_enemyPosVec[i]][i] = Nothing;
                _table[++_enemyPosVec[i]][i] = Enemy;
            }else if(_enemyPosVec[i] == _rowCount-1 && _enemyPosVec[i] != _playerCol){
                _table[_enemyPosVec[i]][i] = Nothing;
                _enemyPosVec[i] = 0;
                _table[_enemyPosVec[i]][i] = Enemy;
                pointEarned();
            }if(i == _playerCol && _enemyPosVec[i] == _rowCount-1){
                _timer->stop();
                qDebug() << "game over";
            }
        }

    }
    if(untilFire>0){
        untilFire--;
    }
    timePassed();
    fieldChanged();

}
void SpaceModel::pause(){
    _timer->stop();
}
void SpaceModel::resume(){
    _timer->start(1000);
}
void SpaceModel::fire(){
    _enemyPosVec[_playerCol] = -1;
    untilFire = 4;
    for(int i=0;i<10;++i){
        pointEarned();
    }
}

