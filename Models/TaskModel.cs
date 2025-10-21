using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomethingToDoApp.Models
{
    [Table("taskModel")]
    public class TaskModel
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("task_name")]
        public string? TaskName { get; set; }

        [Column("task_description")]
        public string? TaskDescription { get; set; }

    }
}
