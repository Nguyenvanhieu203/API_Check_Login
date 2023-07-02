using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.Data
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string NameBook { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Number { get; set; }
    }
}
