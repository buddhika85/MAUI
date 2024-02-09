using MAUIClient.DataServices;
using System.Diagnostics;

namespace MAUIClient
{
    public partial class MainPage : ContentPage
    {
        private readonly IRestDataService _dataService;

        public MainPage(IRestDataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await _dataService.GetAllToDosAsync(); 
        }

        async void OnAddToDoClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("----> Add Button clicked");
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs  e)
        {
            Debug.WriteLine("----> Selected a To Do");
        }
    }

}
