using System;
using System.ComponentModel;
using System.Xml;

namespace PetAuctionHouseGenerator
{
    internal sealed class CustomPet : IPet
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string petText = "Pet Text";
        private Uri? imageLink = null;
        private Uri? imageUrl = null;

        public string Type => "Custom";

        public Uri? ImageLink
        {
            get => imageLink;
            set
            {
                if (ImageLink != value)
                {
                    imageLink = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageLink)));
                }
            }
        }

        public Uri? ImageUrl
        {
            get => imageUrl;
            set
            {
                if (ImageUrl != value)
                {
                    imageUrl = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageUrl)));
                }
            }
        }

        public string PetText
        {
            get => petText;
            set
            {
                if (PetText != value)
                {
                    petText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PetText)));
                }
            }
        }

        public void WriteTo(XmlWriter writer)
        {
            if (ImageUrl is null && PetText.Length == 0)
                return;

            writer.WriteStartElement("Pet");
            writer.WriteAttributeString("Type", "Custom");

            if (ImageUrl is not null)
            {
                if (ImageLink is not null)
                {
                    writer.WriteAttributeString("ImageLink", ImageLink.ToString());
                }

                writer.WriteAttributeString("ImageUrl", ImageUrl.ToString());
            }

            if (PetText.Length > 0)
            {
                writer.WriteAttributeString("PetText", PetText);
            }

            writer.WriteEndElement();
        }
    }
}
