using System.Collections.Specialized;
using System.Windows;

namespace PeletonSoft.Tools.View.XamlExtention.PushBinding
{
    public class PushBindingCollection : FreezableCollection<View.XamlExtention.PushBinding.PushBinding>
    {
        public PushBindingCollection(DependencyObject targetObject)
        {
            TargetObject = targetObject;
            ((INotifyCollectionChanged) this).CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (View.XamlExtention.PushBinding.PushBinding pushBinding in e.NewItems)
                {
                    pushBinding.SetupTargetBinding(TargetObject);
                }
            }
        }

        public DependencyObject TargetObject { get; }
    }
}
