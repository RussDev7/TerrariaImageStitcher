using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TerrariaImageStitcher
{
    public partial class Form1 : Form
    {

        // Say Hello To Decompilers
        private string HelloThere = "Hello there fellow Decompiler, This Program Was Made By D.RU$$ (xXCrypticNightXx).";

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
        public System.Drawing.Bitmap CombineBitmap(string[] files)
        {

            //read all images into memory
            List<System.Drawing.Bitmap> images = new List<System.Drawing.Bitmap>();
            System.Drawing.Bitmap finalImage = null;

            try
            {

                bool firstrun = true;

                int imgwide = 0;
                int imgtall = 0;
                int width = 0;
                int height = 0;

                int widecount = 0;
                int tallcount = 0;
                bool locktall = false;

                int oldvalue = 0;
                bool lockit = false;
                bool dead = false;

                int tempwidth = 1;
                int temptall = 0;

                int tallcountrunner = 0;
                int widthcountrunner = 0;

                foreach (string image in files)
                {
                    int pos = image.LastIndexOf(@"\") + 1;
                    string filenumber = image.Substring(pos, image.Length - pos).GetUntilOrEmpty();

                    if (!lockit)
                    {
                        // Look For First Number
                        if (!dead)
                        {
                            temptall++;
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
                        tempwidth++;
                    }
                    else
                    {
                        if (!dead)
                        {
                            temptall++;
                        }
                    }
                }

                // Setup Progress Bar
                progressBar1.Step = 1;
                progressBar1.Maximum = (tempwidth * temptall) + 1;

                foreach (string image in files)
                {
                    //create a Bitmap from the file and add it to the list
                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(image);

                    //update the size of the final bitmap
                    width += bitmap.Width;
                    height = bitmap.Height > height ? bitmap.Height : height;

                    images.Add(bitmap);
                }

                finalImage = new System.Drawing.Bitmap(width, (temptall * 2048) + 32); // Fix 1.2: width, (temptall * 2048) + 32 - Fixed single horizontal issue.

                //get a graphics object from the image so we can draw on it
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(System.Drawing.Color.Transparent);

                    //go through each image and draw it on the final image
                    foreach (System.Drawing.Bitmap image in images)
                    {

                        if (widthcountrunner >= (temptall * tempwidth) - temptall) // LAST ROW
                        {
                            if (firstrun) // First Row
                            {
                                // Update Image First Run
                                if (temptall <= 1)
                                {
                                    g.DrawImage(image, new System.Drawing.Rectangle(imgwide, imgtall, image.Width + 32, image.Height)); // Fix 1.1: (image.Width + 32) fixed the single width issue.
                                }
                                else
                                {
                                    g.DrawImage(image, new System.Drawing.Rectangle(imgwide, imgtall, image.Width, image.Height + 32));
                                }

                                // Update Bool
                                firstrun = false;
                            }
                            else if (tallcountrunner == temptall - 1) // Bottom Row
                            {
                                // Reset Count
                                tallcountrunner = -1;

                                // Update Image First Run
                                g.DrawImage(image, new System.Drawing.Rectangle(imgwide, imgtall, image.Width, image.Height));
                            }
                            else // Everything Else
                            {
                                // Update Image Each Time After
                                g.DrawImage(image, new System.Drawing.Rectangle(imgwide, imgtall, image.Width, image.Height + 32));
                            }
                        }
                        else
                        { // Everything EXCEPT LAST ROW
                            if (firstrun) // First Row
                            {
                                // Update Image First Run
                                if (temptall <= 1)
                                {
                                    g.DrawImage(image, new System.Drawing.Rectangle(imgwide, imgtall, image.Width + 32, image.Height)); // Fix 1.1: (image.Width + 32) - Horizontal lengh issue.
                                }
                                else
                                {
                                    g.DrawImage(image, new System.Drawing.Rectangle(imgwide, imgtall, image.Width + 32, image.Height + 32));
                                }

                                // Update Bool
                                firstrun = false;
                            }
                            else if (tallcountrunner == temptall - 1) // Bottom Row
                            {
                                // Reset Count
                                tallcountrunner = -1;

                                // Update Image First Run
                                g.DrawImage(image, new System.Drawing.Rectangle(imgwide, imgtall, image.Width + 32, image.Height));
                            }
                            else // Everything Else
                            {
                                // Update Image Each Time After
                                g.DrawImage(image, new System.Drawing.Rectangle(imgwide, imgtall, image.Width + 32, image.Height + 32));
                            }
                        }

                        // Manage Y
                        if (imgtall >= ((2048 * temptall) - 2048))
                        {
                            // Update Firstrun
                            firstrun = true;

                            imgtall = 0;
                            imgwide = imgwide + 2048;

                            // For Cropping Tall
                            if (!locktall)
                            {
                                tallcount = tallcount + image.Height;
                                locktall = true;
                            }

                            // For Cropping Wide
                            widecount = widecount + image.Width;

                            // Progress Progressbar
                            progressBar1.PerformStep();

                        }
                        else
                        {
                            imgtall = imgtall + 2048;

                            // For Cropping
                            if (!locktall)
                            {
                                tallcount = tallcount + image.Height;
                            }

                            // Progress Progressbar
                            progressBar1.PerformStep();

                        }
                        tallcountrunner++;
                        widthcountrunner++;
                    }
                }

                // Crop Image
                Bitmap finalImagecroped = new System.Drawing.Bitmap(widecount, tallcount);
                finalImagecroped = finalImage.Clone(new Rectangle(0, 0, widecount, tallcount), PixelFormat.DontCare);

                // Progress Progressbar
                progressBar1.PerformStep();

                return finalImagecroped;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (System.Drawing.Bitmap image in images)
                {
                    image.Dispose();
                }
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
                Bitmap final = CombineBitmap(PhotosLoc);

                // Save Bitmap
                if (radioButton1.Checked)
                {
                    final.Save(SaveLoc + ".png", ImageFormat.Png);
                }
                else if (radioButton2.Checked)
                {
                    final.Save(SaveLoc + ".jpg", ImageFormat.Jpeg);
                }
                else if (radioButton3.Checked)
                {
                    final.Save(SaveLoc + ".bmp", ImageFormat.Bmp);
                }
                else if (radioButton4.Checked)
                {
                    final.Save(SaveLoc + ".emf", ImageFormat.Emf);
                }
                else if (radioButton5.Checked)
                {
                    final.Save(SaveLoc + ".ico", ImageFormat.Icon);
                }
                else if (radioButton6.Checked)
                {
                    final.Save(SaveLoc + ".gif", ImageFormat.Gif);
                }
                else if (radioButton7.Checked)
                {
                    final.Save(SaveLoc + ".wmf", ImageFormat.Wmf);
                }

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

        #endregion

    }

}
