using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models
{
	public class Developer
	{
		
		public int Id { get; set; }

		[Required]
		[StringLength(64, MinimumLength = 2)]
		public string Name { get; set; }

		[Required]
		[Phone]
		public string Phone { get; set; }

		[Required]
		[StringLength(64, MinimumLength = 2)]
		public string Address { get; set; }

		public virtual ICollection<Game> Games { get; set; }
	}
}
