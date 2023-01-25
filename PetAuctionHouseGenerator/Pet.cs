using System;
using System.ComponentModel;
using System.Xml;

namespace PetAuctionHouseGenerator
{
    internal sealed class Pet : IPet
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string name = "PetName";
        private string petType = "PetType";
        private Uri? imageUrl = new(@"http://pets.neopets.com/cpn/PetName/1/4.png", UriKind.Absolute);

        public string Type => "Standard";

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

                    if (Name.Length == 0)
                        ImageUrl = null;
                    else
                        ImageUrl = new("http://pets.neopets.com/cpn/" + Uri.EscapeDataString(Name.Trim()) + "/1/4.png", UriKind.Absolute);
                }
            }
        }

        public string PetType
        {
            get => petType;
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                if (PetType != value)
                {
                    petType = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PetType)));
                }
            }
        }

        public Uri? ImageUrl
        {
            get => imageUrl;
            private set
            {
                if (ImageUrl != value)
                {
                    imageUrl = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageUrl)));
                }
            }
        }

        public void WriteTo(XmlWriter writer)
        {
            string name = Name.Trim();

            writer.WriteStartElement("Pet");
            writer.WriteAttributeString("Type", "Standard");
            writer.WriteAttributeString("PetName", name);
            writer.WriteAttributeString("PetNameUriEscaped", Uri.EscapeDataString(name));
            writer.WriteAttributeString("PetType", PetType.Trim());
            writer.WriteEndElement();
        }
    }
}
