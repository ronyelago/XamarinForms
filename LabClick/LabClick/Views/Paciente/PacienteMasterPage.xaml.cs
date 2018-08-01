using LabClick.Views.Teste;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteMasterPage : ContentPage
	{

        public List<MasterPageItem> MenuList { get; set; }

        public PacienteMasterPage ()
		{
			InitializeComponent ();

            MenuList = new List<MasterPageItem>
            {
                new MasterPageItem() { Title = "Exames", Icon = "menuexames.png", TargetType = typeof(ListaTeste) },
                new MasterPageItem() { Title = "Novo Exame", Icon = "menunovoexame.png", TargetType = typeof(NovoTeste) }
            };

            masterPageList.ItemsSource = MenuList;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            App.MasterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            App.MasterPage.IsPresented = false;
        }
    }
}