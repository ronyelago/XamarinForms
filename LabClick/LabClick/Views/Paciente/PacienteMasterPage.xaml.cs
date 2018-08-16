﻿using LabClick.Views.Teste;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteMasterPage : MasterDetailPage
	{

        public List<MasterPageItem> MenuList { get; set; }
        public Domain.Entities.Paciente Paciente { get; set; }

        public PacienteMasterPage(Domain.Entities.Paciente paciente)
		{
			InitializeComponent ();

            this.Paciente = paciente;
            // Instancia e exibe a página de detalhes
            this.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(PacienteDetails), Paciente));

            // Cria o menu lateral (sandwich menu)
            MenuList = new List<MasterPageItem>
            {
                new MasterPageItem() { Title = "Exames", Icon = "menuexames.png", TargetType = typeof(ListaTeste) },
                new MasterPageItem() { Title = "Novo Exame", Icon = "menunovoexame.png", TargetType = typeof(DigitalizarTeste) }
            };

            masterPageList.ItemsSource = MenuList;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            // Cria dinamicamente uma instância de uma page de acordo com o 
            // ítem da lista que foi selecionado
            if (page.Name == "ListaTeste")
            {
                this.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ListaTeste), Paciente));
            }
            else
            {
                this.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(DigitalizarTeste), Paciente));
            }

            // Oculta o menu lateral após exibir a page escolhida
            this.IsPresented = false;
        }
    }
}