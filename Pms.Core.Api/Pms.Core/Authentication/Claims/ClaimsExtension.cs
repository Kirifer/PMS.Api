using System.Security.Claims;

namespace Pms.Core.Authentication
{
    public static class ClaimsExtension
    {
        /// <summary>
        /// Gets the first claim with matching type.
        /// </summary>
        public static Claim GetClaim(this ClaimsIdentity claimIdentity, string claimType)
        {
            return claimIdentity.Claims.FirstOrDefault(entity => entity.Type == claimType)!;
        }

        /// <summary>
        /// Gets all claims with matching type.
        /// </summary>
        public static IEnumerable<Claim> GetClaims(this ClaimsIdentity claimIdentity, string claimType)
        {
            return claimIdentity.Claims.Where(entity => entity.Type == claimType);
        }

        /// <summary>
        /// Try to get the claim value
        /// </summary>
        /// <param name="claimIdentity">Claims identity instance</param>
        /// <param name="claimType">Type of the claim which will be obtained</param>
        /// <param name="claimValue">The value of claim to be obtained</param>
        /// <returns>True when the obtainment was successfull, otherwise false</returns>
        public static bool TryGetClaimValue(this ClaimsIdentity claimIdentity, string claimType, out string claimValue)
        {
            claimValue = null!;
            var claim = claimIdentity.GetClaim(claimType);
            if (Equals(claim, null)) return false;

            claimValue = claim.Value;
            return true;
        }

        /// <summary>
        /// Try to get the claims value
        /// </summary>
        /// <param name="claimIdentity">Claims identity instance</param>
        /// <param name="claimType">Type of the claim which will be obtained</param>
        /// <param name="claimList">The claim list of values to be obtained</param>
        /// <returns>True when the obtainment was successfull, otherwise false</returns>
        public static bool TryGetClaimsValue(this ClaimsIdentity claimIdentity, string claimType, out IEnumerable<string> claimList)
        {
            claimList = null!;
            var claims = claimIdentity.GetClaims(claimType);
            if (Equals(claims, null)) return false;

            claimList = claims.Select(claim => claim.Value);
            return true;
        }
    }
}
