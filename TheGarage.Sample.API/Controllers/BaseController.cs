using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace TheGarage.Sample.API.Controllers
{
    public abstract class BaseController : Controller
    {
        public int GarageId { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (User != null)
            {
                if (User.Identity != null)
                {
                    var claims = ((System.Security.Claims.ClaimsIdentity)User.Identity).Claims;
                    if (claims!=null)
                    {
                        var cp = claims.First(p => p.Type == TheGarageClaimTypes.GarageId);
                        if (cp != null)
                        {
                            GarageId = Convert.ToInt32(cp.Value);
                        }
                    }
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
