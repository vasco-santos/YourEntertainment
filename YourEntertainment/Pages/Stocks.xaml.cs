using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mime;
using System.Text.RegularExpressions;
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
using YourEntertainment.Classes;

namespace YourEntertainment.Pages
{
    /// <summary>
    /// Interaction logic for AdicionaStock.xaml
    /// </summary>
    public partial class Stocks : Page
    {
        # region Fields

        private double h;
        private double w;
        private int i;
        private Button[] consultButtons;
        private ToggleButton activeButtonConsultas;
        private ToggleButton activeButton;
        private TextBlock tipoProdutoT;
        private ComboBox tipoProduto;
        private ComboBox tipoProdutoStock;
        private TextBox answerCodigoProduto;
        private TextBox answerNomeProduto;
        private TextBox answerPrecoProduto;
        private TextBox answerDescProduto;
        private TextBox answerQuantidadeProduto;
        private TextBox answerQuantidadeProduto2;
        private TextBlock detProduto;
        private Viewbox viewdetProduto;

        public string nomeProduto { get; private set; }
        public int codigoProduto { get; private set; }
        public int precoProduto { get; private set; }
        public string descProduto { get; private set; }
        public int quantidadeProduto { get; private set; }

        # endregion

        public Stocks()
        {
            InitializeComponent();
            initializeWindow();
        }

        # region Interface

        # region Initialize Window Buttons
        private void initializeWindow()
        {
            // Measures
            h = MainWindow.window.myWindow.getHeight();
            w = MainWindow.window.myWindow.getWidth();

            // Initialize Add Product
            initializeAddProduct();

            // Set Active Button
            activeButton = addProduto;

            // Initialize Add Stock
            initializeAddStock();
            AddStockPanel.Visibility = Visibility.Hidden;

            // Initiliaze Left and Top Bar Buttons
            initializeLeftAndTopBar();

            // Initialize Grid
            scrollSet("", Visibility.Hidden, false);
            ScrollerProdutos.Visibility = Visibility.Hidden;
            ScrollerLoja.Visibility = Visibility.Hidden;
        }

        private void initializeAddStock()
        {
            // Add Stock Form
            AddStockPanel.Height = h * 0.24;
            AddStockPanel.Width = w * 0.35;
            Canvas.SetTop(AddStockPanel, h * 0.2);
            Canvas.SetLeft(AddStockPanel, w * 0.43);

            #region Form1

            // Product Type
            Viewbox viewTipoProdutoT = new Viewbox();
            setSizePositionViewBox(viewTipoProdutoT, 0.03, 0.1, 0.03, 0.037);
            AddStockPanel.Children.Add(viewTipoProdutoT);

            tipoProdutoT = new TextBlock();
            tipoProdutoT.Text = "Tipo de Produto:";
            tipoProdutoT.TextAlignment = TextAlignment.Justify;
            tipoProdutoT.TextTrimming = TextTrimming.CharacterEllipsis;
            viewTipoProdutoT.Child = tipoProdutoT;

            // Product Type Combo Box
            tipoProdutoStock = new ComboBox();
            tipoProdutoStock.Items.Add(TipoProduto.Loja);
            tipoProdutoStock.Items.Add(TipoProduto.Bilheteira);
            tipoProdutoStock.Items.Add(TipoProduto.Cinema);
            tipoProdutoStock.Items.Add(TipoProduto.Literatura);
            tipoProdutoStock.Items.Add(TipoProduto.Musica);
            tipoProdutoStock.Items.Add(TipoProduto.VideoJogo);
            tipoProdutoStock.FontSize = 20;
            tipoProdutoStock.Height = h * 0.03;
            tipoProdutoStock.Width = w * 0.1;
            tipoProdutoStock.SelectionChanged += tipoProdutoStock_SelectionChanged;
            Canvas.SetTop(tipoProdutoStock, h * 0.03);
            Canvas.SetLeft(tipoProdutoStock, w * 0.16);
            AddStockPanel.Children.Add(tipoProdutoStock);
            tipoProdutoStock.SelectedValue = "";

            #endregion

            #region Form2

            // Product Name
            Viewbox viewCodigoProduto = new Viewbox();
            setSizePositionViewBox(viewCodigoProduto, 0.03, 0.045, 0.08, 0.04);
            AddStockPanel.Children.Add(viewCodigoProduto);

            TextBlock codigoProduto = new TextBlock();
            codigoProduto.Text = "Código:";
            codigoProduto.TextAlignment = TextAlignment.Justify;
            codigoProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewCodigoProduto.Child = codigoProduto;

            answerCodigoProduto = new TextBox();
            answerCodigoProduto.FontSize = 18;
            answerCodigoProduto.BorderThickness = new Thickness(w * 0.001);
            answerCodigoProduto.BorderBrush = Brushes.Red;
            setSizePositionTextBox(answerCodigoProduto, 0.03, 0.14, 0.08, 0.16);
            AddStockPanel.Children.Add(answerCodigoProduto);

            answerCodigoProduto.TextChanged += answerCodigoProduto_TextChanged;

            #endregion

            #region Form3

            // Product Quantity
            viewdetProduto = new Viewbox();
            setSizePositionViewBox(viewdetProduto, 0.03, 0.065, 0.13, 0.04);
            AddStockPanel.Children.Add(viewdetProduto);

            detProduto = new TextBlock();
            detProduto.Text = "Quantidade:";
            detProduto.TextAlignment = TextAlignment.Justify;
            detProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewdetProduto.Child = detProduto;

            answerQuantidadeProduto = new TextBox();
            answerQuantidadeProduto.FontSize = 18;
            answerQuantidadeProduto.BorderThickness = new Thickness(w * 0.001);
            answerQuantidadeProduto.BorderBrush = Brushes.Red;
            setSizePositionTextBox(answerQuantidadeProduto, 0.03, 0.14, 0.13, 0.16);
            AddStockPanel.Children.Add(answerQuantidadeProduto);

            answerQuantidadeProduto.TextChanged += answerQuantidadeProduto_TextChanged;

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

            AddStockPanel.Children.Add(addProduct);
            addProduct.Click += addStock_Click;

            #endregion

        }
        
        private void initializeAddProduct()
        {
            // Add Product Form
            AddProductPanel.Height = h * 0.4;
            AddProductPanel.Width = w * 0.35;
            Canvas.SetTop(AddProductPanel, h * 0.3);
            Canvas.SetLeft(AddProductPanel, w * 0.43);

            #region Form1

            // Product Type
            Viewbox viewTipoProdutoT = new Viewbox();
            setSizePositionViewBox(viewTipoProdutoT, 0.03, 0.1, 0.03, 0.04);
            AddProductPanel.Children.Add(viewTipoProdutoT);

            tipoProdutoT = new TextBlock();
            tipoProdutoT.Text = "Tipo de Produto:";
            tipoProdutoT.TextAlignment = TextAlignment.Justify;
            tipoProdutoT.TextTrimming = TextTrimming.CharacterEllipsis;
            viewTipoProdutoT.Child = tipoProdutoT;

            // Product Type Combo Box
            tipoProduto = new ComboBox();
            tipoProduto.Items.Add(TipoProduto.Bilheteira);
            tipoProduto.Items.Add(TipoProduto.Cinema);
            tipoProduto.Items.Add(TipoProduto.Literatura);
            tipoProduto.Items.Add(TipoProduto.Musica);
            tipoProduto.Items.Add(TipoProduto.VideoJogo);
            tipoProduto.FontSize = 20;
            tipoProduto.Height = h * 0.03;
            tipoProduto.Width = w * 0.1;
            Canvas.SetTop(tipoProduto, h * 0.03);
            Canvas.SetLeft(tipoProduto, w * 0.16);
            AddProductPanel.Children.Add(tipoProduto);
            tipoProduto.SelectedValue = "";

            #endregion

            #region Form2

            // Product Name
            Viewbox viewNomeProduto = new Viewbox();
            setSizePositionViewBox(viewNomeProduto, 0.03, 0.045, 0.08, 0.04);
            AddProductPanel.Children.Add(viewNomeProduto);

            TextBlock nomeProduto = new TextBlock();
            nomeProduto.Text = "Nome:";
            nomeProduto.TextAlignment = TextAlignment.Justify;
            nomeProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewNomeProduto.Child = nomeProduto;

            answerNomeProduto = new TextBox();
            answerNomeProduto.FontSize = 18;
            answerNomeProduto.BorderThickness = new Thickness(w * 0.001);
            answerNomeProduto.BorderBrush = Brushes.Red;
            setSizePositionTextBox(answerNomeProduto, 0.03, 0.14, 0.08, 0.16);
            AddProductPanel.Children.Add(answerNomeProduto);
            answerNomeProduto.TextChanged += answerNomeProduto_TextChanged;

            #endregion

            #region Form3

            // Product Price
            Viewbox viewPrecoProduto = new Viewbox();
            setSizePositionViewBox(viewPrecoProduto, 0.03, 0.044, 0.13, 0.04);
            AddProductPanel.Children.Add(viewPrecoProduto);

            TextBlock precoProduto = new TextBlock();
            precoProduto.Text = "Preço:";
            precoProduto.TextAlignment = TextAlignment.Justify;
            precoProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewPrecoProduto.Child = precoProduto;

            answerPrecoProduto = new TextBox();
            answerPrecoProduto.FontSize = 18;
            answerPrecoProduto.BorderThickness = new Thickness(w * 0.001);
            answerPrecoProduto.BorderBrush = Brushes.Red;
            setSizePositionTextBox(answerPrecoProduto, 0.03, 0.1, 0.13, 0.16);
            AddProductPanel.Children.Add(answerPrecoProduto);
            answerPrecoProduto.TextChanged += answerPrecoProduto_TextChanged;

            // Product Price
            Viewbox viewPreco = new Viewbox();
            setSizePositionViewBox(viewPreco, 0.03, 0.215, 0.13, 0.16);
            AddProductPanel.Children.Add(viewPreco);

            TextBlock preco = new TextBlock();
            preco.Text = "€";
            preco.TextAlignment = TextAlignment.Justify;
            preco.TextTrimming = TextTrimming.CharacterEllipsis;
            viewPreco.Child = preco;

            #endregion

            #region Form4

            // Product Quantity
            Viewbox viewQuantidadeProduto = new Viewbox();
            setSizePositionViewBox(viewQuantidadeProduto, 0.03, 0.065, 0.18, 0.044);
            AddProductPanel.Children.Add(viewQuantidadeProduto);

            TextBlock quantidadeProduto = new TextBlock();
            quantidadeProduto.Text = "Quantidade:";
            quantidadeProduto.TextAlignment = TextAlignment.Justify;
            quantidadeProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewQuantidadeProduto.Child = quantidadeProduto;

            answerQuantidadeProduto2 = new TextBox();
            answerQuantidadeProduto2.FontSize = 18;
            answerQuantidadeProduto2.BorderThickness = new Thickness(w * 0.001);
            answerQuantidadeProduto2.BorderBrush = Brushes.Red;
            setSizePositionTextBox(answerQuantidadeProduto2, 0.03, 0.14, 0.18, 0.16);
            AddProductPanel.Children.Add(answerQuantidadeProduto2);

            answerQuantidadeProduto2.TextChanged += answerQuantidadeProduto2_TextChanged;

            #endregion

            #region Form5

            // Product Price
            Viewbox viewDescProduto = new Viewbox();
            setSizePositionViewBox(viewDescProduto, 0.03, 0.065, 0.23, 0.04);
            AddProductPanel.Children.Add(viewDescProduto);

            TextBlock descProduto = new TextBlock();
            descProduto.Text = "Descrição:";
            descProduto.TextAlignment = TextAlignment.Justify;
            descProduto.TextTrimming = TextTrimming.CharacterEllipsis;
            viewDescProduto.Child = descProduto;

            answerDescProduto = new TextBox();
            answerDescProduto.FontSize = 18;
            answerDescProduto.BorderThickness = new Thickness(w * 0.001);
            answerDescProduto.BorderBrush = Brushes.Red;
            setSizePositionTextBox(answerDescProduto, 0.09, 0.14, 0.23, 0.16);
            AddProductPanel.Children.Add(answerDescProduto);

            answerDescProduto.TextChanged += answerDescProduto_TextChanged;

            # endregion

            # region AddButton

            Button addProduct = new Button();
            addProduct.Width = w * 0.06;
            addProduct.Height = h * 0.04;
            Canvas.SetTop(addProduct, h * 0.34);
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

            AddProductPanel.Children.Add(addProduct);
            addProduct.Click += addProduct_Click;

            #endregion
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

        # endregion

        # region Visibilitys
        public void initializeLeftAndTopBar()
        {
            // Left Bar
            MainWindow.window.myWindow.initializeLeftBarButton(sale, "saleButton", 0.08, 0.15, 0.22, 0.112);
            MainWindow.window.myWindow.initializeLeftBarButton(stocks, "AddStockButtonPressed", 0.08, 0.15, 0.42, 0.112);
            MainWindow.window.myWindow.initializeLeftBarButton(dados, "StatisticsButton", 0.08, 0.15, 0.62, 0.112);

            // Top Bar
            MainWindow.window.myWindow.initializeTopBar(logOut, "logOut", 0.035, 0.07, 0.1, 0.85);
            MainWindow.window.myWindow.initializeTopBar(settings, "settings", 0.035, 0.075, 0.1, 0.79);

            //Initialize Left Blue Bar 1
            BlueBarCanvas1.Width = w * 0.14;
            BlueBarCanvas1.Height = h * 0.6;
            Canvas.SetTop(BlueBarCanvas1, h * 0.22);
            Canvas.SetLeft(BlueBarCanvas1, w * 0.21);

            initializeBlueBar(BlueBarCanvas1);
            addProduto.IsChecked = true;

            // Initialize Left Blue Bar 2
            BlueBarCanvas2.Width = w * 0.1;
            BlueBarCanvas2.Height = h * 0.62;
            Canvas.SetTop(BlueBarCanvas2, h * 0.46);
            Canvas.SetLeft(BlueBarCanvas2, w * 0.25);

            initializeBlueBar(BlueBarCanvas2);
            BlueBarCanvas2.Visibility = Visibility.Hidden;
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

        public void scrollSet(string name, Visibility visibility, bool enable)
        {
            initializeProductGrid(name);
            ScrollerProdutos.Visibility = visibility;
            ScrollerProdutos.IsEnabled = enable;
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

        public void initConsultasInt()
        {
            activeButton = consultas;
            
            BlueBarCanvas2.Visibility = Visibility.Visible;
            AddProductPanel.Visibility = Visibility.Hidden;
            AddStockPanel.Visibility = Visibility.Hidden;
            ScrollerLoja.Visibility = Visibility.Visible;

            ScrollerProdutos.Height = h * 0.667;
            ScrollerProdutos.Width = w * 0.29;
            Canvas.SetTop(ScrollerProdutos, h * 0.185);
            Canvas.SetLeft(ScrollerProdutos, w * 0.35);

            ScrollerLoja.Height = h*0.25;
            ScrollerLoja.Width = w * 0.15;
            Canvas.SetTop(ScrollerLoja, h * 0.37);
            Canvas.SetLeft(ScrollerLoja, w * 0.7);

            foreach (ToggleButton f in BlueBarCanvas2.Children)
            {
                f.IsChecked = false;
            }
            scrollSet("", Visibility.Visible, true);
        }

        private void initChangeProduct()
        {
            activeButton = changeProduct;
            noConsultInt();
            detProduto.Text = "Preço:";
            setSizePositionViewBox(viewdetProduto, 0.03, 0.065, 0.13, 0.025);
            AddStockPanel.Visibility = Visibility.Visible;
            AddProductPanel.Visibility = Visibility.Hidden;
            ScrollerProdutos.Height = h * 0.33;
            ScrollerProdutos.Width = w * 0.35;
            Canvas.SetTop(ScrollerProdutos, h * 0.47);
            Canvas.SetLeft(ScrollerProdutos, w * 0.43);
            foreach (ToggleButton f in BlueBarCanvas2.Children)
            {
                f.IsChecked = false;
            }
            try
            {
                scrollSet(tipoProdutoStock.SelectedValue.ToString(), Visibility.Visible, true);
            }
            catch
            {
                scrollSet("", Visibility.Visible, true);
            }
        }

        public void initAddStockInt()
        {
            activeButton = addStock;
            noConsultInt();
            detProduto.Text = "Quantidade:";
            setSizePositionViewBox(viewdetProduto, 0.03, 0.065, 0.13, 0.04);
            AddStockPanel.Visibility = Visibility.Visible;
            AddProductPanel.Visibility = Visibility.Hidden;
            ScrollerProdutos.Height = h * 0.33;
            ScrollerProdutos.Width = w * 0.35;
            Canvas.SetTop(ScrollerProdutos, h * 0.47);
            Canvas.SetLeft(ScrollerProdutos, w * 0.43);
            try
            {
                scrollSet(tipoProdutoStock.SelectedValue.ToString(), Visibility.Visible, true);
            }
            catch 
            {
                scrollSet("", Visibility.Visible, true);
            }
        }

        public void initAddProdutoInt()
        {
            activeButton = addProduto;
            noConsultInt();
            AddProductPanel.Visibility = Visibility.Visible;
            AddStockPanel.Visibility = Visibility.Hidden;
        }

        public void noConsultInt()
        {
            scrollSet("", Visibility.Hidden, false);
            BlueBarCanvas2.Visibility = Visibility.Hidden;
            ScrollerLoja.Visibility = Visibility.Collapsed;
        }

        # endregion

        # region Reset Contents
        public void reset()
        {
            answerCodigoProduto.Text = "";
            answerCodigoProduto.BorderBrush = Brushes.Red;
            answerNomeProduto.Text = "";
            answerNomeProduto.BorderBrush = Brushes.Red;
            answerPrecoProduto.Text = "";
            answerPrecoProduto.BorderBrush = Brushes.Red;
            answerDescProduto.Text = "";
            answerDescProduto.BorderBrush = Brushes.Red;
            answerQuantidadeProduto.Text = "";
            answerQuantidadeProduto.BorderBrush = Brushes.Red;
            answerQuantidadeProduto2.Text = "";
            answerQuantidadeProduto2.BorderBrush = Brushes.Red;
        }
        # endregion

        # endregion

        # region ButtonHandlers

        # region Validações
        bool validarInt(TextBox t, string erro)
        {
            bool b = true;
            if (!Regex.IsMatch(t.Text, @"^\d+$"))
            {
                t.BorderBrush = Brushes.Red;
                MessageBox.Show(erro +" Inválido.");
                b = false;
            }
            else if ((!Regex.IsMatch(t.Text, @"^\d+$")) ||
                     ((Regex.IsMatch(t.Text, @"^\d+$")) &&
                      Convert.ToInt32(t.Text) == 0))
            {
                t.BorderBrush = Brushes.Red;
                MessageBox.Show(erro + " Inválido.");
                b = false;
            }
            return b;
        }

        bool validarCodigo(TextBox t, string erro)
        {
            if (validarInt(t, erro) == false)
            {
                return false;
            }
            try
            {
                string command = "SELECT YourEntertainment.ProductCodeExists ('" + t.Text + "');";
                Connection.Conn.Open();
                SqlCommand cmd = new SqlCommand(command, Connection.Conn);
                Boolean result = (Boolean)cmd.ExecuteScalar();
                Connection.Conn.Close();
                if (result == true)
                {
                    answerNomeProduto.BorderBrush = Brushes.Green;
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            answerNomeProduto.BorderBrush = Brushes.Red;
            MessageBox.Show("Código não existe.");
            return false;
        }

        bool validarString(TextBox t, int max, string erro)
        {
            // Validar campos
            if (t.Text == "" || t.Text.Length > max)
            {
                t.BorderBrush = Brushes.Red;
                MessageBox.Show(erro + " Inválido.");
                return false;
            }
            return true;
        }

        private bool productNameAvailable(string productName)
        {
            try
            {
                string command = "SELECT YourEntertainment.ProductAvailable ('" + productName + "');";
                Connection.Conn.Open();
                SqlCommand cmd = new SqlCommand(command, Connection.Conn);
                Boolean result = (Boolean)cmd.ExecuteScalar();
                Connection.Conn.Close();
                if (result == true)
                {
                    answerNomeProduto.BorderBrush = Brushes.Green;
                    answerNomeProduto.BorderThickness = new Thickness(w * 0.001);
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            answerNomeProduto.BorderBrush = Brushes.Red;
            MessageBox.Show("Produto já existe.");
            return false;
        }

        # endregion

        # region Validar Form de Adicionar Produto e inserir na Base de Dados

        void addProduct_Click(object sender, RoutedEventArgs e)
        {
            if (tipoProduto.SelectedValue == null)
            {
                MessageBox.Show("Tem de preencher o tipo de Produto!");
            }
            else
            {
                string s = tipoProduto.SelectedValue.ToString();
                if (answerNomeProduto.Text == "" || answerDescProduto.Text == "" || answerPrecoProduto.Text == "" || answerQuantidadeProduto2.Text == "" || s == "Loja")
                {
                    MessageBox.Show("Tem de preencher todos os campos!");
                }
                else
                {
                    try
                    {
                        // Validar Nome, descrição e preço do produto
                        if (validarString(answerNomeProduto, 49, "Nome") &&
                            validarString(answerDescProduto, 249, "Descrição") &&
                            validarInt(answerPrecoProduto, "Preço") &&
                            validarInt(answerQuantidadeProduto2, "Quantidade") &&
                            productNameAvailable(answerNomeProduto.Text))
                        {
                            if (s == "Bilheteira")
                            {
                                // Controlo campos vazios
                                precoProduto = Int32.Parse(answerPrecoProduto.Text);
                                nomeProduto = answerNomeProduto.Text;
                                descProduto = answerDescProduto.Text;
                                quantidadeProduto = Int32.Parse(answerQuantidadeProduto2.Text);
                                MainWindow.window.addStock.setBilheteira();
                                MainWindow.window.framename.Navigate(MainWindow.window.addStock);
                            }
                            else
                            {
                                // Controlo campos vazios
                                precoProduto = Int32.Parse(answerPrecoProduto.Text);
                                nomeProduto = answerNomeProduto.Text;
                                descProduto = answerDescProduto.Text;
                                quantidadeProduto = Int32.Parse(answerQuantidadeProduto2.Text);
                                MainWindow.window.addStock.setAudiovisual(s);

                                MainWindow.window.framename.Navigate(MainWindow.window.addStock);
                            }
                        }
                    }
                    catch (NullReferenceException)
                    { }
                }
            }            
        }

        #endregion

        # region Validar Form de Adicionar Stock e inserir na Base de Dados
        private void addStock_Click(object sender, RoutedEventArgs e)
        {
            if (answerCodigoProduto.Text == "" || answerQuantidadeProduto.Text == "")
            {
                MessageBox.Show("Tem de preencher todos os campos!");
            }
            else
            {
                if (activeButton.Equals(addStock))
                {
                    // Validar Campos
                    if (validarCodigo(answerCodigoProduto, "Código") && validarInt(answerQuantidadeProduto, "Quantidade"))
                    {
                        try
                        {
                            # region YourEntertainment.pr_AddStock

                            SqlCommand cmd = new SqlCommand("YourEntertainment.pr_AddStock", Connection.Conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@codigo", SqlDbType.VarChar));
                            cmd.Parameters["@codigo"].Value = answerCodigoProduto.Text;

                            cmd.Parameters.Add(new SqlParameter("@quantidade", SqlDbType.VarChar));
                            cmd.Parameters["@quantidade"].Value = answerQuantidadeProduto.Text;

                            cmd.Parameters.Add(new SqlParameter("@idLoja", SqlDbType.VarChar));
                            cmd.Parameters["@idLoja"].Value = MainWindow.idLoja;

                            Connection.Conn.Open();
                            cmd.ExecuteNonQuery();
                            Connection.Conn.Close();


                            # endregion
                            scrollSet("", Visibility.Visible, true);
                            MessageBox.Show("Stock adicionado com sucesso");
                        }
                        catch
                        {
                            MessageBox.Show("Ligar VPN");
                        }
                    }
                }
                else if (activeButton.Equals(changeProduct))
                {
                    // Validar Campos
                    if (validarCodigo(answerCodigoProduto, "Código") && validarInt(answerQuantidadeProduto, "Preço"))
                    {
                        try
                        {
                            # region Update Product Cost

                            SqlCommand cmd = new SqlCommand("YourEntertainment.pr_UpdateProduct", Connection.Conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar));
                            cmd.Parameters["@id"].Value = answerCodigoProduto.Text;

                            cmd.Parameters.Add(new SqlParameter("@preco", SqlDbType.Decimal));
                            cmd.Parameters["@preco"].Value = decimal.Parse(answerQuantidadeProduto.Text);

                            Connection.Conn.Open();
                            cmd.ExecuteNonQuery();
                            Connection.Conn.Close();

                            # endregion
                            scrollSet("", Visibility.Visible, true);
                            MessageBox.Show("Preço do produto alterado com sucesso");
                        }
                        catch
                        {
                            MessageBox.Show("Ligar VPN");
                        }
                    }
                }
            }
        }
        # endregion

        # region button click Left Bars

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var b = (Button)e.OriginalSource;
            MainWindow.window.myClick.buttonOnClickLeftBar(b.Name);
        }

        public void buttonHandlers(string s)
        {
            switch (s)
            {
                case "addProduto":
                    initAddProdutoInt();
                    break;
                case "addStock":
                    initAddStockInt();
                    break;
                case "consultas":
                    initConsultasInt();
                    break;
                case "changeProduct":
                    initChangeProduct();
                    break;
                default:
                    MainWindow.window.myClick.buttonOnClickLeftBar(s);
                    break;
            }
            reset();

            foreach (ToggleButton f in BlueBarCanvas1.Children)
            {
                if (f.Equals(activeButton))
                {
                    f.IsChecked = true;
                }
                else
                {
                    f.IsChecked = false;
                }
            }
            foreach (ToggleButton f in BlueBarCanvas2.Children)
            {
                f.IsChecked = false;

            }
        }

        private void FilterActivated(object sender, RoutedEventArgs e)
        {
            var b = (ToggleButton)e.OriginalSource;
            buttonHandlers(b.Name);
        }

        private void FilterActivated2(object sender, RoutedEventArgs e)
        {
            var b = (ToggleButton)e.OriginalSource;
            switch (b.Name)
            {
                case "Cinema":
                    initializeProductGrid("Cinema");
                    activeButtonConsultas = Cinema;
                    break;
                case "Musica":
                    initializeProductGrid("Musica");
                    activeButtonConsultas = Musica;
                    break;
                case "Literatura":
                    initializeProductGrid("Literatura");
                    activeButtonConsultas = Literatura;
                    break;
                case "VideoJogo":
                    initializeProductGrid("VideoJogo");
                    activeButtonConsultas = VideoJogo;
                    break;
                case "Bilheteira":
                    initializeProductGrid("Bilheteira");
                    activeButtonConsultas = Bilheteira;
                    break;
                default:
                    MainWindow.window.myClick.buttonOnClickLeftBar(b.Name);
                    break;
            }
            reset();

            foreach (ToggleButton f in BlueBarCanvas1.Children)
            {
                f.IsChecked = false;
                
            }
            foreach (ToggleButton f in BlueBarCanvas2.Children)
            {
                if (f.Equals(activeButtonConsultas))
                {
                    f.IsChecked = true;
                }
                else
                {
                    f.IsChecked = false;
                }
            }
        }
        # endregion

        # region Selection Table
        public ToggleButton getActiveButton()
        {
            return activeButtonConsultas;
        }

        void tipoProdutoStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string s = tipoProdutoStock.SelectedValue.ToString();
                if (s != "Loja")
                {
                    scrollSet(s, Visibility.Visible, true);
                }
                else
                {
                    scrollSet("", Visibility.Visible, true);
                }
            }
            catch (NullReferenceException)
            { }
        }
    #endregion

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

        void answerCodigoProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Validar Campos
            if (!Regex.IsMatch(answerCodigoProduto.Text, @"^\d+$"))
            {
                answerCodigoProduto.BorderBrush = Brushes.Red;
            }
            else
            {
                try
                {
                    string command = "SELECT YourEntertainment.ProductCodeExists ('" + answerCodigoProduto.Text + "');";
                    Connection.Conn.Open();
                    SqlCommand cmd = new SqlCommand(command, Connection.Conn);
                    Boolean result = (Boolean)cmd.ExecuteScalar();
                    Connection.Conn.Close();
                    if (result == true)
                    {
                        answerCodigoProduto.BorderBrush = Brushes.Green;
                    }
                    else
                    {
                        answerCodigoProduto.BorderBrush = Brushes.Red;
                    }
                }
                catch 
                {
                    Connection.Conn.Close();
                }
            }
        }

        private void answerQuantidadeProduto_TextChanged(object sender, TextChangedEventArgs e)
        {           
            validateInt(answerQuantidadeProduto);
        }

        private void answerQuantidadeProduto2_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateInt(answerQuantidadeProduto2);
        }


        void answerDescProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateString(answerDescProduto, 249);
        }

        void answerPrecoProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateInt(answerPrecoProduto);
        }

        void answerNomeProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Validar campos
            if (answerNomeProduto.Text == "" || answerNomeProduto.Text.Length > 29)
            {
                answerNomeProduto.BorderBrush = Brushes.Red;
            }
            else
            {
                Boolean result = false;
                try
                {
                    string command = "SELECT YourEntertainment.ProductAvailable ('" + answerNomeProduto.Text + "');";
                    Connection.Conn.Open();
                    SqlCommand cmd = new SqlCommand(command, Connection.Conn);
                    result = (Boolean)cmd.ExecuteScalar();
                    Connection.Conn.Close();
                }
                catch {
                }
                if (result == true)
                {
                    answerNomeProduto.BorderBrush = Brushes.Green;
                }
                else
                {
                    answerNomeProduto.BorderBrush = Brushes.Red;
                }
            }
        }

        # endregion

        # region Tables

        private void initializeProductGrid(string name)
        {
            try
            {
                Connection.Conn.Open();
                string command;
                if (name == "")
                {
                    command = "exec YourEntertainment.pr_GetAllCategoryProducts " + MainWindow.idLoja;
                }
                else
                {
                    command = "exec YourEntertainment.pr_GetAllCategoryProducts " + MainWindow.idLoja + ", '" + name + "';";
                }
                SqlCommand cmd = new SqlCommand(command);
                SqlDataAdapter sda = new SqlDataAdapter(command, Connection.Conn);
                DataTable dt = new DataTable("Produtos");
                sda.Fill(dt);
                ProdutosGrid.ItemsSource = dt.DefaultView;
                Connection.Conn.Close();
                ProdutosGrid.SelectedCellsChanged +=ProdutosGrid_SelectedCellsChanged;
            }
            catch
            {
                Connection.Conn.Close();
            }
        }

        private void ProdutosGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (activeButton.Equals(addStock))
            {
                DataRowView dataRow = (DataRowView) ProdutosGrid.SelectedItem;
                string cellValue = dataRow.Row.ItemArray[0].ToString();
                answerCodigoProduto.Text = cellValue;
            }
            else if (activeButton.Equals(consultas))
            {
                DataRowView dataRow = (DataRowView)ProdutosGrid.SelectedItem;
                int cellValue = (int) dataRow.Row.ItemArray[0];
                initializeLojaGrid(cellValue);
            }
            else if (activeButton.Equals(changeProduct))
            {
                DataRowView dataRow = (DataRowView)ProdutosGrid.SelectedItem;
                string cellValue = dataRow.Row.ItemArray[0].ToString();
                answerCodigoProduto.Text = cellValue;
            }
        }

        private void initializeLojaGrid(int code)
        {
            try
            {      
                Connection.Conn.Open();
                string command;

                command = "SELECT * FROM YourEntertainment.AvailableProduct (" + code + ");";

                SqlCommand cmd = new SqlCommand(command);
                SqlDataAdapter sdadapt = new SqlDataAdapter(command, Connection.Conn);
                DataTable dataTable = new DataTable("Lojas");
                sdadapt.Fill(dataTable);
                LojaGrid.ItemsSource = dataTable.DefaultView;
                Connection.Conn.Close();
            }
            catch
            {
                Connection.Conn.Close();
            }
        }

        # endregion

    }
}
