using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoListAspNetAndRazor.Entities;

namespace ToDoListAspNetAndRazor.Services
{
	public interface IAccountsService
	{
		UserEnt GetUser();

		string RegistrationUser(UserEnt user, string password);
	}
}
