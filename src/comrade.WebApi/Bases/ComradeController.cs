#region

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace comrade.WebApi.Bases
{
    public class ComradeController : ControllerBase
    {
        [NonAction]
        protected int? GetUserId()
        {
            if (User == null || !User.Claims.Any())
            {
                throw new ("Usuario não esta logado no sistema");
            }

            return User != null ? int.Parse(User.Claims.First(i => i.Type == "Chave").Value) : 0;
        }
    }
}