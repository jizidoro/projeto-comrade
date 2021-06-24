#region

using comrade.External.Bases;

#endregion

namespace comrade.External.Utils
{
    public interface ISingleResultDto<TDto>
        where TDto : Dto
    {
        TDto Data { get; }
    }
}