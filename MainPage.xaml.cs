using CommunityToolkit.Maui.Storage;
using SomethingToDoApp.Models;
using SomethingToDoApp.Sqlite;
using SomethingToDoApp.ViewModels;

namespace SomethingToDoApp
{
    public partial class MainPage : ContentPage
    {

        LocalDbService _db;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            _db = new LocalDbService();

            var tasks = _db.GetTasks();
        }

        private void YesButton_OnClicked(object? sender, EventArgs e)
        {
            Navigation.PushAsync(new RandomTaskPage(_db));

        }

        private void AddButton_OnClicked(object? sender, EventArgs e)
        {
            Navigation.PushAsync(new TaskAddPage(_db));

        }
    }

}

