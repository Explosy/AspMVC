using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace DTO {
  public class UserDTO {
    public int Id { get; set; }
    [Display(Name = "Имя")]
    [Required (ErrorMessage ="Пожалуйста укажите имя")]
    public string Name { get; set; }
    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Пожалуйста укажите Фамилию")]
    public string Surname { get; set; }
    [Display(Name = "Возраст")]
    [Required(ErrorMessage = "Пожалуйста укажите возраст!")]
    public int? Age { get; set; }
    [Display(Name = "E-mail")]
    [Required (ErrorMessage = "Пожалуйста укажите e-mail")]
    [RegularExpression (".+\\@.+\\..+",ErrorMessage = "Пожалуйста укажите e-mail")]
    public string Email { get; set; }
    [Display(Name = "Дата регистрации")]
    public DateTime? RegistationDate { get; set; }
  }
}
