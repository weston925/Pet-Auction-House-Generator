using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace PetAuctionHouseGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.Xml.Xsl.XslCompiledTransform xslCompiledTransform = new();

        private MainWindowData WindowData => (MainWindowData)DataContext;

        public MainWindow()
        {
            using (var stream = new StringReader(Properties.Resources.PetAuctionHouseTransform))
            {
                using var xmlReader = new XmlTextReader(stream);

                xslCompiledTransform.Load(xmlReader);
            }

            InitializeComponent();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            WindowData.Users.Add(new User());
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserListBox.SelectedItem is User user)
                WindowData.Users.Remove(user);
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            var html = new StringBuilder();

            using (var memoryStream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(memoryStream))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("pah");

                    foreach (var user in WindowData.Users)
                    {
                        user.WriteTo(writer);
                    }

                    writer.WriteEndDocument();
                    writer.Close();
                }

                memoryStream.Seek(0, SeekOrigin.Begin);

                var settings = new XmlWriterSettings
                {
                    ConformanceLevel = ConformanceLevel.Auto,
                    Indent = true
                };

                using var reader = XmlReader.Create(memoryStream);

                using (var writer = XmlWriter.Create(html, settings))
                {
                    xslCompiledTransform.Transform(reader, writer);
                }
            }

            Clipboard.SetText(html.ToString());
        }

        private void AddStandardPetButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserListBox.SelectedItem is User user)
            {
                user.Pets.Add(new Pet());
            }
        }

        private void AddCustomPetButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserListBox.SelectedItem is User user)
            {
                user.Pets.Add(new CustomPet());
            }
        }

        private void QuickAddPetButton_Click(object sender, RoutedEventArgs e)
        {
            var quickAddPetWindow = new QuickAddPetWindow
            {
                Owner = this
            };

            var result = quickAddPetWindow.ShowDialog();

            if (result.HasValue && result.Value)
            {
                var quickAddPetData = quickAddPetWindow.WindowData;
                var windowData = WindowData;
                bool foundUser = false;

                foreach (var user in windowData.Users)
                {
                    if (user.Name.Equals(quickAddPetData.UserName, StringComparison.OrdinalIgnoreCase))
                    {
                        foundUser = true;

                        bool addPet = true;

                        foreach (var pet in user.Pets)
                        {
                            if (pet is Pet standardPet && standardPet.Name.Equals(quickAddPetData.Pet.Name, StringComparison.OrdinalIgnoreCase))
                            {
                                if (MessageBox.Show(this, "A pet with this name already exists for the specified user. Do you still want to add it?", "Pet Already Exists", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                                    addPet = false;

                                break;
                            }
                        }

                        if (addPet)
                        {
                            user.Pets.Add(quickAddPetData.Pet);
                        }

                        break;
                    }
                }

                if (!foundUser)
                {
                    var user = new User
                    {
                        Name = quickAddPetData.UserName
                    };

                    user.Pets.Add(quickAddPetData.Pet);
                    windowData.Users.Add(user);
                }
            }
        }
    }
}
