using LabClick.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace LabClick.Services
{
    public class LoginService
    {
        public bool Login(Usuario usuario)
        {
            Uri urinho = new Uri(@"http://apilabclick.mflogic.com.br/usuario/getbyemail={usuario.Email}");
            HttpClient client = new HttpClient();

            var result = client.GetAsync(urinho);

            if (result.Result.IsSuccessStatusCode)
            {
                var content = result.Result.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Usuario>(content.Result);

                if (user.Senha == usuario.Senha)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}
