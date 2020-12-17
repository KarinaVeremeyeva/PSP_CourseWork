// dllmain.cpp : Определяет точку входа для приложения DLL.
#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include "pch.h"
#include "WSDll.h"
#include <exception>

#undef UNICODE

#define WIN32_LEAN_AND_MEAN

#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <string>
#include <iostream>
#include <sstream>
#include <fstream>

#pragma comment (lib, "Ws2_32.lib")

#define DEFAULT_BUFLEN 512

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}


__declspec(dllexport) char* createAndListenSocket(char * serverAddr, u_short port) {
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
    service.sin_addr.s_addr = inet_addr(serverAddr);
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

    // Получение данных от клиента
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

__declspec(dllexport) int sendMessageToSocket(int key, u_short port, char * serverAddr) {
    WSADATA wsaData;
    SOCKET ConnectSocket = INVALID_SOCKET;
    struct addrinfo* result = NULL,
        * ptr = NULL,
        hints;
    int iResult;

    char sendbuf[4] = "";
    _itoa_s(key, sendbuf, 10);

    char portbuf[5] = "";
    _itoa_s(port, portbuf, 10);

    // Инициализация Winsock
    iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
    if (iResult != 0) {
        throw "WSAStartup failed with error";
    }

    ZeroMemory(&hints, sizeof(hints));
    hints.ai_family = AF_UNSPEC;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_protocol = IPPROTO_TCP;

    // Получить адрес и порт сервера
    iResult = getaddrinfo(serverAddr, portbuf, &hints, &result);
    if (iResult != 0) {
        WSACleanup();
        throw "getaddrinfo failed with error";
    }

    // Попытка подключиться к адресу, пока не удастся
    for (ptr = result; ptr != NULL; ptr = ptr->ai_next) {

        // Создаем сокет для подключения к серверу
        ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype,
            ptr->ai_protocol);
        if (ConnectSocket == INVALID_SOCKET) {
            WSACleanup();
            throw "socket failed with error";
        }

        //Подключаемся к серверу
        iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
        if (iResult == SOCKET_ERROR) {
            closesocket(ConnectSocket);
            ConnectSocket = INVALID_SOCKET;
            continue;
        }
        break;
    }

    freeaddrinfo(result);

    if (ConnectSocket == INVALID_SOCKET) {
        WSACleanup();
        throw "Unable to connect to server!\n";
    }

    // Отправляем начальный буфер
    iResult = send(ConnectSocket, sendbuf, 4, 0);
    if (iResult == SOCKET_ERROR) {
        closesocket(ConnectSocket);
        WSACleanup();
        throw "send failed with error";
    }

    printf("Bytes Sent: %ld\n", iResult);

    // Отключаем соединение, так как данные больше не будут отправляться
    iResult = shutdown(ConnectSocket, SD_SEND);
    if (iResult == SOCKET_ERROR) {
        closesocket(ConnectSocket);
        WSACleanup();
        throw "shutdown failed with error";
    }

    // Закрытие сокета
    closesocket(ConnectSocket);
    WSACleanup();

    return key;
}