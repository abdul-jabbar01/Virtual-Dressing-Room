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
using AForge.Imaging.Filters;
using AForge.Math;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using AForge;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Data.SqlClient;



namespace VirtualDressingRoom
{
    /// <summary>
    /// Interaction logic for tryEye.xaml
    /// </summary>
    public partial class tryEye : Window
    {
        string[] brand = new string[10];
        string[] price = new string[10];
        string[] colorname = new string[10];
        string[] stock = new string[10];
        string[] description = new string[10];
        int gender;
        KinectSensor sensor;
        MultiSourceFrameReader read;
        IList<Body> bodies;
        string rightHandState1 = "";
        string rightHandState2 = "";
        string leftHandState1 = "";
        string leftHandState2 = "";
        int where = 1;
        bool done = false;

        HaarCascade face, leftEye, rightEye;
        System.Drawing.Rectangle rect;
        List<System.Drawing.Point> pointLeftSide, pointRightSide;
        bool isFacedetected = false;
        String pathTemp = "temp.jpg";
        HaarCascade _cascadeClassifier;


        Bitmap bmp;
        Bitmap[] bmp1 = new Bitmap[6];
        Bitmap[] bmpF = new Bitmap[6];
        String path = "qwer.jpg";
        String path1 = "1.png";
        String path2 = "11.png";
        String path3 = "9.png";
        String path4 = "8.png";

        HaarCascade eyes_cascade;
        bool isdetected = false;
        float lineSize = 0, lineY = 0, lineX = 0;


        public tryEye()
        {
            InitializeComponent();

            if (genderDetection.finalGender == true)
            {
                try
                {
                    image4.Source = Extensions.Convert(new Bitmap(@"Resource/male.png"));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
               
            }
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            _cascadeClassifier = new HaarCascade("haarcascade_frontalface_default.xml");
            //getinfo();
            // postinfo();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sensor = KinectSensor.GetDefault();

            if (sensor != null)
            {
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


        void moveEye()
        {
            int count = 0;

            System.Drawing.Rectangle[] eyeRect = new System.Drawing.Rectangle[3];
            eyeRect[0] = new System.Drawing.Rectangle(0, 0, 0, 0);
            eyeRect[1] = new System.Drawing.Rectangle(0, 0, 0, 0);
            eyeRect[2] = new System.Drawing.Rectangle(0, 0, 0, 0);

            Image<Bgr, byte> image = new Image<Bgr, byte>(bmp);
            Image<Gray, Byte> gray = image.Convert<Gray, Byte>();

            MCvAvgComp[][] eyes = gray.DetectHaarCascade(eyes_cascade, 1.1, 1, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new System.Drawing.Size(20, 20));

            foreach (MCvAvgComp eyesnap in eyes[0])
            {
                eyeRect[count] = eyesnap.rect;
                image.Draw(eyeRect[count], new Bgr(System.Drawing.Color.Green), 2);
                isdetected = true;
                count++;
            }

            if (isdetected)
            {

                float leftPointX = eyeRect[0].X + (eyeRect[0].Width / 2);
                float leftPointY = eyeRect[0].Y + (eyeRect[0].Height / 2);

                float RightPointX = eyeRect[1].X + (eyeRect[1].Width / 2);
                float RightPointY = eyeRect[1].Y + (eyeRect[1].Height / 2);

                System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Red, 3);
                using (var graphics = Graphics.FromImage(bmp))
                {
                    lineSize = RightPointX - leftPointX;
                    lineSize = lineSize * -1;
                    lineY = (RightPointY + leftPointY) / 2;
                    lineX = (RightPointX + leftPointX) / 2;
                }
            }


            if (lineSize < 1)
            {
                lineSize = lineSize * -1;
            }

            if (lineSize != 0)
            {
                bmp1[0] = new Bitmap(path1);
                bmp1[1] = new Bitmap(path2);
                bmp1[2] = new Bitmap(path3);
                bmp1[3] = new Bitmap(path4);
                //bmp1[4] = new Bitmap(path5);
                //bmp1[5] = new Bitmap(path6);

                float factor = 2f;
                float[] multiplier = new float[6];

                for (int i = 0; i < 4; i++)
                {
                    multiplier[i] = (lineSize * bmp1[i].Height) / bmp1[i].Width;
                    ResizeBilinear filter = new ResizeBilinear((int)(lineSize * factor), (int)(multiplier[i] * factor));
                    bmp1[i] = filter.Apply(bmp1[i]);
                }

                System.Drawing.Color temp = new System.Drawing.Color();
                temp = System.Drawing.Color.FromArgb(255, 255, 255);



                for (int controlFrames = 0; controlFrames < 4; controlFrames++)
                {
                    for (int i = 0; i < bmp.Width; i++)
                    {
                        for (int j = 0; j < bmp.Height; j++)
                        {
                            if (i < bmp1[controlFrames].Width && j < bmp1[controlFrames].Height)
                            {
                                if (bmp1[controlFrames].GetPixel(i, j).A != 0)
                                {
                                    bmpF[controlFrames].SetPixel((i + (int)lineX) - bmp1[controlFrames].Width / 2, (j + (int)lineY) - bmp1[controlFrames].Height / 2, bmp1[controlFrames].GetPixel(i, j));
                                }
                            }
                        }
                    }
                }


                //bmp1[0].Save("1X.jpg");
                //bmp1[1].Save("2X.jpg");

                Bitmap imgg = new Bitmap(bmpF[0]);
                eye0.Source = Extensions.Convert(imgg);
                imgg = new Bitmap(bmpF[1]);
                eye1.Source = Extensions.Convert(imgg);
                imgg = new Bitmap(bmpF[2]);
                eye2.Source = Extensions.Convert(imgg);
                imgg = new Bitmap(bmpF[3]);
                eye3.Source = Extensions.Convert(imgg);
            }

            else
            {
                Console.WriteLine("EYES NOT FOUND! PLEASE TRY ANOTHER IMAGE.");
            }
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

        private void moverighteyes(int val)
        {
            if (val == 1)
            {
                firstEffect.BlurRadius = 3;
                secondEffect.BlurRadius = 10;
                camera.Source = eye1.Source;
                where = 2;
            }
            else if (val == 2)
            {
                secondEffect.BlurRadius = 3;
                thirdEffect.BlurRadius = 10;
                camera.Source = eye2.Source;
                where = 3;
            }
            else if (val == 3)
            {
                thirdEffect.BlurRadius = 3;
                fourthEffect.BlurRadius = 10;
                camera.Source = eye3.Source;
                where = 4;
            }
            else if (val == 4)
            {
                fourthEffect.BlurRadius = 3;
                firstEffect.BlurRadius = 10;
                camera.Source = eye0.Source;
                where = 1;
            }

            //postinfo();
        }

        private void btnDetection_Click(object sender, RoutedEventArgs e)
        {

        }

        private void detectEyes()
        {
            int count = 0;
            System.Drawing.Rectangle[] eyeRect = new System.Drawing.Rectangle[3];
            eyeRect[0] = new System.Drawing.Rectangle(0, 0, 0, 0);
            eyeRect[1] = new System.Drawing.Rectangle(0, 0, 0, 0);
            eyeRect[2] = new System.Drawing.Rectangle(0, 0, 0, 0);

            Image<Bgr, byte> image = new Image<Bgr, byte>(bmp);
            Image<Gray, Byte> gray = image.Convert<Gray, Byte>();

            MCvAvgComp[][] eyes = gray.DetectHaarCascade(eyes_cascade, 1.1, 1, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new System.Drawing.Size(20, 20));

            foreach (MCvAvgComp eyesnap in eyes[0])
            {
                eyeRect[count] = eyesnap.rect;
                image.Draw(eyeRect[count], new Bgr(System.Drawing.Color.Green), 2);
                isdetected = true;
                count++;
            }

            if (isdetected)
            {

                float leftPointX = eyeRect[0].X + (eyeRect[0].Width / 2);
                float leftPointY = eyeRect[0].Y + (eyeRect[0].Height / 2);

                float RightPointX = eyeRect[1].X + (eyeRect[1].Width / 2);
                float RightPointY = eyeRect[1].Y + (eyeRect[1].Height / 2);

                System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Red, 3);
                using (var graphics = Graphics.FromImage(bmp))
                {
                    //graphics.DrawLine(blackPen, leftPointX, leftPointY, RightPointX, RightPointY);
                    lineSize = RightPointX - leftPointX;
                    lineSize = lineSize * -1;
                    lineY = (RightPointY + leftPointY) / 2;
                    lineX = (RightPointX + leftPointX) / 2;
                }
            }
            Bitmap bmpX = new Bitmap(image.Bitmap);
            camera.Source = Extensions.Convert(bmpX);
            moveEye();
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (genderDetection.finalGender == true)
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
                mainWindowMale mw = new mainWindowMale();
                mw.Visibility = Visibility.Visible;
                this.Hide();
            }
            else
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
                this.Hide();
            }
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            movelefteyes(where);
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            moverighteyes(where);
        }

        private void movelefteyes(int val)
        {
            if (val == 1)
            {
                firstEffect.BlurRadius = 3;
                fourthEffect.BlurRadius = 10;
                camera.Source = eye3.Source;
                where = 4;
            }
            else if (val == 2)
            {
                secondEffect.BlurRadius = 3;
                firstEffect.BlurRadius = 10;
                where = 1;
                camera.Source = eye0.Source;
            }
            else if (val == 3)
            {
                thirdEffect.BlurRadius = 3;
                secondEffect.BlurRadius = 10;
                camera.Source = eye1.Source;
                where = 2;
            }
            else if (val == 4)
            {
                fourthEffect.BlurRadius = 3;
                thirdEffect.BlurRadius = 10;
                where = 3;
                camera.Source = eye2.Source;
            }



            //postinfo();
        }

        void Read_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
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

                    bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(bodies);

                    foreach (var body in bodies)
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
                                    movelefteyes(where);
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
                                    moverighteyes(where);
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

        private void getinfo()
        {
            string query = "select brand,article,color,price,stock from Frames;";
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

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            done = true;
            Bitmap finalImage = Extensions.GetBitmap((BitmapSource)camera.Source);
            finalImage.Save("abc.jpg");

            eyes_cascade = new HaarCascade("haarcascade_eye_tree_eyeglasses.xml");

            bmp = new Bitmap("abc.jpg");

            Image<Bgr, byte> image = new Image<Bgr, byte>(bmp);
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
                Crop filter12 = new Crop(new System.Drawing.Rectangle(rect.Left - 150, rect.Top - 100, 150 + rect.Width + 150, 100 + rect.Height + 250));
                bmp = filter12.Apply(bmp);
            }
            else
            {
                MessageBox.Show("Face Not detected, Please recapture.");
            }

            //Image Resizing
            // bmp = new Bitmap(bmp);
            bmpF[0] = bmp;
            bmpF[1] = bmp;
            bmpF[2] = bmp;
            bmpF[3] = bmp;
            bmpF[4] = bmp;
            bmpF[5] = bmp;

            int x = 553;
            float multiplier = (x * bmp.Height) / bmp.Width;
            ResizeBilinear filter = new ResizeBilinear(x, (int)multiplier);

            for (int i = 0; i < 6; i++)
            {
                bmpF[i] = filter.Apply(bmpF[i]);
                bmp = filter.Apply(bmp);
                bmpF[i].Save("XY" + i + ".jpg");

            }

            detectEyes();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            eyes_cascade = new HaarCascade("haarcascade_eye_tree_eyeglasses.xml");

            bmp = new Bitmap("aunty.jpg");
            pathTemp = "temp1.jpg";
            Image<Bgr, byte> image = new Image<Bgr, byte>(bmp);
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
                Crop filter12 = new Crop(new System.Drawing.Rectangle(rect.Left - 150, rect.Top - 100, 150 + rect.Width + 150, 100 + rect.Height + 250));
                bmp = filter12.Apply(bmp);
            }
            else
            {
                MessageBox.Show("Face Not detected, Please recapture.");
            }

            //Image Resizing
            // bmp = new Bitmap(bmp);
            bmpF[0] = bmp;
            bmpF[1] = bmp;
            bmpF[2] = bmp;
            bmpF[3] = bmp;
            bmpF[4] = bmp;
            bmpF[5] = bmp;

            int x = 553;
            float multiplier = (x * bmp.Height) / bmp.Width;
            ResizeBilinear filter = new ResizeBilinear(x, (int)multiplier);

            for (int i = 0; i < 6; i++)
            {
                bmpF[i] = filter.Apply(bmpF[i]);
                bmp = filter.Apply(bmp);
                bmpF[i].Save("XY" + i + ".jpg");

            }

            detectEyes();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
        private void button_Click(object sender, RoutedEventArgs e)
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
    }
}
