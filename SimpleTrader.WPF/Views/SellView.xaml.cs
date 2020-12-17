using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleTrader.WPF.Views
{
    /// <summary>
    /// Interaktionslogik für SellView.xaml
    /// </summary>
    public partial class SellView : UserControl
    {
        public SellView()
        {
            InitializeComponent();
        }

        public ICommand SelectedAssetChangedCommand
        {
            get { return (ICommand)GetValue(SelectedAssetChangedCommandProperty); }
            set { SetValue(SelectedAssetChangedCommandProperty, value); }
        }

        public static readonly DependencyProperty SelectedAssetChangedCommandProperty =
            DependencyProperty.Register("SelectedAssetChangedCommand", typeof(ICommand), typeof(SellView), new PropertyMetadata(null));


        private void cbAssets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbAssets.SelectedItem != null)
            {
                SelectedAssetChangedCommand?.Execute(null);
            }
        }
    }
}
