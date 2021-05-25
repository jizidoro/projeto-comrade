#region

using System.Collections.Generic;
using comrade.Core.Helpers.Interfaces;

#endregion

namespace comrade.Application.Utils
{
    public interface IResultDto : IResult
    {
        IList<string> Mensagens { get; set; }
    }
}