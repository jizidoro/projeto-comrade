#region

using comrade.Domain.Interfaces;

#endregion

namespace comrade.Domain.Bases
{
    public class LookupEntity : ILookupEntity
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}