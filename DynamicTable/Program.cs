using System;
using System.Windows.Forms;

namespace DynamicTable
{


    static class Program
    {
        
        //public const string path = "C:\\Users\\Fin\\Documents\\RR\\";
        //public const string path  = "C:\\Users\\METIIB\\Documents\\RR\\";
        //public const string path  = "Z:\\Downloads\\RR\\";
        public const string path = "C:\\Users\\mr_mi\\OneDrive\\Documents\\RR\\";
        //public const string path = "C:\\Users\\RRCATablet\\Documents\\RR\\";

        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI_Base());


        }

    }
}
