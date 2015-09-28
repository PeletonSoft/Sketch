using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Element.Layout;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;

namespace PeletonSoft.Sketch.ViewModel.Element.Null
{
    public class NullElementViewModel : IElementViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { }
            remove { }
        }

        public void RestoreDefault()
        {
        }

        private readonly ILayoutViewModel _layout;
        public ILayoutViewModel Layout
        {
            get { return _layout; }
        }

        public string Description
        {
            get { return ""; }
            set { }
        }

        public IClotheViewModel Clothe { get; private set; }
        public IReadOnlyList<IElementViewModel> Below { get; private set; }
        public ICommand MoveToElementCommand { get; set; }

        public bool Visibility
        {
            get { return false; }
            set {  }
        }

        public double Opacity
        {
            get { return 0; }
            set { }
        }

        public void AfterInsert()
        {
        }

        public void BeforeDelete()
        {
        }

        public NullElementViewModel()
        {
            _layout = new NullLayoutViewModel();
            Below = new List<IElementViewModel>();
            Clothe = new NullClotheViewModel();
        }

    }
}
