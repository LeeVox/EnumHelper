
namespace EnumHelper_Test.Models
{
    public class StoreModel
    {
        public A theA { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }
    }

    public class A
    {
        public B theB { get; set; }
    }

    public class B
    {
        public ItemType AllItemTypes { get; set; }
        public UserType user { get; set; }
        public string SSS { get; set; }
    }
}