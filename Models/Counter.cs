using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace webapi
{
    //some changes in the Counter class...
    public partial class Counter
    {
        public Counter()
        {
        }

        public Counter(string id, long val)
        {
            Id = id;
            Val = val;
        }

        [Required]
        [MaxLength(32)]
        [Column(TypeName = "char(32)")]
        public string Id { get; set; }
        [Required]
        public long Val { get; set; }
    }
}
