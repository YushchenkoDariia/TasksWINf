using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManager.Presenter;

namespace TaskManager
{
    static class Program
    {
        /// <summary>
        /// Головна точка входу для програми.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Створюємо представлення
            var view = new TaskView();

            // Створюємо презентер і передаємо йому представлення
            var presenter = new TaskPresenter(view);

            // Запускаємо додаток
            Application.Run(view);
        }
    }
}