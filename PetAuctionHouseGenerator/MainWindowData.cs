using System.Collections.ObjectModel;

namespace PetAuctionHouseGenerator
{
    internal sealed class MainWindowData
    {
        public ObservableCollection<User> Users { get; } = new();
    }
}
