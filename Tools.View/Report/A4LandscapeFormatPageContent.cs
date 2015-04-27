using System.Printing;
using System.Windows;
using System.Windows.Documents;

namespace PeletonSoft.Tools.View.Report
{
    public class A4LandscapeFormatPageContent : FormatPageContent
    {
        public override PageOrientation PageOrientation
        {
            get
            {
                return PageOrientation.Landscape;
            }
        }

        public override FixedPageList Pages { get; set; }

        public override void SetSize(FixedPage fixedPage)
        {
            var converter = new LengthConverter();

            // ReSharper disable once PossibleNullReferenceException
            fixedPage.Width = (double)converter.ConvertFromInvariantString("29.7cm");
            // ReSharper disable once PossibleNullReferenceException
            fixedPage.Height = (double)converter.ConvertFromInvariantString("21cm");
        }
    }
}
