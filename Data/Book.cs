using System.ComponentModel.DataAnnotations;

namespace BookStore.Data
{
    public class Book
    {
        [Key, Display(Name = "Book Id")]
        public int Id { get; set; } = default!;

        [Required(ErrorMessage = "Book Name is Required"), Display(Name = "Book Name")]
        public string Name { get; set; } = default!;
        [Required(ErrorMessage = "Authore Name is Required"), Display(Name = "Authore Name")]
        public string Authore { get; set; } = default!;
        [Required(ErrorMessage = "Price is Required Field to add a Book"), Display(Name = "Cost")]
        public long Price { get; set; } = default!;
    }
}
