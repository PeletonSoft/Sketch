namespace PeletonSoft.Tools.Model.NotifyChanged
{
    public static class SelectableListHelper
    {
        public static void Unselect<T>(this ISelectableList<T> list)
        {
            list.SelectedIndex = -1;
        }

        public static bool AllowUnselect<T>(this ISelectableList<T> list)
        {
            return list.SelectedIndex >= 0;
        }
    }
}
