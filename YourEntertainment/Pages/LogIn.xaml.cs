using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YourEntertainment.Classes;
using YourEntertainment.DataBase;

namespace YourEntertainment.Pages
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        private double h;
        private double w;
        private string psize = "";
        private TextBox userName;
        private PasswordBox password;
        private Viewbox userNameV;
        private Viewbox passwordV;

        public LogIn()
        {
            InitializeComponent();
            initializeWindow();
        }

        private void initializeWindow()
        {
            // Measures
            h = MainWindow.window.myWindow.getHeight();
            w = MainWindow.window.myWindow.getWidth();

            // Log In Button
            initializeButton(logIn, "LogInButton", 0.08, 0.05, 0.67, 0.65);
            // UserName Button
            initializeButton(UserNameB, "UserName", 0.15, 0.05, 0.59, 0.32);
            // Password Button
            initializeButton(PasswordB, "Password", 0.15, 0.05, 0.59, 0.53);

            // Initialize TextBox
            userName = new TextBox();
            userNameV = new Viewbox();
            userName.VerticalContentAlignment = VerticalAlignment.Center;
            initializeTextBox(userNameV, userName, 0.15, 0.05, 0.59, 0.32);

            password = new PasswordBox();

            passwordV = new Viewbox();
            password.VerticalContentAlignment = VerticalAlignment.Center;
            // initializeTextBox(passwordV, password, 0.15, 0.05, 0.59, 0.53);

            passwordV.Child = password;
            password.FontSize = 20;
            passwordV.Width = w * 0.15;
            passwordV.Height = h * 0.05;
            password.Width = w * 0.15;
            password.Height = h * 0.05;
            LogInCanvas.Children.Add(passwordV);
            Canvas.SetTop(passwordV, h * 0.59);
            Canvas.SetLeft(passwordV, w * 0.53);
            password.IsEnabled = false;
            password.Visibility = Visibility.Hidden;
        }

        private void reset()
        {
            // Reset Button UserName
            UserNameB.Visibility = Visibility.Visible;
            UserNameB.IsEnabled = true;
            userName.IsEnabled = false;
            userName.Visibility = Visibility.Hidden;
            userName.Text = "";

            // Reset Button Password
            PasswordB.Visibility = Visibility.Visible;
            PasswordB.IsEnabled = true;
            password.IsEnabled = false;
            password.Visibility = Visibility.Hidden;
            password.Password = "";


        }

        private void initializeTextBox(Viewbox viewBox, TextBox textBox, double tbW, double tbH, double top, double left)
        {
            viewBox.Child = textBox;
            textBox.TextWrapping = TextWrapping.Wrap;
            textBox.FontSize = 20;
            viewBox.Width = w * tbW;
            viewBox.Height = h * tbH;
            textBox.Width = w * tbW;
            textBox.Height = h * tbH;
            LogInCanvas.Children.Add(viewBox);
            Canvas.SetTop(viewBox, h * top);
            Canvas.SetLeft(viewBox, w * left);
            textBox.IsEnabled = false;
            textBox.Visibility = Visibility.Hidden;
        }

        private void initializeButton(Button button, string name, double buttonW, double buttonH, double top, double left)
        {
            BitmapImage bitmap = new BitmapImage();
            Image img = new Image();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("/YourEntertainment;component/Theme/Buttons/LogIn/" + name + ".png", UriKind.Relative);
            bitmap.EndInit();
            img.Stretch = Stretch.Fill;
            img.Source = bitmap;
            button.Content = img;
            button.Background = new ImageBrush(bitmap);
            button.Width = w * buttonW;
            button.Height = h * buttonH;

            Canvas.SetTop(button, h * top);
            Canvas.SetLeft(button, w * left);
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var b = (Button)e.OriginalSource;

            switch (b.Name)
            {
                case "logIn":
                    {
                        if (userName.Text.Equals("") || password.Password.Equals("") || userName.Text.Any(Char.IsLetter))
                        {
                            MessageBox.Show("Dados Inválidos");
                            break;
                        }
                        checkLogin(userName.Text, password.Password);
                        reset();
                        break;
                    }
                case "UserNameB":
                    {
                        UserNameB.Visibility = Visibility.Hidden;
                        UserNameB.IsEnabled = false;
                        userName.IsEnabled = true;
                        userName.Visibility = Visibility.Visible;
                        break;
                    }
                case "PasswordB":
                    {
                        PasswordB.Visibility = Visibility.Hidden;
                        PasswordB.IsEnabled = false;
                        password.IsEnabled = true;
                        password.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }

        private void checkLogin(string user, string pwd)
        {
            Connection.Conn.Open();
            SqlCommand cmd = new SqlCommand("YourEntertainment.pr_VerifyLogin", Connection.Conn);
            cmd.CommandType = CommandType.StoredProcedure;

            int x;
            cmd.Parameters.AddWithValue("@id", Int32.Parse(user));
            cmd.Parameters.AddWithValue("@pass", pwd);

            var res = cmd.Parameters.Add("@res", SqlDbType.Bit);
            res.Direction = ParameterDirection.Output;
            var nome = cmd.Parameters.Add("@nome", SqlDbType.VarChar);
            nome.Direction = ParameterDirection.Output;
            nome.Size = 15;
            var idloja = cmd.Parameters.Add("@idloja", SqlDbType.Int);
            idloja.Direction = ParameterDirection.Output;
            var nomeloja = cmd.Parameters.Add("@nome_loja", SqlDbType.VarChar);
            nomeloja.Direction = ParameterDirection.Output;
            nomeloja.Size = 30;
            var mngbit = cmd.Parameters.Add("@manager", SqlDbType.Bit);
            mngbit.Direction = ParameterDirection.Output;


            cmd.ExecuteNonQuery();
            if (Convert.ToBoolean(res.Value))
            {
                MessageBox.Show("LogIn efectuado com sucesso!\nBem vindo " + nome.Value);
                MainWindow.idFunc = Int32.Parse(user);
                MainWindow.nomeFunc = nome.Value.ToString();
                MainWindow.idLoja = (int)idloja.Value;
                MainWindow.nomeLoja = nomeloja.Value.ToString();
                MainWindow.isManager = (bool)mngbit.Value;
                MainWindow.window.framename.Navigate(MainWindow.window.mainPage);
                MainWindow.window.mainPage.initializeUserData();
                MainWindow.window.venda.initializeUserData();
                MainWindow.window.stocks.initializeUserData();
                MainWindow.window.dados.initializeUserData();
                MainWindow.window.settings.initializeUserData();
                MainWindow.window.addStock.initializeUserData();
            }
            else
            {
                MessageBox.Show("LogIn falhou, por favor verifique os dados submetidos" + nome.Value);
            }
            Connection.Conn.Close();
        }
    }
}
