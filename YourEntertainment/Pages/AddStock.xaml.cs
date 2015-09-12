using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddStockAudiovisual.xaml
    /// </summary>
    public partial class AddStock : Page
    {
        # region Fields

        private double h;
        private double w;

        private bool flag;

        private TextBox answerContacto;
        private TextBox answerLocal;
        private TextBox answerEditora;
        private TextBox answerGenero;

        private TextBox answerDia;
        private TextBox answerMes;
        private TextBox answerAno;

        private TextBox answerDiaLancamento;
        private TextBox answerMesLancamento;
        private TextBox answerAnoLancamento;

        private TextBox answerForm1;
        private TextBox answerForm2;
        private TextBox answerForm3;

        private TextBlock ans1Produto;
        private TextBlock ans2Produto;
        private TextBlock ans3Produto;

        private Viewbox viewAns1Produto;
        private Viewbox viewAns2Produto;
        private Viewbox viewAns3Produto;

        private string activeType;

        # endregion
        public AddStock()
        {
            InitializeComponent();
            flag = false;
            activeType = "";
            initializeWindow();

        }
        # region Interface

        private void initializeWindow()
        {
            // Measures
            h = MainWindow.window.myWindow.getHeight();
            w = MainWindow.window.myWindow.getWidth();

            // Add AudioVisual Panel
            initializeAddAudiovisual();

            // Add Bilheteira Panel
            initializeAddBilheteira();

            // Add Product Panel
            initializeAddProduct();

            // Initiliaze Left and Top Bar Buttons
            initializeLeftAndTopBar();

            //AddAudiovisualPanel.Visibility = Visibility.Hidden;
            AddBilheteiraPanel.Visibility = Visibility.Hidden;
        }

        private void initializeAddProduct()
        {
            // Add Product Form
            AddProdutoPanel.Height = h * 0.2;
            AddProdutoPanel.Width = w * 0.35;
            Canvas.SetTop(AddProdutoPanel, h * 0.5);
            Canvas.SetLeft(AddProdutoPanel, w * 0.43);

            #region Form1

            // Product Form 1
            viewAns1Produto = new Viewbox();
            setSizePositionViewBox(viewAns1Produto, 0.03, 0.14, 0.03, 0.02);
            AddProdutoPanel.Children.Add(viewAns1Produto);

            ans1Produto = new TextBlock();
            ans1Produto.TextAlignment = TextAlignment.Justify;
            ans1Produto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewAns1Produto.Child = ans1Produto;

            answerForm1 = new TextBox();
            answerForm1.FontSize = 18;
            answerForm1.BorderBrush = Brushes.Red;
            answerForm1.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerForm1, 0.03, 0.14, 0.03, 0.16);
            AddProdutoPanel.Children.Add(answerForm1);

            answerForm1.TextChanged += answerForm1_TextChanged;

            #endregion

            #region Form2

            // Product Form 2
            viewAns2Produto = new Viewbox();
            setSizePositionViewBox(viewAns2Produto, 0.03, 0.045, 0.08, 0.05);
            AddProdutoPanel.Children.Add(viewAns2Produto);

            ans2Produto = new TextBlock();
            ans2Produto.TextAlignment = TextAlignment.Justify;
            ans2Produto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewAns2Produto.Child = ans2Produto;

            answerForm2 = new TextBox();
            answerForm2.FontSize = 18;
            answerForm2.BorderBrush = Brushes.Red;
            answerForm2.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerForm2, 0.03, 0.14, 0.08, 0.16);
            AddProdutoPanel.Children.Add(answerForm2);

            answerForm2.TextChanged += answerForm2_TextChanged;

            #endregion

            #region Form3

            // Product Form 3
            viewAns3Produto = new Viewbox();
            setSizePositionViewBox(viewAns3Produto, 0.03, 0.065, 0.13, 0.04);
            AddProdutoPanel.Children.Add(viewAns3Produto);

            ans3Produto = new TextBlock();
            ans3Produto.TextAlignment = TextAlignment.Justify;
            ans3Produto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewAns3Produto.Child = ans3Produto;

            answerForm3 = new TextBox();
            answerForm3.FontSize = 18;
            answerForm3.BorderBrush = Brushes.Red;
            answerForm3.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerForm3, 0.03, 0.14, 0.13, 0.16);
            AddProdutoPanel.Children.Add(answerForm3);

            answerForm3.TextChanged += answerForm3_TextChanged;

            #endregion

            # region AddButton

            Button addProduct = new Button();
            addProduct.Width = w * 0.06;
            addProduct.Height = h * 0.04;
            Canvas.SetTop(addProduct, h * 0.29);
            Canvas.SetLeft(addProduct, w * 0.27);

            BitmapImage bitmap = new BitmapImage();
            Image img = new Image();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("/YourEntertainment;component/Theme/Buttons/Forms/Adicionar.png", UriKind.Relative);
            bitmap.EndInit();
            img.Stretch = Stretch.Fill;
            img.Source = bitmap;
            addProduct.Content = img;
            addProduct.Background = new ImageBrush(bitmap);

            AddProdutoPanel.Children.Add(addProduct);
            addProduct.Click += addProduct_Click;

            #endregion
        }

        private void initializeAddBilheteira()
        {
            // Add Product Form
            AddBilheteiraPanel.Height = h * 0.25;
            AddBilheteiraPanel.Width = w * 0.35;
            Canvas.SetTop(AddBilheteiraPanel, h * 0.3);
            Canvas.SetLeft(AddBilheteiraPanel, w * 0.43);

            #region Form1

            // Ticket Date
            Viewbox viewDataProduto = new Viewbox();
            setSizePositionViewBox(viewDataProduto, 0.03, 0.1, 0.03, 0.005);
            AddBilheteiraPanel.Children.Add(viewDataProduto);

            TextBlock dataProduto = new TextBlock();
            dataProduto.Text = "Data:";
            dataProduto.TextAlignment = TextAlignment.Justify;
            dataProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewDataProduto.Child = dataProduto;

            answerDia = new TextBox();
            answerDia.FontSize = 18;
            answerDia.BorderBrush = Brushes.Red;
            answerDia.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerDia, 0.03, 0.03, 0.03, 0.16);
            AddBilheteiraPanel.Children.Add(answerDia);

            answerDia.TextChanged += answerDia_TextChanged;

            answerMes = new TextBox();
            answerMes.FontSize = 18;
            answerMes.BorderBrush = Brushes.Red;
            answerMes.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerMes, 0.03, 0.03, 0.03, 0.2);
            AddBilheteiraPanel.Children.Add(answerMes);

            answerMes.TextChanged += answerMes_TextChanged;

            answerAno = new TextBox();
            answerAno.FontSize = 18;
            answerAno.BorderBrush = Brushes.Red;
            answerAno.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerAno, 0.03, 0.06, 0.03, 0.24);
            AddBilheteiraPanel.Children.Add(answerAno);

            answerAno.TextChanged += answerAno_TextChanged;

            #endregion

            #region Form2

            // Ticket Event Location
            Viewbox viewLocalProduto = new Viewbox();
            setSizePositionViewBox(viewLocalProduto, 0.03, 0.045, 0.08, 0.035);
            AddBilheteiraPanel.Children.Add(viewLocalProduto);

            TextBlock localProduto = new TextBlock();
            localProduto.Text = "Local:";
            localProduto.TextAlignment = TextAlignment.Justify;
            localProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewLocalProduto.Child = localProduto;

            answerLocal = new TextBox();
            answerLocal.FontSize = 18;
            answerLocal.BorderBrush = Brushes.Red;
            answerLocal.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerLocal, 0.03, 0.14, 0.08, 0.16);
            AddBilheteiraPanel.Children.Add(answerLocal);

            answerLocal.TextChanged += answerLocal_TextChanged;

            #endregion

            #region Form3

            // Ticket Event Contact
            Viewbox viewContactoProduto = new Viewbox();
            setSizePositionViewBox(viewContactoProduto, 0.03, 0.065, 0.13, 0.035);
            AddBilheteiraPanel.Children.Add(viewContactoProduto);

            TextBlock contactoProduto = new TextBlock();
            contactoProduto.Text = "Contacto:";
            contactoProduto.TextAlignment = TextAlignment.Justify;
            contactoProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewContactoProduto.Child = contactoProduto;

            answerContacto = new TextBox();
            answerContacto.FontSize = 18;
            answerContacto.BorderBrush = Brushes.Red;
            answerContacto.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerContacto, 0.03, 0.14, 0.13, 0.16);
            AddBilheteiraPanel.Children.Add(answerContacto);

            answerContacto.TextChanged += answerContacto_TextChanged;

            #endregion

            # region AddButton

            Button addProduct = new Button();
            addProduct.Width = w * 0.06;
            addProduct.Height = h * 0.04;
            Canvas.SetTop(addProduct, h * 0.18);
            Canvas.SetLeft(addProduct, w * 0.27);

            BitmapImage bitmap = new BitmapImage();
            Image img = new Image();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("/YourEntertainment;component/Theme/Buttons/Forms/Adicionar.png", UriKind.Relative);
            bitmap.EndInit();
            img.Stretch = Stretch.Fill;
            img.Source = bitmap;
            addProduct.Content = img;
            addProduct.Background = new ImageBrush(bitmap);

            AddBilheteiraPanel.Children.Add(addProduct);
            addProduct.Click += addPBilheteira_Click;

            #endregion
        }

        private void initializeAddAudiovisual()
        {
            // Add Product Form
            AddAudiovisualPanel.Height = h * 0.2;
            AddAudiovisualPanel.Width = w * 0.35;
            Canvas.SetTop(AddAudiovisualPanel, h * 0.3);
            Canvas.SetLeft(AddAudiovisualPanel, w * 0.43);

            #region Form1

            // Product Publisher
            Viewbox viewEditoraProduto = new Viewbox();
            setSizePositionViewBox(viewEditoraProduto, 0.03, 0.1, 0.03, 0.002);
            AddAudiovisualPanel.Children.Add(viewEditoraProduto);

            TextBlock editoraProduto = new TextBlock();
            editoraProduto.Text = "Editora:";
            editoraProduto.TextAlignment = TextAlignment.Justify;
            editoraProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewEditoraProduto.Child = editoraProduto;

            answerEditora = new TextBox();
            answerEditora.FontSize = 18;
            answerEditora.BorderBrush = Brushes.Red;
            answerEditora.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerEditora, 0.03, 0.14, 0.03, 0.16);
            AddAudiovisualPanel.Children.Add(answerEditora);

            answerEditora.TextChanged += answerEditora_TextChanged;

            #endregion

            #region Form2

            // Product Genre
            Viewbox viewGeneroProduto = new Viewbox();
            setSizePositionViewBox(viewGeneroProduto, 0.03, 0.045, 0.08, 0.0303);
            AddAudiovisualPanel.Children.Add(viewGeneroProduto);

            TextBlock generoProduto = new TextBlock();
            generoProduto.Text = "Género:";
            generoProduto.TextAlignment = TextAlignment.Justify;
            generoProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewGeneroProduto.Child = generoProduto;

            answerGenero = new TextBox();
            answerGenero.FontSize = 18;
            answerGenero.BorderBrush = Brushes.Red;
            answerGenero.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerGenero, 0.03, 0.14, 0.08, 0.16);
            AddAudiovisualPanel.Children.Add(answerGenero);

            answerGenero.TextChanged += answerGenero_TextChanged;

            #endregion

            #region Form3

            // Product Date
            Viewbox viewDataProduto = new Viewbox();
            setSizePositionViewBox(viewDataProduto, 0.03, 0.12, 0.13, 0.03);
            AddAudiovisualPanel.Children.Add(viewDataProduto);

            TextBlock dataProduto = new TextBlock();
            dataProduto.Text = "Data de Lançamento:";
            dataProduto.TextAlignment = TextAlignment.Justify;
            dataProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewDataProduto.Child = dataProduto;

            answerDiaLancamento = new TextBox();
            answerDiaLancamento.FontSize = 18;
            answerDiaLancamento.BorderBrush = Brushes.Red;
            answerDiaLancamento.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerDiaLancamento, 0.03, 0.03, 0.13, 0.16);
            AddAudiovisualPanel.Children.Add(answerDiaLancamento);

            answerDiaLancamento.TextChanged += answerDiaLancamento_TextChanged;

            answerMesLancamento = new TextBox();
            answerMesLancamento.FontSize = 18;
            answerMesLancamento.BorderBrush = Brushes.Red;
            answerMesLancamento.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerMesLancamento, 0.03, 0.03, 0.13, 0.2);
            AddAudiovisualPanel.Children.Add(answerMesLancamento);

            answerMesLancamento.TextChanged += answerMesLancamento_TextChanged;

            answerAnoLancamento = new TextBox();
            answerAnoLancamento.FontSize = 18;
            answerAnoLancamento.BorderBrush = Brushes.Red;
            answerAnoLancamento.BorderThickness = new Thickness(w * 0.001);
            setSizePositionTextBox(answerAnoLancamento, 0.03, 0.06, 0.13, 0.24);
            AddAudiovisualPanel.Children.Add(answerAnoLancamento);

            answerAnoLancamento.TextChanged += answerAnoLancamento_TextChanged;

            #endregion
        }

        public void initializeLeftAndTopBar()
        {

            MainWindow.window.myWindow.initializeLeftBarButton(sale, "saleButton", 0.08, 0.15, 0.22, 0.112);
            MainWindow.window.myWindow.initializeLeftBarButton(stocks, "AddStockButtonPressed", 0.08, 0.15, 0.42, 0.112);
            MainWindow.window.myWindow.initializeLeftBarButton(dados, "StatisticsButton", 0.08, 0.15, 0.62, 0.112);

            MainWindow.window.myWindow.initializeTopBar(logOut, "logOut", 0.035, 0.07, 0.1, 0.85);
            MainWindow.window.myWindow.initializeTopBar(settings, "settings", 0.035, 0.075, 0.1, 0.79);

            //Initialize Left Blue Bar 1
            BlueBarCanvas1.Width = w * 0.14;
            BlueBarCanvas1.Height = h * 0.6;
            Canvas.SetTop(BlueBarCanvas1, h * 0.22);
            Canvas.SetLeft(BlueBarCanvas1, w * 0.21);

            initializeBlueBar(BlueBarCanvas1);
            addProduto.IsChecked = true;
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
        private void initializeBlueBar(Canvas cv)
        {
            double padding = cv.Height * 0.1;
            double lastheight = 0;
            foreach (ToggleButton b in cv.Children)
            {
                b.Height = cv.Height * 0.05;
                b.Width = cv.Width;
                Canvas.SetTop(b, lastheight);
                lastheight = lastheight + padding;
            }
        }

        public void setSizePositionTextBox(TextBox element, double height, double width, double top, double left)
        {
            element.Height = h * height;
            element.Width = w * width;
            Canvas.SetTop(element, h * top);
            Canvas.SetLeft(element, w * left);
        }

        public void setSizePositionViewBox(Viewbox element, double height, double width, double top, double left)
        {
            element.Height = h * height;
            element.Width = w * width;
            Canvas.SetTop(element, h * top);
            Canvas.SetLeft(element, w * left);
        }

        # region Reset Contents
        public void reset()
        {
            answerContacto.Text = "";
            answerContacto.BorderBrush = Brushes.Red;
            answerLocal.Text = "";
            answerLocal.BorderBrush = Brushes.Red;
            answerEditora.Text = "";
            answerEditora.BorderBrush = Brushes.Red;
            answerGenero.Text = "";
            answerGenero.BorderBrush = Brushes.Red;
            answerDia.Text = "";
            answerDia.BorderBrush = Brushes.Red;
            answerMes.Text = "";
            answerMes.BorderBrush = Brushes.Red;
            answerAno.Text = "";
            answerAno.BorderBrush = Brushes.Red;
            answerDiaLancamento.Text = "";
            answerDiaLancamento.BorderBrush = Brushes.Red;
            answerMesLancamento.Text = "";
            answerMesLancamento.BorderBrush = Brushes.Red;
            answerAnoLancamento.Text = "";
            answerAnoLancamento.BorderBrush = Brushes.Red;
            answerForm1.Text = "";
            answerForm1.BorderBrush = Brushes.Red;
            answerForm2.Text = "";
            answerForm2.BorderBrush = Brushes.Red;
            answerForm3.Text = "";
            answerForm3.BorderBrush = Brushes.Red;
        }
        # endregion

        # endregion

        # region Button Handlers

        private void FilterActivated(object sender, RoutedEventArgs e)
        {
            var b = (ToggleButton)e.OriginalSource;
            MainWindow.window.stocks.buttonHandlers(b.Name);
            MainWindow.window.framename.Navigate(MainWindow.window.stocks);

            foreach (ToggleButton f in BlueBarCanvas1.Children)
            {
                if (!f.Equals(addProduto))
                {
                    f.IsChecked = false;
                }
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var b = (Button)e.OriginalSource;
            MainWindow.window.myClick.buttonOnClickLeftBar(b.Name);
        }

        private void addProductInfo(SqlCommand cmd)
        {
            cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar));
            cmd.Parameters["@nome"].Value = MainWindow.window.stocks.nomeProduto;

            cmd.Parameters.Add(new SqlParameter("@preco", SqlDbType.Int));
            cmd.Parameters["@preco"].Value = MainWindow.window.stocks.precoProduto;

            cmd.Parameters.Add(new SqlParameter("@descricao", SqlDbType.VarChar));
            cmd.Parameters["@descricao"].Value = MainWindow.window.stocks.descProduto;
        }

        private void addProductShopInfo(SqlCommand cmd)
        {
            cmd.Parameters.Add(new SqlParameter("@idLoja", SqlDbType.Int));
            cmd.Parameters["@idLoja"].Value = MainWindow.idLoja;

            cmd.Parameters.Add(new SqlParameter("@quantidade", SqlDbType.Int));
            cmd.Parameters["@quantidade"].Value = MainWindow.window.stocks.quantidadeProduto;
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            if (answerForm1.Text == "" || answerForm2.Text == "" || answerForm3.Text == "")
            {
                MessageBox.Show("Tem de preencher todos os campos");
            }
            else
            {

                SqlCommand cmd = new SqlCommand("YourEntertainment.pr_AddStockAudiovisualProduct", Connection.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@tipo", SqlDbType.VarChar));
                cmd.Parameters["@tipo"].Value = activeType;

                addProductInfo(cmd);

                cmd.Parameters.Add(new SqlParameter("@editora", SqlDbType.VarChar));
                cmd.Parameters["@editora"].Value = answerEditora.Text;

                cmd.Parameters.Add(new SqlParameter("@genero", SqlDbType.VarChar));
                cmd.Parameters["@genero"].Value = answerGenero.Text;

                cmd.Parameters.Add(new SqlParameter("@dataLancamento", SqlDbType.Date));
                string date = answerMesLancamento.Text + "-" + answerDiaLancamento.Text + "-" + answerAnoLancamento.Text;
                DateTime data = DateTime.ParseExact(date, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                cmd.Parameters["@dataLancamento"].Value = data;

                addProductShopInfo(cmd);

                cmd.Parameters.Add(new SqlParameter("@arg1", SqlDbType.Variant));
                cmd.Parameters["@arg1"].Value = answerForm1.Text;

                cmd.Parameters.Add(new SqlParameter("@arg2", SqlDbType.Variant));
                cmd.Parameters["@arg2"].Value = answerForm2.Text;

                cmd.Parameters.Add(new SqlParameter("@arg3", SqlDbType.Variant));
                cmd.Parameters["@arg3"].Value = answerForm3.Text;

                Connection.Conn.Open();
                cmd.ExecuteNonQuery();
                Connection.Conn.Close();

                MessageBox.Show("Produto de Audiovisual adicionado com sucesso");
            }
        }

        private void addPBilheteira_Click(object sender, RoutedEventArgs e)
        {
            if (answerContacto.Text == "" || answerDia.Text == "" || answerMes.Text == "" || answerAno.Text == "" || answerLocal.Text == "")
            {
                MessageBox.Show("Tem de preencher todos os campos");
            }
            else
            {            
                try
                {
                    
                    SqlCommand cmd = new SqlCommand("YourEntertainment.pr_AddStockBilheteira", Connection.Conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    addProductInfo(cmd);

                    cmd.Parameters.Add(new SqlParameter("@data", SqlDbType.Date));
                    string date = answerMes.Text + "-" + answerDia.Text + "-" + answerAno.Text;
                    DateTime data = DateTime.ParseExact(date, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    cmd.Parameters["@data"].Value = data;
                    
                    cmd.Parameters.Add(new SqlParameter("@contacto", SqlDbType.VarChar));
                    cmd.Parameters["@contacto"].Value = answerContacto.Text;

                    cmd.Parameters.Add(new SqlParameter("@localizacao", SqlDbType.VarChar));
                    cmd.Parameters["@localizacao"].Value = answerLocal.Text;

                    addProductShopInfo(cmd);

                    Connection.Conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Conn.Close();
                    
                    MessageBox.Show("Produto de Bilheteira adicionado com sucesso");

                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    Connection.Conn.Close();
                }
                
            }
        }

        # endregion

        # region AddStock

        public void setBilheteira()
        {
            flag = false;
            AddBilheteiraPanel.Visibility = Visibility.Visible;
            AddAudiovisualPanel.Visibility = Visibility.Hidden;
            AddProdutoPanel.Visibility = Visibility.Hidden;
        }

        public void setAudiovisual(string name)
        {
            flag = true;
            AddBilheteiraPanel.Visibility = Visibility.Hidden;
            AddAudiovisualPanel.Visibility = Visibility.Visible;
            AddProdutoPanel.Visibility = Visibility.Visible;

            switch (name)
            {
                case "Cinema":
                    setSizePositionViewBox(viewAns1Produto, 0.03, 0.14, 0.03, -0.007);
                    ans1Produto.Text = "Realizador:";
                    setSizePositionViewBox(viewAns2Produto, 0.03, 0.045, 0.08, 0.033);
                    ans2Produto.Text = "Idioma: ";
                    setSizePositionViewBox(viewAns3Produto, 0.03, 0.065, 0.13, 0.026);
                    ans3Produto.Text = "Suporte: ";
                    break;
                case "Literatura":
                    setSizePositionViewBox(viewAns1Produto, 0.03, 0.14, 0.03, -0.02);
                    ans1Produto.Text = "ISBN: ";
                    setSizePositionViewBox(viewAns2Produto, 0.03, 0.045, 0.08, 0.033);
                    ans2Produto.Text = "Idioma: ";
                    setSizePositionViewBox(viewAns3Produto, 0.03, 0.065, 0.13, 0.02);
                    ans3Produto.Text = "Autor: ";
                    break;
                case "Musica":
                    setSizePositionViewBox(viewAns1Produto, 0.03, 0.14, 0.03, -0.007);
                    ans1Produto.Text = "Intérprete:";
                    setSizePositionViewBox(viewAns2Produto, 0.03, 0.045, 0.08, 0.034);
                    ans2Produto.Text = "Duração: ";
                    setSizePositionViewBox(viewAns3Produto, 0.03, 0.065, 0.13, 0.026);
                    ans3Produto.Text = "Suporte: ";
                    break;
                case "VideoJogo":
                    setSizePositionViewBox(viewAns1Produto, 0.03, 0.14, 0.03, -0.004);
                    ans1Produto.Text = "Faixa Etária:";
                    setSizePositionViewBox(viewAns2Produto, 0.03, 0.045, 0.08, 0.033);
                    ans2Produto.Text = "Idioma: ";
                    setSizePositionViewBox(viewAns3Produto, 0.03, 0.065, 0.13, 0.032);
                    ans3Produto.Text = "Plataforma: ";
                    break;
            }
            activeType = name;

        }

        # endregion

        # region EventHandlers to Validate input Feedback

        void validateInt(TextBox t)
        {
            // Validar Campos
            if (!Regex.IsMatch(t.Text, @"^\d+$"))
            {
                t.BorderBrush = Brushes.Red;
            }
            else
            {
                t.BorderBrush = Brushes.Green;
            }
        }

        void validateString(TextBox t, int limite)
        {
            // Validar campos
            if (t.Text == "" || t.Text.Length > limite)
            {
                t.BorderBrush = Brushes.Red;
            }
            else
            {
                t.BorderBrush = Brushes.Green;
            }
        }

        void validateChar(TextBox t, int tamanho)
        {
            if (t.Text.Length != tamanho)
            {
                t.BorderBrush = Brushes.Red;
            }
            else
            {
                validateInt(t);
            }
        }

        void answerForm3_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (activeType == "Cinema" || activeType == "Musica")
            {
                validateString(answerForm3, 20);
            }
            else if (activeType == "Literatura")
            {
                validateString(answerForm3, 40);
            }
            else
            {
                validateString(answerForm3, 30);
            }
        }

        void answerForm2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (activeType == "Cinema" || activeType == "Literatura" || activeType == "VideoJogo")
            {
                validateString(answerForm2, 20);
            }
            else if (!answerForm2.Text.Contains(":") || !Regex.IsMatch(answerForm2.Text, @"^\d+$"))
            {   // TIME
                answerForm2.BorderBrush = Brushes.Red;
            }
        }

        void answerForm1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (activeType == "Cinema")
            {
                validateString(answerForm1, 30);
            }
            else if (activeType == "Musica")
            {
                validateString(answerForm1, 25);
            }
            else if (activeType == "Literatura")
            {
                validateString(answerForm1, 15);
            }
            else
            {
                validateString(answerForm1, 10);
            }
        }

        void answerContacto_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateChar(answerContacto, 9);
        }

        void answerLocal_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateString(answerLocal, 29);
        }

        private void answerAno_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                // Validar Ano
                if (answerAno.Text == "" || answerAno.Text.Length > 4 || Convert.ToInt32(answerAno.Text) <= 0)
                {
                    answerAno.BorderBrush = Brushes.Red;
                }
                else
                {
                    validateChar(answerAno, 4);
                }
            }
            catch
            {
                answerAno.BorderBrush = Brushes.Red;
            }
        }

        private void answerMes_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                // Validar Mês
                if (answerMes.Text == "" || answerMes.Text.Length > 2 || Convert.ToInt32(answerMes.Text) <= 0 || Convert.ToInt32(answerMes.Text) > 12)
                {
                    answerMes.BorderBrush = Brushes.Red;
                }
                else
                {
                    validateChar(answerMes, 2);
                }
            }
            catch
            {
                answerMes.BorderBrush = Brushes.Red;
            }
        }

        private void answerDia_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                // Validar Dia
                if (answerDia.Text == "" || answerDia.Text.Length > 2 || Convert.ToInt32(answerDia.Text) <= 0 || Convert.ToInt32(answerDia.Text) > 31)
                {
                    answerDia.BorderBrush = Brushes.Red;
                }
                else
                {
                    validateChar(answerDia, 2);
                }
            }
            catch
            {
                answerDia.BorderBrush = Brushes.Red;
            }
        }

        private void answerAnoLancamento_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                // Validar Ano
                if (answerAnoLancamento.Text == "" || answerAnoLancamento.Text.Length > 4 || Convert.ToInt32(answerAnoLancamento.Text) <= 0)
                {
                    answerAnoLancamento.BorderBrush = Brushes.Red;
                }
                else
                {
                    validateChar(answerAnoLancamento, 4);
                }
            }
            catch
            {
                answerAnoLancamento.BorderBrush = Brushes.Red;
            }
        }

        private void answerMesLancamento_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                // Validar Mês
                if (answerMesLancamento.Text == "" || answerMesLancamento.Text.Length > 2 || Convert.ToInt32(answerMesLancamento.Text) <= 0 || Convert.ToInt32(answerMesLancamento.Text) > 12)
                {
                    answerMesLancamento.BorderBrush = Brushes.Red;
                }
                else
                {
                    validateChar(answerMesLancamento, 2);
                }
            }
            catch
            {
                answerMesLancamento.BorderBrush = Brushes.Red;
            }
        }

        private void answerDiaLancamento_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                // Validar Dia
                if (answerDiaLancamento.Text == "" || answerDiaLancamento.Text.Length > 2 || Convert.ToInt32(answerDiaLancamento.Text) <= 0 || Convert.ToInt32(answerDiaLancamento.Text) > 31)
                {
                    answerDiaLancamento.BorderBrush = Brushes.Red;
                }
                else
                {
                    validateChar(answerDiaLancamento, 2);
                }
            }
            catch
            {
                answerDiaLancamento.BorderBrush = Brushes.Red;
            }
        }

        void answerGenero_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateString(answerGenero, 34);
        }

        void answerEditora_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateString(answerEditora, 24);
        }

        #endregion
    }
}
