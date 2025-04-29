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
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var view = new TaskView();
          
            var presenter = new TaskPresenter(view);

            Application.Run(view);
        }
    }
}