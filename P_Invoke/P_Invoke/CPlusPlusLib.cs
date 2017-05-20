using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace P_Invoke
{
    public class CPlusPlusLib
    {
        private const string _dllName = "D:\\Project\\C++\\P_Invoke\\Release\\CPlusPlusLib.dll";


        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int Add(int a, int b);

        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sub(int a, int b);

        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Multiply(int a, int b);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void PrintMsg(string msg);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void ReverseAnsiString(string rawString,StringBuilder reversedString);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void ReverseStringA(string rawString, [MarshalAs(UnmanagedType.LPWStr)] out string reversedString);
        
        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern string GetStringNew();

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "GetStringNew")]
        public static extern IntPtr GetStringNewA();

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern bool IsAsciiNonblittable(char asciiChar);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern int IsAsciiBlittable(byte asciiChar);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern bool IsWasciiNonblittable(char wchar);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr BSTRString(out IntPtr bstrPtr);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void StructArgumentByVal(ManagedSimpleStruct argStruct);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void StructArgumentByRef(ref ManagedSimpleStruct argStruct);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public extern static IntPtr ReturnStruct();

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public extern static void ReturnStructFromArg(ref IntPtr ppStruct);
        
        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public extern static void GetEmployeeInfo(ref MsEmployee MsEmployee_IntPtrString);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode,EntryPoint = "GetEmployeeInfo")]
        public extern static void GetEmployeeInfoA(ref MsEmployee_IntPtrString ppStruct);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "GetEmployeeInfo")]
        public extern static void GetEmployeeInfo([In, Out] MsEmployee_Class employee);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public extern static void StructInStructByRef(ref Person person);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public extern static void StructInStructByVal(ref Person2 person);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public extern static void StructInStructByVal(ref Person2_Flattened person);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public extern static uint ArrayOfChar([In, Out] char[] charArray, int arraySize);

        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public extern static void ArrayOfString([In, Out] string[] charArray, int arraySize);



    }
}
