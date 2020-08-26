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
using System.Drawing;

using System.ComponentModel;
using System.Data;
using AForge.Imaging.Filters;
using AForge.Math;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using AForge;
using System.Data.SqlClient;

namespace VirtualDressingRoom
{
    /// <summary>
    /// Interaction logic for tryHair.xaml
    /// </summary>
    public partial class tryHair : Window
    {

        Bitmap[] bmp = new Bitmap[3];
        Bitmap[] bmpC = new Bitmap[3];
        HaarCascade face, leftEye, rightEye;
        System.Drawing.Rectangle rect;
        List<System.Drawing.Point> pointLeftSide, pointRightSide;
        bool isFacedetected = false;
        String path = "abc.jpg";
        String path1 = "1.jpg";
        String path2 = "2.jpg";
        String path3 = "3.jpg";
        String pathTemp = "temp.jpg";
        HaarCascade _cascadeClassifier;

        //Variables to get values of color from color samples
        int[] maxRR = new int[3];
        int[] maxGG = new int[3];
        int[] maxBB = new int[3];
        int whichOne = 1;
        string[] brand = new string[10];
        string[] price = new string[10];
        string[] colorname = new string[10];
        string[] stock = new string[10];
        string[] description = new string[10];

        KinectSensor sensor;
        MultiSourceFrameReader read;
        Bitmap imageX;
        IList<Body> _bodies;
        string rightHandState1 = "";
        int where = 1;
        string rightHandState2 = "";
        string leftHandState1 = "";
        string leftHandState2 = "";
        public tryHair()
        {
            InitializeComponent();

            pointLeftSide = new List<System.Drawing.Point>();
            pointRightSide = new List<System.Drawing.Point>();
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            _cascadeClassifier = new HaarCascade("haarcascade_frontalface_default.xml");
            leftEye = new HaarCascade("lefteye.xml");
            rightEye = new HaarCascade("righteye.xml");
            //getinfo();
            //postinfo();

        }


        private void funcCropping()
        {
            bmp[0] = new Bitmap(path);

            Image<Bgr, byte> image = new Image<Bgr, byte>(bmp[0]);
            Image<Gray, byte> grayframe = image.Convert<Gray, byte>();
            var faces =
                    grayframe.DetectHaarCascade(
                            face, 1.4, 4,
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
                bmp[0] = filter.Apply(bmp[0]);
                bmp[0].Save(pathTemp);
                bmp[0] = new Bitmap(pathTemp);
                bmp[1] = bmp[0];
                bmp[2] = bmp[0];
            }
            else
            {
                MessageBox.Show("Face Not detected, Please recapture.");
            }
        }


        private void colorHair()
        {
            funcCropping();

            bmp[0] = new Bitmap(pathTemp);

            Image<Bgr, byte> image = new Image<Bgr, byte>(bmp[0]);
            Image<Gray, byte> grayframe = image.Convert<Gray, byte>();
            var faces =
                    grayframe.DetectHaarCascade(
                            face, 1.4, 4,
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
                int x = rect.Right - rect.Width / 2;
                int y = rect.Y;
                int x1 = x;
                int y1 = y;
                int x2 = x;
                int y2 = y - 10;
                System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Red, 3);
                System.Drawing.Color rgb = bmp[0].GetPixel(x2, y2);
                int minR = rgb.R, minG = rgb.G, minB = rgb.B;
                int maxR = rgb.R, maxG = rgb.G, maxB = rgb.B;
                using (var graphics = Graphics.FromImage(bmp[0]))
                {
                    graphics.DrawLine(blackPen, x2 - rect.Width / 5, y2, x2 + rect.Width / 5, y2);
                }
                for (int i = x2 - rect.Width / 5; i < x2 + rect.Width / 5; i++)
                {
                    rgb = bmp[0].GetPixel(i, y2);
                    if (minR > rgb.R)
                        minR = rgb.R;
                    if (minG > rgb.G)
                        minG = rgb.G;
                    if (minB > rgb.B)
                        minB = rgb.B;

                    if (maxR < rgb.R)
                        maxR = rgb.R;
                    if (maxG < rgb.G)
                        maxG = rgb.G;
                    if (maxB < rgb.B)
                        maxB = rgb.B;
                }
            }

        }



        private void colorHair2()
        {
            bmpC[0] = new Bitmap(path1);
            bmpC[1] = new Bitmap(path2);
            bmpC[2] = new Bitmap(path3);

            for (int control = 0; control < 3; control++)
            {
                Image<Bgr, byte> image = new Image<Bgr, byte>(bmpC[control]);
                Image<Gray, byte> grayframe = image.Convert<Gray, byte>();

                System.Drawing.Color rgb = bmpC[control].GetPixel(0, 0);
                int minRR = rgb.R, minGG = rgb.G, minBB = rgb.B;
                maxRR[control] = rgb.R;
                maxGG[control] = rgb.G;
                maxBB[control] = rgb.B;

                for (int i = 0; i < bmpC[control].Width; i++)
                {
                    for (int j = 0; j < bmpC[control].Height; j++)
                    {
                        rgb = bmpC[control].GetPixel(i, j);
                        if (minRR > rgb.R)
                            minRR = rgb.R;
                        if (minGG > rgb.G)
                            minGG = rgb.G;
                        if (minBB > rgb.B)
                            minBB = rgb.B;

                        if (maxRR[control] < rgb.R)
                            maxRR[control] = rgb.R;
                        if (maxGG[control] < rgb.G)
                            maxGG[control] = rgb.G;
                        if (maxBB[control] < rgb.B)
                            maxBB[control] = rgb.B;
                    }
                }

                maxRR[control] = maxRR[control] - minRR / 2;
                maxGG[control] = maxGG[control] - minGG / 2;
                maxBB[control] = maxBB[control] - minBB / 2;
            }
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
        private void colorHair3()
        {
            bmp[0] = new Bitmap(pathTemp);
            bmp[1] = new Bitmap(pathTemp);
            bmp[2] = new Bitmap(pathTemp);

            int x = rect.Right - rect.Width / 2;
            int y = rect.Y;
            int x1 = x;
            int y1 = y;
            int x2 = x;
            int y2 = y - 10;

            System.Drawing.Color rgb = bmp[0].GetPixel(x2, y2);
            int minR = rgb.R, minG = rgb.G, minB = rgb.B;
            int maxR = rgb.R, maxG = rgb.G, maxB = rgb.B;

            for (int i = x2 - rect.Width / 5; i < x2 + rect.Width / 5; i++)
            {
                rgb = bmp[0].GetPixel(i, y2);
                if (minR > rgb.R)
                    minR = rgb.R;
                if (minG > rgb.G)
                    minG = rgb.G;
                if (minB > rgb.B)
                    minB = rgb.B;

                if (maxR < rgb.R)
                    maxR = rgb.R;
                if (maxG < rgb.G)
                    maxG = rgb.G;
                if (maxB < rgb.B)
                    maxB = rgb.B;
            }

            int newColorR, newColorG, newColorB;
            int orgColorR, orgColorG, orgColorB;

            orgColorR = maxR - minR / 2;
            orgColorG = maxG - minG / 2;
            orgColorB = maxB - minB / 2;

            //Hair Colouring is done here
            for (int control = 0; control < 3; control++)
            {
                orgColorR = maxRR[control] - orgColorR;
                orgColorG = maxGG[control] - orgColorG;
                orgColorB = maxBB[control] - orgColorB;

                for (int i = 0; i < bmp[control].Width; i++)
                {
                    for (int j = 0; j < bmp[control].Height; j++)
                    {
                        System.Drawing.Color temp = bmp[control].GetPixel(i, j);

                        if (temp.R > minR && temp.R < maxR && temp.G > minG && temp.G < maxG && temp.B > minB && temp.B < maxB)
                        {
                            newColorR = temp.R + orgColorR;
                            newColorG = temp.G + orgColorG;
                            newColorB = temp.B + orgColorB;

                            if (newColorR < 0)
                            {
                                newColorR = 0;
                            }

                            if (newColorG < 0)
                            {
                                newColorG = 0;
                            }

                            if (newColorB < 0)
                            {
                                newColorB = 0;
                            }

                            if (newColorR > 255)
                            {
                                newColorR = 255;
                            }
                            if (newColorG > 255)
                            {
                                newColorG = 255;
                            }

                            if (newColorB > 255)
                            {
                                newColorB = 255;
                            }

                            bmp[control].SetPixel(i, j, System.Drawing.Color.FromArgb(255, newColorR, newColorG, newColorB));
                        }
                    }
                }
            }       //Main loop ends here

            camera.Source = Convert(bmp[0], whichOne);
            hair1.Source = Convert(bmp[0], whichOne);
            Bitmap thuk = new Bitmap(pathTemp);
            hair2.Source = Convert(thuk, whichOne);
            hair3.Source = Convert(bmp[2], whichOne);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sensor = KinectSensor.GetDefault();

            if (sensor != null)
            {
                sensor.Open();

                read = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                read.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
            }
        }

        private void initframe()
        {
            read = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
            read.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
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
        private void moverighthairs(int val)
        {
            if (val == 1)
            {
                firstEffect.BlurRadius = 3;
                secondEffect.BlurRadius = 10;
                camera.Source = hair2.Source;
                where = 2;
            }
            else if (val == 2)
            {
                secondEffect.BlurRadius = 3;
                thirdEffect.BlurRadius = 10;
                where = 3;
                camera.Source = hair3.Source;
            }
            else if (val == 3)
            {
                thirdEffect.BlurRadius = 3;
                firstEffect.BlurRadius = 10;
                camera.Source = hair1.Source;
                where = 1;
            }
            else
            {
                where = 1;
            }

            //postinfo();
        }
        private void movelefthairs(int val)
        {
            if (val == 1)
            {
                firstEffect.BlurRadius = 3;
                thirdEffect.BlurRadius = 10;
                camera.Source = hair3.Source;
                where = 3;
            }
            else if (val == 2)
            {
                secondEffect.BlurRadius = 3;
                firstEffect.BlurRadius = 10;
                camera.Source = hair1.Source;
                where = 1;
            }
            else if (val == 3)
            {
                thirdEffect.BlurRadius = 3;
                secondEffect.BlurRadius = 10;
                camera.Source = hair2.Source;
                where = 2;
            }
            else
            {

            }

            //postinfo();
        }
        bool done = false;



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

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            movelefthairs(where);
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            moverighthairs(where);
        }

        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();
            // Color Camera Code
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null && done == false)
                {
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
                                label1.Content = "detected";
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
                                    movelefthairs(where);
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
                                    moverighthairs(where);
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
                                        Debug.WriteLine("relax");
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            movelefthairs(where);
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            done = true;
            Bitmap finalImage = Extensions.GetBitmap((BitmapSource)camera.Source);

            finalImage.Save("abc.jpg");
            pathTemp = "temp.jpg";
            finalImage.Dispose();
            whichOne = 1;
            colorHair();
            colorHair2();
            colorHair3();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            done = false;
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            done = true;
            path = "aunty1.jpg";
            pathTemp = "temp2.jpg";
            whichOne = 2;
            colorHair();
            colorHair2();
            colorHair3();
        }
        public static BitmapSource Convert(System.Drawing.Bitmap bitmap, int x)
        {
            if (x == 1)
            {
                var bitmapData = bitmap.LockBits(
               new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
               System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
                var bitmapSource = BitmapSource.Create(
                    bitmapData.Width, bitmapData.Height, 96, 96, PixelFormats.Bgr32, null,
                    bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);
                bitmap.UnlockBits(bitmapData);
                return bitmapSource;
            }
            else
            {
                var bitmapData = bitmap.LockBits(
               new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
               System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
                var bitmapSource = BitmapSource.Create(
                    bitmapData.Width, bitmapData.Height, 96, 96, PixelFormats.Bgr24, null,
                    bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);
                bitmap.UnlockBits(bitmapData);
                return bitmapSource;
            }

        }
        private void getinfo()
        {
            string query = "select brand,article,color,price,stock from Dyes;";
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
