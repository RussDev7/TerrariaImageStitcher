using ImageMagick;
using System;
using System.Windows.Forms;

namespace TerrariaImageStitcher
{
    public partial class Form1 : Form
    {
        // Say Hello To Decompilers
        private readonly string HelloThere = "Hello there fellow Decompiler, This Program Was Made By dannyruss (xXCrypticNightXx).";

        #region Main Code

        public Form1()
        {
            InitializeComponent();
        }

        public string[] PhotosLoc = { "" };
        public string SaveLoc = "";
        public int imgwide = 0;
        public int imgtall = 0;

        // Stitch Photo Function
        public void CombineBitmap(string[] files)
        {
            // Setup Progress Bar
            progressBar1.Step = 1;
            progressBar1.PerformStep();

            try
            {
                int imgwide = 0;
                int imgtall = 0;

                int widecount = 0;
                int tallcount = 0;

                int oldvalue = 0;
                bool lockit = false;
                bool dead = false;

                int stackedImagesTall = 0;
                int stackedImagesWide = 1;

                int tallcountrunner = 0;

                foreach (string image in files)
                {
                    // Add each image from files to an image array.
                    // images.Add(new MagickImage(image));

                    // Continue to do math on existing images.
                    int pos = image.LastIndexOf(@"\") + 1;
                    string filenumber = image.Substring(pos, image.Length - pos).GetUntilOrEmpty();

                    if (!lockit)
                    {
                        // Look For First Number
                        if (!dead)
                        {
                            stackedImagesTall++;
                            oldvalue = int.Parse(filenumber);
                        }
                        lockit = true;
                    }
                    else if (int.Parse(filenumber) != oldvalue)
                    {
                        // New Value Changed!
                        dead = true;
                        lockit = false;
                        oldvalue = int.Parse(filenumber);
                        stackedImagesWide++;
                    }
                    else
                    {
                        if (!dead)
                        {
                            stackedImagesTall++;
                        }
                    }
                }
                // Setup Progress Bar.
                progressBar1.Maximum = (stackedImagesWide * stackedImagesTall) + 2;

                // Grab the final images width and height to reduce load time.
                foreach (string file in files)
                {
                    using (MagickImage image = new MagickImage(file))
                    {
                        tallcountrunner++;
                        if (imgwide == 0)
                            tallcount += image.Height;
                        if (tallcountrunner < stackedImagesTall)
                        {
                            imgtall += 2016;
                        }
                        else
                        {
                            tallcountrunner = 0;
                            imgtall = 0;
                            imgwide += 2016;
                            widecount += image.Width;
                        }
                    }
                }
                // Define real image height.
                int IWidth = widecount - ((stackedImagesWide - 1) * 32);
                int IHeight = tallcount - ((stackedImagesTall - 1) * 32);

                // Reset variables.
                widecount = 0;
                tallcount = 0;
                imgtall = 0;
                imgwide = 0;
                tallcountrunner = 0;

                // Define a new image using the finals height and width.
                MagickImage finalImage = new MagickImage(MagickColors.White, IWidth, IHeight);

                // Go through each image and draw it on the final image
                foreach (string file in files)
                {
                    using (MagickImage image = new MagickImage(file))
                    {
                        // Define image quality.
                        image.Quality = 100;

                        // Draw new image on top of final.
                        finalImage.Draw(new DrawableComposite(imgwide, imgtall, image));

                        tallcountrunner++;
                        if (imgwide == 0)
                            tallcount += image.Height;
                        if (tallcountrunner < stackedImagesTall)
                        {
                            imgtall += 2016;
                            progressBar1.PerformStep();
                        }
                        else
                        {
                            tallcountrunner = 0;
                            imgtall = 0;
                            imgwide += 2016;
                            progressBar1.PerformStep();
                            widecount += image.Width;
                        }
                    }
                }

                // Define settings and export image.
                using (IMagickImage result = finalImage)
                {
                    // Get the image format.
                    string imageFormat = "";
                    if (radioButton1.Checked)
                    {
                        result.Format = MagickFormat.Png;
                        imageFormat = ".png";
                    }
                    else if (radioButton2.Checked)
                    {
                        result.Format = MagickFormat.Jpeg;
                        imageFormat = ".jpeg";
                    }
                    else if (radioButton3.Checked)
                    {
                        result.Format = MagickFormat.Bmp;
                        imageFormat = ".bmp";
                    }
                    else if (radioButton4.Checked)
                    {
                        result.Format = MagickFormat.Emf;
                        imageFormat = ".emf";
                    }
                    else if (radioButton5.Checked)
                    {
                        result.Format = MagickFormat.Icon;
                        imageFormat = ".icon";
                    }
                    else if (radioButton6.Checked)
                    {
                        result.Format = MagickFormat.Gif;
                        imageFormat = ".gif";
                    }
                    else if (radioButton6.Checked)
                    {
                        result.Format = MagickFormat.Wmf;
                        imageFormat = ".wmf";
                    }

                    // Define quality.
                    result.Quality = 100;

                    // Write output file.
                    result.Write(textBox2.Text + imageFormat);
                }

                // Progress Progressbar
                progressBar1.PerformStep();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        // Stitch The File
        private void Button1_Click(object sender, EventArgs e)
        {

            // Check If Locations Are Populated
            if (textBox1.Text == "")
            {
                MessageBox.Show("ERROR: Please Add Some Photos To Stitch!");
                return;
            }
            else if (!textBox1.Text.Contains(","))
            {
                MessageBox.Show("ERROR: Please Select At Least Two Photos!");
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("ERROR: Please Add A Save Location!");
                return;
            }
            else
            {
                // Reset ProgressBar
                progressBar1.Value = 0;

                // Convert Bitmap
                CombineBitmap(PhotosLoc);

                // Job Finished
                MessageBox.Show("Stitch Has Completed!");
            }
        }

        // Get Save Location
        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "Stitched Photo Save Name"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SaveLoc = dialog.FileName;
                textBox2.Text = dialog.FileName;
            }
        }

        // Get Photos Loc
        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog x = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Png Files|*.png",
                Title = "Select Photos To Stitch"
            };
            x.ShowDialog();
            PhotosLoc = x.FileNames;

            int PhotoCount = 0;

            // Sort Textbox
            foreach (string s in x.FileNames)
            {
                if (textBox1.Text == "")
                {
                    // Get Dir Count
                    PhotoCount++;
                    textBox1.Text = s;
                }
                else
                {
                    // Get Dir Count
                    PhotoCount++;
                    textBox1.Text = s + ", " + textBox1.Text;
                }
            }

        }
    }
    #endregion

    #region Helper Classes

    static class Helper
    {
        public static string GetUntilOrEmpty(this string text, string stopAt = "-")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }
    }
    #endregion
}
