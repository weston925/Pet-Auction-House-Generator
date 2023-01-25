using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace PetAuctionHouseGenerator
{
    internal sealed class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string name = "UserName";

        public ObservableCollection<IPet> Pets { get; } = new();

        public string Name
        {
            get => name;
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                if (Name != value)
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public void WriteTo(XmlWriter writer)
        {
            string name = Name.Trim();

            writer.WriteStartElement("User");
            writer.WriteAttributeString("UserName", name);
            writer.WriteAttributeString("UserNameUriEscaped", Uri.EscapeDataString(name));

            foreach (var pet in Pets)
            {
                pet.WriteTo(writer);
            }

            writer.WriteEndElement();
        }
    }
}
