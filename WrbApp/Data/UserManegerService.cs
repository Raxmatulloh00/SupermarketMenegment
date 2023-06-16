using AntDesign;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NuGet.Packaging;
using Plugins.DataStore.SQL;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WrbApp.Data
{
	public class UserManegerService
	{
		readonly UserManager<IdentityUser> _userManager;
		readonly RoleManager<IdentityRole> _roleManager;
		readonly WrbAppContext _appContext;

		public UserManegerService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, WrbAppContext _appContext)
		{
			_userManager = userManager;
			this._appContext = _appContext;
		}
		//public async Task<List<Roles>> GetRolesAsync()
		//{

		//}

		public async Task DelateUser(string id)
		{
			List<IdentityUser> users = new();
			IdentityUser user = new();

			try
			{
				users = _appContext.Users.ToList();
				user = users?.FirstOrDefault(w => w?.Id == id);
				await _userManager.DeleteAsync(user);

			}
			catch (Exception)
			{
			}
		}

		public string HashPassword(string password)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(password);
				byte[] hash = sha256.ComputeHash(bytes);
				string hashedPassword = Convert.ToBase64String(hash);
				return hashedPassword;
			}
		}

		public async Task Edit(string id,
					string name,
					string password,
					string confirmpassword,
					List<string> roleId)
		{
			List<IdentityUser> users = new();
			/*List<IdentityUserRole<string>> rols = new();*/
			IdentityUser user = new();
			IdentityRole role = new();

			try
			{
				users = _appContext.Users.ToList();
				user = users?.FirstOrDefault(f => f?.Id == id);
				user.UserName = name;
				user.PasswordHash = HashPassword(password);
				user.PasswordHash = HashPassword(confirmpassword);
				if (roleId.Count > 1)
				{
					bool firstTime = false;
					foreach (var item in roleId)
					{
						var userID = _appContext.UserRoles.Where(w => w.UserId == id);
						if (userID != null && !firstTime)
						{
							firstTime = true;
							_appContext.UserRoles.RemoveRange(userID);
							await Task.Delay(500);
							_appContext.SaveChanges();
						}
						userID.First().RoleId = item;
						_appContext.UserRoles.AddRange(userID);
					}
					_appContext.SaveChanges();
				}
				else
				{

					var usera = _appContext.UserRoles.Where(w => w.UserId == id).ToList();
					List<Roles> userRolesTable = new List<Roles>();
					foreach (var item in usera)
					{
						var dsad = new Roles();
						{
							dsad.UserId = item.UserId;
							dsad.RoleId = item.RoleId;
						}
						userRolesTable.Add(dsad);
					}
					if (userRolesTable.Count > 1)
					{
						var upd = usera.Where(w => w.RoleId == roleId.First()).First() ;
						_appContext.UserRoles.Remove(upd);
						await Task.Delay(500);
						_appContext.SaveChanges();
						upd.RoleId = roleId.First();
						_appContext.UserRoles.Update(upd);
					}
					else
					{
						var updateUser = usera.First();
						_appContext.UserRoles.Update(updateUser);
					}
					_appContext.SaveChanges();
				}

				//await _userManager.UpdateAsync(user);
			}
			catch (Exception e)
			{

			}
		}

		public async Task<List<UsersList>> GetAllUser()
		{
			List<UsersList> MyUsers = new();
			try
			{

				var userroles = _appContext.UserRoles;
				List<Roles> rols = new List<Roles>();
				foreach (var role in userroles)
				{
					Roles roles = new Roles
					{
						RoleId = role.RoleId,
						UserId = role.UserId
					};
					rols.Add(roles);
				}

				foreach (var item in _userManager.Users)
				{
					UsersList userDBl = new UsersList();
					List<string> userRoles = rols.Where(x => x.UserId == item.Id)
												.Select(x => x.RoleId == "1" ? "Admin" : "Cashier")
												.ToList();
					userDBl = new UsersList
					{
						UserId = item.Id,
						userName = item.UserName,
						roles = userRoles
					};
					MyUsers.Add(userDBl);
				}
			}
			catch (Exception e)
			{

				Console.WriteLine(e.Message);

			}
			//user = _userManager.Users.FirstOrDefault(x => x.Id == "b8fa71be-41d1-4588-9e87-f310acf25f9d");
			Console.WriteLine(MyUsers);
			return MyUsers;
		}
	}

	public class UsersList
	{
		public string UserId { get; set; }
		public string? userName { get; set; }
		public List<string> roles { get; set; }
	}
	public class UserRoles
	{
		public string? roleId { get; set; }
		public string? roleName { get; set; }
	}
	public class Roles
	{
		public string UserId { get; set; }
		public string RoleId { get; set; }
	}
}
