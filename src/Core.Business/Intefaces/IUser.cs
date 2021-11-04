using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Core.Business.Intefaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        string GetUserToken();
        bool IsAuthenticated(); 
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
        HttpContext GetHttpContext();
    }
}