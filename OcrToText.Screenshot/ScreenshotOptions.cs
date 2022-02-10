﻿using System.Windows.Media;

namespace OcrToText.Screenshot
{
    public class ScreenshotOptions
    {
        public ScreenshotOptions()
        {
            BackgroundOpacity = 0.5;
            SelectionRectangleBorderBrush = Brushes.Red;
        }

        public bool CloseAfterSelection { get; set; }

        /// <summary>
        /// Background opacity when selecting region to capture.
        /// </summary>
        public double BackgroundOpacity { get; set; }

        /// <summary>
        /// Brush used to draw border of selection rectangle.
        /// </summary>
        public Brush SelectionRectangleBorderBrush { get; set; }
    }
}