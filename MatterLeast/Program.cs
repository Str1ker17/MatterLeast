using System.Windows.Forms;

namespace MattermostApp {
    class Program {
        static Form formMain = null;

        public static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            formMain = new FormMain();

            Application.Run(formMain);
        }
    }
}
