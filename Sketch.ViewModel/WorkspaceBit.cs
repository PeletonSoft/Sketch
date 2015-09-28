using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.NotifyChanged.ChangedItem;
using PeletonSoft.Tools.Model.NotifyChanged.ChangedItem.ChangedInfo;
using PeletonSoft.Tools.Model.NotifyChanged.Render;

namespace PeletonSoft.Sketch.ViewModel
{
    public sealed class WorkspaceBit : IWorkspaceBit
    {
        #region INotifyItemChanged implement

        private bool _elementListChangedInited;
        private ItemChangedEventHandler _itemChanged;
        public event ItemChangedEventHandler ItemChanged
        {
            add
            {
                if (!_elementListChangedInited)
                {
                    Workspace.ElementList.ItemChanged +=
                        (sender, args) => OnElementListChanged(args.ChangedInfo);
                    _elementListChangedInited = true;
                }
                _itemChanged += value;
            }
            remove
            {
                _itemChanged += value;
            }
        }

        private void OnElementListChanged(ItemChangedInfo changedInfo)
        {
            var handler = _itemChanged;
            if (handler != null)
            {
                handler(this, new ItemChangedEventArgs(changedInfo));
            }
        }
        #endregion

        public ICommandFactory CommandFactory
        {
            get { return Workspace.CommandFactory; }
        }

        public RenderChangedDispatcher<IElementViewModel, IElementViewModel, IEnumerable<Point>> RenderChangedDispatcher
        {
            get { return Workspace.ElementList.RenderChangedDispatcher; }
        }

        public WorkspaceBit(IWorkspaceViewModel workspace)
        {
            _elementListChangedInited = false;
            Workspace = workspace;
        }

        private IWorkspaceViewModel Workspace { get; set; }

        public IScreenViewModel Screen
        {
            get { return Workspace.Screen; }
        }

        public IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories
        {
            get { return Workspace.Factories; }
        }

        public IReadOnlyList<IElementViewModel> GetBelowElements(IElementViewModel element)
        {
            return Workspace.ElementList.GetBelow(element);
        }

    }
}