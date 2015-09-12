using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using YourEntertainment.DataBase;

namespace YourEntertainment.Pages
{
    /// <summary>
    /// Interaction logic for Venda.xaml
    /// </summary>
    public partial class Venda : Page
    {
        private double h;
        private double w;
        private ObservableCollection<vendaItem> products;
        private string currentFilter;
        private int clientPts=0;
        public Venda()
        {
            InitializeComponent();
            initializeWindow();
            initializeButtons();
            products = new ObservableCollection<vendaItem>();
            
        }

        #region UI Interface
        private void initializeWindow()
        {
            // Measures
            h = MainWindow.window.myWindow.getHeight();
            w = MainWindow.window.myWindow.getWidth();
            ScrollerVenda.Visibility = Visibility.Hidden;
            
            // Initiliaze Left and Top Bar Buttons
            initializeLeftAndTopBar();
            
            // Initialize Left Canvas
            LeftCanvas.Width = w*0.351;
            LeftCanvas.Height = h*0.66;
            Canvas.SetTop(LeftCanvas,h*0.185);
            Canvas.SetLeft(LeftCanvas,w*0.205);
            setLeftCanvasComponents();

            // Initialize Right Canvas
            RightCanvas.Width = w*0.345;
            RightCanvas.Height = h*0.66;
            Canvas.SetTop(RightCanvas,h*0.185);
            Canvas.SetLeft(RightCanvas,w*0.556);
            setRightCanvasComponents();
        }
      
        private void setLeftCanvasComponents()
        {
            ScrollerProdutos.Height = LeftCanvas.Height * 0.66;
            ScrollerProdutos.Width = LeftCanvas.Width;
            ProductFilters.Height = LeftCanvas.Height * 0.05;
            ProductFilters.Width = ScrollerProdutos.Width;
            ToggleButton[] buttons = new ToggleButton[] { All, Literatura, Musica, Cinema, Videojogos, Bilheteira };
            foreach (var b in buttons)
            {
                b.MinWidth = ProductFilters.Width * 0.166;
                b.Height = ProductFilters.Height;
                //if (b.Name.Equals("All"))
                  //  b.IsChecked = true;
            }
            SearchByCode.Width = LeftCanvas.Width;
            SearchByCode.Height = LeftCanvas.Height * 0.07;
            foreach (FrameworkElement f in SearchByCode.Children)
            {
                f.Width = LeftCanvas.Width * 0.2;
                f.Height = LeftCanvas.Height * 0.05;
                if (f.Name.Equals("CodigoBox"))
                {
                    f.Width = LeftCanvas.Width * 0.5;
                    f.Margin = new Thickness(f.Width * 0.1, 0, f.Width * 0.1, 0);
                }
            }
            
            Canvas.SetTop(ScrollerProdutos, LeftCanvas.ActualHeight);
            Canvas.SetTop(ProductFilters, ScrollerProdutos.Height);
            Canvas.SetTop(SearchByCode, LeftCanvas.Height * 0.8);
           
            Connection.Conn.Open();
            string command = "exec YourEntertainment.pr_GetProducts "+1;
            SqlDataAdapter sda = new SqlDataAdapter(command, Connection.Conn);
            DataTable dt = new DataTable("Produtos");
            sda.Fill(dt);
            produtos.ItemsSource = dt.DefaultView;
            Connection.Conn.Close();
        }

        private void setRightCanvasComponents()
        {
            ScrollerVenda.Visibility = Visibility.Visible;
            ScrollerVenda.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            ScrollerVenda.Height = RightCanvas.Height*0.5;
            ScrollerVenda.Width = RightCanvas.Width * 0.9;
            ValuePanel.Width = RightCanvas.Width*0.5;         
            border1.BorderThickness = new Thickness(w*0.0003);
            border2.BorderThickness = new Thickness(w*0.0003);
            foreach (FrameworkElement i in ValuePanel.Children)
            {
                i.MinWidth = RightCanvas.Width*0.25;
            }
            ClientInfo.Height = RightCanvas.Height*0.4;
            ClientInfo.Width = RightCanvas.Width;
            

            double padding = ClientInfo.Height*0.1;
            double lastheight=0;
            foreach (FrameworkElement f in ClientInfo.Children)
            {
                
                if (f.Name.Any(char.IsUpper))
                {
                    f.Width = ClientInfo.Width * 0.5;
                    f.Height = LeftCanvas.Height*0.05;
                    Canvas.SetLeft(f, ClientInfo.Width * 0.3);
                    Canvas.SetTop(f,lastheight);
                    
                }
                else
                {
                    Canvas.SetLeft(f, ClientInfo.Width * 0.1);
                    Canvas.SetTop(f,lastheight+padding*1.1);
                }
                lastheight = lastheight + padding;
            }
            SearchAderente.Width = ClientInfo.Width*0.1;
            

            Canvas.SetLeft(ScrollerVenda,RightCanvas.Width*0.1);
            Canvas.SetLeft(ValuePanel,RightCanvas.Width*0.1);
            Canvas.SetLeft(SearchAderente,ClientInfo.Width*0.85);
            Canvas.SetTop(ValuePanel,ScrollerVenda.Height);
            Canvas.SetTop(ClientInfo,RightCanvas.Width*0.65);
            Canvas.SetTop(SearchAderente,ClientInfo.Height*0.1);
            setScrollerVendaBinding();
        }

        private void initializeLeftAndTopBar()
        {
            MainWindow.window.myWindow.initializeTopBar(logOut, "logOut", 0.035, 0.07, 0.1, 0.85);
            MainWindow.window.myWindow.initializeTopBar(settings, "settings", 0.035, 0.075, 0.1, 0.79);
            MainWindow.window.myWindow.initializeLeftBarButton(sale, "saleButtonPressed", 0.08, 0.15, 0.22, 0.112);
            MainWindow.window.myWindow.initializeLeftBarButton(stocks, "AddStockButton", 0.08, 0.15, 0.42, 0.112);
            MainWindow.window.myWindow.initializeLeftBarButton(dados, "StatisticsButton", 0.08, 0.15, 0.62, 0.112);

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
        
        private void setScrollerVendaBinding()
        {        
            var gridview = new GridView();
            gridview.Columns.Add(new GridViewColumn
            {
                Width = ScrollerVenda.Width * 0.20,
                Header = "Codigo",
                DisplayMemberBinding = new Binding("Codigo")
            });
            gridview.Columns.Add(new GridViewColumn
            {
                Width = ScrollerVenda.Width * 0.5,
                Header = "Nome",
                DisplayMemberBinding = new Binding("Nome")
            });
            gridview.Columns.Add(new GridViewColumn
            {
                Width = ScrollerVenda.Width * 0.15,
                Header = "Valor",
                DisplayMemberBinding = new Binding("Valor")
            });
            gridview.Columns.Add(new GridViewColumn
            {
                Width = ScrollerVenda.Width * 0.15,
                Header = "Qtd",
                DisplayMemberBinding = new Binding("Qtd")
            });
            venda.View = gridview;
            valueBox.Text = Convert.ToString(0.00);

            /*
            Canvas.SetTop(ScrollerVenda, h * 0.185);
            Canvas.SetLeft(ScrollerVenda, w * 0.69);
            //venda.ItemsPanel =
            // = ScrollerVenda.Width/2;
           
            */
        }

        private void initializeButtons()
        {
            foreach (FrameworkElement f in addremove.Children)
            {
                f.Width = RightCanvas.Width * 0.1;
                f.Height = RightCanvas.Width * 0.1;
            }

            finish.Width = RightCanvas.Width*0.4;
            finish.Height = RightCanvas.Height*0.08;

            Canvas.SetTop(finish,ScrollerVenda.Height);
            Canvas.SetLeft(finish,RightCanvas.Width*0.6);
            Canvas.SetTop(addremove, RightCanvas.Height * 0.2);
        }

        private void ShowConfirmMenu()
        {
            
            ConfirmSale.Width = w;
            ConfirmSale.Height = h;
            ConfirmForm.Width = w * 0.4;      
            ConfirmForm.Height = h * 0.6;
            TextBox[] fields = { CAderenteBox,CNameBox,CNifBox,CvalorfinalBox };
            TextBlock[] cnames = { Caderente,Cnome,Cnif,Cvalorfinal };
            String[] values = {AderenteBox.Text, NameBox.Text, NifBox.Text, ValorfinalBox.Text};
            double padding = ConfirmForm.Height * 0.1;
            double lastheight = ConfirmForm.Height*0.1;

            for (int i = 0; i < 4; i++)
            {
                var block = cnames[i];
                var field = fields[i];
                var text = values[i];
                if (i == 3)
                {
                    block.Height = ConfirmForm.Height * 0.1;
                    block.Width = ConfirmForm.Width * 0.2;
                    Canvas.SetTop(block, lastheight + padding*1.1);
                    Canvas.SetLeft(block, ConfirmForm.Width*0.15);

                    field.Width = ConfirmSale.Width * 0.2;
                    field.Height = h * 0.04;
                    Canvas.SetTop(field, lastheight +padding);
                    Canvas.SetLeft(field, ConfirmForm.Width * 0.28);
                    field.Text = text;
                    break;
                }

                block.Height = ConfirmForm.Height*0.1;
                block.Width = ConfirmForm.Width*0.2;
                Canvas.SetTop(block, lastheight*1.05);
                Canvas.SetLeft(block,ConfirmForm.Width*0.15);

                field.Width = ConfirmSale.Width * 0.2;
                field.Height = h * 0.04;
                Canvas.SetTop(field, lastheight);
                Canvas.SetLeft(field, ConfirmForm.Width * 0.28);
                field.Text = text;
                lastheight = lastheight + padding;
            }
            
           ConfirmButtons.Height = ConfirmForm.Height*0.08;
           foreach (Button b in ConfirmButtons.Children)
           {
               b.MinWidth = ConfirmForm.Width*0.1;
           
           }
           Canvas.SetBottom(ConfirmButtons,padding);
           Canvas.SetLeft(ConfirmButtons,ConfirmForm.Width*0.38);
           ConfirmSale.Visibility = Visibility.Visible;


        }

        #endregion
        #region ButtonHandlers
        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var b = (Button)e.OriginalSource;
            MainWindow.window.myClick.buttonOnClickLeftBar(b.Name);
            ResetSale();
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            if(produtos.SelectedItem != null)
            {
                DataRowView dataRow = (DataRowView) produtos.SelectedItem;
                string cellValue = dataRow.Row.ItemArray[1].ToString();
                int pcode = Convert.ToInt32(dataRow.Row.ItemArray[0]);
                double ivalue = Convert.ToDouble(dataRow.Row.ItemArray[2]);
                int qtd;

                var p = products.FirstOrDefault(x => x.getNome() == cellValue);
                if (p != null)
                {
                    int i = products.IndexOf(p);
                    var item = new vendaItem(pcode, cellValue, p.getValor() + ivalue, p.getQtd() + 1);
                    products[i] = item;
                }
                else
                {
                    var item = new vendaItem(pcode, cellValue, ivalue, 1);
                    products.Add(item);
                }

                venda.ItemsSource = products;
                valueBox.Text = Convert.ToString((Convert.ToDouble(valueBox.Text.Split(' ')[0]) + ivalue) + " €");
                updateValorFinal(ivalue,1);
                
            }
        }

        private void updateValorFinal(double ivalue,int op)
        {
            switch (op)
            {
                case 1:
                    if (clientPts == 0)
                    {
                        ValorfinalBox.Text = valueBox.Text;
                    }
                    else
                    {
                        if (clientPts > 100)
                        {
                            ValorfinalBox.Text = Convert.ToString((Convert.ToDouble(valueBox.Text.Split(' ')[0]) + ivalue) * 0.9 + " €");
                        }
                    }
                    break;
                case 2:
                     if (clientPts == 0)
                    {
                        ValorfinalBox.Text = valueBox.Text;
                    }
                    else
                    {
                        if (clientPts > 100)
                        {
                            ValorfinalBox.Text = Convert.ToString((Convert.ToDouble(valueBox.Text.Split(' ')[0]) - ivalue) * 0.9 + " €");
                        }
                    }    
                    break;
            }
           
        }

        private void Finish_OnClick(object sender, RoutedEventArgs e)
        {
            if(products.Count != 0)
                ShowConfirmMenu();
            else
            {
                MessageBox.Show("Por favor insira pelo menos um produto para venda");
            }
            /*
             * Connection.Conn.Open();
            string command = "INSERT INTO YourEntertainment.Venda VALUES";
            SqlCommand cmd = new SqlCommand(command);
            cmd.BeginExecuteNonQuery();
            Connection.Conn.Close();*/
        }

        private void Remove_OnClick(object o, RoutedEventArgs e)
        {
            var p = venda.SelectedItem as vendaItem;
            if (produtos.SelectedItem != null)
            {
                if (p != null)
                {
                    int i = products.IndexOf(p);

                    if (p.getQtd() > 1)
                    {
                        var item = new vendaItem(p.getCodigo(), p.getNome(), p.getValor() - (p.getValor()/p.getQtd()),
                            p.getQtd() - 1);
                        products[i] = item;
                    }
                    else
                    {
                        products.RemoveAt(i);
                    }
                    valueBox.Text =
                        Convert.ToString((Convert.ToDouble(valueBox.Text.Split(' ')[0]) - (p.getValor()/p.getQtd())) +" €");
                    updateValorFinal((p.getValor()/p.getQtd()), 2);
                }
            }

        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            Connection.Conn.Open();
            SqlCommand cmd = new SqlCommand("YourEntertainment.pr_InsertVenda", Connection.Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd = setProcedureParameters(cmd);
            int i = cmd.ExecuteNonQuery();
            ConfirmSale.Visibility = Visibility.Hidden;
            Connection.Conn.Close();
            MessageBox.Show("Venda Registada");
            ResetSale();
        }

        private void ResetSale()
        {
            TextBox[] fields = { AderenteBox, NameBox, NifBox, ValorfinalBox };
            foreach (var f in fields)
            {
                f.Text = "";
            }
            valueBox.Text = "0";
            products = new ObservableCollection<vendaItem>();
            venda.ItemsSource = products;
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            ConfirmSale.Visibility = Visibility.Hidden;
        }

        private void PesquisarCodigo_OnClick(object sender, RoutedEventArgs e)
        {
            if (CodigoBox.Text.Equals("") || CodigoBox.Text.Any(char.IsLetter))
            {
                MessageBox.Show("Por favor insira o código de produto válido");
                CodigoBox.Text = "";

            }
            else
            {
                getProduct(1, Convert.ToInt32(CodigoBox.Text));
            }
        }

        private void SearchAderente_OnClick(object sender, RoutedEventArgs e)
        {
            if (AderenteBox.IsEnabled == false)
            {
                AderenteBox.IsEnabled = true;
                NameBox.IsEnabled = true;
                NifBox.IsEnabled = true;
                AderenteBox.Text = "";
                NameBox.Text = "";
                NifBox.Text = "";
            }
            else
            {
                if (AderenteBox.Text.Equals("") || AderenteBox.Text.Any(char.IsLetter))
                {
                    MessageBox.Show("Por favor insira o código do aderente");
                }
                else
                {
                    if(getAderenteData(Convert.ToInt32(AderenteBox.Text)))
                    {
                        AderenteBox.IsEnabled = false;
                        NameBox.IsEnabled = false;
                        NifBox.IsEnabled = false;
                    }
                    
                }
            }
        }
        
        #endregion
        #region SQLAux
        private SqlCommand setProcedureParameters(SqlCommand cmd)
        {

            var valor = Convert.ToDouble(valueBox.Text.Split(' ')[0]);
            var loja = MainWindow.idLoja; 
            var idfunc = MainWindow.idFunc; 

            var nif = NifBox.Text;
            var aderente = AderenteBox.Text;
            var nome = NameBox.Text;
            var tvp = new DataTable();
            tvp.Columns.Add("Codigo", typeof(int));
            tvp.Columns.Add("Qtd", typeof(int));

            int i = 0;

            foreach (var p in products)
            {

                tvp.Rows.Add(p.getCodigo(), p.getQtd());

            }

            ArrayList prvalues = new ArrayList() { valor, loja, idfunc, aderente, nif, nome, tvp };
            String[] pr = { "@val", "@loja", "@idfunc", "@aderente", "@nif", "@nome", "@List"};

            for (i = 0; i < 7; i++)
            {
                if (pr[i].Equals("@List"))
                {
                    SqlParameter tvparam = cmd.Parameters.AddWithValue("@List", tvp);
                    tvparam.SqlDbType = SqlDbType.Structured;
                    break;
                }
                if ((prvalues[i] == null))
                {
                    if(pr[i].Equals("@nif") || pr[i].Equals("@aderente"))
                        cmd.Parameters.Add(new SqlParameter(pr[i],SqlDbType.Int));
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter(pr[i], SqlDbType.Char, 9));
                    }
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter(pr[i], prvalues[i]));
                }
            }
            return cmd;
        }
         
        private void getProduct(int loja, int codigo)
        {

            Connection.Conn.Open();
            SqlCommand cmd = new SqlCommand("YourEntertainment.pr_GetProductByCode", Connection.Conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@loja", loja);
            cmd.Parameters.AddWithValue("@codigo", codigo);

            var returnName = cmd.Parameters.Add("@nome", SqlDbType.VarChar);
            returnName.Size = 50;
            returnName.Direction = ParameterDirection.Output;
            var returnPrice = cmd.Parameters.Add("@preco", SqlDbType.Decimal);
            returnPrice.Direction = ParameterDirection.Output;
            var res = cmd.Parameters.Add("@res", SqlDbType.Bit);
            res.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if ( Convert.ToBoolean(res.Value) == false)
            {
                MessageBox.Show("Produto não encontrado");
                CodigoBox.Text = "";
            }
            else
            {
                var p = products.FirstOrDefault(x => x.getNome().Equals(returnName.Value));
                if (p != null)
                {
                    int i = products.IndexOf(p);
                    var item = new vendaItem(codigo, returnName.Value.ToString(), p.getValor() + Convert.ToDouble(returnPrice.Value), p.getQtd() + 1);
                    products[i] = item;
                }
                else
                {
                    var item = new vendaItem(codigo,returnName.Value.ToString() , Convert.ToDouble(returnPrice.Value), 1);
                    products.Add(item);
                }

                venda.ItemsSource = products;
                valueBox.Text = Convert.ToString((Convert.ToDouble(valueBox.Text.Split(' ')[0]) + Convert.ToDouble(returnPrice.Value)) + " €");
                updateValorFinal(Convert.ToDouble(returnPrice.Value),1);
            }
            Connection.Conn.Close();

        }

        private bool getAderenteData(int id)
        {
            bool res = false;
          
            Connection.Conn.Open();
            SqlCommand cmd = new SqlCommand("YourEntertainment.pr_GetAderenteInfo", Connection.Conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", id);
            var returnName = cmd.Parameters.Add("@name", SqlDbType.VarChar);
            returnName.Size = 30;
            returnName.Direction = ParameterDirection.Output;
            var returnNif = cmd.Parameters.Add("@nif", SqlDbType.Char);
            returnNif.Size = 9;
            returnNif.Direction = ParameterDirection.Output;
            var returnPontos= cmd.Parameters.Add("@pontos", SqlDbType.Int);
            returnPontos.Direction = ParameterDirection.Output;
            var returnRes = cmd.Parameters.Add("@res", SqlDbType.Bit);
            returnRes.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            if( Convert.ToBoolean(returnRes.Value)==false)
            {
                MessageBox.Show("Aderente não encontrado na base de dados");              
            }
            else
            {
                NameBox.Text = (string) cmd.Parameters["@name"].Value;
                NifBox.Text = (string) cmd.Parameters["@nif"].Value;
                if (Int32.Parse(returnPontos.Value.ToString()) >= 100)
                    ValorfinalBox.Text = (Convert.ToDouble(valueBox.Text.Split(' ')[0])*0.9 + " €");
                else
                    ValorfinalBox.Text = valueBox.Text;
                res= true;
            }
            Connection.Conn.Close();
            clientPts = Int32.Parse(returnPontos.Value.ToString());
            return res;
        }

        private void FilterActivated(object sender, RoutedEventArgs e)
        {
            var b = (ToggleButton) e.OriginalSource;
            switch (b.Name)
            {
                case "All":
                    currentFilter = "";
;                    break;
                case "Musica":
                    currentFilter = "Musica";
                    break;
                case "Literatura":
                    currentFilter = "Literatura";
                    break;
                case "Videojogos":
                    currentFilter = "Videojogo";
                    break;
                case "Bilheteira":
                    currentFilter = "Bilheteira";
                    break;
                case "Cinema":
                    currentFilter = "Cinema";
                    break;
            }
            foreach (ToggleButton f in ProductFilters.Children)
            {
                if (f.Name.Equals(currentFilter))
                {
                    f.IsChecked = true;
                }
                else
                {
                    f.IsChecked = false;
                }
            }
            updateProductList(currentFilter);
        }
        private void updateProductList(string s)
        {
            Connection.Conn.Open();
            string command = "exec YourEntertainment.pr_GetProducts "+MainWindow.idLoja+",'"+s+"'";
            SqlDataAdapter sda = new SqlDataAdapter(command, Connection.Conn);
            DataTable dt = new DataTable("Produtos");
            sda.Fill(dt);
            produtos.ItemsSource = dt.DefaultView;
            Connection.Conn.Close();
        }

        #endregion
        #region Class vendaItem
        private class vendaItem
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public double Valor { get; set; }
            public int Qtd { get; set; }

            public vendaItem(int codigo, string nome, double valor, int qtd)
            {
                Codigo = codigo;
                Nome = nome;
                Valor = valor;
                Qtd = qtd;
            }

            public vendaItem()
            {

            }

            public int getCodigo()
            {
                return Codigo;
            }
            public string getNome()
            {
                return Nome;
            }

            public double getValor()
            {
                return Valor;
            }

            public void setValor(double valor)
            {
                Valor = valor;
            }

            public int getQtd()
            {
                return Qtd;
            }

            public void setQtd(int q)
            {
                Qtd = q;
            }

        }
        #endregion      
    }
}
