#include "stdafx.h"
#include "Encryption.h"
#include <iostream>
#include <fstream>
#include <files.h>
#include <aes.h>
#include <filters.h>
#include <Windows.h>

using namespace std;
Encryption::Encryption()
{
}


Encryption::~Encryption()
{
}

void Encryption::EncryptFileA(char* path)
{
	ifstream infile(path,std::ios::binary);

	unsigned char ckey[] = "helloworldkey";
	unsigned char ivec[] = "goodbyworldkey";
	
	

	//CryptoPP::FileSource f(path, false, nullptr);

	int i = 0;

	//auto fileSize = GetFileSize(path)

	
}
