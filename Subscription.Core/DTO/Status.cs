using System.ComponentModel.DataAnnotations;

namespace Domain.Domain
{
    public class Status
    {
        [Key]
        public int id;

        [MaxLength(50)]
        public string name;

    }
}
