using System;
using System.ComponentModel.DataAnnotations;


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

    public override bool Equals(object obj) {
      if ((obj is null) && (GetType() != obj.GetType())) {
        return false;
      }
      else {
        UserDTO userDTO = obj as UserDTO;
        if (Id.Equals(userDTO.Id) &&
          Name.Equals(userDTO.Name) &&
          Surname.Equals(userDTO.Surname)&&
          Age.Equals(userDTO.Age) &&
          Email.Equals(userDTO.Email)&&
          RegistationDate.Equals(userDTO.RegistationDate)){
          return true;
        }
        return false;
      }
    }

    public override int GetHashCode() {
      return Id.GetHashCode();
    }
  }
}
