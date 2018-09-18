using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LabClick.ViewModels
{
    public class DigitalizarTesteViewModel : INotifyPropertyChanged
    {
        private int pacienteId;
        private int clinicaId;
        private string status;
        private int exameId;
        private byte[] imagem;
        private string code;
        private DateTime dataCadastro;
        private bool scanned;
        private string imagemShow;

        public string ImageShow
        {
            get { return this.ImageShow; }
            set { this.imagemShow = value; }
        }
        public int PacienteId
        {
            get { return this.pacienteId; }
            set { this.pacienteId = value; }
        }
        public int ClinicaId
        {
            get { return this.clinicaId; }
            set { this.clinicaId = value; }
        }
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        public int ExameId
        {
            get { return this.exameId; }
            set { this.exameId = value; }
        }
        public TesteImagemViewModel TesteImagemViewModel { get; set; }
        public string Code
        {
            get { return this.code; }
            set
            {
                this.code = value;
                OnPropertyChanged();
            }
        }
        public DateTime DataCadastro
        {
            get { return this.dataCadastro; }
            set { this.dataCadastro = value; }
        }
        public bool Scanned
        {
            get { return this.scanned; }
            set { this.scanned = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool IsValid()
        {
            if (this.imagem == null || string.IsNullOrEmpty(this.code) && Scanned)
            {
                return false;
            }

            return true;
        }
    }
}
