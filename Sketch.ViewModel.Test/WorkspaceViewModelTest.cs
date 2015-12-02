﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Core;
using NUnit.Framework;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.ObjectEvent.ChangedItem;
using PeletonSoft.Tools.Model.Setting;
using Rhino.Mocks;

namespace PeletonSoft.Sketch.ViewModel.Test
{
    [TestFixture]
    public class WorkspaceViewModelTest
    {
        [Test]
        public void ElementList_ThreeLayerAndChangeSize_FreeOpacityMask()
        {
            
            var factories = new IElementFactoryViewModel<IElementViewModel>[]
            {
                new PortiereFactoryViewModel(), 
                new PleatFactoryViewModel(), 
                new TulleFactoryViewModel()
            };
            var screen = new ScreenViewModel()
            {
                Width = 4,
                Height = 2
            };
            
            var workspace = new WorkspaceViewModel()
            {
                Factories = factories,
                Screen = screen,
                CommandFactory = MockRepository.GenerateStub<ICommandFactory>(),
                SettingProvider = MockRepository.GenerateStub<ISettingProvider>()
            };

            var elementList = workspace.ElementList;
            elementList.AppendElement(factories[0]);
            elementList.AppendElement(factories[1]);
            elementList.AppendElement(factories[2]);

            Assert.That(elementList.Count(), Is.EqualTo(3));
            Assert.That(elementList.Collection.Last().Layout.OpacityMask, Is.Null);
            screen.Width = 5;
            Assert.That(elementList.Collection.Last().Layout.OpacityMask, Is.Null);

        }
    }
}
