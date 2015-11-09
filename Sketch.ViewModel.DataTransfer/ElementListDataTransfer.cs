using System;
using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer
{
    [Serializable]
    public sealed class ElementListDataTransfer : IElementListDataTransfer
    {
        public IList<TypeContentDataTransfer<IElementDataTransfer>> List = 
            new List<TypeContentDataTransfer<IElementDataTransfer>>();
    }
}
