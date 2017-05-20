using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace P_Invoke
{
    class Program
    {

        static void Main(string[] args)
        {
            int result1 = CPlusPlusLib.Add(1, 2);
            int result2 = CPlusPlusLib.Sub(1, 2);
            int result3 = CPlusPlusLib.Multiply(2, 2);
            Console.WriteLine(string.Format("{0} {1} {2}", result1, result2, result3));

            /////////////////////////////////////////////////////////////////////
            CPlusPlusLib.PrintMsg("\nTest CPlusPlusLib");
            
            string rawString = "12345";
            StringBuilder reversedString = new StringBuilder(rawString.Length);
            CPlusPlusLib.ReverseAnsiString(rawString, reversedString);
            Console.WriteLine(string.Format("{0} , {1}", rawString, reversedString.ToString()));

            rawString = "123456";
            string reversedStringA;
            CPlusPlusLib.ReverseStringA(rawString, out reversedStringA);
            Console.WriteLine(string.Format("{0} , {1}", rawString, reversedStringA));

            string strNew = CPlusPlusLib.GetStringNew();
            Console.WriteLine(strNew);

            IntPtr stringPtr = CPlusPlusLib.GetStringNewA();
            string strNewA = System.Runtime.InteropServices.Marshal.PtrToStringUni(stringPtr);
            Console.WriteLine(strNewA);

            ///////////////////////////////////////////////////////////////////
            System.Diagnostics.Stopwatch mWatch = new System.Diagnostics.Stopwatch();
            const int testCycle = 600000;

            mWatch.Reset();
            mWatch.Start();
            for (int i = 0; i < testCycle; i++)
            {
                CPlusPlusLib.IsAsciiNonblittable('a');
            }
            mWatch.Stop();
            Console.WriteLine("\ntime elapsed {0}",mWatch.ElapsedTicks);

            byte mb = (byte)'a';
            mWatch.Reset();
            mWatch.Start();
            for (int i = 0; i < testCycle; i++)
            {
                CPlusPlusLib.IsAsciiBlittable(mb);
            }
            mWatch.Stop();
            Console.WriteLine("time elapsed {0}", mWatch.ElapsedTicks);

            mWatch.Reset();
            mWatch.Start();
            for (int i = 0; i < testCycle; i++)
            {
                CPlusPlusLib.IsWasciiNonblittable('a');
            }
            mWatch.Stop();
            Console.WriteLine("time elapsed {0}", mWatch.ElapsedTicks);

            //////////////////////////////////////////////////////////////////
            IntPtr pString = IntPtr.Zero;
            IntPtr result = IntPtr.Zero;
            result = CPlusPlusLib.BSTRString(out pString);

            if (IntPtr.Zero != pString)
            {
                string argString = Marshal.PtrToStringBSTR(pString);
                Console.WriteLine("\nargument out BSTR：{0}", argString);
                // Free BSTR
                Marshal.FreeBSTR(pString);
            }

            if (IntPtr.Zero != result)
            {
                string retString = Marshal.PtrToStringBSTR(result);
                Console.WriteLine("function return BSTR：{0}", retString);
                // Free BSTR
                Marshal.FreeBSTR(result);
            }

            ///////////////////////////////////////////////////////////////////
            StructArgument();
            StructReturn();

            //////////////////////////////////////////////////////
            StructAllocString();
            StructAllocString_IntPtrString();
            ClassNonBlittlable();

            ////////////////////////////////////////////////////
            StructAsRefField();
            StructAsValField();
            StructAsValFieldFlattened();

            ///////////////////////////////////////////////////
            CharArray();
            StringArray();



            Console.WriteLine("\r\nPress any key to exit...");
            Console.Read();
        }





        private static void StructArgument()
        {
            Console.WriteLine("\r\n");
            ManagedSimpleStruct simpleStruct = new ManagedSimpleStruct();
            simpleStruct.intValue = 10;
            simpleStruct.shortValue = 20;
            simpleStruct.floatValue = 3.5f;
            simpleStruct.doubleValue = 6.8f;

            CPlusPlusLib.StructArgumentByVal(simpleStruct);

            Console.WriteLine("\r\n");

            ManagedSimpleStruct argStruct = new ManagedSimpleStruct();
            argStruct.intValue = 1;
            argStruct.shortValue = 2;
            argStruct.floatValue = 3.0f;
            argStruct.doubleValue = 4.5f;
            CPlusPlusLib.StructArgumentByRef(ref argStruct);

            Console.WriteLine("new struct value：int = {0}, short = {1}, float = {2:f6}, double = {3:f6}",
                argStruct.intValue, argStruct.shortValue, argStruct.floatValue, argStruct.doubleValue);
        }

        private static void StructReturn()
        {

            IntPtr pStruct = CPlusPlusLib.ReturnStruct();
            ManagedSimpleStruct retStruct = (ManagedSimpleStruct)Marshal.PtrToStructure(pStruct, typeof(ManagedSimpleStruct));
            Marshal.FreeCoTaskMem(pStruct);

            Console.WriteLine("\nreturn struct value：int = {0}, short = {1}, float = {2:f6}, double = {3:f6}",
                retStruct.intValue, retStruct.shortValue, retStruct.floatValue, retStruct.doubleValue);

            IntPtr ppStruct = IntPtr.Zero;
            CPlusPlusLib.ReturnStructFromArg(ref ppStruct);
            ManagedSimpleStruct retStructA = (ManagedSimpleStruct)Marshal.PtrToStructure(ppStruct, typeof(ManagedSimpleStruct));
            Marshal.FreeCoTaskMem(ppStruct);

            Console.WriteLine("return struct value：int = {0}, short = {1}, float = {2:f6}, double = {3:f6}",
                retStruct.intValue, retStruct.shortValue, retStruct.floatValue, retStruct.doubleValue);
        }
        
        private static void StructAllocString()
        {
            MsEmployee employee = new MsEmployee();
            employee.EmployeeID = 10001;
            CPlusPlusLib.GetEmployeeInfo(ref employee);

            Console.WriteLine("\nemployee Info:");
            Console.WriteLine("ID: {0}", employee.EmployeeID);
            Console.WriteLine("Year:{0}", employee.EmployedYear);
            Console.WriteLine("Alias: {0}", employee.Alias);
            Console.WriteLine("Name: {0}", employee.DisplayName);
        }

        private static void StructAllocString_IntPtrString()
        {
            MsEmployee_IntPtrString employee = new MsEmployee_IntPtrString();
            employee.EmployeeID = 10001;
            CPlusPlusLib.GetEmployeeInfoA(ref employee);

            string displayName = Marshal.PtrToStringAnsi(employee.DisplayName);
            string alias = Marshal.PtrToStringAnsi(employee.Alias);

            Marshal.FreeCoTaskMem(employee.DisplayName);
            Marshal.FreeCoTaskMem(employee.Alias);

            Console.WriteLine("\nemployee Info IntPtrString:");
            Console.WriteLine("ID: {0}", employee.EmployeeID);
            Console.WriteLine("Year:{0}", employee.EmployedYear);
            Console.WriteLine("Alias: {0}", alias);
            Console.WriteLine("Name: {0}", displayName);
        }

        private static void ClassNonBlittlable()
        {
            MsEmployee_Class employee = new MsEmployee_Class();
            employee.EmployeeID = 10001;

            CPlusPlusLib.GetEmployeeInfo(employee);

            Console.WriteLine("\nemployee Info class:");
            Console.WriteLine("ID: {0}", employee.EmployeeID);
            Console.WriteLine("Year:{0}", employee.EmployedYear);
            Console.WriteLine("Alias: {0}", employee.Alias);
            Console.WriteLine("Name: {0}", employee.DisplayName);
        }

        private static void StructAsRefField()
        {
            Console.WriteLine("\nThe structure is a member of the value type");
            // Creat PersonName
            PersonName name = new PersonName();
            name.last = "Hou";
            name.first = "Jiajun";

            // Creat Person
            Person person = new Person();
            person.age = 27;

            IntPtr nameBuffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(name));
            Marshal.StructureToPtr(name, nameBuffer, false);

            person.name = nameBuffer;

            Console.WriteLine("Name in：{0}", name.displayName);
            CPlusPlusLib.StructInStructByRef(ref person);

            PersonName newValue = (PersonName)Marshal.PtrToStructure(person.name, typeof(PersonName));

            // Free memory
            Marshal.DestroyStructure(nameBuffer, typeof(PersonName));

            Console.WriteLine("Name out：{0}", newValue.displayName);

        }

        private static void StructAsValField()
        {
            Console.WriteLine("\nThe structure is a member of the value type");
            Person2 person = new Person2();
            person.name.last = "Hou";
            person.name.first = "Jiajun";
            person.name.displayName = string.Empty;
            person.age = 26;

            CPlusPlusLib.StructInStructByVal(ref person);
        }

        private static void StructAsValFieldFlattened()
        {
            Console.WriteLine("\nThe structure is a member of the value type（flattened）");
            Person2_Flattened person = new Person2_Flattened();
            person.last = "Hou";
            person.first = "Jiajun";
            person.displayName = string.Empty;
            person.age = 26;

            CPlusPlusLib.StructInStructByVal(ref person);
        }

        private static void CharArray()
        {
            char[] charArray = new char[] { 'a', '1', 'b', '2', 'c', '3', '4' };
            Console.WriteLine("\nelement before：");
            foreach (char c in charArray)
            {
                Console.Write("{0} ", c);
            }

            uint digitCount = CPlusPlusLib.ArrayOfChar(charArray, charArray.Length);

            Console.WriteLine("count number：{0}", digitCount);

            Console.WriteLine("element after：");
            foreach (char c in charArray)
            {
                Console.Write("{0} ", c);
            }
        }

        private static void StringArray()
        {
            string[] strings = new string[] {
                "This is the first string.",
                "Those are brown horse.",
                "The quick brown fox jumps over a lazy dog." };

            Console.WriteLine("\nelement string before：");
            foreach (string originalString in strings)
            {
                Console.WriteLine(originalString);
            }

            CPlusPlusLib.ArrayOfString(strings, strings.Length);

            Console.WriteLine("element string after：");
            foreach (string reversedString in strings)
            {
                Console.WriteLine(reversedString);
            }
        }


    }
}
