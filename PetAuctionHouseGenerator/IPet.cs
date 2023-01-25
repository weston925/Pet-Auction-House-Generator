using System;
using System.ComponentModel;
using System.Xml;

namespace PetAuctionHouseGenerator
{
    internal interface IPet : INotifyPropertyChanged
    {
        string Type { get; }

        Uri? ImageUrl { get; }

        void WriteTo(XmlWriter writer);
    }
}
