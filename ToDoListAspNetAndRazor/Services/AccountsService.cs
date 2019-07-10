using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoListAspNetAndRazor.Entities;

namespace ToDoListAspNetAndRazor.Services
{
	public class AccountsService: IAccountsService
	{
		private UserManager<UserEnt> _userManager;

		public AccountsService(UserManager<UserEnt> userManager)
		{
			_userManager = userManager;
		}

		public UserEnt GetUser()
		{
			throw new NotImplementedException();
		}


		public string RegistrationUser(UserEnt user, string password)
		{
			
			var result = _userManager.CreateAsync(user, password);


			return "";
		}
	}
}
