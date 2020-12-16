#undef UNICODE

#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>
#include <string>

#pragma comment (lib, "Ws2_32.lib")

#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "8080"

char * __cdecl createAndListenSocket(char * serverAddress, u_short port) {
    // Инициализация Winsock
    WSADATA wsaData;
    int iResult = 0;

    SOCKET ListenSocket = INVALID_SOCKET;
    sockaddr_in service;

    iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
    if (iResult != NO_ERROR) {
        throw L"WSAStartup() failed with error: " + iResult;
    }

    // Создание сокета для прослушивания входящих запросов на соединение
    ListenSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (ListenSocket == INVALID_SOCKET) {
        WSACleanup();
        throw L"socket function failed with error: " + WSAGetLastError();
    }
    // Структура sockaddr_in указывает семейство IP адреса,
    // IP адресс и порт для привязанного сокета.
    service.sin_family = AF_INET;
    service.sin_addr.s_addr = inet_addr(serverAddress);
    service.sin_port = htons(port);

    iResult = bind(ListenSocket, (SOCKADDR*)&service, sizeof(service));
    if (iResult == SOCKET_ERROR) {
        iResult = closesocket(ListenSocket);
        WSACleanup();
        throw L"closesocket function failed with error " + WSAGetLastError();
    }
    // Слушаем входящие запросы на соединение на созданном сокете
    if (listen(ListenSocket, SOMAXCONN) == SOCKET_ERROR)
        wprintf(L"listen function failed with error: %d\n", WSAGetLastError());

    wprintf(L"Listening on socket...\n");

    // Получение данных от клиента from
    SOCKET ClientSocket = INVALID_SOCKET;
    // Принимаем клиентский сокет
    ClientSocket = accept(ListenSocket, NULL, NULL);
    if (ClientSocket == INVALID_SOCKET) {
        closesocket(ListenSocket);
        WSACleanup();
        throw "accept failed with error: " + WSAGetLastError();
    }

    char recvbuf[DEFAULT_BUFLEN];
    int recvbuflen = DEFAULT_BUFLEN;

    // Принимает пока не отключится соединение
    do {

        iResult = recv(ClientSocket, recvbuf, recvbuflen, 0);
    } while (iResult > 0);

    // Закрытие сокета
    iResult = closesocket(ListenSocket);
    if (iResult == SOCKET_ERROR) {
        WSACleanup();
        throw L"closesocket function failed with error " + WSAGetLastError();
    }

    WSACleanup();

    return recvbuf;
}