﻿using FingersFly.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Security.Claims;

namespace FingersFly.API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static async Task<AppUser> GetUserByEmail(
            this UserManager<AppUser> userManager, ClaimsPrincipal claims)
        {
            var user = await userManager.Users
                .FirstOrDefaultAsync(x => x.Email == claims.GetEmail());

            if (user == null)
            {
                throw new AuthenticationException("User not found!");
            }

            return user;
        }

        public static async Task<AppUser> GetUserByEmailWithAddress(
            this UserManager<AppUser> userManager, ClaimsPrincipal claims)
        {
            var user = await userManager.Users
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Email == claims.GetEmail());

            if (user == null)
            {
                throw new AuthenticationException("User not found!");
            }

            return user;
        }

        public static string GetEmail(this ClaimsPrincipal claims)
        {
            var email = claims.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                throw new AuthenticationException("Email not found!");
            }

            return email;
        }
    }
}
