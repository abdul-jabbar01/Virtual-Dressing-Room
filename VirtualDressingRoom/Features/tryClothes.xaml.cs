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
using AForge.Imaging.Filters;
using System.Data;
using System.Data.SqlClient;

namespace VirtualDressingRoom
{
    /// <summary>
    /// Interaction logic for tryClothes.xaml
    /// </summary>
    /// 

    public partial class tryClothes : Window
    {

        #region Members
        bool colorFrameCheck = false;
        string[] brand = new string[4];
        string[] price = new string[4];
        string[] colorname = new string[4];
        string[] stock = new string[4];
        string[] description = new string[4];
        string[] urisourcearray = new string[4];
        string[] dressNames = new string[4];

        System.Drawing.Point[] RightShirtShoulderArray = new System.Drawing.Point[4];
        System.Drawing.Point[] LeftShirtShoulderArray = new System.Drawing.Point[4];
        int skinTone = 1;
        KinectSensor sensor;
        MultiSourceFrameReader read;

        int x = 0;
        int where1 = 1;
        System.Drawing.Point shoulderLeft;
        System.Drawing.Point shoulderRight;
        System.Drawing.Point armLeft;
        System.Drawing.Point armRight;
        System.Drawing.Point start;
        System.Drawing.Point shirtShoulderLeft;
        System.Drawing.Point shirtShoulderRight;
        Bitmap cloth;
        Bitmap cloth1;
        int counter = 0;
        public static KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> _bodies;
        ImageSource image1 = null;
        bool doneCheck = false;
        bool firstCheck = false;

        int where = 0;
        #endregion

        public tryClothes()
        {
            InitializeComponent();
            //label6.Content = "Please wait. We are finding best fit for you.";


            if (genderDetection.finalGender == true)
            {
                try
                {
                    image4.Source = Extensions.Convert(new Bitmap(@"Resource\male.png"));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                urisourcearray[2] = "sweater_brown.png";
                LeftShirtShoulderArray[2].X = 98;
                LeftShirtShoulderArray[2].Y = 70;
                RightShirtShoulderArray[2].X = 402;
                RightShirtShoulderArray[2].Y = 60;

                urisourcearray[1] = "grey_male.png";
                LeftShirtShoulderArray[1].X = 78;
                LeftShirtShoulderArray[1].Y = 67;
                RightShirtShoulderArray[1].X = 418;
                RightShirtShoulderArray[1].Y = 56;

                urisourcearray[0] = "sweater_male.png";
                LeftShirtShoulderArray[0].X = 98;
                LeftShirtShoulderArray[0].Y = 70;
                RightShirtShoulderArray[0].X = 402;
                RightShirtShoulderArray[0].Y = 60;

                urisourcearray[3] = "polo_red.png";
                LeftShirtShoulderArray[3].X = 115;
                LeftShirtShoulderArray[3].Y = 66;
                RightShirtShoulderArray[3].X = 415;
                RightShirtShoulderArray[3].Y = 80;
                clotha.Source = Extensions.Convert(new Bitmap(urisourcearray[0]));
                clothb.Source = Extensions.Convert(new Bitmap(urisourcearray[1]));
                clothc.Source = Extensions.Convert(new Bitmap(urisourcearray[2]));
                clothd.Source = Extensions.Convert(new Bitmap(urisourcearray[3]));
                dressNames[0] = urisourcearray[0];
                dressNames[1] = urisourcearray[1];
                dressNames[2] = urisourcearray[2];
                dressNames[3] = urisourcearray[3];
                cloth = new Bitmap(urisourcearray[0]);
                cloth1 = new Bitmap(urisourcearray[0]);
                shirtShoulderLeft = LeftShirtShoulderArray[0];
                shirtShoulderRight = RightShirtShoulderArray[0];

            }
            else
            {
                urisourcearray[2] = "white_frock.png";
                LeftShirtShoulderArray[2].X = 138;
                LeftShirtShoulderArray[2].Y = 78;
                RightShirtShoulderArray[2].X = 282;
                RightShirtShoulderArray[2].Y = 74;

                urisourcearray[1] = "blue_frock.png";
                LeftShirtShoulderArray[1].X = 155;
                LeftShirtShoulderArray[1].Y = 76;
                RightShirtShoulderArray[1].X = 343;
                RightShirtShoulderArray[1].Y = 78;

                urisourcearray[0] = "yellow_pink_frock.png";
                LeftShirtShoulderArray[0].X = 170;
                LeftShirtShoulderArray[0].Y = 73;
                RightShirtShoulderArray[0].X = 322;
                RightShirtShoulderArray[0].Y = 73;

                urisourcearray[3] = "green_frock.png";
                LeftShirtShoulderArray[3].X = 155;
                LeftShirtShoulderArray[3].Y = 76;
                RightShirtShoulderArray[3].X = 343;
                RightShirtShoulderArray[3].Y = 78;
                clotha.Source = Extensions.Convert(new Bitmap(urisourcearray[0]));
                clothb.Source = Extensions.Convert(new Bitmap(urisourcearray[1]));
                clothc.Source = Extensions.Convert(new Bitmap(urisourcearray[2]));
                clothd.Source = Extensions.Convert(new Bitmap(urisourcearray[3]));
                dressNames[0] = urisourcearray[0];
                dressNames[1] = urisourcearray[1];
                dressNames[2] = urisourcearray[2];
                dressNames[3] = urisourcearray[3];
                cloth = new Bitmap(urisourcearray[0]);
                cloth1 = new Bitmap(urisourcearray[0]);
                shirtShoulderLeft = LeftShirtShoulderArray[0];
                shirtShoulderRight = RightShirtShoulderArray[0];
            }

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

        bool xx = false;
        Bitmap bmpF;
        async void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            if (doneCheck == false)
            {

                if (true)
                {
                    var reference = e.FrameReference.AcquireFrame();

                    // Color
                    using (var frame = reference.ColorFrameReference.AcquireFrame())
                    {
                        if (frame != null)
                        {
                            xx = true;
                            image1 = frame.ToBitmap();
                        }
                    }

                    using (var frame = reference.BodyFrameReference.AcquireFrame())
                    {
                        if (frame != null)
                        {
                            if (xx == true)
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
                                            List<ColorSpacePoint> points = canvas.DrawSkeleton(body);
                                            if (points != null)
                                            {
                                                bmpF = Extensions.cropImage((Bitmap)Extensions.GetBitmap((BitmapSource)image1), new System.Drawing.Rectangle(armLeft.X - 200, 0, armRight.X - armLeft.X + 400, 1080));
                                                //await Task.Factory.StartNew(() => 
                                                await Task.Factory.StartNew(() => applyDress(points));
                                                camera.Source = Extensions.Convert(bmpF);
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
                }
            }
        }

        void applyDress(List<ColorSpacePoint> points)
        {

            firstCheck = true;
            cloth1 = cloth;
            counter++;

            //Crop filter1 = new Crop();
            //Bitmap bmpF = ;
            shoulderLeft.X = ((int)points[4].X - 20) - armLeft.X + 200;
            shoulderLeft.Y = (int)points[4].Y - 30;
            shoulderRight.X = ((int)points[8].X + 20) - armLeft.X + 200;
            shoulderRight.Y = (int)points[8].Y - 30;

            armLeft.X = (int)points[6].X;
            armLeft.Y = (int)points[6].Y;
            armRight.X = (int)points[10].X;
            armRight.Y = (int)points[10].Y;
            start.X = (shoulderRight.X - shoulderLeft.X) / 3 + shoulderLeft.X;
            start.Y = (int)points[2].Y;
            int shirtWidth = shirtShoulderRight.X - shirtShoulderLeft.X;
            int imageWidth = shoulderRight.X - shoulderLeft.X;

            double factor = (double)imageWidth / (double)shirtWidth;
            int resizeX = (int)((double)cloth1.Width * factor);
            int resizeY = (int)((resizeX * cloth1.Height) / cloth1.Width);
            ResizeBilinear filter = new ResizeBilinear(resizeX, (int)resizeY);

            cloth1 = filter.Apply(cloth1);

            bool check = false;
            int newI = 0;
            int newJ = 0;

            int preImage = 0;
            int preShirt = newJ;
            for (int iii = 0; iii < cloth1.Height; iii++)
            {
                for (int jjj = 0; jjj < cloth1.Width; jjj++)
                {
                    if (cloth1.GetPixel(jjj, iii).A != 0)
                    { newI = iii + 1; preShirt = jjj; iii = cloth1.Height; jjj = cloth1.Width; }
                }
            }
            for (int jjj = 0; jjj < cloth1.Width; jjj++)
            {
                if (cloth1.GetPixel(jjj, newI).A != 0)
                {
                    newJ = jjj;
                    break;
                }
            }
            int difference = 0;
            bool diff = false;

            Console.WriteLine(DateTime.Now.Second.ToString() + "  a  " + DateTime.Now.Millisecond.ToString());
            for (int i = 0; i < bmpF.Height; i++)
            {
                diff = false;
                for (int j = 0; j < bmpF.Width; j++)
                {
                    if (i == start.Y && j == start.X && check == false)
                    {
                        check = true;
                        preImage = j;
                        break;
                    }
                    if (check == true)
                    {
                        difference = preShirt - newJ;
                        if (j == preImage - difference && diff == false)
                        {
                            preShirt = newJ;
                            preImage = preImage - difference;
                            diff = true;
                        }
                        if (diff == true)
                        {
                            if (cloth1.GetPixel(newJ, newI).A != 0)
                            {
                                bmpF.SetPixel(j, i, cloth1.GetPixel(newJ, newI));
                            }
                            if (newJ < cloth1.Width - 2)
                                newJ++;
                        }


                    }
                }
                if (check == true)
                {
                    if (newI < cloth1.Height - 2)
                    {
                        newI++;
                        for (int jjj = 0; jjj < cloth1.Width; jjj++)
                        {
                            if (cloth1.GetPixel(jjj, newI).A != 0)
                            {
                                newJ = jjj;
                                break;
                            }
                        }
                    }
                }

            }
            Console.WriteLine(DateTime.Now.Second.ToString() + "  b  " + DateTime.Now.Millisecond.ToString());
            x++;
        }
        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (genderDetection.finalGender == true)
            {
                this.Hide();
                this.IsEnabled = false;
                if (_reader != null)
                {
                    _reader.Dispose();
                }

                if (_sensor != null)
                {
                    _sensor.Close();
                }
                mainWindowMale mw = new mainWindowMale();
                mw.Visibility = Visibility.Visible;
                this.Hide();
            }
            else
            {
                this.Hide();
                this.IsEnabled = false;
                if (_reader != null)
                {
                    _reader.Dispose();
                }

                if (_sensor != null)
                {
                    _sensor.Close();
                }
                MainWindow mw = new MainWindow(0);
                mw.Visibility = Visibility.Visible;
                this.Hide();
            }
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                move_left(where1);
                camera.Source = Extensions.Convert(cloth);
            }
            catch (Exception)
            {
                MessageBox.Show("You are going out of bound. Please try again for better results. ");
            }

        }
        void upClick()
        {
            try
            {
                move_left(where1);
                camera.Source = Extensions.Convert(cloth);
            }
            catch (Exception)
            {
                MessageBox.Show("You are going out of bound. Please try again for better results. ");
            }

        }
        void downClick()
        {
            try
            {
                move_down(where1);
                camera.Source = Extensions.Convert(cloth);
            }
            catch (Exception)
            {
                MessageBox.Show("You are going out of bound. Please try again for better results.");
            }

        }

        private void down_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                move_down(where1);
                camera.Source = Extensions.Convert(cloth);
            }
            catch (Exception)
            {
                MessageBox.Show("You are going out of bound. Please try again for better results.");
            }

        }
        private void move_down(int val)
        {
            if (val == 1)
            {
                firstEffect.BlurRadius = 3;
                secondEffect.BlurRadius = 10;
                where1 = 2;
            }
            else if (val == 2)
            {
                secondEffect.BlurRadius = 3;
                thirdEffect.BlurRadius = 10;
                where1 = 3;
            }
            else if (val == 3)
            {
                thirdEffect.BlurRadius = 3;
                fourthEffect.BlurRadius = 10;
                where1 = 4;
            }
            else if (val == 4)
            {
                fourthEffect.BlurRadius = 3;
                firstEffect.BlurRadius = 10;
                where1 = 1;
            }
            cloth = new Bitmap(dressNames[where1 - 1]);
            shirtShoulderLeft = LeftShirtShoulderArray[where1 - 1];
            shirtShoulderRight = RightShirtShoulderArray[where1 - 1];
        }

        private void move_left(int val)
        {


            if (val <= 1)
            {
                firstEffect.BlurRadius = 3;
                fourthEffect.BlurRadius = 10;
                where1 = 4;
            }
            else if (val == 2)
            {
                secondEffect.BlurRadius = 3;
                firstEffect.BlurRadius = 10;
                where1 = 1;
            }
            else if (val == 3)
            {
                thirdEffect.BlurRadius = 3;
                secondEffect.BlurRadius = 10;
                where1 = 2;
            }
            else if (val == 4)
            {
                fourthEffect.BlurRadius = 3;
                thirdEffect.BlurRadius = 10;
                where1 = 3;
            }

            cloth = new Bitmap(dressNames[where1 - 1]);
            shirtShoulderLeft = LeftShirtShoulderArray[where1 - 1];
            shirtShoulderRight = RightShirtShoulderArray[where1 - 1];
            // postinfo();
            //else if (val == 5)
            //{
            //    clothd.Margin = new Thickness(1556, 40, 0, 0);
            //    cloth3.Margin = new Thickness(1202, 0, 0, 0);
            //}
        }

        private void getinfo()
        {
            //Debug.WriteLine("hello ahmad");
            string query = "select brand,article,color,price,stock,picture,leftshoulderx,leftshouldery,rightshoulderx,rightshouldery from Clothes where skintoneid = '" + skinTone + "';";

            if (genderDetection.finalGender == true)
            {
                query = "select brand,article,color,price,stock,picture,leftshoulderx,leftshouldery,rightshoulderx,rightshouldery from Clothes where skintoneid = '" + skinTone + "' and gender like 'm';";
            }
            else
            {
                query = "select brand,article,color,price,stock,picture,leftshoulderx,leftshouldery,rightshoulderx,rightshouldery from Clothes where skintoneid = '" + skinTone + "' and gender like 'f';";
            }
            string connection = genderDetection.connectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                int i = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader rdr = cmd.ExecuteReader();
                Debug.WriteLine(rdr.FieldCount);
                while (rdr.Read() && i < 4)
                {
                    string get0 = rdr.GetString(0);
                    string get = rdr.GetString(1);
                    string get1 = rdr.GetString(2);
                    string get2 = rdr.GetString(3);
                    string get3 = rdr.GetString(4);
                    string get4 = rdr.GetString(5);
                    int get5 = rdr.GetInt32(6);
                    int get6 = rdr.GetInt32(7);
                    int get7 = rdr.GetInt32(8);
                    int get8 = rdr.GetInt32(9);
                    brand[i] = get0;
                    description[i] = get;
                    colorname[i] = get1;
                    stock[i] = get3;
                    urisourcearray[i] = get4;
                    dressNames[i] = get4;
                    LeftShirtShoulderArray[i].X = get5;
                    LeftShirtShoulderArray[i].Y = get6;
                    RightShirtShoulderArray[i].X = get7;
                    RightShirtShoulderArray[i].Y = get8;

                    i++;
                }
                rdr.Close();
                shirtShoulderLeft = LeftShirtShoulderArray[0];
                shirtShoulderRight = RightShirtShoulderArray[0];
            }
            for (int j = 0; j < 10; j++)
            {
                Debug.WriteLine(brand[j]);
            }

            var uriSource = new Uri(urisourcearray[0], UriKind.Absolute);
            clotha.Source = new BitmapImage(uriSource);
            uriSource = new Uri(urisourcearray[1], UriKind.Absolute);
            clothb.Source = new BitmapImage(uriSource);
            uriSource = new Uri(urisourcearray[2], UriKind.Absolute);
            clothc.Source = new BitmapImage(uriSource);
            uriSource = new Uri(urisourcearray[3], UriKind.Absolute);
            clothd.Source = new BitmapImage(uriSource);
            dressNames[0] = urisourcearray[1];
            dressNames[1] = urisourcearray[2];
            dressNames[2] = urisourcearray[3];
            dressNames[3] = urisourcearray[4];
            cloth = new Bitmap(urisourcearray[0]);
            cloth1 = new Bitmap(urisourcearray[1]);
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
