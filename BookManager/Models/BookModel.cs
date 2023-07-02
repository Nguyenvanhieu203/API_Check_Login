using System.ComponentModel.DataAnnotations;

namespace BookManager.Models
{
    public class BookModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string NameBook { get; set; }
        public string Category { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public int Number { get; set; }
    }
}
