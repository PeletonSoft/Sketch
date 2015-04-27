using System.Printing;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PeletonSoft.Tools.View.Controls
{
    public class FixedDocumentViewer : DocumentViewer
    {
        protected override void OnPrintCommand()
        {
            var queue = LocalPrintServer.GetDefaultPrintQueue();
            var ticket = queue.DefaultPrintTicket;

            var printDialog = new PrintDialog
            {
                PrintQueue = queue,
                PrintTicket = ticket
            };
            

            var docSeq = Document as FixedDocument;
            printDialog.PrintTicket.PageOrientation = PageOrientation;

            if (docSeq != null && printDialog.ShowDialog() == true)
            {
                docSeq.PrintTicket = printDialog.PrintTicket;
                var writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);
                writer.WriteAsync(docSeq, printDialog.PrintTicket);
            }
        }

        public PageOrientation PageOrientation { get; set; }

        public FixedDocumentViewer()
        {
            PageOrientation = PageOrientation.Landscape;
        }
    }
}
