using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;

namespace ToDoListAspNetAndRazor.Config
{
	public class IdentityServerConfig
	{
		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>()
			{
				new ApiResource()
				{
					Name = IdentityServerSettings.ApiName,
					DisplayName = "API",
					UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Role},
					Scopes =
					{
						new Scope()
						{
							Name = IdentityServerSettings.ApiName,
							DisplayName = "API"
						}
					}
				}
			};
		}

		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>()
			{
				new Client()
				{
					ClientId = IdentityServerSettings.ClientId,
					ClientName = "MVC Client",
					AllowedGrantTypes = new[] {GrantType.ResourceOwnerPassword, "external"},

					AccessTokenType = AccessTokenType.Jwt,
					AccessTokenLifetime = IdentityServerSettings.AccessTokenLifetime,

					RequireConsent = false,

					ClientSecrets =
					{
						new Secret(IdentityServerSettings.ClientSecret.Sha256())
					},

					AllowedScopes =
					{
						IdentityServerSettings.ApiName
					},

					AllowOfflineAccess = true,

					//AllowedCorsOrigins = IdentityServerSettings.AllowedOrigins
				}
			};
		}

		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>()
			{
				// new IdentityResources.OpenId(),
				// new IdentityResources.Profile()
			};
		}
	}
}
