using System.Collections.Generic;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Markup;

namespace PeletonSoft.Tools.View.Report
{
    [ContentProperty("Pages")]
    public abstract class FormatPageContent
    {
        public abstract PageOrientation PageOrientation { get; }
        public abstract FixedPageList Pages { get; set; }
        public abstract void SetSize(FixedPage fixedPage);
    }

    public class FixedPageList : List<FixedPage>
    {
    }
}
