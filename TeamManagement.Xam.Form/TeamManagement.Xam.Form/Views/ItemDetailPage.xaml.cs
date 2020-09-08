using System.ComponentModel;
using Xamarin.Forms;
using TeamManagement.Xam.Form.ViewModels;

namespace TeamManagement.Xam.Form.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}