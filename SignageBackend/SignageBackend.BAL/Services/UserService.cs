using System;
using SignageBackend.BAL.Services;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using SignageBackend.DAL.Models;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using SignageBackend.Common.Utilites;


namespace SignageBackend.BAL.Services
{
    public class UserService : IUser
    {

        private WSignageContext _context;

        public UserService(WSignageContext context)
        {
            _context = context;
        }

        public void CreateOrUpdate(UserView userView)
        {
            try
            {
                //var passwordhash = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(userView.PasswordHash)).Select(s => s.ToString("x2")));


                if (userView.UserId != null)
                {
                    if (userView.UserId != 0)
                    {
                        User a = _context.Users.Where(x => x.UserId == userView.UserId).FirstOrDefault();
                        if (a != null)
                        {
                            a.FirstName = userView.FirstName;
                            a.LastName = userView.LastName;
                            a.UserName = userView.Username;
                            a.Email = userView.Email;
                            //a.PasswordHash = passwordhash;
                            a.RoleId = userView.RoleId;
                            a.IsActive = userView.IsActive;
                            a.CreatedAt = userView.CreatedAt;
                            a.UpdatedAt = DateTime.Now;

                            _context.Users.Update(a);
                            _context.SaveChanges();

                        }
                    }
                    else
                    {
                        var passwordhash = CommonUtillity.EncryptData(userView.PasswordHash);
                        User a = new User();
                        a.FirstName = userView.FirstName;
                        a.LastName = userView.LastName;
                        a.Email = userView.Email;
                        a.UserName = userView.Username;
                        a.PasswordHash = passwordhash;
                        a.RoleId = userView.RoleId;
                        a.IsActive = userView.IsActive;
                        a.CreatedAt = DateTime.Now;
                        a.UpdatedAt = null;

                        _context.Users.Add(a);
                        _context.SaveChanges();

                    }

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public async Task<PaginatedResult<UserView>> GetAll(
                string? searchTerm = null,     // Optional search term
                string sortBy = "Name",       // Default sort by "Name"
                bool sortDescending = false,  // Default ascending order
                int pageNumber = 1,           // Default page number
                int pageSize = 10             // Default page size
            )
        {
            // Ensure page number and size are valid
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = from u in _context.Users
                        join v in _context.Roles
                           on u.RoleId equals v.Id
                        select new UserView
                        {
                            UserId = u.UserId,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Username = u.UserName,
                            PasswordHash = u.PasswordHash,
                            Email = u.Email,
                            IsActive = u.IsActive,
                            RoleName = v.RoleName,
                            RoleId = u.RoleId,
                            UpdatedAt = u.UpdatedAt,
                            CreatedAt = u.CreatedAt,
                        };

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x => x.FirstName.Contains(searchTerm));
            }

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(x => x.FirstName) : query.OrderBy(x => x.FirstName),
                "createdat" => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                "updatedat" => sortDescending ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderBy(x => x.UpdatedAt),
                _ => sortDescending ? query.OrderByDescending(x => x.FirstName) : query.OrderBy(x => x.FirstName) // Default sorting
            };

            // Get total item count for pagination
            int totalItems = await query.CountAsync();

            // Calculate total pages
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Apply paging
            query = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // Project to LayoutView and execute the query
            var items = await query.Select(u => new UserView
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PasswordHash = u.PasswordHash,
                Username = u.Username,
                Email = u.Email,
                IsActive = u.IsActive,
                RoleName = u.RoleName,
                RoleId = u.RoleId,
                UpdatedAt = u.UpdatedAt,
                CreatedAt = u.CreatedAt
            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<UserView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };

        }

        public async Task<UserView> GetById(int id)
        {
            var query = await (from u in _context.Users
                               join v in _context.Roles
                               on u.RoleId equals v.Id
                               where u.UserId == id
                               select new UserView
                               {
                                   UserId = u.UserId,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Username = u.UserName,
                                   Email = u.Email,
                                   IsActive = u.IsActive,
                                   RoleId = u.RoleId,
                                   RoleName = v.RoleName,
                                   UpdatedAt = u.UpdatedAt,
                                   CreatedAt = u.CreatedAt,
                               }).FirstOrDefaultAsync();
            return query;
        }

        public async Task<UserView> GetByEmail(string email)
        {

            var query = await (from u in _context.Users
                               where u.Email == email
                               select new UserView
                               {
                                   UserId = u.UserId,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Username = u.UserName,
                                   Email = u.Email,
                                   IsActive = u.IsActive,
                                   RoleId = u.RoleId,
                                   UpdatedAt = u.UpdatedAt,
                                   CreatedAt = u.CreatedAt,
                               }).FirstOrDefaultAsync();
            return query;
        }

        public async Task<UserView> DeleteById(int id)
        {
            // Fetch the user from the database
            User a = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            // Check if the user exists
            if (a == null)
            {
                // Handle user not found case (throw exception or return null)
                return null; // Or throw an exception
            }
            else
            {
                var userView = new UserView
                {
                    UserId = a.UserId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Username = a.UserName,
                    Email = a.Email,
                    IsActive = false,
                    RoleId = a.RoleId,
                    UpdatedAt = a.UpdatedAt,
                    CreatedAt = a.CreatedAt,
                };
                _context.Users.Update(a);
                _context.SaveChanges();
                return userView;

            }

        }


        public AuthenticateView Authenticate(UserLoginView userLoginView)
        {

            AuthenticateView authenticateView = new AuthenticateView();
            if (userLoginView != null)
            {
                var query = _context.Users.Where(x => x.Email == userLoginView.Email).FirstOrDefault();
                if (query != null)
                {
                    var encrptPWD = CommonUtillity.EncryptData(userLoginView.Password);
                    if (query.PasswordHash == encrptPWD)
                    {
                        var queryrole = _context.Roles.Where(x => x.Id == query.RoleId).FirstOrDefault();
                        authenticateView.Id = query.UserId;
                        authenticateView.Email = userLoginView.Email;
                        authenticateView.FirstName = query.FirstName;
                        authenticateView.LastName = query.LastName;
                        authenticateView.UserName = query.UserName;
                        authenticateView.RoleId = query.RoleId;
                        authenticateView.RoleName = queryrole.RoleName;
                        authenticateView.Status = "Successfully";
                    }
                    else
                    {
                        authenticateView.Email = userLoginView.Email;
                        authenticateView.Status = "Password not Matched";
                    }
                }
                else
                {
                    authenticateView.Email = userLoginView.Email;
                    authenticateView.Status = "Email Id not Exists";
                }
            }

            return authenticateView;
        }
    }

}
