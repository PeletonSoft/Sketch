using System.Windows;
using PeletonSoft.Tools.Model.Draw;

namespace PeletonSoft.Tools.View
{
    public interface ILineView
    {
        double ThumbX { get; }
        double ThumbY { get; }
        ILineViewModel GetDataContext();

        event RoutedEventHandler ThumbClick;
    }
}
