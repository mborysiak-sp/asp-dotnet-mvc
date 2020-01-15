using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Data
{
	public class AccountContext : IdentityDbContext<User>
	{
		public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }
	}
}
