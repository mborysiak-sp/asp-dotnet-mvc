using MVCProject.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models
{
	public class Game
	{
		public int Id { get; set; }

		[Required]
		[MoneyValidator]
		public double Price { get; set; }

		[Required]
		[StringLength(64, MinimumLength = 2)]
		public string Name { get; set; }

		public int? DeveloperId { get; set; }
		[ForeignKey("Developer")]
		public virtual Developer Developer { get; set; }
	}
}
