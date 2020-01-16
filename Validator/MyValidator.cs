using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Validator
{
	public class MoneyValidator : ValidationAttribute
	{

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			double value2 = (double)value;
			if(value2 > 1 && value2 < 10000)
				return ValidationResult.Success;
			return new ValidationResult("Too small or too big value");
		}
	}
}
