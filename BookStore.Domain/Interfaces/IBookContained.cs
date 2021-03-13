using BookStore.Domain.EF;

namespace BookStore.Domain.Interfaces
{
    public interface IBookContained
    {
        Book Book { get; set; }
    }
}