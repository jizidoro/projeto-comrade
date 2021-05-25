#region

using comrade.Application.Utils;

#endregion

namespace comrade.Application.Bases
{
    public class LookupDto : EntityDto, ILookupDto
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}