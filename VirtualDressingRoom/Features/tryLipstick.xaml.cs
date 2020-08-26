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
using Microsoft.Kinect.Face;
using System.Drawing;
using AForge.Imaging.Filters;
using System.Data;
using System.Data.SqlClient;

namespace VirtualDressingRoom
{
    /// <summary>
    /// Interaction logic for tryLipstick.xaml
    /// </summary>
    public partial class tryLipstick : Window
    {
        string[] brand = new string[4];
        string[] price = new string[4];
        string[] colorname = new string[4];
        string[] stock = new string[4];
        string[] description = new string[4];
        KinectSensor sensor;
        MultiSourceFrameReader read;
        IList<Body> _bodies;
        string rightHandState1 = "";
        string rightHandState2 = "";
        string leftHandState1 = "";
        string leftHandState2 = "";
        ColorSpacePoint mouthLeft;
        ColorSpacePoint mouthRight;
        ColorSpacePoint UpperLipMidTop;
        ColorSpacePoint UpperLipMidBottom;
        ColorSpacePoint LowerLipMidTop;
        ColorSpacePoint LowerLipMidBottom;
        ColorSpacePoint lowerM1;
        ColorSpacePoint lowerM2;
        ColorSpacePoint upperM1;
        ColorSpacePoint upperM2;
        ColorSpacePoint upperM3;
        ColorSpacePoint upperM4;
        private BodyFrameSource _bodySource = null;
        private BodyFrameReader _bodyReader = null;
        private HighDefinitionFaceFrameSource _faceSource = null;
        private HighDefinitionFaceFrameReader _faceReader = null;
        private FaceAlignment _faceAlignment = null;
        private FaceModel _faceModel = null;
        private List<Ellipse> _points = new List<Ellipse>();
        int where = 0;
        int[] colorArrayRed = new int[4];
        int[] colorArrayGreen = new int[4];
        int[] colorArrayBlue = new int[4];
        int red;
        int where1 = 1;
        int green;
        int blue;
        int frameCount = 0;
        bool isFaceDetected = false;
        public tryLipstick()
        {
            InitializeComponent();
            //getinfo();
            //postinfo();
            mouthLeft.X = 0;
            mouthLeft.Y = 0;
            mouthRight.X = 0;
            UpperLipMidBottom.X = 0;
            UpperLipMidTop.X = 0;
            LowerLipMidBottom.X = 0;
            LowerLipMidTop.X = 0;

            mouthRight.Y = 0;
            UpperLipMidBottom.Y = 0;
            UpperLipMidTop.Y = 0;
            LowerLipMidBottom.Y = 0;
            LowerLipMidTop.Y = 0;
            lowerM1.X = 0;
            lowerM1.Y = 0;
            lowerM2.X = 0;
            lowerM2.Y = 0;
            upperM1.X = 0;
            upperM1.Y = 0;
            upperM2.X = 0;
            upperM2.Y = 0;
            upperM3.X = 0;
            upperM3.Y = 0;
            upperM4.X = 0;
            upperM4.Y = 0;
            colorArrayRed[0] = 117;
            colorArrayGreen[0] = 0;
            colorArrayBlue[0] = 0;


            colorArrayRed[1] = 89;
            colorArrayGreen[1] = 6;
            colorArrayBlue[1] = 70;

            colorArrayRed[2] = 95;
            colorArrayGreen[2] = 23;
            colorArrayBlue[2] = 20;



            colorArrayRed[3] = 218;
            colorArrayGreen[3] = 80;
            colorArrayBlue[3] = 81;

            red = 117;
            green = 0;
            blue = 0;


        }

        /// //////////////
        /// 
        private void BodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    Body[] bodies = new Body[frame.BodyCount];
                    frame.GetAndRefreshBodyData(bodies);

                    Body body = bodies.Where(b => b.IsTracked).FirstOrDefault();

                    if (!_faceSource.IsTrackingIdValid)
                    {
                        if (body != null)
                        {
                            _faceSource.TrackingId = body.TrackingId;
                        }
                    }
                }
            }
        }
        private void FaceReader_FrameArrived(object sender, HighDefinitionFaceFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null && frame.IsFaceTracked)
                {
                    if (frameCount % 1 == 0)
                    {

                        frame.GetAndRefreshFaceAlignmentResult(_faceAlignment);
                        UpdateFacePoints();
                    }
                    else if (isFaceDetected == false)
                    {
                        frame.GetAndRefreshFaceAlignmentResult(_faceAlignment);
                        UpdateFacePoints();
                    }
                }
            }
        }
        private void UpdateFacePoints()
        {
            if (_faceModel == null) return;

            IReadOnlyList<CameraSpacePoint> vertices = _faceModel.CalculateVerticesForAlignment(_faceAlignment);

            if (vertices.Count > 0)
            {
                if (_points.Count == 0)
                {
                    for (int index = 0; index < vertices.Count; index++)
                    {
                        if (index == 91)
                            mouthLeft = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);
                        else if (index == 687)
                            mouthRight = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);
                        else if (index == 19)
                            UpperLipMidTop = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);
                        else if (index == 1072)
                            UpperLipMidBottom = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);
                        else if (index == 10)
                            LowerLipMidTop = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);
                        else if (index == 8)
                            LowerLipMidBottom = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);
                        else if (index == 519)
                            lowerM1 = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);
                        else if (index == 644)
                            lowerM2 = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);
                        else if (index == 485)
                            upperM1 = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);
                        else if (index == 191)
                            upperM2 = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);
                        else if (index == 813)
                            upperM3 = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);
                        else if (index == 761)
                            upperM4 = sensor.CoordinateMapper.MapCameraPointToColorSpace(vertices[index]);

                    }
                    isFaceDetected = true;
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("buhuhu");
            sensor = KinectSensor.GetDefault();
            if (sensor != null)
            {
                _bodySource = sensor.BodyFrameSource;
                _bodyReader = _bodySource.OpenReader();
                _bodyReader.FrameArrived += BodyReader_FrameArrived;

                _faceSource = new HighDefinitionFaceFrameSource(sensor);

                _faceReader = _faceSource.OpenReader();
                _faceReader.FrameArrived += FaceReader_FrameArrived;

                _faceModel = new FaceModel();
                _faceAlignment = new FaceAlignment();
                sensor.Open();

                read = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                read.MultiSourceFrameArrived += Read_MultiSourceFrameArrived;
            }
        }
        private void initframe()
        {
            read = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
            read.MultiSourceFrameArrived += Read_MultiSourceFrameArrived;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (read != null)
            {
                read.Dispose();
            }

            if (sensor != null)
            {
                sensor.Close();
            }
        }

        void Read_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (isFaceDetected == true)
                    {
                        Bitmap image1 = Extensions.GetBitmap((BitmapSource)frame.ToBitmap());
                        ApplyLipstick(image1);
                        frameCount++;
                    }
                    else
                        camera.Source = frame.ToBitmap();
                }
            }

            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {

                    canvas.Children.Clear();

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
                                Joint head = body.Joints[JointType.Head];
                                //Right Hand Gesture for previous 
                                if (handRight.Position.Y > head.Position.Y && handLeft.Position.Y < head.Position.Y)
                                {
                                    check1 = true;
                                }
                                else
                                {
                                    check1 = false;
                                }
                                if (check1 == true)
                                {
                                    upClick();
                                    for (int i = 0; i < 2000; i++)
                                        Debug.WriteLine("relax");
                                }

                                //LEFT hand gesture for next

                                if (handLeft.Position.Y > head.Position.Y && handRight.Position.Y < head.Position.Y)
                                {
                                    check2 = true;
                                }
                                else
                                {
                                    check2 = false;
                                }
                                if (check2 == true)
                                {
                                    downClick();
                                    for (int i = 0; i < 2000; i++)
                                        Debug.WriteLine("relax");

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
                                //Select Back
                                if (leftHandState == "Closed" && handLeft.Position.Y > head.Position.Y)
                                {
                                    for (int i = 0; i < 2000; i++)
                                    {
                                        Debug.WriteLine("relax");
                                    }
                                    if (read != null)
                                    {
                                        read.Dispose();
                                    }

                                    if (sensor != null)
                                    {
                                        sensor.Close();
                                    }

                                    if (genderDetection.finalGender == false)
                                    {
                                        this.Hide();
                                        this.IsEnabled = false;
                                        MainWindow genderObject = new MainWindow(0);
                                        genderObject.Visibility = Visibility.Visible;
                                    }
                                    else
                                    {
                                        this.Hide();
                                        this.IsEnabled = false;
                                        mainWindowMale genderObject = new mainWindowMale();
                                        genderObject.Visibility = Visibility.Visible;
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        private void moverightlips(int val)
        {
            if (val == 0)
            {
                firstEffect.BlurRadius = 3;
                secondEffect.BlurRadius = 10;
                camera.Source = lip2.Source;
                where = 1;
            }
            else if (val == 1)
            {
                secondEffect.BlurRadius = 3;
                thirdEffect.BlurRadius = 10;
                camera.Source = lip3.Source;
                where = 2;
            }
            else if (val == 2)
            {
                thirdEffect.BlurRadius = 3;
                fourthEffect.BlurRadius = 10;
                camera.Source = lip4.Source;
                where = 3;
            }
            else if (val == 3)
            {
                fourthEffect.BlurRadius = 3;
                firstEffect.BlurRadius = 10;
                camera.Source = lip1.Source;
                where = 0;
            }

        }
        private void moveleftlips(int val)
        {
            if (val == 0)
            {
                firstEffect.BlurRadius = 3;
                fourthEffect.BlurRadius = 10;
                where = 3;
                camera.Source = lip4.Source;
            }
            else if (val == 1)
            {
                secondEffect.BlurRadius = 3;
                firstEffect.BlurRadius = 10;
                camera.Source = lip1.Source;
                where = 0;
            }
            else if (val == 2)
            {
                thirdEffect.BlurRadius = 3;
                secondEffect.BlurRadius = 10;
                camera.Source = lip2.Source;
                where = 1;
            }
            else if (val == 3)
            {
                fourthEffect.BlurRadius = 3;
                thirdEffect.BlurRadius = 10;
                camera.Source = lip3.Source;
                where = 2;
            }


        }


        void ApplyLipstick(Bitmap image)
        {
            Graphics g = Graphics.FromImage(image);

            System.Drawing.Point[] points = new System.Drawing.Point[3];
            points[0].X = (int)mouthLeft.X;
            points[0].Y = (int)mouthLeft.Y;
            points[1].X = (int)lowerM1.X;
            points[1].Y = (int)lowerM1.Y;
            points[2].X = (int)LowerLipMidBottom.X;
            points[2].Y = (int)LowerLipMidBottom.Y;
            g.DrawCurve(new System.Drawing.Pen(System.Drawing.Color.Black), points);

            points[2].X = (int)mouthRight.X;
            points[2].Y = (int)mouthRight.Y;
            points[1].X = (int)lowerM2.X;
            points[1].Y = (int)lowerM2.Y;
            points[0].X = (int)LowerLipMidBottom.X;
            points[0].Y = (int)LowerLipMidBottom.Y;
            g.DrawCurve(new System.Drawing.Pen(System.Drawing.Color.Black), points);

            points = new System.Drawing.Point[4];
            points[0].X = (int)mouthLeft.X;
            points[0].Y = (int)mouthLeft.Y;
            points[1].X = (int)upperM1.X;
            points[1].Y = (int)upperM1.Y;
            points[2].X = (int)upperM2.X;
            points[2].Y = (int)upperM2.Y;
            points[3].X = (int)UpperLipMidTop.X;
            points[3].Y = (int)UpperLipMidTop.Y;
            g.DrawCurve(new System.Drawing.Pen(System.Drawing.Color.Black), points);

            points[3].X = (int)mouthRight.X;
            points[3].Y = (int)mouthRight.Y;
            points[2].X = (int)upperM4.X;
            points[2].Y = (int)upperM4.Y;
            points[1].X = (int)upperM3.X;
            points[1].Y = (int)upperM3.Y;
            points[0].X = (int)UpperLipMidTop.X;
            points[0].Y = (int)UpperLipMidTop.Y;
            g.DrawCurve(new System.Drawing.Pen(System.Drawing.Color.Black), points);


            int x = (int)mouthLeft.X;
            int y = (int)UpperLipMidTop.Y - 10;
            int height = (int)LowerLipMidBottom.Y + 10 - (int)UpperLipMidTop.Y + 10;
            int width = (int)mouthRight.X - (int)mouthLeft.X;

            System.Drawing.Color previousColor = image.GetPixel((int)UpperLipMidTop.X, ((int)UpperLipMidBottom.Y + (int)UpperLipMidTop.Y) / 2);

            int diffR = red - previousColor.R;
            int diffG = green - previousColor.G;
            int diffB = blue - previousColor.B;

            bool check = false;
            int red1 = 255;
            int green1 = 255;
            int blue1 = 255;
            int ccv = 0;

            for (int i = x + 1; i < (x + width); i++)
            {
                check = false;

                for (int j = y; j < (y + height); j++)
                {
                    System.Drawing.Color ccX = image.GetPixel(i, j);
                    if (ccX.R == 0 && ccX.G == 0 && ccX.B == 0)
                    {
                        if (check == false)
                            check = true;
                        else
                            break;
                    }
                    else if (check == true)
                    {
                        red1 = ccX.R + diffR;
                        green1 = ccX.G + diffG;
                        blue1 = ccX.B + diffB;

                        if (red1 <= 0)
                            red1 = 1;
                        if (red1 >= 255)
                            red1 = 254;
                        if (green1 <= 0)
                            green1 = 1;
                        if (green1 >= 255)
                            green1 = 254;
                        if (blue1 <= 0)
                            blue1 = 1;
                        if (blue1 >= 255)
                            blue1 = 254;
                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(red1, green1, blue1);

                        image.SetPixel(i, j, newColor);
                        ccv++;
                    }
                }
            }
            camera.Source = Extensions.Convert(Extensions.cropImage(image, new System.Drawing.Rectangle((int)mouthLeft.X - 200, (int)UpperLipMidTop.Y - 400, 420, 650)));


        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ////Apply Coating
            //Bitmap image1 = Extensions.GetBitmap((BitmapSource)camera.Source);
            //ApplyLipstick(image1);
        }
        private void button_ClickX(object sender, RoutedEventArgs e)
        {
            if (read != null)
            {
                read.Dispose();
            }

            if (sensor != null)
            {
                sensor.Close();
            }
            this.Hide();
            this.IsEnabled = false;
            genderDetection genderObject = new genderDetection();
            genderObject.Visibility = Visibility.Visible;
        }
        private void button_ClickY(object sender, RoutedEventArgs e)
        {
            if (read != null)
            {
                read.Dispose();
            }

            if (sensor != null)
            {
                sensor.Close();
            }

            if (genderDetection.finalGender == false)
            {
                this.Hide();
                this.IsEnabled = false;
                MainWindow genderObject = new MainWindow(0);
                genderObject.Visibility = Visibility.Visible;
            }
            else
            {
                this.Hide();
                this.IsEnabled = false;
                mainWindowMale genderObject = new mainWindowMale();
                genderObject.Visibility = Visibility.Visible;
            }


        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            moveleftlips(where);
            red = colorArrayRed[where];
            green = colorArrayGreen[where];
            blue = colorArrayBlue[where];
            //postinfo();
        }
        void upClick()
        {

            moveleftlips(where);
            red = colorArrayRed[where];
            green = colorArrayGreen[where];
            blue = colorArrayBlue[where];
        }
        void downClick()
        {
            moverightlips(where);
            red = colorArrayRed[where];
            green = colorArrayGreen[where];
            blue = colorArrayBlue[where];
        }
        private void moveRight_Click(object sender, RoutedEventArgs e)
        {
            moverightlips(where);
            red = colorArrayRed[where];
            green = colorArrayGreen[where];
            blue = colorArrayBlue[where];
            //postinfo();
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (read != null)
            {
                read.Dispose();
            }

            if (sensor != null)
            {
                sensor.Close();
            }
            this.Hide();
            this.IsEnabled = false;
            MainWindow mw = new MainWindow(0);
            mw.Visibility = Visibility.Visible;
        }


        private void getinfo()
        {
            Debug.WriteLine("hello ahmad");
            string query = "select brand,article,color,price,stock from Lipstick;";
            string connection = genderDetection.connectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                int i = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader rdr = cmd.ExecuteReader();
                Debug.WriteLine(rdr.FieldCount);
                while (rdr.Read())
                {
                    string get0 = rdr.GetString(0);
                    string get = rdr.GetString(1);
                    string get1 = rdr.GetString(2);
                    string get2 = rdr.GetString(3);
                    string get3 = rdr.GetString(4);
                    brand[i] = get0;
                    description[i] = get;
                    colorname[i] = get1;
                    price[i] = get2;
                    stock[i] = get3;
                    i++;
                }
                rdr.Close();
            }
            for (int j = 0; j < 10; j++)
            {
                Debug.WriteLine(brand[j]);
            }
        }


    }
}
