#region

using System;
using System.Reflection;
using comrade.Domain.Models;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Extensions;

#endregion

namespace comrade.UnitTests.Helpers
{
    public static class Utilities
    {
        private const string JsonPath = "comrade.Infrastructure.SeedData";

        #region DadosIniciais

        public static void InitializeDbForTests(ComradeContext db)
        {
            try
            {
                var assembly = Assembly.GetAssembly(typeof(JsonUtilities));

                if (assembly is not null)
                {
                    db.Airplanes.AddRange(
                        JsonUtilities.GetListFromJson<Airplane>(
                            assembly.GetManifestResourceStream($"{JsonPath}.airplane.json")));

                    db.UsuarioSistemas.AddRange(
                        JsonUtilities.GetListFromJson<UsuarioSistema>(
                            assembly.GetManifestResourceStream($"{JsonPath}.usuarioSistema.json")));
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion
    }
}