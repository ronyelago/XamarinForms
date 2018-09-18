namespace LabClick.ViewModels
{
    public class TesteImagemViewModel
    {
        private byte[] imagem;

        public int Id { get; set; }
        public byte[] Imagem
        {
            get { return this.imagem; }
            set
            {
                if (value != null)
                {
                    this.imagem = value;
                }
            }
        }
    }
}
