
using SomethingToDoApp.Models;
using SQLite;

namespace SomethingToDoApp.Sqlite
{
    public class LocalDbService
    {
        private const string DB_Name = "SomethingToDoTasks.db3";
        private readonly SQLiteAsyncConnection _connection;
        private bool initialized = false;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_Name));
            _connection.CreateTableAsync<TaskModel>();
        }

        async Task Init()
        {
            if (!initialized) { return; }

            await _connection.CreateTableAsync<TaskModel>();
            var anyTasks = await _connection.Table<TaskModel>().CountAsync() > 0;

            if (!anyTasks)
            {
                var demoTask = new TaskModel
                {
                    TaskName = "Demo Task",
                    TaskDescription = "This is a demo task description"
                };
                await _connection.InsertAsync(demoTask);
            }

            initialized = true;
        }
        public async Task<List<TaskModel>> GetTasks()
        {
            await Init();
            return await _connection.Table<TaskModel>().ToListAsync();
        }

        public async Task<TaskModel> GetRandomTask()
        {
            await Init();
            var allTasks = await GetTasks();
            if (!allTasks.Any()) return null;

            var random = new Random();
            return allTasks[random.Next(allTasks.Count)];
        }
        public async Task<TaskModel> GetTaskById(int id)
        {
            await Init();
            return await _connection.Table<TaskModel>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task CreateTask(TaskModel task)
        {
            await Init();
            await _connection.InsertAsync(task);
        }
        public async Task UpdateTask(TaskModel task)
        {
            await Init();
            await _connection.UpdateAsync(task);
        }
        public async Task DeleteTask(TaskModel task)
        {
            await Init();
            await _connection.DeleteAsync(task);
        }

    }
}
