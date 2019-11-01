#include "spaceview.h"

#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    SpaceView w;
    w.show();
    return a.exec();
}
