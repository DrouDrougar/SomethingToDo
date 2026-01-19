
using SomethingToDoApp.Sqlite;


namespace SomethingToDoApp;

public partial class RandomTaskPage : ContentPage
{
    private readonly LocalDbService _db;
    public RandomTaskPage(LocalDbService db)
	{
		InitializeComponent();

        _db = db;
        LoadRandomTask();
    }

    private async void LoadRandomTask()
    {
        try
        {
            var allTasks = await _db.GetTasks();

            if (allTasks == null || !allTasks.Any())
            {
                NameLabel.Text = "No tasks found";
                DescriptionLabel.Text = "Please add some tasks first";
                return;
            }

            var random = new Random();
            var randomTask = allTasks[random.Next(allTasks.Count)];

            NameLabel.Text = randomTask.TaskName;
            DescriptionLabel.Text = randomTask.TaskDescription;
        }
        catch (Exception ex)
        {
            NameLabel.Text = "Error";
            DescriptionLabel.Text = $"Could not load task: {ex.Message}";
        }
    }
}