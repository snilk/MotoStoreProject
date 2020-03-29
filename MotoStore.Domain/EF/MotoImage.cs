
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoStore.Domain.EF
{
    public class MotoImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MotoId { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(MotoId))]
        public virtual Motorcycle Motorcycle { get; set; }
    }
}