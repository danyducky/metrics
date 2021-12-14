using System;
using System.ComponentModel.DataAnnotations;
using Metrics.Api.Validation;

namespace Metrics.Api.Models.User
{
    public class UserDateModel
    {
        [Required(ErrorMessage = "Required field!")]
        [DataType(DataType.Date, ErrorMessage = "Paste DateTime!")]
        public DateTime RegistrationDate { get; set; }
        
        [Required(ErrorMessage = "Required field!")]
        [DataType(DataType.Date, ErrorMessage = "Paste DateTime!")]
        [GreaterDate]
        public DateTime LastActivityDate { get; set; }
    }
}