using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Invoice_GenUI.Models
{
    public class NavigationService
    {
        private readonly StartUpWindow _startWindow;

        public void GoBack(Type windowType, object[] constructorArgs)
        {
            if(typeof(Window).IsAssignableFrom(windowType))
            {
                Window window = (Window)Activator.CreateInstance(windowType, constructorArgs);
                window.Show();
            }
            else
            {
                MessageBox.Show("No previous page");
            }
        }
    }
}
