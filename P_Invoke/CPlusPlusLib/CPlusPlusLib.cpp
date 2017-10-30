// CPlusPlusLib.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CPlusPlusLib.h"



int __stdcall Add(int a, int b)
{
	return a + b;
}

int __cdecl Sub(int a, int b)
{
	return a - b;
}

int Multiply(int a, int b)
{
	return a*b;
}

void __stdcall PrintMsg(const char* msg)
{
	printf("%s\n", msg);
	return;
}

void __stdcall ReverseAnsiString(char* rawString, char* reversedString)
{
	int strLength = (int)strlen(rawString);
	
	strcpy_s(reversedString, strLength+1, rawString);//三个参数
	
	char tempChar;
	for (int i = 0; i < strLength / 2; i++)
	{
		tempChar = reversedString[i];
		reversedString[i] = reversedString[strLength - 1 - i];
		reversedString[strLength - 1 - i] = tempChar;
	}
}

void __stdcall ReverseStringA(char* rawString, wchar_t**  reversedString)
{
	char* CStr = new char[strlen(rawString) + 1];

	ReverseAnsiString(rawString, CStr);
	size_t len = strlen(CStr) + 1;
	size_t converted = 0;
	*reversedString = (wchar_t*)malloc(len * sizeof(wchar_t));
	mbstowcs_s(&converted, *reversedString, len, CStr, _TRUNCATE);

	delete CStr;
}

wchar_t* __stdcall GetStringNew()
{
	int iBufferSize = 128;
	wchar_t* pBuffer = new wchar_t[iBufferSize];
	if (NULL != pBuffer)
	{
		wcscpy_s(pBuffer, iBufferSize / sizeof(wchar_t), L"String from NEW");
	}
	return pBuffer;
}

bool __stdcall IsAsciiNonblittable(char asciiChar)
{
	return (__isascii(asciiChar) != 0);
}

int __stdcall IsAsciiBlittable(__int8 asciiChar)
{
	return __isascii(asciiChar);
}

bool __stdcall IsWasciiNonblittable(wchar_t wasciiChar)
{
	return (iswascii(wasciiChar) != 0);
}

BSTR __stdcall BSTRString(BSTR* ppString)
{
	 *ppString = NULL;
	 *ppString = SysAllocString(L"BSTR string from argument");

	 BSTR result = SysAllocString(L"Return BSTR string");
	 return result;
}

void __stdcall StructArgumentByVal(SIMPLESTRUCT simpleStruct)
{
	printf("input struct value: int = %d, short = %d, float = %f, double = %f\n",
		simpleStruct.intValue, simpleStruct.shortValue, simpleStruct.floatValue, simpleStruct.doubleValue);
}

void __stdcall StructArgumentByRef(PSIMPLESTRUCT ppStruct)
{
	if (NULL != ppStruct)
	{
		printf("input struct value: int = %d, short = %d, float = %f, double = %f\n",
			ppStruct->intValue, ppStruct->shortValue, ppStruct->floatValue, ppStruct->doubleValue);

		// change value
		ppStruct->intValue++;
		ppStruct->shortValue++;
		ppStruct->floatValue += 1;
		ppStruct->doubleValue += 1;
	}
}

PSIMPLESTRUCT __stdcall ReturnStruct(void)
{
	PSIMPLESTRUCT pStruct = (PSIMPLESTRUCT)CoTaskMemAlloc(sizeof(SIMPLESTRUCT));

	pStruct->intValue = 5;
	pStruct->shortValue = 4;
	pStruct->floatValue = 3.0;
	pStruct->doubleValue = 2.1;

	return pStruct;
}

void __stdcall ReturnStructFromArg(PSIMPLESTRUCT* ppStruct)
{
	if (NULL != ppStruct)
	{
		*ppStruct = (PSIMPLESTRUCT)CoTaskMemAlloc(sizeof(SIMPLESTRUCT));

		(*ppStruct)->intValue = 5;
		(*ppStruct)->shortValue = 4;
		(*ppStruct)->floatValue = 3.0;
		(*ppStruct)->doubleValue = 2.1;
	}
	return;
}

void __stdcall GetEmployeeInfo(PMSEMPLOYEE pEmployee)
{
	if (NULL != pEmployee)
	{
		pEmployee->employedYear = 2;
		pEmployee->alias = (char*)CoTaskMemAlloc(255);
		pEmployee->displayName = (char*)CoTaskMemAlloc(255);

		strcpy_s(pEmployee->alias, 255, "man");
		strcpy_s(pEmployee->displayName, 255, "Jiajun");
	}
}

void __stdcall StructInStructByRef(PPERSON pPerson)
{
	size_t firstLen = strlen(pPerson->pName->first);
	size_t lastLen = strlen(pPerson->pName->last);

	char* temp = (char*)CoTaskMemAlloc(sizeof(char) * (firstLen + lastLen + 2));
	sprintf_s(temp, firstLen + lastLen + 2, "%s %s", pPerson->pName->last, pPerson->pName->first);

	CoTaskMemFree(pPerson->pName->displayName);
	pPerson->pName->displayName = temp;
}

void __stdcall StructInStructByVal(PPERSON2 pPerson)
{
	printf("last name = %s\nfirst name = %s\nage = %i\n",
		pPerson->name.last, pPerson->name.first, pPerson->age);
}

UINT __stdcall ArrayOfChar(char charArray[], int arraySize)
{
	int result = 0;
	for (int i = 0; i < arraySize; i++)
	{
		if (isdigit(charArray[i]))
		{
			result++;
			charArray[i] = '@';
		}
	}
	return result;
}

void __stdcall ArrayOfString(char* ppStrArray[], int size)
{
	for (int i = 0; i < size; i++)
	{
		// 翻转字符串
		ReverseAnsiString(ppStrArray[i], ppStrArray[i]);
	}
}

void __stdcall CallResulOfFunction(int a, int b, CallbackFunction cbFunc)
{
	cbFunc(a + b);
}

void __stdcall CallResulOfFunction_A(PMSEMPLOYEE pEmployee, CallbackFunction_A cbFunc)
{
	int mStatus = -1;
	if (NULL != pEmployee)
	{
		/*pEmployee->employedYear = 2;
		pEmployee->alias = (char*)CoTaskMemAlloc(255);
		pEmployee->displayName = (char*)CoTaskMemAlloc(255);

		strcpy_s(pEmployee->alias, 255, "man");
		strcpy_s(pEmployee->displayName, 255, "HouJiajun");*/
		GetEmployeeInfo(pEmployee);
		mStatus = 0;
	}
	cbFunc(mStatus, pEmployee);
}

