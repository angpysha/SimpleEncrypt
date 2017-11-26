#include "stdafx.h"
#include <Windows.h>
#include "ApiManaged.h"



namespace MixedLibrary {
	ApiManaged::ApiManaged()
	{
	}

	void ApiManaged::AllocConsoleEx() {
	AllocConsole();
	}
}
