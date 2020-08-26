using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace VirtualDressingRoom
{
    class Skin
    {
        private HaarCascade haarCascade, eyes;
        BitmapImage bmp = new BitmapImage(new Uri(@"bin\Debug\frontal face.jpg"));
        Bitmap bmp1;
        int redval = 0, greenval = 0, blueval = 0;
        private Bitmap BitmapImage2Bitmap(BitmapImage bmp)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encode = new BmpBitmapEncoder();
                encode.Frames.Add(BitmapFrame.Create(bmp));
                encode.Save(stream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(stream);
                return new Bitmap(bitmap);
            }
        }

        public int SkinToneCalculate(Bitmap imageBitmap)
        {
            bmp1 = imageBitmap;
            Image<Bgr, Byte> img = new Image<Bgr, byte>(bmp1);
            Image<Gray, Byte> grayimg = img.Convert<Gray, Byte>();
            haarCascade = new HaarCascade(@"bin\Debug\haarcascade_frontalface_alt.xml");
            eyes = new HaarCascade(@"bin\Debug\haarcascade_eye1.xml");
            var detectedFaces = grayimg.DetectHaarCascade(haarCascade)[0];
            int count = detectedFaces.Length;
            if (count == 0)
            {
                return -1;
            }
            else
            {
                int x, y, locx, locy;
                int facey = 0;
                foreach (var face in detectedFaces)
                {
                    img.Draw(face.rect, new Bgr(255, 255, 255), 3);
                    x = face.rect.Width;
                    y = face.rect.Height;
                    Debug.WriteLine(x + " new vals " + y);
                    locx = face.rect.X;
                    locy = face.rect.Y;
                    facey = face.rect.Location.Y;
                    var detectedeyes = grayimg.DetectHaarCascade(eyes)[0];
                    int i1 = 0;
                    int[] array = new int[100];
                    foreach (var eye in detectedeyes)
                    {
                        img.Draw(eye.rect, new Bgr(0, 0, 0), 3);
                        Debug.WriteLine("Eye nUmber: " + eye.rect.Location);
                        array[i1] = eye.rect.Location.Y;
                        i1++;
                    }
                    for (int i = 0; i < 100; i++) { if (array[i] == 0) { array[i] = 141525244; } }
                    int minpix = array.Min();
                    Debug.WriteLine(minpix);

                    int mid = ((minpix - facey) / 2) + facey;
                    double distance = x * 0.40;
                    double distance1 = x - distance;
                    int d = Convert.ToInt16(distance);
                    int d1 = Convert.ToInt16(distance1);
                    int redb = 0, greenb = 0, blueb = 0;
                    int count1 = 0;
                    for (int i = d; i < d1; i++)
                    {
                        redb = redb + img.Bitmap.GetPixel(i, mid).R;
                        greenb = greenb + img.Bitmap.GetPixel(i, mid).G;
                        blueb = blueb + img.Bitmap.GetPixel(i, mid).B;

                        count1++;
                    }

                    int r1 = redb / count1;
                    int g1 = greenb / count1;
                    int b1 = blueb / count1;
                    Debug.WriteLine(r1 + " " + g1 + " " + b1);
                    //image1.Source = ToBitmapSource(img);
                    Bitmap bmp2 = new Bitmap(50, 50);
                    for (int i = 0; i < 50; i++)
                    {
                        for (int j = 0; j < 50; j++)
                        {
                            bmp2.SetPixel(i, j, System.Drawing.Color.FromArgb(r1, g1, b1));
                        }
                    }
                    Image<Bgr, Byte> img1122 = new Image<Bgr, byte>(bmp2);
                    //image.Source = ToBitmapSource(img1122);
                    redval = r1;
                    greenval = g1;
                    blueval = b1;
                }
                return 1;
            }


        }

        public int suggestskin()
        {
            int[,] vals = new int[6, 3] { { 255, 224, 196 }, { 255, 220, 178 }, { 238, 207, 179 }, { 227, 185, 143 }, { 222, 166, 118 }, { 106, 79, 60 } };
            int[] final = new int[6];
            for (int i = 0; i < 6; i++)
            {
                int sum = 0;
                sum = sum + Math.Abs(redval - vals[i, 0]);
                sum = sum + Math.Abs(greenval - vals[i, 1]);
                sum = sum + Math.Abs(blueval - vals[i, 2]);
                final[i] = sum;
                Debug.WriteLine(final[i]);
            }
            int minindex = Array.IndexOf(final, final.Min());
            Debug.WriteLine(minindex);
            Bitmap bmp2 = new Bitmap(50, 50);
            return minindex;
        }
    }
}
