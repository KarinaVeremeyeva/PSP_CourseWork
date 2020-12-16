#pragma once
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <windows.h>
#include <WinSock2.h>

#define WINSOCKLIBRARY_API __declspec(dllexport)

extern "C" WINSOCKLIBRARY_API char* createAndListenSocket(char* serverAddress, u_short port);

extern "C" WINSOCKLIBRARY_API int sendMessageToSocket(int key, u_short port);