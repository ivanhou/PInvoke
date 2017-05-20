using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace P_Invoke
{

    [StructLayout(LayoutKind.Sequential)]
    public struct ManagedSimpleStruct
    {
        public int intValue;
        public short shortValue;
        public float floatValue;
        public double doubleValue;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct MsEmployee
    {
        public uint EmployeeID;
        public short EmployedYear;
        public string DisplayName;
        public string Alias;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MsEmployee_IntPtrString
    {
        public uint EmployeeID;
        public short EmployedYear;
        public IntPtr DisplayName;
        public IntPtr Alias;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class MsEmployee_Class
    {
        private uint _employeeID;
        private short _employedYear;
        private string _displayName;
        private string _alias;

        #region Properties
        public uint EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }

        public short EmployedYear
        {
            get { return _employedYear; }
            set { _employedYear = value; }
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }
        #endregion
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct PersonName
    {
        public string first;
        public string last;
        public string displayName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Person
    {
        public IntPtr name;
        public int age;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Person2
    {
        public PersonName name;
        public int age;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Person2_Flattened
    {
        public string first;
        public string last;
        public string displayName;
        public int age;
    }



}
