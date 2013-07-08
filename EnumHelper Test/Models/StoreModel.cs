using System;

namespace EnumHelper_Test.Models
{
    public class Student
    {
        public PersonalInfo PersonalInfo { get; set; }
        public Course AttendedCourses { get; set; } // enum property
    }

    public class PersonalInfo
    {
        public string Name { get; set; }
        public Gender Gender; // enum field
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        [EnumDescription("Undefined Gender")] // you can use EnumDescription in namespace System
        Unknown = 1024
    }

    // One student can attend many courses
    [Flags]
    public enum Course
    {
        Math = 1,
        Chemistry = 2,
        Physics = 4,
        [System.ComponentModel.Description("Computer Sciene")] // you can use Description in namespace System.ComponentModel
        ComputerSicene = 8,
        Philosophy = 16,
        Literature = 32
    }
}