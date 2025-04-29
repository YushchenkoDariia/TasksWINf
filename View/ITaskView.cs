using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;
namespace TaskManager.View
{
    public interface ITaskView
    {
        // Властивість для отримання введеного опису завдання
        string TaskDescription { get; }

        // Властивість для отримання індексу вибраного завдання
        int SelectedTaskIndex { get; }

        // Подія, яка спрацьовує при натисканні кнопки "Додати завдання"
        event EventHandler AddTaskClicked;

        // Подія, яка спрацьовує при натисканні кнопки "Видалити завдання"
        event EventHandler DeleteTaskClicked;

        // Подія, яка спрацьовує при натисканні кнопки "Редагувати завдання"
        event EventHandler EditTaskClicked;

        // Подія, яка спрацьовує при зміні статусу завдання
        event EventHandler<TaskStatusChangedEventArgs> TaskStatusChanged;

        // Метод для оновлення списку завдань
        void UpdateTaskList(IEnumerable<TaskItem> tasks);

        // Метод для очищення поля введення
        void ClearTaskDescription();

        // Метод для заповнення поля введення для редагування
        void SetTaskDescription(string description);

        // Метод для відображення повідомлення про помилку
        void ShowErrorMessage(string message);

        // Метод для відображення повідомлення про успіх
        void ShowSuccessMessage(string message);
    }

    // Клас аргументів події зміни статусу завдання
    public class TaskStatusChangedEventArgs : EventArgs
    {
        public int TaskIndex { get; private set; }
        public bool IsCompleted { get; private set; }

        public TaskStatusChangedEventArgs(int taskIndex, bool isCompleted)
        {
            TaskIndex = taskIndex;
            IsCompleted = isCompleted;
        }
    }
}