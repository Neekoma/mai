using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Emgu.CV;

using Emgu.CV.Structure;
using System.Net.Sockets;

using System.Net;

namespace WebCamForm
{
    public partial class Form1 : Form
    {
        byte[] statusFace = { 0 };
        private static IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
        UdpClient client = new UdpClient(1234);
        private UdpClient client1 = new UdpClient("127.0.0.1", 4321);
        private static CascadeClassifier classifier = new CascadeClassifier("haarcascade_frontalface_alt.xml");
        IPEndPoint ip1 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4321);
        public Form1()
        {
            InitializeComponent();
            getFrame();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Bitmap bitmap = new Bitmap(pictureBox1.Image);
            Image<Bgr, byte> grayImage = new Image<Bgr, byte>(bitmap);
            Rectangle[] faces = classifier.DetectMultiScale(grayImage, 1.4, 0);
            if (faces.Length > 0)
            {
               statusFace[0] = 1;
               client1.Send(statusFace, statusFace.Length,ip1);
            }
            foreach (var face in faces)
            {
                using (Graphics graphics1 = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Green, 3))
                    {
                        graphics1.DrawRectangle(pen, face);
                        
                    }
                }
                
                
            }
            pictureBox1.Image = bitmap;
            label1.Text = "TYT";


        }
        public async void getFrame()
        {
            while (true)
            {
                var data = await client.ReceiveAsync();
                using (MemoryStream ms = new MemoryStream(data.Buffer))
                {
                    Bitmap bitmap = new Bitmap(ms);
                    Image<Bgr, byte> grayImage = new Image<Bgr, byte>(bitmap);
                    grayImage.SmoothGaussian(5);
                    Rectangle[] faces = classifier.DetectMultiScale(grayImage, 1.4, 0);

                    if (faces.Length > 0)
                    {
                        statusFace[0] = 1;
                        await client1.SendAsync(statusFace, statusFace.Length);
                    }
                    foreach (var face in faces)
                    {
                        using (Graphics graphics1 = Graphics.FromImage(bitmap))
                        {
                            using (Pen pen = new Pen(Color.Green, 3))
                            {
                                graphics1.DrawRectangle(pen, face);
                                
                            }
                        }


                    }
                    pictureBox1.Image = bitmap;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}
