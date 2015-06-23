using System;
using System.Collections.Generic;
using System.Windows;

namespace PeletonSoft.Tools.View
{
    public class CurrentControl : ICurrent<FrameworkElement>
    {
        public FrameworkElement Current { get; set; }
    }
}
