using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoListAspNetAndRazor.Entities;
using ToDoListAspNetAndRazor.Services;

namespace ToDoListAspNetAndRazor.Pages
{
	public class AboutModel : PageModel
	{
		public string Message { get; set; }

		private IAccountsService _accountsService;

		public AboutModel(IAccountsService accountsService)
		{
			_accountsService = accountsService;
		}

		public void OnGet()
		{
		
		}
	}
}
