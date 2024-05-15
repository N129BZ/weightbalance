using Syncfusion.Maui.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WeightBalance.Models
{
    public class DefaultState : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public SfRadioGroupKey GroupKey { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class DefaultStateViewModel : INotifyPropertyChanged
    {
        public SfRadioGroupKey GroupKey { get; set; }

        private ObservableCollection<DefaultState> _states;
        public ObservableCollection<DefaultState> States
        {
            get
            {
                GroupKey = new SfRadioGroupKey();
                if (_states != null)
                {
                    foreach (var item in _states)
                    {
                        item.GroupKey = GroupKey;
                    }
                }

                return _states;
            }
            set
            {
                if (_states == value)
                    return;

                _states = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
