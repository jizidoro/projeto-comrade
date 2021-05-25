#region

using System.Collections.Generic;
using comrade.Application.Bases;

#endregion

namespace comrade.Application.Utils
{
    public interface IListResultDto<TDto> : IResultDto
        where TDto : Dto
    {
        IList<TDto> Data { get; set; }
    }
}