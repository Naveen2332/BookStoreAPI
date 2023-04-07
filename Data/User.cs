using System.ComponentModel.DataAnnotations;

namespace BookStore.Data
{
    public class User
    {
        [Display(Name = "Userr Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Field is Required"), Range(5, 20)]
        public string Name { get; set; } = default!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        [DataType(DataType.Password), Required]
        public string Password { get; set; } = default!;
        public bool Active { get; set; }
    }
}
