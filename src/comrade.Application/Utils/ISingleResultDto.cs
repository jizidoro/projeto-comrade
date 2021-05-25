#region

using comrade.Application.Bases;

#endregion

namespace comrade.Application.Utils
{
    public interface ISingleResultDto<TDto> : IResultDto
        where TDto : Dto
    {
        TDto Data { get; }
    }
}