using System;
using System.Collections.Generic;

namespace LabClick.ViewModels
{
    public class NewPatientViewModel
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
