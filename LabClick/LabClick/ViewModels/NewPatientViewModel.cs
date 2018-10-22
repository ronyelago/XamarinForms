using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace LabClick.ViewModels
{
    public class NewPatientViewModel : INotifyPropertyChanged
    {
        private string cep;
        private string uf;
        private string cidade;
        private string bairro;
        private string logradouro;
        private int numero;

        // Propriedades do Paciente
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public bool EmailIsValid { get; set; }
        public bool CpfIsValid { get; set; }
        public string Telefone { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        // Propriedades do Endereço do Paciente
        public int EnderecoId { get; set; }
        public string Cep
        {
            get { return this.cep; }
            set
            {
                this.cep = value;
                OnPropertyChanged();
            }
        }
        public string UF
        {
            get { return this.uf; }
            set
            {
                this.uf = value;
                OnPropertyChanged();
            }
        }
        public string Cidade
        {
            get { return this.cidade; }
            set
            {
                this.cidade = value;
                OnPropertyChanged();
            }
        }
        public string Bairro
        {
            get { return this.bairro; }
            set
            {
                this.bairro = value;
                OnPropertyChanged();
            }
        }
        public string Logradouro
        {
            get { return this.logradouro; }
            set
            {
                this.logradouro = value;
                OnPropertyChanged();
            }
        }
        public int Numero
        {
            get { return this.numero; }
            set
            {
                this.numero = value;
                OnPropertyChanged();
            }
        }

        public Domain.Entities.Endereco GetAddress(string cep)
        {
            if (cep != null && !string.IsNullOrEmpty(cep))
            {
                Uri urinho = new Uri($@"http://viacep.com.br/ws/{cep}/json/");
                HttpClient client = new HttpClient();

                var result = client.GetAsync(urinho);

                if (result.Result.IsSuccessStatusCode)
                {
                    var content = result.Result.Content.ReadAsStringAsync();
                    var endereco = JsonConvert.DeserializeObject<EnderecoViewModel>(content.Result);

                    if (endereco.IsValid())
                    {
                        var address = Mapper.Map<Domain.Entities.Endereco>(endereco);
                        return address;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public HttpResponseMessage Add(NewPatientViewModel viewModel)
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

            try
            {
                var resultado = client.PostAsync(urinho, contentinho);
                var enderecoId = int.Parse(resultado.Result.Content.ReadAsStringAsync().Result);

                if (resultado.Result.IsSuccessStatusCode)
                {
                    try
                    {
                        //Cadastro do Paciente
                        paciente.ClinicaId = 1;
                        paciente.EnderecoId = enderecoId;

                        var pacienteSerialized = JsonConvert.SerializeObject(paciente);

                        Uri uri = new Uri(@"http://apilabclick.mflogic.com.br/paciente/pacientes");
                        var content = new StringContent(pacienteSerialized, Encoding.UTF8, "application/json");

                        var result = client.PostAsync(uri, content);

                        if (result.Result.IsSuccessStatusCode)
                        {
                            return new HttpResponseMessage(HttpStatusCode.OK);
                        }
                        else
                        {
                            return new HttpResponseMessage(HttpStatusCode.BadRequest);
                        }
                    }
                    catch (Exception)
                    {
                        return new HttpResponseMessage(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage Update(NewPatientViewModel viewModel)
        {
            Domain.Entities.Paciente paciente;
            Domain.Entities.Endereco endereco;
            HttpClient client = new HttpClient();

            paciente = Mapper.Map<Domain.Entities.Paciente>(viewModel);
            endereco = Mapper.Map<Domain.Entities.Endereco>(viewModel);

            //Cadastro do Endereço
            var enderecoSerialized = JsonConvert.SerializeObject(endereco);
            var contentinho = new StringContent(enderecoSerialized, Encoding.UTF8, "application/json");
            Uri urinho = new Uri(@"http://apilabclick.mflogic.com.br/endereco/updateEndereco");

            try
            {
                var resultado = client.PostAsync(urinho, contentinho);
                var enderecoId = int.Parse(resultado.Result.Content.ReadAsStringAsync().Result);

                if (resultado.Result.IsSuccessStatusCode)
                {
                    try
                    {
                        //Cadastro do Paciente
                        paciente.ClinicaId = 1;
                        paciente.EnderecoId = enderecoId;

                        var pacienteSerialized = JsonConvert.SerializeObject(paciente);

                        Uri uri = new Uri(@"http://apilabclick.mflogic.com.br/paciente/updatePaciente");
                        var content = new StringContent(pacienteSerialized, Encoding.UTF8, "application/json");

                        var result = client.PostAsync(uri, content);

                        if (result.Result.IsSuccessStatusCode)
                        {
                            return new HttpResponseMessage(HttpStatusCode.OK);
                        }
                        else
                        {
                            return new HttpResponseMessage(HttpStatusCode.BadRequest);
                        }
                    }
                    catch (Exception)
                    {

                        return new HttpResponseMessage(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Verifica se todos os campos estão preenchidos 
        /// e se os campos CPF e Email são válidos.
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

            if (!CpfValidade(this.Cpf) || !EmailValidate(this.Email))
            {
                return false;
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

        public bool EmailValidate(string email)
        {
            string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            if (!String.IsNullOrEmpty(email))
            {
                if (Regex.IsMatch(email, emailPattern))
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

        public bool CpfValidade(string cpf)
        {
            string cpfPattern = @"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})";

            if (!String.IsNullOrEmpty(cpf))
            {
                if (Regex.IsMatch(cpf, cpfPattern))
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
