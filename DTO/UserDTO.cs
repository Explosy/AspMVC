using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace DTO {
  public class UserDTO {
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
