using System;
using System.Linq;
using System.Windows.Forms;

namespace Game
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int applicationId;
            var isArgsParsable = int.TryParse(args[0], out applicationId);

            var availableIds = new int[] { 1, 2 };

            if(!isArgsParsable || !availableIds.Contains(applicationId))
            {
                throw new ArgumentException("Должен быть параметр 1 или 2");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(applicationId));
        }
    }
}
