/********************************************************************************
** Form generated from reading UI file 'spaceview.ui'
**
** Created by: Qt User Interface Compiler version 5.13.1
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_SPACEVIEW_H
#define UI_SPACEVIEW_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_SpaceView
{
public:
    QWidget *centralwidget;
    QMenuBar *menubar;
    QStatusBar *statusbar;

    void setupUi(QMainWindow *SpaceView)
    {
        if (SpaceView->objectName().isEmpty())
            SpaceView->setObjectName(QString::fromUtf8("SpaceView"));
        SpaceView->resize(300, 400);
        centralwidget = new QWidget(SpaceView);
        centralwidget->setObjectName(QString::fromUtf8("centralwidget"));
        SpaceView->setCentralWidget(centralwidget);
        menubar = new QMenuBar(SpaceView);
        menubar->setObjectName(QString::fromUtf8("menubar"));
        menubar->setGeometry(QRect(0, 0, 300, 20));
        SpaceView->setMenuBar(menubar);
        statusbar = new QStatusBar(SpaceView);
        statusbar->setObjectName(QString::fromUtf8("statusbar"));
        SpaceView->setStatusBar(statusbar);

        retranslateUi(SpaceView);

        QMetaObject::connectSlotsByName(SpaceView);
    } // setupUi

    void retranslateUi(QMainWindow *SpaceView)
    {
        SpaceView->setWindowTitle(QCoreApplication::translate("SpaceView", "SpaceView", nullptr));
    } // retranslateUi

};

namespace Ui {
    class SpaceView: public Ui_SpaceView {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_SPACEVIEW_H
