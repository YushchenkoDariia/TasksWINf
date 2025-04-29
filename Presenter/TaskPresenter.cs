using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;
using TaskManager.View;


namespace TaskManager.Presenter
{
    public class TaskPresenter
    {
        private readonly ITaskView _view;
        private readonly List<TaskItem> _taskItems;
        private int _editingTaskIndex = -1;

        // Конструктор презентера
        public TaskPresenter(ITaskView view)
        {
            _view = view;
            _taskItems = new List<TaskItem>();

            // Підписуємося на події представлення
            _view.AddTaskClicked += HandleAddTaskClicked;
            _view.DeleteTaskClicked += HandleDeleteTaskClicked;
            _view.EditTaskClicked += HandleEditTaskClicked;
            _view.TaskStatusChanged += HandleTaskStatusChanged;
        }

        // Обробник події додавання нового завдання
        private void HandleAddTaskClicked(object sender, EventArgs e)
        {
            string description = _view.TaskDescription.Trim();

            if (string.IsNullOrEmpty(description))
            {
                _view.ShowErrorMessage("Опис завдання не може бути порожнім.");
                return;
            }

            if (_editingTaskIndex >= 0)
            {
                // Редагування існуючого завдання
                _taskItems[_editingTaskIndex].Description = description;
                _view.ShowSuccessMessage("Завдання успішно відредаговано.");
                _editingTaskIndex = -1;
            }
            else
            {
                // Додавання нового завдання
                var taskItem = new TaskItem(description);

                // Підписуємося на подію зміни статусу завдання за допомогою лямбда-виразу
                taskItem.StatusChanged += (s, args) => UpdateTaskList();

                _taskItems.Add(taskItem);
                _view.ShowSuccessMessage("Завдання успішно додано.");
            }

            _view.ClearTaskDescription();
            UpdateTaskList();
        }

        // Обробник події видалення завдання
        private void HandleDeleteTaskClicked(object sender, EventArgs e)
        {
            int selectedIndex = _view.SelectedTaskIndex;

            if (selectedIndex >= 0 && selectedIndex < _taskItems.Count)
            {
                _taskItems.RemoveAt(selectedIndex);
                UpdateTaskList();
                _view.ShowSuccessMessage("Завдання успішно видалено.");
            }
            else
            {
                _view.ShowErrorMessage("Виберіть завдання для видалення.");
            }
        }

        // Обробник події редагування завдання
        private void HandleEditTaskClicked(object sender, EventArgs e)
        {
            int selectedIndex = _view.SelectedTaskIndex;

            if (selectedIndex >= 0 && selectedIndex < _taskItems.Count)
            {
                _editingTaskIndex = selectedIndex;
                _view.SetTaskDescription(_taskItems[selectedIndex].Description);
            }
            else
            {
                _view.ShowErrorMessage("Виберіть завдання для редагування.");
            }
        }

        // Обробник події зміни статусу завдання
        private void HandleTaskStatusChanged(object sender, TaskStatusChangedEventArgs e)
        {
            if (e.TaskIndex >= 0 && e.TaskIndex < _taskItems.Count)
            {
                _taskItems[e.TaskIndex].SetCompletionStatus(e.IsCompleted);
                // Оновлення списку вже викликається через підписку на подію StatusChanged
            }
        }

        // Метод для оновлення списку завдань у представленні
        private void UpdateTaskList()
        {
            _view.UpdateTaskList(_taskItems.ToList());
        }
    }
}