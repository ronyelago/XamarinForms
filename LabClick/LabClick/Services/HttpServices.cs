using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LabClick.Services
{
    public class HttpServices
    {
        public async Task<List<Domain.Entities.Paciente>> GetPatientList(string nome)
        {
            List<Domain.Entities.Paciente> pacientes = new List<Domain.Entities.Paciente>();

            var client = new HttpClient();
            var result = await client.GetAsync($@"http://apilabclick.mflogic.com.br/paciente/getByName={nome}");
            var content = result.Content.ReadAsStringAsync();

            pacientes = JsonConvert.DeserializeObject<List<Domain.Entities.Paciente>>(content.Result);

            return pacientes;
        }
    }
}
