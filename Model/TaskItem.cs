using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskManager.Model
{
    // Делегат для події зміни статусу завдання
    public delegate void StatusChangedEventHandler(object sender, EventArgs e);

    public class TaskItem
    {
        private string _description;
        private bool _isCompleted;

        // Подія, яка спрацьовує при зміні статусу завдання
        public event StatusChangedEventHandler StatusChanged;

        // Властивість для опису завдання
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        // Властивість для статусу завдання
        public bool IsCompleted
        {
            get { return _isCompleted; }
            private set { _isCompleted = value; }
        }

        // Конструктор
        public TaskItem(string description, bool isCompleted = false)
        {
            _description = description;
            _isCompleted = isCompleted;
        }

        // Метод для зміни статусу завдання
        public void SetCompletionStatus(bool isCompleted)
        {
            if (_isCompleted != isCompleted)
            {
                _isCompleted = isCompleted;
                // Викликаємо подію, якщо вона має підписників
                StatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // Перевизначення методу ToString для зручного відображення
        public override string ToString()
        {
            return $"{Description} - {(IsCompleted ? "Виконано" : "Не виконано")}";
        }
    }
}