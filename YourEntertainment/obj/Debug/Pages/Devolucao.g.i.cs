﻿#pragma checksum "..\..\..\Pages\Devolucao.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "558ED6AF67A8D2560B3B0364A8A9B5B3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace YourEntertainment.Pages {
    
    
    /// <summary>
    /// Devolucao
    /// </summary>
    public partial class Devolucao : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Pages\Devolucao.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid DevolucaoGrid;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Pages\Devolucao.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas DevolucaoCanvas;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Pages\Devolucao.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image selected;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Pages\Devolucao.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sale;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Pages\Devolucao.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button refund;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Pages\Devolucao.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addStock;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\Devolucao.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button statistics;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Pages\Devolucao.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logOut;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Pages\Devolucao.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button settings;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/YourEntertainment;component/pages/devolucao.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Devolucao.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DevolucaoGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.DevolucaoCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.selected = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.sale = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Pages\Devolucao.xaml"
            this.sale.Click += new System.Windows.RoutedEventHandler(this.ButtonOnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.refund = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Pages\Devolucao.xaml"
            this.refund.Click += new System.Windows.RoutedEventHandler(this.ButtonOnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.addStock = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\Pages\Devolucao.xaml"
            this.addStock.Click += new System.Windows.RoutedEventHandler(this.ButtonOnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.statistics = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Pages\Devolucao.xaml"
            this.statistics.Click += new System.Windows.RoutedEventHandler(this.ButtonOnClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.logOut = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\Pages\Devolucao.xaml"
            this.logOut.Click += new System.Windows.RoutedEventHandler(this.ButtonOnClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.settings = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\Pages\Devolucao.xaml"
            this.settings.Click += new System.Windows.RoutedEventHandler(this.ButtonOnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

