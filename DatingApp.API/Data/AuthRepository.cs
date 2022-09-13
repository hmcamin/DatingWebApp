using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Register(User user, string password) {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<User> Login(string username, string password){
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null)
                return null;

            if(VerifyPasswordHash(password, user.passwordHash, user.passwordSalt))
                return null;

            return user;
        }

        public async Task<bool> UserExists(string username){
            if(await _context.Users.AnyAsync(x => x.Username == username)) return true;
            
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt){
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.computeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }
        private void VerifyPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt){
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.computeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(i=0; i< computedHash.length; i++){
                    if(computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
            
        }
    }
}