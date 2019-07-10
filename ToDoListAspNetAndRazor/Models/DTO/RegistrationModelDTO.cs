using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ToDoListAspNetAndRazor.Models.DTO
{
	public class RegistrationModelDTO
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		
		public string Password { get; set; }
	}
}
