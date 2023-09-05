using DataStore.SQL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Web_Hotel.Data
{
    public class UserMenegerServise
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly Travel _travel;

        public UserMenegerServise(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, Travel travel)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _travel = travel;
        }

        public async Task<bool> CreateNewUser(string login, string password, List<string>? roles)
        {
            var user = new IdentityUser { UserName = login, Email = login };
            var resalt = await _userManager.CreateAsync(user, password);
            var dbUser = _userManager.Users.FirstOrDefault(f => f.UserName == login);

            if (roles?.Count > 1 && roles.Count != null)
            {
                foreach (var item in roles)
                {
                    var _user = _travel.Users.FirstOrDefault(f => f.UserName == login);
                    var dbRole = _roleManager.Roles.FirstOrDefault(f => f.Id == item).Name;
                    await _userManager.AddToRoleAsync(user, dbRole);
                }
            }
            else if (roles?.Count != 0)
            {
                var dbRole = _roleManager.Roles.FirstOrDefault(f => roles.Contains(f.Id));
                await _userManager.AddToRoleAsync(dbUser, dbRole?.Name);
            }
            if (resalt.Succeeded)
            {
                return true;
            }
            return false;

        }

        public async Task<List<UserList>> GetAllUser()
        {
            List<UserList> MyUser = new();
            try
            {
                var userroles = _travel.UserRoles;
                List<Roles> rols = new List<Roles>();
                foreach (var role in userroles)
                {
                    Roles newrole = new Roles
                    {
                        UserId = role.UserId,
                        RoleId = role.RoleId,
                    };
                    rols.Add(newrole);
                }
                foreach (var item in _userManager.Users)
                {
                     UserList newUsers = new UserList();
                    List<string> userRoles = rols.Where(x => x.UserId == item.Id)
                                                .Select(x => x.RoleId == "1" ? "Admin" : "Admin")
                                                .ToList();
                    newUsers = new UserList
                    {
                        UserId = item.Id,
                        userName = item.UserName,
                        roles = userRoles
                    };
                    MyUser.Add(newUsers);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            Console.WriteLine(MyUser);
            return MyUser;
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
        
        public async Task Edit (string id, string name, List<string> roleId)
        {
            List<IdentityUser> users = new();
            List<string> userRoles = new();
            IdentityUser user = new();
            IdentityRole role = new();

            foreach (var item in _travel.UserRoles.Where(w => w.UserId == id))
            {
                userRoles.Add(item.RoleId);
            }
            try
            {
                users = _travel.Users.ToList();
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
                _travel.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task DeleteUser (string id)
        {
            List<IdentityUser> users = new();
            IdentityUser user = new();

            try
            {
                users = _travel.Users.ToList();
                user = users?.FirstOrDefault (f => f.Id == id);
                await _userManager.DeleteAsync(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }



    public class UserList
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