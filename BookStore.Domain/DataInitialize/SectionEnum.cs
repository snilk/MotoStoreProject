
using System.ComponentModel;

namespace BookStore.Domain.DataInitialize
{
    public enum SectionEnum
    {
        [Description("DataBase")]
        DataBase,
        [Description("Programming Languages")]
        ProgrammingLanguages,
        [Description("Operating Systems")]
        OperatingSystems,
        [Description("Multimedia & Design")]
        MultimediaAndDesign,
        [Description("Computer Security")]
        ComputerSecurity
    }
}
