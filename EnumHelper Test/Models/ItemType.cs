using System;

namespace EnumHelper_Test.Models
{
    [Flags]
    public enum ItemType
    {
        /*
         * Provide EnumDescription attribute
         * if you want to change display text
         * when render DropDownList, ListBox...
         */
        [EnumDescription("This is Desktop")]
        Desktop = 1,

        Laptop = 2,

        [EnumDescription("iPad, Samsumg Galaxy Tab...")]
        Tablet = 4,

        /* We can use the original Description attribute */
        [System.ComponentModel.Description("Smart Phone")]
        Phone = 8
    }
}