#ifndef SPACEVIEW_H
#define SPACEVIEW_H

#include <QMainWindow>
#include <QGridLayout>
#include <QVBoxLayout>
#include <QPushButton>
#include "newgame.h"
#include "spacemodel.h"
#include <QWidget>
#include <QVector>
#include <QLCDNumber>
#include <QHBoxLayout>


QT_BEGIN_NAMESPACE
namespace Ui { class SpaceView; }
QT_END_NAMESPACE

class SpaceView : public QWidget
{
    Q_OBJECT

public:
    SpaceView(QWidget *parent = nullptr);
    ~SpaceView();
    int getRow(){return _rowCount;}
    int getCol(){return _colCount;}
    void drawTable();
protected:
    void keyPressEvent(QKeyEvent *event);

private slots:
    void model_fieldChanged();
    void model_newGame();
    void model_pointEarned();
    void model_timePassed();
private:
    QGridLayout *_mainGrid, *_asd;
    SpaceModel _model;
    int _rowCount;
    int _colCount;
    QVector<QVector<QPushButton*>> _buttonVec;
    newGame *_newGame;
    QLCDNumber *_lcdEnemy, *_lcdTime;
    QVBoxLayout *_az;
    QHBoxLayout *_amaz;
};
#endif // SPACEVIEW_H

