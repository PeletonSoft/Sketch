using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.DataTransfer;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Serialize;
using PeletonSoft.Tools.Model.Setting;

namespace PeletonSoft.Sketch.Runner.Memento
{
    public sealed class Caretaker : ICaretaker<WorkspaceDataTransfer>
    {
        public ISettingData SettingData { get; }
        public string ContentFileName = "content.xml";

        public Caretaker(ISettingData settingData)
        {
            SettingData = settingData;
        }

        public void SaveToFile(IVisualOriginator<WorkspaceDataTransfer> originator)
        {

            if (SettingData.ReadOnly || !Directory.Exists(SettingData.SavePath))
            {
                return;
            }

            if (!Directory.Exists(SettingData.GetOrderSavePath()))
            {
                Directory.CreateDirectory(SettingData.GetOrderSavePath());
            }

            var dataTransfer = originator.Save();
            var serializer = new XmlSerializer(StandardXmlPrimitive.Primitives, dataTransfer);
            var xml = serializer.Serialize();

            xml.Save(Path.Combine(SettingData.GetOrderSavePath(), ContentFileName));
            originator.ImageBox?.WriteToFile(Path.Combine(SettingData.GetOrderSavePath(), "content.png"));

            serializer.List.ToList()
                .ForEach(file => file.Value.WriteToFile(Path.Combine(SettingData.GetOrderSavePath(), file.Key)));
        }

        public void RestoreFromFile(IVisualOriginator<WorkspaceDataTransfer> originator)
        {
            if (!Directory.Exists(SettingData.GetOrderSavePath()) ||
                !File.Exists(Path.Combine(SettingData.GetOrderSavePath(), ContentFileName)))
            {
                return;
            }

            var xml = XElement.Load(Path.Combine(SettingData.GetOrderSavePath(), ContentFileName));

            var deserializer = new XmlDeserializer(StandardXmlPrimitive.Primitives,
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => x.GetName().Name == "Sketch.ViewModel.DataTransfer")
                    .SelectMany(x => x.GetTypes())
                    .Where(x => x.GetInterfaces().Any(i => i == typeof(IDataTransfer)))
                    .ToArray(),
                (fileName, size) =>
                {
                    var fullFileName = Path.Combine(SettingData.GetOrderSavePath(), fileName);
                    return File.Exists(fullFileName)
                        ? new PngImageBox(
                            File.ReadAllBytes(fullFileName),
                            (int) size.Width, (int) size.Height)
                        : null;
                });

            var dataTransfer = (WorkspaceDataTransfer)deserializer.Deserialize(xml, typeof(WorkspaceDataTransfer));
            originator.Restore(dataTransfer);
        }
    }
}
