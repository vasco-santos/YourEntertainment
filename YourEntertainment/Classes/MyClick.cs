using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourEntertainment.Classes
{
    public class MyClick
    {
        public void buttonOnClickLeftBar(string p)
        {
            switch (p)
            {
                case "sale":
                    MainWindow.window.framename.Navigate(MainWindow.window.venda);
                    break;
                case "stocks":
                    MainWindow.window.framename.Navigate(MainWindow.window.stocks);
                    break;
                case "dados":
                    MainWindow.window.framename.Navigate(MainWindow.window.dados);
                    break;
                case "logOut":
                    MainWindow.window.framename.Navigate(MainWindow.window.logIn);
                    break;
                case "settings":
                    MainWindow.window.framename.Navigate(MainWindow.window.settings);
                    break;
            }
        }

    }
}
