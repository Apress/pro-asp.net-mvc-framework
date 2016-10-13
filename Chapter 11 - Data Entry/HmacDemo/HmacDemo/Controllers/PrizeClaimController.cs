using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace HmacDemo.Controllers
{
    public class PrizeClaimController : Controller
    {
        public ViewResult ClaimForm()
        {
            string prize = "$10.00";
            DateTime expiry = DateTime.Now.AddMinutes(15);

            return View(new
            {
                PrizeWon = prize,
                PrizeHash = TamperProofing.GetExpiringHMAC(prize, expiry)
            });
        }

        public string SubmitClaim(string PrizeWon, string PrizeHash, string Address)
        {
            var verificationResult = TamperProofing.Verify(PrizeWon, PrizeHash);
            if (verificationResult == TamperProofing.HMACResult.OK)
                return string.Format("OK, we'll send the {0} to {1}",
                    HttpUtility.HtmlEncode(PrizeWon), HttpUtility.HtmlEncode(Address));
            else
                return "Sorry, you tried to cheat or were too slow.";
        }
    }
}
