using System.Windows;
using System.Windows.Documents;
using System.Windows.Interactivity;
using PeletonSoft.Tools.View.Controls;
using PeletonSoft.Tools.View.Report;

namespace PeletonSoft.Tools.View.Behavior
{
    public class AddPageContentBehavior : Behavior<FixedDocumentViewer>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            var document = new FixedDocument();

            if (FormatPageContent?.Pages == null)
            {
                return;
            }

            foreach (var page in FormatPageContent.Pages)
            {
                FormatPageContent.SetSize(page);
                var pageContent = new PageContent
                {
                    Child = page
                };
                document.Pages.Add(pageContent);
            }

            AssociatedObject.PageOrientation = FormatPageContent.PageOrientation;
            AssociatedObject.Document = document;
        }

        public FormatPageContent FormatPageContent
        {
            get { return (FormatPageContent) GetValue(FormatPageContentProperty); }
            set { SetValue(FormatPageContentProperty, value); }
        }

        public static readonly DependencyProperty FormatPageContentProperty = DependencyProperty.Register(
          nameof(FormatPageContent), typeof(FormatPageContent), typeof(AddPageContentBehavior), new PropertyMetadata(null));
    }
}
