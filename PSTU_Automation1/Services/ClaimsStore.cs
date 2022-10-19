using System.Collections.Generic;
using System.Security.Claims;

namespace PSTU_Automation1.Services
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Dean Office", "Dean Office"),
            new Claim("Hall","Hall"),
            new Claim("Department","Department"),
        };
    }
}
