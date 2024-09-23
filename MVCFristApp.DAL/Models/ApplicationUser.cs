using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFristApp.DAL.Models
{
	public class ApplicationUser:IdentityUser
	{
		public bool IsAgree;
		public string FName;
		public string LName;
	}
}
