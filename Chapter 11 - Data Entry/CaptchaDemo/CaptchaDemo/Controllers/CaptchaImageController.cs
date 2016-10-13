using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.Mvc;
using CaptchaDemo.Helpers;
using StringFormat=System.Drawing.StringFormat;

namespace CaptchaDemo.Controllers
{
    public class CaptchaImageController : Controller
    {
        private const int ImageWidth = 200, ImageHeight = 70;
        private const string FontFamily = "Times New Roman";
        private readonly static Brush Foreground = Brushes.Navy;
        private readonly static Color Background = Color.Silver;

        private const int WarpFactor = 5;
        private const Double xAmp = WarpFactor * ImageWidth / 100;
        private const Double yAmp = WarpFactor * ImageHeight / 85;
        private const Double xFreq = 2 * Math.PI / ImageWidth;
        private const Double yFreq = 2 * Math.PI / ImageHeight;

        private GraphicsPath DeformPath(GraphicsPath path)
        {
            PointF[] deformed = new PointF[path.PathPoints.Length];
            Random rng = new Random();
            Double xSeed = rng.NextDouble() * 2 * Math.PI;
            Double ySeed = rng.NextDouble() * 2 * Math.PI;
            for (int i = 0; i < path.PathPoints.Length; i++)
            {
                PointF original = path.PathPoints[i];
                Double val = xFreq * original.X + yFreq * original.Y;
                int xOffset = (int)(xAmp * Math.Sin(val + xSeed));
                int yOffset = (int)(yAmp * Math.Sin(val + ySeed));
                deformed[i] = new PointF(original.X + xOffset, original.Y + yOffset);
            }
            return new GraphicsPath(deformed, path.PathTypes);
        }


        public void Render(string challengeGuid)
        {
            // Retrieve the solution text from Session[]
            string key = CaptchaHelper.SessionKeyPrefix + challengeGuid;
            string solution = (string)HttpContext.Session[key];

            if (solution != null)
            {
                // Make a blank canvas to render the CAPTCHA on
                using (Bitmap bmp = new Bitmap(ImageWidth, ImageHeight))
                using (Graphics g = Graphics.FromImage(bmp))
                using (Font font = new Font(FontFamily, 1f))
                {
                    g.Clear(Background);

                    // Perform trial rendering to determine best font size
                    SizeF finalSize;
                    SizeF testSize = g.MeasureString(solution, font);
                    float bestFontSize = Math.Min(ImageWidth / testSize.Width,
                                            ImageHeight / testSize.Height) * 0.95f;

                    using (Font finalFont = new Font(FontFamily, bestFontSize))
                    {
                        finalSize = g.MeasureString(solution, finalFont);
                    }

                    // Get a path representing the text centered on the canvas
                    g.PageUnit = GraphicsUnit.Point;
                    PointF textTopLeft = new PointF((ImageWidth - finalSize.Width) / 2,
                                                  (ImageHeight - finalSize.Height) / 2);
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddString(solution, new FontFamily(FontFamily), 0,
                            bestFontSize, textTopLeft, StringFormat.GenericDefault);

                        // Render the path to the bitmap
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.FillPath(Foreground, DeformPath(path));
                        g.Flush();

                        // Send the image to the response stream in PNG format
                        Response.ContentType = "image/png";
                        bmp.Save(Response.OutputStream, ImageFormat.Png);
                    }
                }
            }
        }
    }

}