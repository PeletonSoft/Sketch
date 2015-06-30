using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Memento.Container;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento
{
    public class WorkspaceMemento : IMemento<IWorkspaceViewModel>
    {
        public ScreenMemento Screen { get; set; }
        public PresentContainerMemento Presents { get; set; }

        public WorkModeContainerMemento WorkModes { get; set; }
        public ElementListMemento ElementList { get; set; }

        string Present { get; set; }
        string WorkMode { get; set; }

        public void GetState(IWorkspaceViewModel originator)
        {
            Screen = new ScreenMemento();
            WorkModes = new WorkModeContainerMemento();
            Presents = new PresentContainerMemento();
            ElementList = new ElementListMemento();

            Screen.GetState(originator.Screen);
            Presents.GetState(originator.Presents);
            ElementList.GetState(originator.ElementList);
            WorkModes.GetState(originator.WorkModes);

            Present = originator.Present.GetType().Name;
            WorkMode = originator.WorkMode.GetType().Name;
        }

        public void SetState(IWorkspaceViewModel originator)
        {
            originator.RestoreDefault();

            Screen.SetState(originator.Screen);
            Presents.SetState(originator.Presents);
            WorkModes.SetState(originator.WorkModes);
            ElementList.SetState(originator.ElementList);

            originator.Present = Present.SetByTypeName<IPresentViewModel>(originator.Presents);
            originator.WorkMode = WorkMode.SetByTypeName<IWorkModeViewModel>(originator.WorkModes);
        }

        public IEnumerable<IFileBox> GetFiles()
        {
            var filess = new[]
            {
                Presents.GetFiles(),
                WorkModes.GetFiles(),
                Screen.GetFiles(),
                ElementList.GetFiles()
            };
            return filess.GetFiles();
        }

        public XElement GetXml(Dictionary<string, IFileBox> files)
        {
            return new XElement("root",
                new XElement("Screen", Screen.GetXml(files).Elements()),
                new XElement("Presents", Presents.GetXml(files).Elements()),
                new XElement("WorkModes", WorkModes.GetXml(files).Elements()),
                new XElement("ElementList", ElementList.GetXml(files).Elements()),
                new XElement("Present", Present),
                new XElement("WorkMode", WorkMode)
                );
        }

        public void SetXml(XElement xml, string path)
        {
            Screen = new ScreenMemento();
            Presents = new PresentContainerMemento();
            WorkModes = new WorkModeContainerMemento();
            ElementList = new ElementListMemento();

            Screen.SetXml(xml.Element("Screen"), path);
            Presents.SetXml(xml.Element("Presents"), path);
            WorkModes.SetXml(xml.Element("WorkModes"), path);
            ElementList.SetXml(xml.Element("ElementList"), path);
            Present = (string) xml.Element("Present");
            WorkMode = (string)xml.Element("WorkMode");
        }
    }

}
