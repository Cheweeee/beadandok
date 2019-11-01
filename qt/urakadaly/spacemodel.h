#ifndef SPACEMODEL_H
#define SPACEMODEL_H

#include <QVector>
#include <QObject>
#include <QTimer>

class SpaceModel : public QObject
{
    Q_OBJECT
public:
    enum Characters {Player, Enemy, Nothing};
    SpaceModel();
    void setRow(int row) {_rowCount = row;}
    void setCol(int col) {_colCount = col;}
    void newGame();
    Characters getField(int row, int col);
    void stepGame(QString direction);
    void pause();
    void resume();
    void fire();
    int untilFire = 0;
public slots:
    void timerFoo();
signals:
    void fieldChanged();
    void pointEarned();
    void timePassed();
    /*void gameWon(huntModel::Player player);
    void gameOver();

    void newGameNeeded();*/

private:
    int _rowCount;
    int _colCount;
    QVector<QVector<Characters>> _table;
    QVector<int> _enemyPosVec;
    int _playerCol;
    QTimer *_timer;
};

#endif // SPACEMODEL_H
