using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.User
{
    public class UserPostModel
    {
        [Required]
        [RegularExpression("^[A-Z][A-Z a-z ]{4,}$",ErrorMessage ="First Character in UpperCase and Minimum 6 character in FirstName")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[A-Z][A-Z a-z ]{4,}$", ErrorMessage = "First Character in UpperCase and Minimum 5 character in LastName")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[0-9a-zA-Z]+([+#%&_.-][a-zA-Z0-9]+)*[@][0-9]?[a-zA-Z]{2,}[.][a-zA-Z]{2,3}([.][a-zA-Z]{2,3})?$", ErrorMessage = "Enter a Valid Email-Id")]
        //- E.g. abc.xyz@bl.co.in
        public string Email { get; set; }


        [Required]
        [RegularExpression("^(?=.*[A-Z])[A-Z a-z 0-9 $#@!&*?|]{8,}$", ErrorMessage = "Password Have minimum 8 Characters, Should have at least 1 Upper Case and Should have numeric number and Has Special Character")]

        public string Password { get; set; }


    }
}
