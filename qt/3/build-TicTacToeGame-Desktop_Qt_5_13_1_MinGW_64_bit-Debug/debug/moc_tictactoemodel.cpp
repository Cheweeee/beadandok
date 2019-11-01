/****************************************************************************
** Meta object code from reading C++ file 'tictactoemodel.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.13.1)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include <memory>
#include "../../Project/tictactoemodel.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'tictactoemodel.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.13.1. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
QT_WARNING_PUSH
QT_WARNING_DISABLE_DEPRECATED
struct qt_meta_stringdata_TicTacToeModel_t {
    QByteArrayData data[9];
    char stringdata0[80];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_TicTacToeModel_t, stringdata0) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_TicTacToeModel_t qt_meta_stringdata_TicTacToeModel = {
    {
QT_MOC_LITERAL(0, 0, 14), // "TicTacToeModel"
QT_MOC_LITERAL(1, 15, 7), // "gameWon"
QT_MOC_LITERAL(2, 23, 0), // ""
QT_MOC_LITERAL(3, 24, 22), // "TicTacToeModel::Player"
QT_MOC_LITERAL(4, 47, 6), // "player"
QT_MOC_LITERAL(5, 54, 8), // "gameOver"
QT_MOC_LITERAL(6, 63, 12), // "fieldChanged"
QT_MOC_LITERAL(7, 76, 1), // "x"
QT_MOC_LITERAL(8, 78, 1) // "y"

    },
    "TicTacToeModel\0gameWon\0\0TicTacToeModel::Player\0"
    "player\0gameOver\0fieldChanged\0x\0y"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_TicTacToeModel[] = {

 // content:
       8,       // revision
       0,       // classname
       0,    0, // classinfo
       3,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       3,       // signalCount

 // signals: name, argc, parameters, tag, flags
       1,    1,   29,    2, 0x06 /* Public */,
       5,    0,   32,    2, 0x06 /* Public */,
       6,    3,   33,    2, 0x06 /* Public */,

 // signals: parameters
    QMetaType::Void, 0x80000000 | 3,    4,
    QMetaType::Void,
    QMetaType::Void, QMetaType::Int, QMetaType::Int, 0x80000000 | 3,    7,    8,    4,

       0        // eod
};

void TicTacToeModel::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        auto *_t = static_cast<TicTacToeModel *>(_o);
        Q_UNUSED(_t)
        switch (_id) {
        case 0: _t->gameWon((*reinterpret_cast< TicTacToeModel::Player(*)>(_a[1]))); break;
        case 1: _t->gameOver(); break;
        case 2: _t->fieldChanged((*reinterpret_cast< int(*)>(_a[1])),(*reinterpret_cast< int(*)>(_a[2])),(*reinterpret_cast< TicTacToeModel::Player(*)>(_a[3]))); break;
        default: ;
        }
    } else if (_c == QMetaObject::IndexOfMethod) {
        int *result = reinterpret_cast<int *>(_a[0]);
        {
            using _t = void (TicTacToeModel::*)(TicTacToeModel::Player );
            if (*reinterpret_cast<_t *>(_a[1]) == static_cast<_t>(&TicTacToeModel::gameWon)) {
                *result = 0;
                return;
            }
        }
        {
            using _t = void (TicTacToeModel::*)();
            if (*reinterpret_cast<_t *>(_a[1]) == static_cast<_t>(&TicTacToeModel::gameOver)) {
                *result = 1;
                return;
            }
        }
        {
            using _t = void (TicTacToeModel::*)(int , int , TicTacToeModel::Player );
            if (*reinterpret_cast<_t *>(_a[1]) == static_cast<_t>(&TicTacToeModel::fieldChanged)) {
                *result = 2;
                return;
            }
        }
    }
}

QT_INIT_METAOBJECT const QMetaObject TicTacToeModel::staticMetaObject = { {
    &QObject::staticMetaObject,
    qt_meta_stringdata_TicTacToeModel.data,
    qt_meta_data_TicTacToeModel,
    qt_static_metacall,
    nullptr,
    nullptr
} };


const QMetaObject *TicTacToeModel::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *TicTacToeModel::qt_metacast(const char *_clname)
{
    if (!_clname) return nullptr;
    if (!strcmp(_clname, qt_meta_stringdata_TicTacToeModel.stringdata0))
        return static_cast<void*>(this);
    return QObject::qt_metacast(_clname);
}

int TicTacToeModel::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QObject::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 3)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 3;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 3)
            *reinterpret_cast<int*>(_a[0]) = -1;
        _id -= 3;
    }
    return _id;
}

// SIGNAL 0
void TicTacToeModel::gameWon(TicTacToeModel::Player _t1)
{
    void *_a[] = { nullptr, const_cast<void*>(reinterpret_cast<const void*>(std::addressof(_t1))) };
    QMetaObject::activate(this, &staticMetaObject, 0, _a);
}

// SIGNAL 1
void TicTacToeModel::gameOver()
{
    QMetaObject::activate(this, &staticMetaObject, 1, nullptr);
}

// SIGNAL 2
void TicTacToeModel::fieldChanged(int _t1, int _t2, TicTacToeModel::Player _t3)
{
    void *_a[] = { nullptr, const_cast<void*>(reinterpret_cast<const void*>(std::addressof(_t1))), const_cast<void*>(reinterpret_cast<const void*>(std::addressof(_t2))), const_cast<void*>(reinterpret_cast<const void*>(std::addressof(_t3))) };
    QMetaObject::activate(this, &staticMetaObject, 2, _a);
}
QT_WARNING_POP
QT_END_MOC_NAMESPACE
