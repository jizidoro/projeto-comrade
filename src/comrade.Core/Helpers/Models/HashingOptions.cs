#region

using System;

#endregion

namespace comrade.Core.Helpers.Models
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 100;

        public static implicit operator HashingOptions(int v)
        {
            throw new NotImplementedException();
        }
    }
}