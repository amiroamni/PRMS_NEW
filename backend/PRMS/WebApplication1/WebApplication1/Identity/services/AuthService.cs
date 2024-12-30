using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PRMS_BackendAPI.Identity.IdentityInterface;
using PRMS_BackendAPI.Identity.Identitys;
using PRMS_BackendAPI.Identity.Infra_Identitiy;
using PRMS_BackendAPI.Identity.Infra_Identitiy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace PRMS_BackendAPI.Identity.services
    {
        public class AuthService : IAuthService
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly JwtSettings _jwtSettings;
        private readonly PRMS_IdentityDbContext _identityDbContext;


        public AuthService(UserManager<ApplicationUser> userManager,
                IOptions<JwtSettings> jwtSettings,
                  PRMS_IdentityDbContext identityDbContext,
                SignInManager<ApplicationUser> signInManager)
            {
            _identityDbContext = identityDbContext;
            _userManager = userManager;
                _jwtSettings = jwtSettings.Value;
                _signInManager = signInManager;
            }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception($"User with {request.Email} not found.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception($"Credentials for '{request.Email} aren't valid'.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var roles = await _userManager.GetRolesAsync(user);

            AuthResponse response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
                Role = new List<string> { user.Role }


            };

            return response;
        }

        public async Task<RegistrationResponse> Register(RegistraionRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception($"Username '{request.UserName}' already exists.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true

            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return new RegistrationResponse() { UserId = user.Id };
                }
                else
                {
                    // Concatenate error messages for better readability
                    var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"User creation failed: {errorMessages}");
                }
            }
            else
            {
                throw new Exception($"Email {request.Email} already exists.");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            // Retrieve user claims and roles
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = new List<string> { user.Role };

            // Ensure at least one role exists; if none, default to "User"
            var role = roles.FirstOrDefault() ?? "User";

            // Debugging step: Log the retrieved roles
            Console.WriteLine($"User {user.UserName} has role: {role}");

            // Add the core claims
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id),
        new Claim("Role", role) 
        // Single role claim, explicitly added
    };

            // Include additional user claims if they exist
            claims.AddRange(userClaims);

            // Generate signing credentials and create the token
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutees),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }





    }
}

