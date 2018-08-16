namespace LabClick.ViewModels
{
    public class EnderecoViewModel
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }

        /// <summary>
        /// Verifica se o objeto possui os campos Logradouro,
        /// Bairro, Localidade e UF diferentes de null.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (Logradouro != null && Bairro != null && Localidade != null && Uf != null)
            {
                return true;
            }

            return false;
        }
    }
}
