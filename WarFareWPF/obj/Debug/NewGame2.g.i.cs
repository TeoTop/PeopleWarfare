﻿#pragma checksum "..\..\NewGame2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E54DC638BA84404406B56C92BAEFCA13"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Converters;
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


namespace WarFareWPF {
    
    
    /// <summary>
    /// NewGame2
    /// </summary>
    public partial class NewGame2 : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 4 "..\..\NewGame2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WarFareWPF.NewGame2 _this;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\NewGame2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox items1;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\NewGame2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox items2;
        
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
            System.Uri resourceLocater = new System.Uri("/WarFareWPF;component/newgame2.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NewGame2.xaml"
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
            this._this = ((WarFareWPF.NewGame2)(target));
            
            #line 4 "..\..\NewGame2.xaml"
            this._this.Closing += new System.ComponentModel.CancelEventHandler(this._this_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 18 "..\..\NewGame2.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Prec);
            
            #line default
            #line hidden
            return;
            case 3:
            this.items1 = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.items2 = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            
            #line 111 "..\..\NewGame2.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.precSkin1);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 113 "..\..\NewGame2.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.suivSkin1);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 119 "..\..\NewGame2.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.precSkin2);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 121 "..\..\NewGame2.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.suivSkin2);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 128 "..\..\NewGame2.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateGame);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 4:
            
            #line 82 "..\..\NewGame2.xaml"
            ((System.Windows.Controls.TextBox)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.p1_KeyUp);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 99 "..\..\NewGame2.xaml"
            ((System.Windows.Controls.TextBox)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.p1_KeyUp);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

