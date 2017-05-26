using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConversorTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    foreach (var item in files)
                    {
                        Converter(item);
                    }

                    System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
        }

        private void Converter(string directory)
        {
            var ffmpeg = new FFMpegConverter();
            ConvertSettings convertSettings = new ConvertSettings()
            {
                
                CustomOutputArgs = "-s 320x240 -b:v 128k -bufsize 128k "
            };
            var fileOutput =  String.Concat(System.IO.Path.GetDirectoryName(directory), "/", System.IO.Path.GetFileNameWithoutExtension(directory), "2.mp4");
            ffmpeg.ConvertMedia(directory, Format.mov, fileOutput, Format.mp4, convertSettings);

        }
    }
}
