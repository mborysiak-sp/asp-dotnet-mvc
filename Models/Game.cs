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
		[Range(0.99, 100000)]
		public double Price { get; set; }

		[Required]
		[StringLength(64, MinimumLength = 2)]
		public string Name { get; set; }

		[ForeignKey("Developer")]
		[Key]
		public int? DeveloperId { get; set; }
		
		public virtual Developer Developer { get; set; }
	}
}
