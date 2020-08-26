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
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using AForge.Video; //uncomment
using AForge.Video.DirectShow;//uncomment
using AForge.Imaging;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using System.Media;
using AForge.Imaging.Filters;
using System.Threading;

namespace VirtualDressingRoom
{
    /// <summary>
    /// Interaction logic for genderDetection.xaml
    /// </summary>
    public partial class genderDetection : Window
    {
        bool issDetected = true;
        Image<Bgr, byte> Img;
        ImageSource ImageSourcee;
        public static Bitmap globalBitmap;
        Bitmap img;
        private HaarCascade male, frontal_face, female;
        bool isMale = false;
        bool isFemale = false;
        bool isBoolfemale = false;
        bool isDetected = false;
        bool isFinal = false;
        public static bool finalGender = false;
        Bitmap Image;
        System.Windows.Threading.DispatcherTimer dispatchTimer;// = new System.Windows.Threading.DispatcherTimer();
        Stopwatch maleTimer;
        bool checkingFromImage = false;
        bool isCaptured = false;
        public static string connectionString = @"Data Source=DESKTOP-J1CMG8D\SQLEXPRESS;Initial Catalog='Exclate Persona';Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        Bitmap globalBitmap1 = new Bitmap(500, 600);

        public genderDetection()
        {
            InitializeComponent();
            dispatchTimer = new System.Windows.Threading.DispatcherTimer();
            dispatchTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatchTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dispatchTimer.Start();
            dispatchTimer.IsEnabled = false;
            checkingFromImage = true;
            male = new HaarCascade("Latest.xml");
            female = new HaarCascade("female.xml");

            isMale = false;
            isFemale = false;
            isDetected = false;
            isFinal = false;
            label1.Content = "".ToString();

            frontal_face = new HaarCascade("haarcascade_frontalface_default.xml");
            maleTimer = new Stopwatch();
            label1.Content = "Not Detected Yet";
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (isCaptured == true)
            {
                Image = globalBitmap1;
                if (checkingFromImage)
                {
                    Img = new Image<Bgr, byte>(Image);
                    Image<Bgr, byte> nextFrame = Img;
                    Image<Gray, byte> grayframe_frontal_face = nextFrame.Convert<Gray, byte>();
                    var frontal_facefaces = grayframe_frontal_face.DetectHaarCascade(frontal_face, 1.4, 4, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                    new System.Drawing.Size(nextFrame.Width / 8, nextFrame.Height / 8))[0];

                    foreach (var face in frontal_facefaces)
                    {
                        label.Content = "face detected";
                        nextFrame.Draw(face.rect, new Bgr(System.Drawing.Color.RoyalBlue), 5);
                        isDetected = true;
                    }


                    if (!maleTimer.IsRunning)
                        maleTimer.Start();

                    if (maleTimer.Elapsed.Milliseconds <= 1200 && isMale == false && isFemale == false)
                    {
                        if (maleTimer.Elapsed.Milliseconds == 1200)
                            maleTimer.Reset();

                        if (maleTimer.Elapsed.Milliseconds <= 1000)
                        {
                            Image<Gray, byte> grayframe_gender = nextFrame.Convert<Gray, byte>();
                            var gender_detection = grayframe_frontal_face.DetectHaarCascade(female, 1.4, 4, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                            new System.Drawing.Size(nextFrame.Width / 8, nextFrame.Height / 8))[0];

                            foreach (var face in gender_detection)
                            {
                                if (isDetected)
                                {
                                    isFemale = true;
                                }
                            }
                        }

                        else if (maleTimer.Elapsed.Milliseconds > 1000)
                        {
                            //Image<Gray, byte> grayframe_gender = nextFrame.Convert<Gray, byte>();
                            //var gender_detection = grayframe_frontal_face.DetectHaarCascade(male, 1.4, 4, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                            //new System.Drawing.Size(nextFrame.Width / 8, nextFrame.Height / 8))[0];

                            //foreach (var face in gender_detection)
                            //{
                            if (isDetected)
                            {
                                isMale = true;
                            }
                            // }
                        }
                    }
                }
            }

            if (isMale && isFinal == false)
            {
                // label1.Content = "MALE".ToString();
                isFinal = true;
            }

            if (isFemale && isFinal == false)
            {
                label1.Content = "FEMALE".ToString();
                isFinal = true;
            }
        }

        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> bodies;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _sensor.Open();
                _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);

                _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
            }
        }
        private void initframe()
        {
            _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
            _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
        }

        int counter = 0;
        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {

            var reference = e.FrameReference.AcquireFrame();
            // Color Camera Code

            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null && isCaptured == false)
                {
                    feed.Source = frame.ToBitmap();
                    ImageSourcee = feed.Source;
                }
            }

            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {

                    bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(bodies);

                    foreach (var body in bodies)
                    {
                        if (body != null)
                        {
                            if (body.IsTracked)
                            {
                                label.Content = "detected";
                                bool check1 = false;
                                bool check2 = false;
                                // Find the joints
                                Joint handRight = body.Joints[JointType.HandRight];
                                Joint thumbRight = body.Joints[JointType.ThumbRight];
                                Joint handLeft = body.Joints[JointType.HandLeft];
                                Joint thumbLeft = body.Joints[JointType.ThumbLeft];
                                Joint shoulder = body.Joints[JointType.ShoulderRight];
                                Joint shoulderl = body.Joints[JointType.ShoulderLeft];
                                Joint head = body.Joints[JointType.Head];

                                // Menu Right
                                if (thumbRight.Position.X < shoulderl.Position.X)
                                {
                                    for (int i = 0; i < 2000; i++)
                                    { Debug.WriteLine("relax"); }

                                    select_female();
                                }
                                if (thumbLeft.Position.X > shoulder.Position.X)
                                {
                                    for (int i = 0; i < 2000; i++)
                                    { Debug.WriteLine("relax"); }
                                    select_male();
                                }
                            }
                        }
                    }

                }
            }
        }

        private void buttonFemale_Click(object sender, RoutedEventArgs e)
        {
            select_female();
        }
        void select_female()
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
            this.Hide();
            this.IsEnabled = false;

            finalGender = false;
            MainWindow mw = new MainWindow(0);
            mw.Visibility = Visibility.Visible;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            globalBitmap = Extensions.GetBitmap((BitmapSource)ImageSourcee);
            globalBitmap.Save("qwer.jpg");

            Bitmap bmpFin = new Bitmap(globalBitmap);

            Crop filter1 = new Crop(new System.Drawing.Rectangle(800, 200, 500, 600));
            globalBitmap1 = filter1.Apply(bmpFin);

            feed.Source = Extensions.Convert(globalBitmap1);

            isCaptured = true;
            // }
            //catch(Exception E)
            //{

            //}
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            isCaptured = false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            select_male();
        }

        void some()
        {
            Thread.Sleep(800);
            label1.Content = "Male";
        }
        void some2()
        {
            Thread.Sleep(800);
            label1.Content = "Female";
        }
        private FilterInfoCollection videoDevices;
        private bool DeviceExist = false;

        private VideoCaptureDevice videoSource = null;

        Bitmap bmp;
        bool isFacedetected = false;
        System.Drawing.Rectangle rect;

        private void funcCropping(Bitmap path)
        {
            bmp = new Bitmap(path);

            Image<Bgr, byte> image = new Image<Bgr, byte>(bmp);
            Image<Gray, byte> grayframe = image.Convert<Gray, byte>();
            var faces =
                    grayframe.DetectHaarCascade(
                            frontal_face, 1.4, 4,
                            HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                            new System.Drawing.Size(grayframe.Width / 8, grayframe.Height / 8)
                            )[0];
            foreach (var Face in faces)
            {
                rect = new System.Drawing.Rectangle(Face.rect.X, Face.rect.Y, Face.rect.Width, Face.rect.Height);
                isFacedetected = true;
            }
            if (isFacedetected)
            {
                System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Red, 3);
                Crop filter = new Crop(new System.Drawing.Rectangle(rect.Left - 150, rect.Top - 100, 150 + rect.Width + 150, 100 + rect.Height + 250));
                bmp = filter.Apply(bmp);
                bmp.Save("qwer.jpg");
                globalBitmap1 = bmp;
            }
            else
            {
                issDetected = false;
                //feed.Source = Extensions.Convert(new Bitmap("molvi.jpg"));
                //MessageBox.Show("Face Not detected, Please recapture.");
            }
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            globalBitmap = Extensions.GetBitmap((BitmapSource)ImageSourcee);

            funcCropping(globalBitmap);
            feed.Source = Extensions.Convert(globalBitmap1);
            globalBitmap1.Save("someshit.jpg");
            //globalBitmap1 = new Bitmap("female1.jpg");
            isCaptured = true;
            //if (isDetected)
            some();
        }

        private void button1_Copy_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            globalBitmap = Extensions.GetBitmap((BitmapSource)ImageSourcee);

            funcCropping(globalBitmap);
            feed.Source = Extensions.Convert(globalBitmap1);
            //globalBitmap1 = new Bitmap("female1.jpg");
            isCaptured = true;
            //if (issDetected)
            some2();
        }

        void select_male()
        {

            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
            this.Hide();
            this.IsEnabled = false; ///////////////////
            finalGender = true;
            mainWindowMale mwm = new mainWindowMale();
            mwm.Visibility = Visibility.Visible;

        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
            this.Hide();
            this.IsEnabled = false;
        }
    }
}
