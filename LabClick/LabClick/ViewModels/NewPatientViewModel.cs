using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

namespace LabClick.ViewModels
{
    public class NewPatientViewModel : INotifyPropertyChanged
    {
        // Propriedades do Paciente
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        // Propriedades do Endereço do Paciente
        public string Cep { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }

        public bool Add(NewPatientViewModel viewModel)
        {
            Domain.Entities.Paciente paciente;
            Domain.Entities.Endereco endereco;
            HttpClient client = new HttpClient();

            paciente = Mapper.Map<Domain.Entities.Paciente>(viewModel);
            endereco = Mapper.Map<Domain.Entities.Endereco>(viewModel);

            //Cadastro do Endereço
            var enderecoSerialized = JsonConvert.SerializeObject(endereco);
            var contentinho = new StringContent(enderecoSerialized, Encoding.UTF8, "application/json");
            Uri urinho = new Uri(@"http://apilabclick.mflogic.com.br/endereco/enderecos");

            var resultado = client.PostAsync(urinho, contentinho);
            var enderecoId = int.Parse(resultado.Result.Content.ReadAsStringAsync().Result);

            //Cadastro do Paciente
            paciente.ClinicaId = 1;
            paciente.EnderecoId = enderecoId;

            var pacienteSerialized = JsonConvert.SerializeObject(paciente);

            Uri uri = new Uri(@"http://apilabclick.mflogic.com.br/paciente/pacientes");
            var content = new StringContent(pacienteSerialized, Encoding.UTF8, "application/json");

            var result = client.PostAsync(uri, content);

            if (result.Result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Verifica se todos os campos estão preenchidos.
        /// Se sim, retorna true, se não, retorna false.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            var properties = GetPropertyValues(this);

            foreach (var prop in properties)
            {
                if (prop == null)
                {
                    return false;
                }
            }

            return true;
        }

        private static List<object> GetPropertyValues(object obj)
        {
            Type t = obj.GetType();
            var props = t.GetProperties();
            var objList = new List<object>();

            foreach (var prop in props)
            {
                if (prop.GetIndexParameters().Length == 0)
                {
                    objList.Add(prop.GetValue(obj));
                }
            }

            return objList;
        }
    }
}
