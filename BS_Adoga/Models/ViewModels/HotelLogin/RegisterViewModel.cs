namespace BS_Adoga.Models.ViewModels.HotelLogin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class RegisterViewModel
    {

        [Required(ErrorMessage = "�z������J�^��m�W")]
        [Display(Name = "�^��m�W")]
        [StringLength(50)]
        [RegularExpression(@"[a-zA-Z -]*$", ErrorMessage = "�ȯ঳�^��j�p�g�M�ťթM-�Ÿ��I")]
        public string Name { get; set; }

        [Required(ErrorMessage = "�z������J�q�l�l��A��Email������z�n�J���b��")]
        [Display(Name = "�q�l�H�c")]
        [DataType(DataType.EmailAddress, ErrorMessage = "�п�J���T���q�l�H�c�榡")]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "�K�X")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "�K�X�����׻ݦA6~20�Ӧr�����I")]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [Display(Name = "�T�{�K�X")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "�K�X�����׻ݦA6~20�Ӧr�����I")]
        [Compare("Password", ErrorMessage = "�⦸��J���K�X�����۲šI")]
        public string ConfirmPassword { get; set; }

    }
}
