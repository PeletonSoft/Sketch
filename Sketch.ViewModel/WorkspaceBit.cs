using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList.ChangedInfo.Primitive;
using PeletonSoft.Tools.Model.NotifyChanged.Render;

namespace PeletonSoft.Sketch.ViewModel
{
    public sealed class WorkspaceBit : IWorkspaceBit
    {
        #region INotifyElementListChanged implement

        private bool _elementListChangedInited;
        private ElementListChangedEventHandler _elementListChanged;
        public event ElementListChangedEventHandler ElementListChanged
        {
            add
            {
                if (!_elementListChangedInited)
                {
                    Workspace.ElementList.ElementListChanged +=
                        (sender, args) => OnElementListChanged(args.ChangedInfo);
                    _elementListChangedInited = true;
                }
                _elementListChanged += value;
            }
            remove
            {
                _elementListChanged += value;
            }
        }

        private void OnElementListChanged(ElementListChangedInfo changedInfo)
        {
            var handler = _elementListChanged;
            if (handler != null)
            {
                handler(this, new ElementListChangedEventArgs(changedInfo));
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

        public IList<IElementViewModel> GetBelowElements(IElementViewModel element)
        {
            return Workspace.ElementList.GetBelowElements(element);
        }

    }
}