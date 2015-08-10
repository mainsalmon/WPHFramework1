using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WPHFramework1
{
    public class Person : ValidatingModelBase
    {
        public Person(string name, int age, string hairColor)
        {
            Name = name;
            Age = age;
            HairColor = hairColor;
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            private set {
                _name = value;
                NotifyOfPropertyChange(() => Name);
             
            }
        }
        private int _age;
        //  [Required(ErrorMessage="Age is a required field")] commented out as it never gets applied for int
        [Range(10, 60, ErrorMessage = "Must be between 10 and 60")]
        public int Age
        {
            get { return _age; }
            set
            {
                if (value != _age)
                {
                    _age = value;
                    NotifyOfPropertyChange(() => Age);
                    ValidateAsync();
                }
            }
        }

        private string _hairColor;
        [Required(ErrorMessage = "Hair Color is a required field")]
        [StringLength(10, ErrorMessage="Must be less than 10 characters")]
        [CustomValidation(typeof(Person), "HairColorValidate")] // first param is the class that contains the validation method, second is the name of the method
        public string HairColor
        {
            get { return _hairColor; }
            set
            {
                if (value != _hairColor)
                {
                    _hairColor = value;
                    NotifyOfPropertyChange(() => HairColor);
                    ValidateAsync();
                }
            }
        }

        #region Custom Validations (beyond the simple validations accomplished via attributes

        /// <summary>
        /// Example of custom validation method.  This approach is nice in that it can perform complex validation
        /// involving combinations of property values
        /// </summary>
        /// <param name="obj">the new value to be validated</param>
        /// <param name="context">the instance of the object that is being validated</param>
        /// <returns>if failure, then returns message and list of fields involved in the validation failure</returns>
        public static ValidationResult HairColorValidate(object obj, ValidationContext context)
        {
           // var personBeingValidated = (Person)context.ObjectInstance;

            string newColor = obj.ToString();
            string[] allowedColors = new string[] { "brown", "black", "red" };
            if (allowedColors.Contains(newColor)){
                return ValidationResult.Success;
            }

            return new ValidationResult("The hair color must be black or brown", new List<string> { "HairColor" });

            // To force other, presumably related, fields to be shown as in error, add to list of property names like this:
            //return new ValidationResult("The hair color must be black or brown",
            //      new List<string> { "HairColor", "Age" });

        }

        #endregion Custom Validations (beyond the simple validations accomplished via attributes
    }
}
