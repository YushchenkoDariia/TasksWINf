using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManager.Model;
using TaskManager.View;

namespace TaskManager
{
    public partial class TaskView : Form, ITaskView
    {
        // Реалізація подій
        public event EventHandler AddTaskClicked;
        public event EventHandler DeleteTaskClicked;
        public event EventHandler EditTaskClicked;
        public event EventHandler<TaskStatusChangedEventArgs> TaskStatusChanged;

        // Реалізація властивостей
        public string TaskDescription => txtTaskDescription.Text;
        public int SelectedTaskIndex => checkedListTasks.SelectedIndex;

        // Конструктор форми
        public TaskView()
        {
            InitializeComponent();

            // Підписка на події кнопок
            btnAddTask.Click += (sender, e) => AddTaskClicked?.Invoke(this, EventArgs.Empty);
            btnDeleteTask.Click += (sender, e) => DeleteTaskClicked?.Invoke(this, EventArgs.Empty);
            btnEditTask.Click += (sender, e) => EditTaskClicked?.Invoke(this, EventArgs.Empty);

            // Початковий стан кнопок редагування і видалення - неактивні
            btnDeleteTask.Enabled = false;
            btnEditTask.Enabled = false;

            // Підписка на подію вибору елемента списку
            checkedListTasks.SelectedIndexChanged += CheckedListTasks_SelectedIndexChanged;
        }

        // Обробник події вибору елемента в списку
        private void CheckedListTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isItemSelected = checkedListTasks.SelectedIndex != -1;
            btnDeleteTask.Enabled = isItemSelected;
            btnEditTask.Enabled = isItemSelected;
        }

        // Метод для оновлення списку завдань
        public void UpdateTaskList(IEnumerable<TaskItem> tasks)
        {
            checkedListTasks.Items.Clear();
            foreach (var task in tasks)
            {
                int index = checkedListTasks.Items.Add(task);
                checkedListTasks.SetItemChecked(index, task.IsCompleted);
            }
        }

        // Метод для очищення поля введення
        public void ClearTaskDescription()
        {
            txtTaskDescription.Clear();
            txtTaskDescription.Focus();
        }

        // Метод для заповнення поля введення для редагування
        public void SetTaskDescription(string description)
        {
            txtTaskDescription.Text = description;
            txtTaskDescription.Focus();
            txtTaskDescription.SelectAll();
        }

        // Метод для відображення повідомлення про помилку
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Метод для відображення повідомлення про успіх
        public void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Обробник події зміни стану CheckBox у списку завдань
        private void checkedListTasks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Відкладаємо виклик події, щоб дати CheckedListBox завершити оновлення свого стану
            BeginInvoke(new Action(() => {
                bool isChecked = e.NewValue == CheckState.Checked;
                TaskStatusChanged?.Invoke(this, new TaskStatusChangedEventArgs(e.Index, isChecked));
            }));
        }

        // Метод для ініціалізації дизайну форми
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskView));
            this.txtTaskDescription = new System.Windows.Forms.TextBox();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.checkedListTasks = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.btnEditTask = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTaskDescription
            // 
            this.txtTaskDescription.Location = new System.Drawing.Point(16, 45);
            this.txtTaskDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTaskDescription.Name = "txtTaskDescription";
            this.txtTaskDescription.Size = new System.Drawing.Size(505, 22);
            this.txtTaskDescription.TabIndex = 0;
            // 
            // btnAddTask
            // 
            this.btnAddTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAddTask.Location = new System.Drawing.Point(531, 45);
            this.btnAddTask.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(120, 28);
            this.btnAddTask.TabIndex = 1;
            this.btnAddTask.Text = "Додати";
            this.btnAddTask.UseVisualStyleBackColor = false;
            // 
            // checkedListTasks
            // 
            this.checkedListTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListTasks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.checkedListTasks.FormattingEnabled = true;
            this.checkedListTasks.Location = new System.Drawing.Point(16, 100);
            this.checkedListTasks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkedListTasks.Name = "checkedListTasks";
            this.checkedListTasks.Size = new System.Drawing.Size(505, 356);
            this.checkedListTasks.TabIndex = 2;
            this.checkedListTasks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListTasks_ItemCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.MistyRose;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Опис завдання:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.MistyRose;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(16, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Список завдань:";
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDeleteTask.Enabled = false;
            this.btnDeleteTask.Location = new System.Drawing.Point(531, 100);
            this.btnDeleteTask.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(120, 28);
            this.btnDeleteTask.TabIndex = 5;
            this.btnDeleteTask.Text = "Видалити";
            this.btnDeleteTask.UseVisualStyleBackColor = false;
            // 
            // btnEditTask
            // 
            this.btnEditTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnEditTask.Enabled = false;
            this.btnEditTask.Location = new System.Drawing.Point(531, 135);
            this.btnEditTask.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditTask.Name = "btnEditTask";
            this.btnEditTask.Size = new System.Drawing.Size(120, 28);
            this.btnEditTask.TabIndex = 6;
            this.btnEditTask.Text = "Редагувати";
            this.btnEditTask.UseVisualStyleBackColor = false;
            // 
            // TaskView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TasksWINf.Properties.Resources.pink_7761356_640;
            this.ClientSize = new System.Drawing.Size(667, 492);
            this.Controls.Add(this.btnEditTask);
            this.Controls.Add(this.btnDeleteTask);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListTasks);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.txtTaskDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TaskView";
            this.Text = "Менеджер завдань";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox txtTaskDescription;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.CheckedListBox checkedListTasks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.Button btnEditTask;
    }
}