using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Layout;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;

namespace PeletonSoft.Sketch.ViewModel.Element.Null
{
    public class NullElementViewModel : IElementViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { }
            remove { }
        }

        public ILayoutViewModel Layout { get; }

        public string Description
        {
            get { return ""; }
            set { }
        }

        public IClotheViewModel Clothe { get; }
        public IReadOnlyList<IElementViewModel> Below { get; }
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
            Layout = new NullLayoutViewModel();
            Below = new List<IElementViewModel>();
            Clothe = new NullClotheViewModel();
        }

        public IElementDataTransfer CreateState() => null;
        public void Save(IElementDataTransfer state) => DoNothing();
        public void Restore(IElementDataTransfer state) => DoNothing();
    }
}
