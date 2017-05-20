//#ifdef CPLUSPLUSLIB_EXPORTS
//#define CPLUSPLUSLIB_API __declspec(dllexport) 
//#else
//#define CPLUSPLUSLIB_API __declspec(dllimport) 
//#endif

typedef struct _SIMPLESTRUCT
{
	int    intValue;
	short  shortValue;
	float  floatValue;
	double doubleValue;
} SIMPLESTRUCT, *PSIMPLESTRUCT;

typedef struct _MSEMPLOYEE
{
	UINT  employeeID;
	short employedYear;
	char* displayName;
	char* alias;
} *PMSEMPLOYEE;

typedef struct _PERSONNAME
{
	char* first;
	char* last;
	char* displayName;
} PERSONNAME, *PPERSONNAME;

typedef struct _PERSON
{
	PPERSONNAME pName;
	int age;
} PERSON, *PPERSON;

typedef struct _PERSON2
{
	PERSONNAME name;
	int age;
} PERSON2, *PPERSON2;



#ifndef CPLUSPLUSLIB_H_
#define CPLUSPLUSLIB_H_

extern "C" __declspec(dllexport) int __stdcall Add(int a, int b);

extern "C" __declspec(dllexport) int __cdecl Sub(int a, int b);

extern "C" __declspec(dllexport) int Multiply(int a, int b);

extern "C" __declspec(dllexport) void __stdcall PrintMsg(const char* msg);

extern "C" __declspec(dllexport) void __stdcall ReverseAnsiString(char* rawString, char* reversedString);

extern "C" __declspec(dllexport) void __stdcall ReverseStringA(char* rawString, wchar_t** reversedString);

extern "C" __declspec(dllexport) wchar_t* __stdcall GetStringNew();

extern "C" __declspec(dllexport) bool __stdcall IsAsciiNonblittable(char asciiChar);

extern "C" __declspec(dllexport) int __stdcall IsAsciiBlittable(__int8 asciiChar);

extern "C" __declspec(dllexport) bool __stdcall IsWasciiNonblittable(wchar_t wasciiChar);

extern "C" __declspec(dllexport) BSTR __stdcall BSTRString(BSTR* ppString);

extern "C" __declspec(dllexport) void __stdcall StructArgumentByVal(SIMPLESTRUCT simpleStruct);

extern "C" __declspec(dllexport) void __stdcall StructArgumentByRef(PSIMPLESTRUCT ppStruct);

extern "C" __declspec(dllexport) PSIMPLESTRUCT __stdcall ReturnStruct(void);

extern "C" __declspec(dllexport) void __stdcall ReturnStructFromArg(PSIMPLESTRUCT* ppStruct);

extern "C" __declspec(dllexport) void __stdcall GetEmployeeInfo(PMSEMPLOYEE pEmployee);

extern "C" __declspec(dllexport) void __stdcall StructInStructByRef(PPERSON pPerson);

extern "C" __declspec(dllexport) void __stdcall StructInStructByVal(PPERSON2 pPerson);

extern "C" __declspec(dllexport) UINT __stdcall ArrayOfChar(char charArray[], int arraySize);

extern "C" __declspec(dllexport) void __stdcall ArrayOfString(char* ppStrArray[], int size);



#endif