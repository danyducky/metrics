using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Metrics.Api.Models.User
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Required array!")]
        public IList<UserDateModel> Dates { get; set; }
    }
}