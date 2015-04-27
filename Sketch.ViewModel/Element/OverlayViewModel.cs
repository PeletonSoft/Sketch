using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class OverlayViewModel : IElementViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }

        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {

        }
        #endregion

        public OverlayViewModel(IWorkspaceBit workspaceBit)
        {
            WorkspaceBit = workspaceBit;
            OverHeight = workspaceBit.Screen.Height;
            OverWidth = workspaceBit.Screen.Width;

            workspaceBit.ElementListChanged +=
                (sender, args) =>
                {
                    var changeInfo = args.ChangedInfo;
                    OnPropertyChanged("Below");
                    SelectedIndex = changeInfo.RecalculateIndex(SelectedIndex);
                    SubscribePropertyChanged();
                };

            this.SetPropertyChanged("SelectedIndex", () =>
            {
                OnPropertyChanged("SelectedItem");
                SubscribePropertyChanged();
            });

            this.SetPropertyChanged(
                new[] {"OverWidth", "OverHeight", "OverOffsetX", "OverOffsetY"},
                () => OnPropertyChanged("OverRect"));
        }

        private int _selectedIndex = -1;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {

                if (value < 0 || value >= Below.Count())
                {
                    value = -1;
                }

                SetField(ref _selectedIndex, value);
            }
        }

        public IElementViewModel SelectedItem
        {
            get
            {
                return
                    (SelectedIndex >= 0 && SelectedIndex < Below.Count())
                        ? Below[SelectedIndex]
                        : null;
            }
        }

        public IWorkspaceBit WorkspaceBit { get; private set; }

        public IClotheViewModel Clothe
        {
            get { return null; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetField(ref _description, value); }
        }


        public ILayoutViewModel Layout
        {
            get { return SelectedItem != null ? SelectedItem.Layout : null; }
        }

        public ICommand MoveToElementCommand { get; set; }

        public IList<IElementViewModel> Below
        {
            get { return WorkspaceBit.GetBelowElements(this); }
        }

        public bool Visibility
        {
            get { return SelectedItem != null && SelectedItem.Visibility; }
            set
            {
                if (SelectedItem != null)
                {
                    SelectedItem.Visibility = value;
                }
            }
        }

        public double Opacity
        {
            get { return SelectedItem != null ? SelectedItem.Opacity : 0; }
            set
            {
                if (SelectedItem != null)
                {
                    SelectedItem.Opacity = value;
                }
            }
        }


        public void AfterInsert()
        {
        }

        public void BeforeDelete()
        {
            
        }

        private IElementViewModel LastSubscribeElement { get; set; }
        private void SubscribePropertyChanged()
        {
            if (LastSubscribeElement != null)
            {
                LastSubscribeElement.PropertyChanged -= SubscribeElementPropertyChanged;
            }

            LastSubscribeElement = SelectedItem;
            if (LastSubscribeElement != null)
            {
                LastSubscribeElement.PropertyChanged += SubscribeElementPropertyChanged;
            }

            OnPropertyChanged("OverRect");
            OnPropertyChanged("Opacity");
            OnPropertyChanged("Visibility");
            OnPropertyChanged("Layout");
        }

        private void SubscribeElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var properties = new[] {"Opacity", "Visibility","Layout"};
            if (properties.Any(x => x == args.PropertyName))
            {
                OnPropertyChanged(args.PropertyName);
            }
        }

        private double _overWidth;
        public double OverWidth
        {
            get { return _overWidth; }
            set { SetField(ref _overWidth, value); }
        }

        private double _overHeight;
        public double OverHeight
        {
            get { return _overHeight; }
            set { SetField(ref _overHeight, value); }
        }

        private double _overOffsetX;
        public double OverOffsetX
        {
            get { return _overOffsetX; }
            set { SetField(ref _overOffsetX, value); }
        }

        private double _overOffsetY;

        public double OverOffsetY
        {
            get { return _overOffsetY; }
            set { SetField(ref _overOffsetY, value); }
        }

        public Rect OveRect
        {
            get { return new Rect(OverOffsetX, OverOffsetY, OverWidth, OverHeight); }
        }
    }
}
