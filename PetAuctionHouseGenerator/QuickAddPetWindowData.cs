using System.ComponentModel;

namespace PetAuctionHouseGenerator
{
    internal sealed class QuickAddPetWindowData
        : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string userName = "UserName";

        public Pet Pet { get; } = new Pet();

        public string UserName
        {
            get => userName;
            set
            {
                if (UserName != value)
                {
                    userName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserName)));
                }
            }
        }
    }
}
