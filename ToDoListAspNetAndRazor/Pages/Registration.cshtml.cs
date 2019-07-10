using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoListAspNetAndRazor.Entities;
using ToDoListAspNetAndRazor.Models.DTO;
using ToDoListAspNetAndRazor.Services;

namespace ToDoListAspNetAndRazor.Pages
{
    public class RegistrationModel : PageModel
    {
	    private IAccountsService _accountsService;

	    public RegistrationModel(IAccountsService accountsService)
	    {
		    _accountsService = accountsService;
	    }

		[BindProperty]
	    public RegistrationModelDTO model { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
	        if (!model.Email.IsNullOrEmpty() && !model.Password.IsNullOrEmpty() && !model.UserName.IsNullOrEmpty())
	        {
		        var user = new UserEnt()
		        {
			        UserName = model.UserName,
			        Email = model.Email
		        };
		        var token = _accountsService.RegistrationUser(user, model.Password);

		        return StatusCode(200, "Successful");
	        }

	        return BadRequest("ok");
        }


	}
}