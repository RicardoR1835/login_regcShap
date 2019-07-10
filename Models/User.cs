using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loginRegC.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required]
        [MinLength(2)]
        public string FName {get;set;}

        [Required]
        [MinLength(2)]
        public string LName {get;set;}

        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [Required]
        [NotMapped]
        [MinLength(8)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PasswordConfirm {get;set;}
    }

    public class LogUser
    {
        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get;set;}
    }

    public class IndexViewModel
    {
        public User NewUser {get;set;}
        public LogUser OldUser {get;set;}
    }

}