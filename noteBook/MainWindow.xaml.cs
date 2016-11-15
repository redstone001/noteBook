using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
namespace noteBook
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<DayOfWeek, int> weekSeek = new Dictionary<DayOfWeek, int>() { { DayOfWeek.Monday, 0 },
                                                { DayOfWeek.Tuesday, 1 }, { DayOfWeek.Wednesday, 2 }, { DayOfWeek.Thursday, 3 },
                                                    { DayOfWeek.Friday, 4 }, { DayOfWeek.Saturday, 5 }, { DayOfWeek.Sunday, 6 } };
        public MainWindow()
        {
            InitializeComponent();

            initGrid();
           // fillMonth(DateTime.Now.Year,DateTime.Now.Month);
            
        }

        private void Item_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            Console.WriteLine("move");
        }

        private void Item_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("change");
        }



        private void Maingd_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            IInputElement ele = maingd.InputHitTest(e.GetPosition((Grid)sender));

        }

        private void fillMonth(int year,int month)
        {
            
            var day1 = new DateTime(year,month, 1);
            int seek = weekSeek[day1.DayOfWeek];
            int maxDay = DateTime.DaysInMonth(year,month);
            var now = DateTime.Now;
            for (int r = 1; r <= 7; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    var obj = maingd.FindName(string.Format("day{0}_{1}", r, c));
                    if (obj is Label)
                    {
                        Label ele = obj as Label;
                        int day = (r - 1) * 7 + c - seek + 1;
                        if (day < 1 || day > maxDay) //不存在的日期
                        {
                            ele.Content = "";
                        }
                        else
                        {
                            ele.Content = day.ToString();
                            ele.MouseDoubleClick += Ele_MouseDoubleClick;
                            ele.MouseEnter += Ele_MouseEnter;
                            ele.MouseLeave += Ele_MouseLeave;
                            
                            
                            if (now.Year == year && now.Month == month && now.Day == day)
                            {
                                //ele.BorderThickness = new Thickness(2,2,2,2);
                                //ele.FontStyle = FontStyles.Oblique;
                               
                                Ellipse es = new Ellipse();
                                es.Stroke = Brushes.Yellow;
                                es.VerticalAlignment = VerticalAlignment.Top;
                                es.HorizontalAlignment = HorizontalAlignment.Center;
                                es.StrokeThickness = 2;
                                this.LayoutUpdated += (a, arg) =>
                                {
                                    es.Width = ele.ActualWidth;
                                    es.Height = ele.ActualHeight;
                                };
                                es.SetValue(Grid.RowProperty,r);
                                es.SetValue(Grid.ColumnProperty,c);
                                ((Grid)ele.Parent).Children.Add(es);
                            }
                        }
                    }
                }
            }
        }

    

        private void Ele_MouseLeave(object sender, MouseEventArgs e)
        {
            Label lb = (Label)sender;
            if (lb.Tag is Brush)
            {
                lb.Foreground = lb.Tag as Brush;
            }
        }

        private void Ele_MouseEnter(object sender, MouseEventArgs e)
        {
            Label lb = (Label)sender;
            lb.Tag = lb.Foreground;
            lb.Foreground = Brushes.LightYellow;
        }

        private void Ele_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            db.Model.todo model = new db.Model.todo();
            model.content = "test";
            model.dateValue = DateTime.Now;
            model.deadDate = DateTime.Now;
            model.importantStar = 5;
            model.urgencyStar = 5;
            db.BLL.todo bll = new db.BLL.todo();
            if (bll.Add(model))
            {
                MessageBox.Show("添加完成");
            }

        }

        private void maingd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                base.DragMove();
            }
        }
        private void initGrid()
        {
            System.Drawing.Bitmap bitmap = Properties.Resources.plus;

            MemoryStream stream = new MemoryStream();

            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            ImageBrush imageBrush = new ImageBrush();

            ImageSourceConverter imageSourceConverter = new ImageSourceConverter();

            imageBrush.ImageSource = (ImageSource)imageSourceConverter.ConvertFrom(stream);
            

            for (int x = 1; x < 6; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    Grid gd = new Grid();
                    gd.SetValue(Grid.RowProperty,x);
                    gd.SetValue(Grid.ColumnProperty,y);
                    gd.RowDefinitions.Add(new RowDefinition());
                    gd.RowDefinitions.Add(new RowDefinition());
                    gd.RowDefinitions.Add(new RowDefinition());
                    gd.ColumnDefinitions.Add(new ColumnDefinition());
                    gd.ColumnDefinitions.Add(new ColumnDefinition());
                    gd.ColumnDefinitions.Add(new ColumnDefinition());
                    //gd.MouseEnter += Gd_MouseEnter;
                    //gd.MouseLeave += Gd_MouseLeave;
                    gd.IsMouseDirectlyOverChanged += Gd_IsMouseDirectlyOverChanged;
                    Label lb = new Label();
                    lb.Content = (x * y).ToString();
                    lb.SetValue(Grid.RowProperty,1);
                    lb.SetValue(Grid.ColumnProperty, 1);
                    gd.Children.Add(lb);

                    Button btn = new Button();
                    //btn.Content = "A";
                    btn.SetValue(Grid.RowProperty, 0);
                    btn.SetValue(Grid.ColumnProperty, 0);
                    btn.Background = imageBrush;
                    btn.BorderThickness = new Thickness(0,0,0,0);
                    btn.Visibility = Visibility.Hidden;
                    gd.Children.Add(btn);                                        
                    maingd.Children.Add(gd);
                }
            }
        }

        private void Gd_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("get");
        }

        private void Gd_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid gd = (Grid)sender;
            foreach (var item in gd.Children)
            {
                if (item is Button) { ((Button)item).Visibility = Visibility.Hidden; break; }
            }
        }

        private void Gd_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid gd = (Grid)sender;
            foreach (var item in gd.Children)
            {
                if (item is Button) { ((Button)item).Visibility = Visibility.Visible;break; }
            }
        }
    }
}
