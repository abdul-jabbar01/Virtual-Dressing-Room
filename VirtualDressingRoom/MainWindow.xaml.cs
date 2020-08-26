using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VirtualDressingRoom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members

        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> _bodies;
        int gender;
        #endregion

        #region Constructor
        string rightHandState1 = "";
        string rightHandState2 = "";
        string leftHandState1 = "";
        string leftHandState2 = "";
        int point = 1;
        public MainWindow(int gender)
        {
            InitializeComponent();
            this.gender = gender;
            if (gender == 1)
            {
                //label5.Content = "Main Menu Male";
                image1.Visibility = Visibility.Hidden;

                image1_Copy.Visibility = Visibility.Hidden;
                image1_Copy1.Visibility = Visibility.Hidden;

            }
            else
            {
                //label5.Content = "Main Menu Female";


            }
        }

        #endregion

        #region Event handlers


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
        void leftmove(int val, int loop)
        {
            int b = 0;
            Debug.WriteLine("Left Hand Moved Left");
            for (int i = 0; i < loop; i++)
            {
                b++;
                Debug.WriteLine(" ");
            }
            point--;
            if (val == 1)
            {
                grid1.Margin = new Thickness(64, 350, 0, 0);
                //txt1.Margin = new Thickness(117, 650, 0, 0);
                grid4.Margin = new Thickness(1030, 300, 0, 0);
                // txt4.Margin = new Thickness(1191, 600, 0, 0);
                point = 4;
            }
            else if (val == 2)
            {
                grid2.Margin = new Thickness(386, 350, 0, 0);
                // txt2.Margin = new Thickness(486, 650, 0, 0);
                grid1.Margin = new Thickness(64, 300, 0, 0);
                // txt1.Margin = new Thickness(117, 600, 0, 0);
            }
            else if (val == 3)
            {
                grid3.Margin = new Thickness(708, 350, 0, 0);
                // txt3.Margin = new Thickness(846, 650, 0, 0);
                grid2.Margin = new Thickness(386, 300, 0, 0);
                //txt2.Margin = new Thickness(486, 600, 0, 0);
            }
            else if (val == 4)
            {
                // txt4.Margin = new Thickness(1191, 650, 0, 0);
                grid4.Margin = new Thickness(1030, 350, 0, 0);
                grid3.Margin = new Thickness(708, 300, 0, 0);
                // txt3.Margin = new Thickness(846, 600, 0, 0);
            }
            else
            {

            }
            //int a = 0;
            //Debug.WriteLine("Right Hand Moved Right");
            //for (int i = 0; i < 3000; i++)
            //{
            //    a++;
            //    Debug.WriteLine(" ");
            //}

        }
        void rightmove(int val, int loop)
        {
            int b = 0;
            Debug.WriteLine("Left Hand Moved Left");
            for (int i = 0; i < loop; i++) //3000
            {
                b++;
                Debug.WriteLine(" ");
            }
            point++;
            if (val == 1)
            {
                grid1.Margin = new Thickness(64, 350, 0, 0);
                grid2.Margin = new Thickness(386, 300, 0, 0);
            }
            else if (val == 2)
            {
                grid2.Margin = new Thickness(386, 350, 0, 0);
                // txt2.Margin = new Thickness(486, 650, 0, 0);
                grid3.Margin = new Thickness(708, 300, 0, 0);
                // txt3.Margin = new Thickness(846, 600, 0, 0);
            }
            else if (val == 3)
            {
                grid3.Margin = new Thickness(708, 350, 0, 0);
                // txt3.Margin = new Thickness(846, 650, 0, 0);
                grid4.Margin = new Thickness(1030, 300, 0, 0);
                // txt4.Margin = new Thickness(1191,600,0,0);
            }
            else if (val > 3)
            {
                point = 1;
                // txt4.Margin = new Thickness(1414,650,0,0);
                grid4.Margin = new Thickness(1030, 350, 0, 0);
                grid1.Margin = new Thickness(64, 300, 0, 0);
                // txt1.Margin = new Thickness(117,600,0,0);
            }
            else
            {
                point = 1;
            }
            //int a = 0;
            //Debug.WriteLine("Right Hand Moved Right");
            //for (int i = 0; i < 3000; i++)
            //{
            //    a++;
            //    Debug.WriteLine(" ");
            //}

        }
        void initframe()
        {
            _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
            _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
        }
        void grabbed()
        {
            int c = 0;
            Debug.WriteLine("Grabbed");
            for (int i = 0; i < 1; i++) //3000
            {
                c++;
                Debug.WriteLine(" ");
            }
        }
        void back()
        {
            int d = 0;
            Debug.WriteLine("Back to Pavilion");
            for (int i = 0; i < 1; i++) //3000
            {
                d++;
                Debug.WriteLine(" ");
            }
        }
        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();
            // Color Camera Code
            //using (var frame = reference.ColorFrameReference.AcquireFrame())
            //{
            //    if (frame != null)
            //    {
            //        camera.Source = frame.ToBitmap();
            //    }
            //}

            // Body
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    //    canvas.Children.Clear();

                    _bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(_bodies);

                    foreach (var body in _bodies)
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
                                Joint wristleft = body.Joints[JointType.WristLeft];
                                Joint wristright = body.Joints[JointType.WristRight];
                                Joint head = body.Joints[JointType.Head];


                                //wave right
                                // Hand above elbow
                                if (body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.ElbowRight].Position.Y)
                                {
                                    // Hand right of elbow
                                    if (body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.ElbowRight].Position.X)
                                    {
                                        rightmove(point, 0);
                                        for (int i = 0; i < 2000; i++)
                                        {
                                            Debug.WriteLine("relax");
                                        }
                                    }

                                    // Hand has not dropped but is not quite where we expect it to be, pausing till next frame
                                    //return GesturePartResult.Undetermined;
                                }
                                //waveleft
                                // hand above elbow
                                if (body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.ElbowLeft].Position.Y)
                                {
                                    // hand right of elbow
                                    if (body.Joints[JointType.HandLeft].Position.X < body.Joints[JointType.ElbowLeft].Position.X)
                                    {
                                        // return GesturePartResult.Succeeded;
                                        leftmove(point, 0);
                                        for (int i = 0; i < 2000; i++)
                                        {
                                            Debug.WriteLine("relax");
                                        }
                                    }

                                    // hand has not dropped but is not quite where we expect it to be, pausing till next frame
                                    //return GesturePartResult.Undetermined;
                                }

                                ////Menu Right
                                //if (thumbRight.Position.X < shoulderl.Position.X)
                                //{
                                //    rightmove(point, 0);
                                //    for (int i = 0; i < 2000; i++)
                                //    {
                                //        Debug.WriteLine("relax");
                                //    }
                                //}
                                ////menuLeft
                                //if (thumbLeft.Position.X > shoulder.Position.X)
                                //{
                                //    leftmove(point, 0);
                                //    for (int i = 0; i < 2000; i++)
                                //    {
                                //        Debug.WriteLine("relax");
                                //    }
                                //}


                                string rightHandState = "-";
                                switch (body.HandRightState)
                                {
                                    case HandState.Open:
                                        rightHandState = "Open";
                                        break;
                                    case HandState.Closed:
                                        rightHandState = "Closed";
                                        break;
                                    case HandState.Lasso:
                                        rightHandState = "Lasso";
                                        break;
                                    case HandState.Unknown:
                                        rightHandState = "Unknown...";
                                        break;
                                    case HandState.NotTracked:
                                        rightHandState = "Not tracked";
                                        break;
                                    default:
                                        break;
                                }
                                string leftHandState = "-";
                                switch (body.HandLeftState)
                                {
                                    case HandState.Open:
                                        leftHandState = "Open";
                                        break;
                                    case HandState.Closed:
                                        leftHandState = "Closed";
                                        break;
                                    case HandState.Lasso:
                                        leftHandState = "Lasso";
                                        break;
                                    case HandState.Unknown:
                                        leftHandState = "Unknown...";
                                        break;
                                    case HandState.NotTracked:
                                        leftHandState = "Not tracked";
                                        break;
                                    default:
                                        break;
                                }
                                //SELECT MENU
                                if (rightHandState == "Closed" && handRight.Position.Y > head.Position.Y)
                                {
                                    for (int i = 0; i < 2000; i++)
                                    {
                                        Debug.WriteLine("relax");
                                    }
                                    if (_reader != null)
                                    {
                                        _reader.Dispose();
                                    }

                                    if (_sensor != null)
                                    {
                                        _sensor.Close();
                                    }

                                    select(point);
                                }
                                //Select Back
                                if (leftHandState == "Closed" && handLeft.Position.Y > head.Position.Y)
                                {
                                    for (int i = 0; i < 2000; i++)
                                    {
                                        Debug.WriteLine("relax");
                                    }
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
                                    genderDetection genderObject = new genderDetection();
                                    genderObject.Visibility = Visibility.Visible;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        private void btnRightGesture_Click(object sender, RoutedEventArgs e)
        {
            rightmove(point, 0);
        }

        private void btnSelection_Click(object sender, RoutedEventArgs e)
        {
            select(point);
        }
        private void select(int point)
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
            if (point == 1)
            {
                if (gender == 0)
                {
                    this.Hide();
                    this.IsEnabled = false;
                    tryLipstick tl = new tryLipstick(); tl.Visibility = Visibility.Visible;
                }

            }
            else if (point == 2)
            {
                try
                {
                    this.Hide();
                    this.IsEnabled = false;
                    tryClothes tc = new tryClothes();
                    tc.Visibility = Visibility.Visible;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Unknown Error occured. ");
                }
            }
            else if (point == 3)
            {
                this.Hide();
                this.IsEnabled = false;
                tryHair th = new tryHair();
                th.Visibility = Visibility.Visible;
            }
            else if (point == 4)
            {
                this.Hide();
                this.IsEnabled = false;
                tryEye te = new tryEye();
                te.Visibility = Visibility.Visible;
            }
            else
            {
                point = 1;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            leftmove(point, 0);
        }

        private void button_Click(object sender, RoutedEventArgs e)
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
            genderDetection genderObject = new genderDetection();
            genderObject.Visibility = Visibility.Visible;
        }
    }
}
