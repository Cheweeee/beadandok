#ifndef NEWGAME_H
#define NEWGAME_H

#include <QDialog>
#include <QLabel>
#include <QSpinBox>
#include <QPushButton>

class newGame : public QDialog
{
    Q_OBJECT
public:
    explicit newGame(QWidget *parent = 0);
    int getRow() {return _row;}
    int getCol() {return _col;}

private slots:
    void choice1();
    void choice2();
    void choice3();

private:
    int _row, _col;
};

#endif // NEWGAME_H
