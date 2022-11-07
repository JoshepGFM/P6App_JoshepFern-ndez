using ShoppingP6_JoshepFernandez.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ShoppingP6_JoshepFernandez.Views
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