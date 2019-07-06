using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using OpenCvSharp.CPlusPlus;

namespace XuLyAnh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #region PropertyChanged for Binding Wpf
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string obj)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(obj));
        }
        #endregion

        private string imgSrc;
        public string ImgSrc
        {
            get { return imgSrc; }
            set { imgSrc = value; OnPropertyChanged("ImgSrc"); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ImgSrc = @"E:\Anh\a.jpg";
        }

        private void btnXuLy_Click(object sender, RoutedEventArgs e)
        {
            Mat matGoc = OpenCvSharp.CPlusPlus.Cv2.ImRead(@"E:\Anh\chamDen.jpg", OpenCvSharp.LoadMode.Color);
            Cv2.NamedWindow("Color");
            Cv2.ImShow("Color", matGoc);

            //Convert Color To GrayScale
            Mat anhXam = new Mat(),thresh=new Mat();
            Cv2.CvtColor(matGoc, anhXam, OpenCvSharp.ColorConversion.BgrToGray);
            Cv2.NamedWindow("Gray");
            Cv2.ImShow("Gray", anhXam);

            // Threshold
            Cv2.Threshold(anhXam,thresh,100,255,OpenCvSharp.ThresholdType.BinaryInv);
            Cv2.NamedWindow("Thresh");
            Cv2.ImShow("Thresh", thresh);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
