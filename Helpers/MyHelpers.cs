using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Helpers
{
	public class MyHelpers
	{
		public static HtmlString Link(string attribute)
		{
			return new HtmlString(string.Format($"<li class='nav-item'><a class='nav-link text-dark' href='/{attribute}'>{attribute}</a></li>"));
		}
	}
}
