using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataLayer.Entityes
{
    public partial class User
    {
        public int Id { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Возраст")]
        public int? Age { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Дата регистрации")]
        public DateTime? RegistationDate { get; set; }
    }
}
