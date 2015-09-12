using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YourEntertainment.DataBase;

namespace YourEntertainment.Pages
{
    /// <summary>
    /// Interaction logic for Gestão.xaml
    /// </summary>
    public partial class Dados : Page
    {
        private double h;
        private double w;
        private string currentOption;

        public Dados()
        {
            InitializeComponent();
            initializeWindow();
        }

        private void initializeWindow()
        {
            // Measures
            h = MainWindow.window.myWindow.getHeight();
            w = MainWindow.window.myWindow.getWidth();

            // Initiliaze Left and Top Bar Buttons
            initializeLeftAndTopBar();
            //Initialize BlueBar
            BlueBarCanvas.Width = w*0.1;
            BlueBarCanvas.Height = h*0.62;
            Canvas.SetTop(BlueBarCanvas, h*0.22);
            Canvas.SetLeft(BlueBarCanvas, w*0.22);
            initializeBlueBar();

            //initialize Info
            InfoCanvas.Width = w*0.55;
            InfoCanvas.Height = h*0.62;
            Canvas.SetTop(InfoCanvas, h*0.22);
            Canvas.SetLeft(InfoCanvas, w*0.35);
            initializeInfo();
        }

        public void initializeLeftAndTopBar()
        {
            MainWindow.window.myWindow.initializeLeftBarButton(sale, "saleButton", 0.08, 0.15, 0.22, 0.112);
            MainWindow.window.myWindow.initializeLeftBarButton(stocks, "AddStockButton", 0.08, 0.15, 0.42, 0.112);
            MainWindow.window.myWindow.initializeLeftBarButton(dados, "StatisticsButtonPressed", 0.08, 0.15, 0.62, 0.112);

            MainWindow.window.myWindow.initializeTopBar(logOut, "logOut", 0.035, 0.07, 0.1, 0.85);
            MainWindow.window.myWindow.initializeTopBar(settings, "settings", 0.035, 0.075, 0.1, 0.79);
        }
        public void initializeUserData()
        {
            lojaData.Text = MainWindow.nomeLoja;
            lojaData.Foreground = Brushes.DarkSlateGray;
            funcData.Text = "Funcionário: " + MainWindow.nomeFunc;
            funcData.Foreground = Brushes.DarkSlateGray;


            lojaView.MinWidth = w * 0.06;
            lojaView.Height = h * 0.03;
            funcView.MinWidth = w * 0.06;
            funcView.Height = h * 0.03;
            Canvas.SetTop(lojaView, h * 0.128);
            Canvas.SetTop(funcView, h * 0.128);
            Canvas.SetLeft(lojaView, w * 0.4);
            Canvas.SetLeft(funcView, w * 0.5);
        }

        private void initializeBlueBar()
        {
            double padding = BlueBarCanvas.Height*0.1;
            double lastheight = 0;
            foreach (ToggleButton b in BlueBarCanvas.Children)
            {
                b.Height = BlueBarCanvas.Height*0.05;
                b.Width = BlueBarCanvas.Width;
                Canvas.SetTop(b, lastheight);
                lastheight = lastheight + padding;
            }
        }

        private void initializeInfo()
        {
            StatsGrid.Width = w*0.35;
            StatsGrid.Height = h*0.4;

            Canvas.SetTop(StatsGrid, InfoCanvas.Height*0.1);
            Canvas.SetLeft(StatsGrid, InfoCanvas.Width*0.2);
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var b = (Button) e.OriginalSource;
            MainWindow.window.myClick.buttonOnClickLeftBar(b.Name);
        }

        private void FilterActivated(object sender, RoutedEventArgs e)
        {
            InfoCanvas.Visibility = Visibility.Visible;
            var b = (ToggleButton) e.OriginalSource;
            String command;
            switch (b.Name)
            {
                case "Diario":
                    currentOption = "Diario";
                    command = "exec YourEntertainment.pr_DailySales "+MainWindow.idLoja;
                    StatsGrid.FontSize = 15;
                    break;
                case "MaisVendidos":
                    currentOption = "MaisVendidos";
                    command = "exec YourEntertainment.pr_MostSold " + MainWindow.idLoja;
                    StatsGrid.FontSize = 20;
                    break;
                case "VendasPorFunc":
                    currentOption = "VendasPorFunc";
                    command = "exec YourEntertainment.pr_SalesByEmployee " + MainWindow.idLoja;
                    StatsGrid.FontSize = 20;
                    break;
                case "VendasPorLoja":
                    currentOption = "VendasPorLoja";
                    command = "exec YourEntertainment.pr_SalesByStore";
                    StatsGrid.FontSize = 20;
                    break;
                default:
                    command = "exec YourEntertainment.pr_DailySales "+MainWindow.idLoja;
                    StatsGrid.FontSize = 20;
                    break;
            }

            foreach (ToggleButton f in BlueBarCanvas.Children)
            {
                if (f.Name.Equals(currentOption))
                {
                    f.IsChecked = true;
                }
                else
                {
                    f.IsChecked = false;
                }
            }
            Connection.Conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(command, Connection.Conn);
            DataTable dt = new DataTable("Estatisticas");
            sda.Fill(dt);
            StatsGrid.ItemsSource = dt.DefaultView;
            Connection.Conn.Close();
        }

      
    }
}
