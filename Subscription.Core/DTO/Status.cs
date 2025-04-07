using System.ComponentModel.DataAnnotations;

namespace Subscription.Core.DTO
{
    public class Status
    {
        [Key]
        public int id;

        [MaxLength(50)]
        public string name;

    }
}
