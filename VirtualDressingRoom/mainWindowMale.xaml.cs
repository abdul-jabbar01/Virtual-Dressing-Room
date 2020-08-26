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

namespace VirtualDressingRoom
{
    /// <summary>
    /// Interaction logic for mainWindowMale.xaml
    /// </summary>
    public partial class mainWindowMale : Window
    {
        int pointmale = 1;
        #region Members

        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> bodies;
        int gender;
        #endregion

        #region Constructor
        string rightHandState1 = "";
        string rightHandState2 = "";
        string leftHandState1 = "";
        string leftHandState2 = "";
        #endregion
        public mainWindowMale()
        {
            InitializeComponent();
        }
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
        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            // Body
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

                                //wave right
                                // Hand above elbow
                                if (body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.ElbowRight].Position.Y)
                                {
                                    // Hand right of elbow
                                    if (body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.ElbowRight].Position.X)
                                    {
                                        moverightmale(pointmale);
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
                                        moveleftmale(pointmale);
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
                                //    moverightmale(pointmale);
                                //    for (int i = 0; i < 2000; i++)
                                //    {
                                //        Debug.WriteLine("relax");
                                //    }
                                //}
                                ////menuLeft
                                //if (thumbLeft.Position.X > shoulder.Position.X)
                                //{
                                //    moveleftmale(pointmale);
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

                                    btnselectionmale(pointmale);
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

        void initframe()
        {
            _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
            _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
        }
        void grabbed()
        {
            int c = 0;
            Debug.WriteLine("Grabbed");
            for (int i = 0; i < 3000; i++)
            {
                c++;
                Debug.WriteLine(" ");
            }
        }
        private void mwmbtnRightGesture_Click(object sender, RoutedEventArgs e)
        {
            moverightmale(pointmale);
        }

        private void moverightmale(int val)
        {
            if (val == 2)
            {
                mwmgrid1.Margin = new Thickness(386, 300, 0, 0);
                mwmgrid3.Margin = new Thickness(708, 350, 0, 0);
                pointmale = 1;
            }
            else if (val == 1)
            {
                mwmgrid1.Margin = new Thickness(386, 350, 0, 0);
                mwmgrid3.Margin = new Thickness(708, 300, 0, 0);
                pointmale = 2;
            }
            else
            {
            }
        }

        private void mwmbtnSelection_Click(object sender, RoutedEventArgs e)
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }

            btnselectionmale(pointmale);
        }

        private void btnselectionmale(int val)
        {
            if (val == 1)
            {
                this.Hide();
                this.IsEnabled = false;
                tryClothes tc = new tryClothes();
                tc.Visibility = Visibility.Visible;
            }
            else if (val == 2)
            {
                this.Hide();
                this.IsEnabled = false;
                tryEye te = new tryEye();
                te.Visibility = Visibility.Visible;
            }
            else
            {

            }
        }

        private void mwmbtnBack_Click(object sender, RoutedEventArgs e)
        {
            moveleftmale(pointmale);
        }
        private void moveleftmale(int val)
        {
            if (val == 2)
            {
                mwmgrid1.Margin = new Thickness(386, 300, 0, 0);
                mwmgrid3.Margin = new Thickness(708, 350, 0, 0);
                pointmale = 1;
            }
            else if (val == 1)
            {
                mwmgrid1.Margin = new Thickness(386, 350, 0, 0);
                mwmgrid3.Margin = new Thickness(708, 300, 0, 0);
                pointmale = 2;
            }
            else
            {

            }
        }

        private void mwmbutton_Click(object sender, RoutedEventArgs e)
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
