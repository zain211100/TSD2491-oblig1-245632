using System.ComponentModel.DataAnnotations;

namespace TSD2491_oblig1_245632.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public int Times { get; set; }
    }
}
