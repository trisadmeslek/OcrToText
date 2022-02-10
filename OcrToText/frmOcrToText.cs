using AForge.Imaging.Filters;
using OpenCvSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Tesseract;
using Clipboard = System.Windows.Forms.Clipboard;

namespace OcrToText
{
    public partial class frmOcrToText : Form
    {
        readonly string[] langList = new string[] { "Afrikaans,afr", "Amharic,amh", "Arabic,ara", "Assamese,asm", "Azerbaijani,aze", "Azerbaijani - Cyrilic,aze_cyrl", "Belarusian,bel", "Bengali,ben", "Tibetan,bod", "Bosnian,bos", "Breton,bre", "Bulgarian,bul", "Catalan; Valencian,cat", "Cebuano,ceb", "Czech,ces", "Chinese - Simplified,chi_sim", "Chinese - Simplified (vertical),chi_sim_vert", "Chinese - Traditional,chi_tra", "Chinese - Traditional (vertical),chi_tra_vert", "Cherokee,chr", "Corsican,cos", "Welsh,cym", "Danish,dan", "German,deu", "Dhivehi; Divehi; Maldivian,div", "Dzongkha,dzo", "Greek: Modern (1453-),ell", "English,eng", "English: Middle (1100-1500),enm", "Esperanto,epo", "Math / equation detection module,equ", "Estonian,est", "Basque,eus", "Faroese,fao", "Persian,fas", "Filipino (old - Tagalog),fil", "Finnish,fin", "French,fra", "German - Fraktur,frk", "French: Middle (ca.1400-1600),frm", "Western Frisian,fry", "Scottish Gaelic,gla", "Irish,gle", "Galician,glg", "Greek: Ancient (to 1453) (contrib),grc", "Gujarati,guj", "Haitian; Haitian Creole,hat", "Hebrew,heb", "Hindi,hin", "Croatian,hrv", "Hungarian,hun", "Armenian,hye", "Inuktitut,iku", "Indonesian,ind", "Icelandic,isl", "Italian,ita", "Italian - Old,ita_old", "Javanese,jav", "Japanese,jpn", "Japanese (vertical),jpn_vert", "Kannada,kan", "Georgian,kat", "Georgian - Old,kat_old", "Kazakh,kaz", "Central Khmer,khm", "Kirghiz; Kyrgyz,kir", "Kurmanji (Kurdish - Latin Script),kmr", "Korean,kor", "Korean (vertical),kor_vert", "Lao,lao", "Latin,lat", "Latvian,lav", "Lithuanian,lit", "Luxembourgish,ltz", "Malayalam,mal", "Marathi,mar", "Macedonian,mkd", "Maltese,mlt", "Mongolian,mon", "Maori,mri", "Malay,msa", "Burmese,mya", "Nepali,nep", "Dutch; Flemish,nld", "Norwegian,nor", "Occitan (post 1500),oci", "Oriya,ori", "Orientation and script detection module,osd", "Panjabi; Punjabi,pan", "Polish,pol", "Portuguese,por", "Pushto; Pashto,pus", "Quechua,que", "Romanian; Moldavian; Moldovan,ron", "Russian,rus", "Sanskrit,san", "Sinhala; Sinhalese,sin", "Slovak,slk", "Slovenian,slv", "Sindhi,snd", "Spanish; Castilian,spa", "Spanish; Castilian - Old,spa_old", "Albanian,sqi", "Serbian,srp", "Serbian - Latin,srp_latn", "Sundanese,sun", "Swahili,swa", "Swedish,swe", "Syriac,syr", "Tamil,tam", "Tatar,tat", "Telugu,tel", "Tajik,tgk", "Thai,tha", "Tigrinya,tir", "Tonga,ton", "Turkish,tur", "Uighur; Uyghur,uig", "Ukrainian,ukr", "Urdu,urd", "Uzbek,uzb", "Uzbek - Cyrilic,uzb_cyrl", "Vietnamese,vie", "Yiddish,yid", "Yoruba,yor", };

        KeyboardHook _hook = new KeyboardHook();
        Bitmap _bmp = null;
        bool _isFormClosing = false;
        public frmOcrToText()
        {
            InitializeComponent();
            cmbLang.Items.AddRange(langList);
            iconOcrToText.Icon = SystemIcons.Shield;
            _hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            _hook.RegisterHotKey(OcrToText.ModifierKeys.Alt, Keys.S);
            _hook.RegisterHotKey(OcrToText.ModifierKeys.Alt, Keys.Q);
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            BitmapSource bitmapSource = null;
            if (e.Modifier == OcrToText.ModifierKeys.Alt && e.Key == Keys.S)
            {
                if (!this.TryCaptureRegion(out bitmapSource))
                    return;

                _bmp = GetBitmap(bitmapSource);
                this.Show();
            }
            else if (e.Modifier == OcrToText.ModifierKeys.Alt && e.Key == Keys.Q)
            {
                if (!this.TryCaptureRegion(out bitmapSource))
                    return;

                _bmp = GetBitmap(bitmapSource);
                SendPictureToMSPaint(_bmp);
            }
        }

        [DllImport("User32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);


        private static void SendPictureToMSPaint(Bitmap bmp)
        {
            Clipboard.SetData(System.Windows.Forms.DataFormats.Bitmap, bmp);

            //optional#1 - open MSPaint yourself
            var proc = Process.Start("mspaint");

            IntPtr msPaint = IntPtr.Zero;
            while (msPaint == IntPtr.Zero) //optional#1 - if opening MSPaint yourself, wait for it to appear
                msPaint = FindWindowEx(IntPtr.Zero, new IntPtr(0), "MSPaintApp", null);

            SetForegroundWindow(msPaint); //optional#2 - if not opening MSPaint yourself

            IntPtr currForeground = IntPtr.Zero;
            while (currForeground != msPaint)
            {
                Thread.Sleep(250); //sleep before get to exit loop and send immediately
                currForeground = GetForegroundWindow();
            }
            SendKeys.SendWait("^v");
        }

        bool TryCaptureRegion(out BitmapSource bitmapSource)
        {
            bitmapSource = Screenshot.Screenshot.CaptureRegion();
            return bitmapSource != null;
        }

        Bitmap GetBitmap(BitmapSource source)
        {
            Bitmap bmp = new Bitmap(
              source.PixelWidth,
              source.PixelHeight,
              PixelFormat.Format32bppRgb);
            BitmapData data = bmp.LockBits(
              new Rectangle(System.Drawing.Point.Empty, bmp.Size),
              ImageLockMode.WriteOnly,
              PixelFormat.Format32bppRgb);
            source.CopyPixels(
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);
            bmp.UnlockBits(data);

            return bmp;
        }

        private void cxtMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "itemExit")
            {
                _isFormClosing = !_isFormClosing;
                System.Windows.Forms.Application.Exit();
            }

        }

        void PreProcessForOcr()
        {
            Mat mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(_bmp);
            Mat outputArray = mat.EmptyClone();
            //Cv2.ImShow("bmp to mat conversion", mat);

            Cv2.CvtColor(mat, outputArray, ColorConversionCodes.BGR2GRAY);
            mat = outputArray.Clone();
            outputArray.Dispose();
            outputArray = mat.EmptyClone();
            //Cv2.ImShow("convert to grayscale", mat);

            Cv2.BitwiseNot(mat, outputArray);
            mat = outputArray.Clone();
            outputArray.Dispose();
            outputArray = mat.EmptyClone();
            //Cv2.ImShow("bitwise not", mat);

            double threshold = Cv2.Threshold(mat, outputArray, 0, 255, ThresholdTypes.BinaryInv | ThresholdTypes.Otsu);
            mat = outputArray.Clone();
            outputArray.Dispose();
            outputArray = mat.EmptyClone();
            //Cv2.ImShow("binarization", mat);

            Cv2.FastNlMeansDenoising(mat, outputArray, 5, 7, 21);
            mat = outputArray.Clone();
            outputArray.Dispose();
            outputArray = mat.EmptyClone();
            //Cv2.ImShow("denoising", mat);

            OpenCvSharp.Size size = new OpenCvSharp.Size(_bmp.Width * 10, _bmp.Height * 10);

            Cv2.Resize(mat, outputArray, size, 0, 0, InterpolationFlags.LinearExact);
            mat = outputArray.Clone();
            outputArray.Dispose();
            outputArray = mat.EmptyClone();
            //Cv2.ImShow("resizing", mat);

            _bmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);

            //Dilatation dilatation = new Dilatation();
            //_bmp = dilatation.Apply(_bmp);
            //Cv2.ImShow("dilatation", OpenCvSharp.Extensions.BitmapConverter.ToMat(_bmp));

            //Erosion erosion = new Erosion();
            //_bmp = erosion.Apply(_bmp);
            //Cv2.ImShow("erosion", OpenCvSharp.Extensions.BitmapConverter.ToMat(_bmp));

            //DeskewImage();
        }

        void DeskewImage()
        {
            Bitmap tempImage = _bmp;
            Bitmap image;
            //if (tempImage.PixelFormat.ToString().Equals("Format8bppIndexed"))
            //{
            image = tempImage.Clone() as Bitmap;
            //}
            //else
            //{
            //    image = AForge.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(tempImage);
            //}

            tempImage.Dispose();

            AForge.Imaging.DocumentSkewChecker skewChecker = new AForge.Imaging.DocumentSkewChecker();
            // get documents skew angle
            double angle = skewChecker.GetSkewAngle(image);

            if (angle < -45)
                angle = -(90 + angle);
            else
                angle = -angle;

            // create rotation filter
            AForge.Imaging.Filters.RotateBilinear rotationFilter = new AForge.Imaging.Filters.RotateBilinear(-angle);
            rotationFilter.FillColor = Color.Black;
            // rotate image applying the filter
            Bitmap rotatedImage = rotationFilter.Apply(image);

            _bmp = rotatedImage;
            Cv2.ImShow("deskew", OpenCvSharp.Extensions.BitmapConverter.ToMat(_bmp));

            image.Dispose();
        }

        private void cmbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Hide();

            try
            {
                PreProcessForOcr();
                string lang = cmbLang.SelectedItem != null ? cmbLang.SelectedItem.ToString() : "eng";

                string[] splittedLangs = lang.Split(',');
                lang = splittedLangs[splittedLangs.Length - 1];

                TesseractEngine engine = new TesseractEngine("./tessdata", lang, EngineMode.Default);
                engine.SetVariable("preserve_interword_spaces", 1);
                engine.SetVariable("tessedit_write_images", 1);
                Page page = engine.Process(_bmp, PageSegMode.Auto);
                string result = page.GetText();

                if (!string.IsNullOrWhiteSpace(result))
                    result = result.Trim();
                else
                    return;

                Clipboard.SetText(result);

                page.Dispose();
                engine.Dispose();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.StackTrace);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            cmbLang_SelectedIndexChanged(sender, e);
        }

        private void frmOcrToText_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
