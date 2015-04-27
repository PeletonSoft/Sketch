using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PeletonSoft.Tools.Model.Draw;

namespace PeletonSoft.Tools.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для LineView.xaml
    /// </summary>
    public partial class LineView : UserControl, ILineView
    {
        public LineView()
        {
            InitializeComponent();
        }
        public double OffsetX
        {
            get
            {
                return (double)GetValue(OffsetXProperty);
            }
            set
            {
                SetValue(OffsetXProperty, value);
            }
        }


        public static readonly DependencyProperty OffsetXProperty = DependencyProperty.Register(
          "OffsetX", typeof(double), typeof(LineView), new PropertyMetadata(0.0));


        public double OffsetY
        {
            get
            {
                return (double)GetValue(OffsetYProperty);
            }
            set
            {
                SetValue(OffsetYProperty, value);
            }
        }


        public static readonly DependencyProperty OffsetYProperty = DependencyProperty.Register(
          "OffsetY", typeof(double), typeof(LineView), new PropertyMetadata(0.0));

        public double ThumbX
        {
            get
            {
                return (double)GetValue(ThumbXProperty);
            }
            set
            {
                SetValue(ThumbXProperty, value);
            }
        }


        public static readonly DependencyProperty ThumbXProperty = DependencyProperty.Register(
          "ThumbX", typeof(double), typeof(LineView), new PropertyMetadata(0.0));

        public double ThumbY
        {
            get
            {
                return (double)GetValue(ThumbYProperty);
            }
            set
            {
                SetValue(ThumbYProperty, value);
            }
        }

        public static readonly DependencyProperty ThumbYProperty = DependencyProperty.Register(
          "ThumbY", typeof(double), typeof(LineView), new PropertyMetadata(0.0));

        public Brush LineColor
        {
            get
            {
                return (Brush)GetValue(LineColorProperty);
            }
            set
            {
                SetValue(LineColorProperty, value);
            }
        }


        public static readonly DependencyProperty LineColorProperty = DependencyProperty.Register(
          "LineColor", typeof(Brush), typeof(LineView), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            DependencyObject obj = this;
            UIElement control = null;
            Canvas canvas = null;

            while (true)
            {
                var cnv = VisualTreeHelper.GetParent(obj);

                if (cnv is Canvas)
                {
                    canvas = (Canvas)cnv;
                    control = (UIElement)obj;

                    break;
                }

                if (cnv == null)
                {
                    break;
                }

                obj = cnv;
            }

            if (canvas != null)
            {
                var p = e.GetPosition(canvas);

                ThumbX = p.X - (double)control.GetValue(Canvas.LeftProperty);
                ThumbY = p.Y - (double)control.GetValue(Canvas.TopProperty);
            }
        }

        public static readonly RoutedEvent ThumbClickEvent = EventManager.RegisterRoutedEvent(
            "ThumbClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LineView));

        // Provide CLR accessors for the event 
        public ILineViewModel GetDataContext()
        {
            try
            {
                return (ILineViewModel)DataContext;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public event RoutedEventHandler ThumbClick
        {
            add { AddHandler(ThumbClickEvent, value); }
            remove { RemoveHandler(ThumbClickEvent, value); }
        }
        private void Line_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var args = new RoutedEventArgs(ThumbClickEvent);
            RaiseEvent(args);
        }
        

    }

}
