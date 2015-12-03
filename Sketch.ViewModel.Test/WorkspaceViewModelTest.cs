using System.Linq;
using NUnit.Framework;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Serialize;
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

        [Test]
        public void ElementList_AddAllElement_NoError()
        {
            var factories = new IElementFactoryViewModel<IElementViewModel>[]
            {
                new PortiereFactoryViewModel(),
                new PleatFactoryViewModel(),
                new TulleFactoryViewModel(),
                new TieBackFactoryViewModel(), 
                new PanelFactoryViewModel(), 
                new HardPelmetFactoryViewModel(), 
                new ApplicationFactoryViewModel(), 
                new ScanFactoryViewModel(), 
                new OverlayFactoryViewModel(), 
                new FilletFactoryViewModel(), 
                new EqualSwagFactoryViewModel(), 
                new EqualTailFactoryViewModel(), 
                new ScaleneSwagFactoryViewModel(), 
                new ScaleneTailFactoryViewModel(),
                new DeJabotFactoryViewModel(), 
                new LatticeFactoryViewModel(), 
                new RomanBlindFactoryViewModel()
            };
            var screen = new ScreenViewModel()
            {
                Width = 4,
                Height = 2
            };

            var settingData = MockRepository.GenerateStub<ISettingData>();
            settingData.Stub(sd => sd.ProgramName).Return("MyName");
            settingData.Stub(sd => sd.Version).Return("1.0.0.0");

            var settingProvider = MockRepository.GenerateStub<ISettingProvider>();
            settingProvider.Stub(sp => settingProvider.GetSettingData()).Return(settingData);

            var workspace = new WorkspaceViewModel()
            {
                Factories = factories,
                Screen = screen,
                CommandFactory = MockRepository.GenerateStub<ICommandFactory>(),
                SettingProvider = settingProvider
            };

            var elementList = workspace.ElementList;
            factories.ToList()
                .ForEach(factory => elementList.AppendElement(factory));

            Assert.That(elementList.Count(), Is.EqualTo(factories.Length));

            var dataTransfer = workspace.Save();

            Assert.DoesNotThrow(() =>
            {
                var serializer = new XmlSerializer(StandardXmlPrimitive.Primitives, dataTransfer);
                serializer.Serialize();
            });
            workspace.Restore(dataTransfer);
            Assert.That(elementList.Count(), Is.EqualTo(factories.Length));
        }

    }
}
