#-------------------------------------------------
#
# Project created by QtCreator 2014-05-09T14:40:36
#
#-------------------------------------------------

QT       += testlib

QT       -= gui

TARGET = tictactoemodeltest
CONFIG   += console
CONFIG   -= app_bundle

TEMPLATE = app


SOURCES += tictactoemodeltest.cpp \
    tictactoemodel.cpp
DEFINES += SRCDIR=\\\"$$PWD/\\\"

HEADERS += \
    tictactoemodel.h \
    tictactoedataaccess.h \
    tictactoedataaccessmock.h
