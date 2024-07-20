﻿using CarWashAPI.DTO;
using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CarWashAPI.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RegistrationResult> RegisterUserAsync(RegisterDto registerDto)
        {
            if (await EmailExistsAsync(registerDto.Email))
            {
                return new RegistrationResult { Succeeded = false, Errors = new[] { "Email already exists" } };
            }

            var hashedPassword = HashPassword(registerDto.Password);

            switch (registerDto.Role)
            {
                case "Admin":
                    var admin = new Admin
                    {
                        Name = registerDto.Name,
                        Email = registerDto.Email,
                        Password = hashedPassword,
                        PhoneNumber = registerDto.PhoneNumber,
                        Role = registerDto.Role
                    };
                    _context.Admins.Add(admin);
                    break;
                case "User":
                    var user = new User
                    {
                        Name = registerDto.Name,
                        Email = registerDto.Email,
                        Password = hashedPassword,
                        PhoneNumber = registerDto.PhoneNumber,
                        Role = registerDto.Role
                    };
                    _context.Users.Add(user);
                    break;
                case "Washer":
                    var washer = new Washer
                    {
                        Name = registerDto.Name,
                        Email = registerDto.Email,
                        Password = hashedPassword,
                        PhoneNumber = registerDto.PhoneNumber,
                        Role = registerDto.Role
                    };
                    _context.Washers.Add(washer);
                    break;
                default:
                    return new RegistrationResult { Succeeded = false, Errors = new[] { "Invalid role" } };
            }

            await _context.SaveChangesAsync();
            return new RegistrationResult { Succeeded = true };
        }

        public async Task<dynamic> LoginAsync(string email, string password)
        {
            var admin = await GetAdminByEmailAsync(email);
            if (admin != null && VerifyPassword(password, admin.Password))
            {
                return admin;
            }

            var user = await GetUserByEmailAsync(email);
            if (user != null && VerifyPassword(password, user.Password))
            {
                return user;
            }

            var washer = await GetWasherByEmailAsync(email);
            if (washer != null && VerifyPassword(password, washer.Password))
            {
                return washer;
            }

            return null;
        }

        public async Task<User> GetUserByIdAsync(int UserId)
        {
            return await _context.Users.FindAsync(UserId);
        }

        public async Task<Admin> GetAdminByIdAsync(int AdminId)
        {
            return await _context.Admins.FindAsync(AdminId);
        }

        public async Task<Washer> GetWasherByIdAsync(int WasherId)
        {
            return await _context.Washers.FindAsync(WasherId);
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Admins.AnyAsync(a => a.Email == email) ||
                   await _context.Users.AnyAsync(c => c.Email == email) ||
                   await _context.Washers.AnyAsync(w => w.Email == email);
        }

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Washer> GetWasherByEmailAsync(string email)
        {
            return await _context.Washers.FirstOrDefaultAsync(w => w.Email == email);
        }

        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {

            try
            {
                if (userDto.Role == "User")
                {
                    var user = await _context.Users.FindAsync(userDto.UserId);
                    ToUserEntity(userDto,user);
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else if (userDto.Role == "Washer")
                {
                    var washer = await _context.Washers.FindAsync(userDto.WasherId);
                    ToWasherEntity(userDto,washer);
                    _context.Entry(washer).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    var admin = await _context.Admins.FindAsync(userDto.AdminId);
                    ToAdminEntity(userDto,admin);
                    _context.Entry(admin).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(string email)
        {
            var admin = await GetAdminByEmailAsync(email);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            }
            else
            {
                var user = await GetUserByEmailAsync(email);
                if (user != null)
                {
                    _context.Users.Remove(user);
                }
                else
                {
                    var washer = await GetWasherByEmailAsync(email);
                    if (washer != null)
                    {
                        _context.Washers.Remove(washer);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }

        private Washer ToWasherEntity(UserDto userDto,Washer washer)
        {
            washer.WasherId = userDto.WasherId ?? 0;
            washer.Name = userDto.Name;
            washer.Email = userDto.Email;
            washer.Password = userDto.Password;
            washer.PhoneNumber = userDto.PhoneNumber;
            washer.ProfilePicture = userDto.ProfilePicture;
            washer.Address = userDto.Address;
            washer.Role = userDto.Role;
            return washer;
           
        }

        private Admin ToAdminEntity(UserDto userDto, Admin admin)
        {
            admin.Name = userDto.Name;
            admin.Email = userDto.Email;
            admin.Password = userDto.Password;
            admin.PhoneNumber = userDto.PhoneNumber;
            admin.ProfilePicture = userDto.ProfilePicture;
            admin.Address = userDto.Address;
            admin.Role = userDto.Role;
            return admin;
        }

        private User ToUserEntity(UserDto userDto, User user)
        {
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.PhoneNumber = userDto.PhoneNumber;
            user.ProfilePicture = userDto.ProfilePicture;
            user.Address = userDto.Address;
            user.Role = userDto.Role;
            return user;

        }

    }

    public class RegistrationResult
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
    }


}
