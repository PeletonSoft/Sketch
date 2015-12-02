using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PeletonSoft.Tools.View.XamlExtention
{
    public class InterfaceTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var containerElement = container as FrameworkElement;

            if (item == null || containerElement == null)
                return base.SelectTemplate(item, container);

            var itemType = item.GetType();

            var dataTypes = Enumerable.Repeat(itemType, 1)
                .Concat(itemType.GetInterfaces());

            var template = dataTypes
                .Select(t => new DataTemplateKey(t))
                .Select(containerElement.TryFindResource)
                .OfType<DataTemplate>()
                .FirstOrDefault();

            return template ?? base.SelectTemplate(item, container);
        }
    }
}
