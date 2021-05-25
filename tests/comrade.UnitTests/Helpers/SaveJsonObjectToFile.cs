#region

using System.Collections.Generic;
using System.IO;
using System.Text.Json;

#endregion

namespace comrade.UnitTests.Helpers
{
    public class SaveJsonObjectToFile<TEntity>
    {
        public void Excute(List<TEntity> result, string nome)
        {
            var oto = JsonSerializer.Serialize(result);

            var path = @"c:\temp\" + nome + ".json";

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                File.WriteAllText(path, oto);
            }
        }
    }
}