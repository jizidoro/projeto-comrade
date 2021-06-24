#region

using System.Collections.Generic;
using comrade.External.Bases;

#endregion

namespace comrade.External.Utils
{
    public interface IListResultDto<T>
    {
        IList<T> Data { get; set; }
    }
}