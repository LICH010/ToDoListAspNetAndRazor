using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAspNetAndRazor.Config
{
	public class IdentityServerSettings
	{
		public static string ApiName { get; set; }
		public static string ClientId { get; set; }
		public static string ClientSecret { get; set; }
		public static string ServerUrl { get; set; }
		public static int AccessTokenLifetime { get; set; }
	}
}
