using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataLayer.Entities {
  public partial class User {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int? Age { get; set; }
    public string Email { get; set; }
    public DateTime? RegistationDate { get; set; }
  }
}
