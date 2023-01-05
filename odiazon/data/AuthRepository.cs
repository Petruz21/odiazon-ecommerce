using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using odiazon.data_response;
using odiazon.models.m_user;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace odiazon.data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Response<string>> Login(string email, string password)
        {
            Response<string> response = new ();
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()))
                    ?? throw new Exception("User not found");

                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new Exception("Wrong password, try again");
                }
                else
                {
                    response.Data = CreateToken(user);
                }
                return response;

            } catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<Response<int>> Register(User user, string password)
        {
            Response<int> response = new();

            if (await UserExists(user.Email))
            {
                response.Success = false;
                response.Message = "User alredy exists";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;
            return response;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Token").Value)); // aggiungere la chiave segreta che sta dentro appsettings.json

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); //token;
        }
        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
