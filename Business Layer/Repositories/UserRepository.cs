﻿using AutoMapper;
using Business_Layer.DataAccess;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public readonly SignInManager<User> _signInManager;
        public readonly AdminAccount _adminAccount;
        public readonly JWTSetting _jWTSetting;
        public readonly FastFoodDeliveryDBContext _context;
        public readonly IMapper _mapper;

        public UserRepository(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            IOptionsMonitor<AdminAccount> adminAccount,
            IOptionsMonitor<JWTSetting> jWTSetting,
            FastFoodDeliveryDBContext context,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _adminAccount = adminAccount.CurrentValue;
            _jWTSetting = jWTSetting.CurrentValue;
            _context = context;
            _mapper = mapper;
        }

        public async Task<APIResponseModel> Login(LoginVM model)
        {
            var secretKeyBytes = Encoding.UTF8.GetBytes(_jWTSetting.Key);
            var jwtTokenHander = new JwtSecurityTokenHandler();
            var result = await ValidLogin(model);
            if (!result.IsSuccess)
            {
                return result;
            }
            var claims = await GetClaimsUsers(model);
            var tokenDecription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(3),
                Issuer = _jWTSetting.Issuer,
                Audience = _jWTSetting.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256)
            };
            var token = jwtTokenHander.CreateToken(tokenDecription);
            string accessToken = jwtTokenHander.WriteToken(token);
            if (!result.IsSuccess)
                return result;
            result.Data = accessToken;
            return result;
        }

        private async Task<APIResponseModel> ValidLogin(LoginVM model)
        {
            var result = new APIResponseModel()
            {
                code = 200,
                message = "Ok",
                IsSuccess = true,
            };
            var userIdentity = await _userManager.FindByNameAsync(model.UserName);
            if (userIdentity == null || !await _userManager.CheckPasswordAsync(userIdentity, model.Password))
            {
                if (model.UserName == _adminAccount.username)
                {
                    var adminAccount = await _userManager.FindByNameAsync(_adminAccount.username);
                    if (adminAccount == null)
                    {
                        var admin = new User()
                        {
                            Email = model.UserName,
                            SecurityStamp = Guid.NewGuid().ToString(),
                            UserName = model.UserName,
                            FullName = model.UserName,
                            Address = model.UserName,


                        };
                        var resultCreateUser = await _userManager.CreateAsync(admin, _adminAccount.password);
                        var resultRole = await _userManager.AddToRoleAsync(admin, "Admin");
                    }
                }
                else
                {
                    return new APIResponseModel
                    {
                        code = 400,
                        IsSuccess = false,
                        message = "Username or password is incorrect!",
                    };
                }

            }
            return result;
        }

        private async Task<List<Claim>> GetClaimsUsers(LoginVM model)
        {
            List<Claim> result;
            var user = await _userManager.FindByNameAsync(model.UserName);
            var roles = await _userManager.GetRolesAsync(user);
            string role = roles[0].ToString();
            result = new List<Claim>()
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
                new("UserId", user.Id),
                new(ClaimTypes.Role, role),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            return result;
        }



        public async Task<APIResponseModel> Register(RegisterVM model)
        {
            APIResponseModel result = new APIResponseModel()
            {
                code = 200,
                IsSuccess = true,
                message = "User created success",
            };
            //Check có trùng Email, Username của hệ thống không.
            var userExistMail = await _userManager.FindByEmailAsync(model.Email);
            var userExistName = await _userManager.FindByNameAsync(model.Username);
            if (userExistMail != null || userExistName != null)
            {
                return new APIResponseModel
                {
                    code = 400,
                    message = "User has been already existed!",
                    IsSuccess = false,
                };
            }
            // Nếu chưa tồn tại thid tạo ra cho 1 cái account mới

            var user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FullName = model.FullName,
                Address = model.Address,

            };
            var resultCreateUser = await _userManager.CreateAsync(user, model.Password);
            var resultRole = await _userManager.AddToRoleAsync(user, "User");

            if (!resultCreateUser.Succeeded)
            {
                return new APIResponseModel()
                {
                    code = 200,
                    message = "Error when create user",
                    IsSuccess = false,
                };
            }
            return result;
        }
    }
}