using System.Windows;

namespace PetAuctionHouseGenerator
{
    /// <summary>
    /// Interaction logic for QuickAddPetWindow.xaml
    /// </summary>
    public partial class QuickAddPetWindow : Window
    {
        internal QuickAddPetWindowData WindowData => (QuickAddPetWindowData)DataContext;

        public QuickAddPetWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
