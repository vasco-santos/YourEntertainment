using System;
using System.Windows;
using YourEntertainment.Pages;
using YourEntertainment.Classes;

namespace YourEntertainment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow window;
        public MyWindow myWindow;
        public MyClick myClick;

        private String active;
        private static DateTime data;
        public double height;
        public double width;

        // Initialize Frames
        public Stocks stocks;
        public Dados dados;
        public LogIn logIn;
        public MainPage mainPage;
        public Settings settings;
        public Venda venda;
        public AddStock addStock;

        // static values
        public static string nomeFunc;
        public static string nomeLoja;
        public static int idFunc;
        public static int idLoja;
        public static bool isManager;

        public MainWindow()
        {
            InitializeComponent();

            // Window Measures
            window = this;
            height = this.Height;
            width = this.Width;
            myWindow = new MyWindow(height, width);
            myClick = new MyClick();

            // Initialize Frames
            stocks = new Stocks();
            addStock = new AddStock();
            dados = new Dados();
            logIn = new LogIn();
            mainPage = new MainPage();
            settings = new Settings();
            venda = new Venda();

            framename.Navigate(logIn);
        }

        public String Frame { get { return active; } set { active = value; } }
    }
}
