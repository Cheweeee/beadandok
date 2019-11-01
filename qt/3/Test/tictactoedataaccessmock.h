#ifndef TICTACTOEDATAACCESSMOCK_H
#define TICTACTOEDATAACCESSMOCK_H

#include "tictactoedataaccess.h"

class TicTacToeDataAccessMock : public TicTacToeDataAccess // mock object, csak teszteléshez
{
public:
    bool isAvailable() const { return true; } // rendelkezésre állás lekérdezése
    QVector<QString> saveGameList() const // mentett játékok lekérdezése
    {
        return QVector<QString>(5); // üres listát adunk vissza
    }

    bool loadGame(int gameIndex, QVector<int> &saveGameData) // játék betöltése
    {
        saveGameData.resize(11); // minden érték 0 lesz
        saveGameData[1] = 1; // kivéve a rákövetkező játékos

        qDebug() << "game loaded to slot (" << gameIndex << ") with values: ";
        for (int i = 0; i < 11; i++)
            qDebug() << saveGameData[i] << " ";
        qDebug() << endl;

        return true;
    }

    bool saveGame(int gameIndex, const QVector<int> &saveGameData) // játék mentése
    {
        qDebug() << "game saved to slot (" << gameIndex << ") with values: ";
        for (int i = 0; i < 11; i++)
            qDebug() << saveGameData[i] << " ";
        qDebug() << endl;

        return true;
    }
};

#endif // TICTACTOEDATAACCESSMOCK_H
