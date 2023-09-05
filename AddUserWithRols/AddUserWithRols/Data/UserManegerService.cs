using Microsoft.AspNetCore.Identity;
using System.Text;
using AntDesign;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NuGet.Packaging;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AddUserWithRols.Data;

namespace WrbApp.Data
{
    public class UserManegerService
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly AddUserContext _appContext;
        public Dictionary<string, string> MyRoles { get; set; } = new Dictionary<string, string>();

        public UserManegerService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AddUserContext _appContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this._appContext = _appContext;
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

        //public string HashPassword(string password)
        //{
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        byte[] bytes = Encoding.UTF8.GetBytes(password);
        //        byte[] hash = sha256.ComputeHash(bytes);
        //        string hashedPassword = Convert.ToBase64String(hash);
        //        return hashedPassword;
        //    }
        //}

        public async Task Edit(string id, string name, List<string> roleId)
        {
            List<IdentityUser> users = new();
            List<string> userRoles = new();
            IdentityUser user = new();
            IdentityRole role = new();
            foreach (var item in _appContext.UserRoles.Where(x => x.UserId == id))
            {
                userRoles.Add(item.RoleId);
            }
            try
            {
                users = _appContext.Users.ToList();
                user = users?.FirstOrDefault(f => f?.Id == id);
                user.UserName = name;
                var newRoles = roleId.Where(x => !userRoles.Contains(x));
                foreach (var item in newRoles)
                {
                    var dbRole = _roleManager.Roles.FirstOrDefault(w => w.Id == item).Name;
                    await _userManager.AddToRoleAsync(user, dbRole);
                }
                var removeRoles = userRoles.Where(x => !roleId.Contains(x));
                foreach (var item in removeRoles)
                {
                    var dbRole = _roleManager.Roles.FirstOrDefault(w => w.Id == item).Name;
                    await _userManager.RemoveFromRoleAsync(user, dbRole);
                }
                _appContext.SaveChanges();
            }

            //await _userManager.UpdateAsync(user);
            catch (Exception e)
            {

            }
        }
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

    }

    public class UsersList
    {
        public string UserId { get; set; }
        public string? userName { get; set; }
        public List<string> roles { get; set; }
    }
    public class Roles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}