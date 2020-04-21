
using System.ComponentModel;

namespace BookStore.Domain.DataInitialize
{
    public enum LevelEnum
    { 
        [Description("Beginner")]
        Beginner, 
        [Description("Junior")]
        Junior, 
        [Description("Middle")]
        Middle, 
        [Description("Senior")]
        Senior
    }
}
