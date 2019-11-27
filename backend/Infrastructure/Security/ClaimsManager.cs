using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Data.Contexts;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using backend.Data.QueryObjects;
using System.Web;
using Microsoft.AspNetCore;

namespace backend.Infrastructure.ClaimsManager
{
    public class ClaimsManager
    {
        private readonly Dictionary<string,string> _claimsDict;

        public ClaimsManager(System.Security.Claims.ClaimsPrincipal user)
        {
            _claimsDict = new Dictionary<string,string>();

            user.Claims.ToList()
                .ForEach(_ => _claimsDict.Add(_.Type, _.Value));
        }

        public string GetEmailClaim() => _claimsDict["Email"];

        public string GetRoleClaim() => 
            _claimsDict["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

        public int GetUserIdClaim() => int.Parse(_claimsDict["UserId"]);

    }
}