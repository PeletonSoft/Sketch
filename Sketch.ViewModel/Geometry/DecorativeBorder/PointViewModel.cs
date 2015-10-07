using System;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface.Draw;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.Draw;

namespace PeletonSoft.Sketch.ViewModel.Geometry.DecorativeBorder
{
    public class PointViewModel : VertexViewModel, IPointViewModel
    {
        public PointViewModel(double x, double y, 
            Action<IPointViewModel> deleteAction, ICommandFactory commandFactory) :
            base(x, y)
        {
            DeleteCommand = commandFactory.CreateCommand(() => deleteAction(this));
        }

        public ICommand DeleteCommand { get; set; }

    }

}
