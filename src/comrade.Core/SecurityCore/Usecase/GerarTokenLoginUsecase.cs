#region

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using comrade.Core.SecurityCore.Validation;
using comrade.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace comrade.Core.SecurityCore.Usecase
{
    public class GerarTokenLoginUsecase : IGerarTokenLoginUsecase
    {
        private readonly IConfiguration _configuration;
        private readonly UsuarioSistemaValidarSenha _usuarioSistemaValidarSenha;


        public GerarTokenLoginUsecase(
            IConfiguration configuration,
            UsuarioSistemaValidarSenha usuarioSistemaValidarSenha
        )
        {
            _configuration = configuration;
            _usuarioSistemaValidarSenha = usuarioSistemaValidarSenha;
        }

        public async Task<SecurityResult> Execute(string chave, string senha)
        {
            var success = int.TryParse(chave, out var number);
            if (success)
            {
                var result = await Task.Run(() =>
                {
                    var resultSenha = _usuarioSistemaValidarSenha.Execute(number, senha);


                    if (resultSenha.Sucesso)
                    {
                        var usuSelecionado = resultSenha.Data;

                        var perfil = "Role";

                        var user = new User
                        {
                            Chave = chave,
                            Nomeario = usuSelecionado.Nome,
                            Papeis = new List<string> {string.IsNullOrEmpty(perfil) ? "" : perfil}
                        };

                        user.Token = GenerateToken(user);

                        return new SecurityResult(user);
                    }

                    return new SecurityResult(resultSenha.Codigo, resultSenha.Mensagem);
                });

                return result;
            }

            return new SecurityResult(400, "Erro na chave do usuario");
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var clains = new List<Claim>
            {
                new(ClaimTypes.Name, user.Chave),
                new("Nome", user.Nomeario)
            };

            foreach (var role in user.Papeis)
            {
                clains.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(clains),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}