using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoListAspNetAndRazor.Entities;

namespace ToDoListAspNetAndRazor.Contexts
{
	public class DataContext : IdentityDbContext<UserEnt>
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options) {}
	}
}
