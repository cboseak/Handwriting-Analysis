using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        const string TEST = @"mnist_test.csv";
        const string TRAIN = @"mnist_test.csv";
        const string TRAIN2 = @"trainconvert.csv";
        const string SVMPATH = @"svm.txt";

        Matrix<float> TrainingData, TestData;
        Matrix<int> TrainingLabels, TestLabels;
        SVM svm;

        public Form1()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            LoadTestingData();
            LoadTrainingData();
            svm = LoadSVM(SVMPATH);
            //var x = PrepTrainingData();
            //WriteToCsv(x);
            // MessageBox.Show("init complete");
        }

        private void WriteToCsv(List<List<float>> data)
        {
            
            var lines = new List<string>();
            foreach(var d in data)
            {
                var temp = String.Join(",", d.ToArray());
                lines.Add(temp);
            }
            File.WriteAllLines("trainconvert.csv",lines.ToArray());
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs m)
        {
            try
            {
                if (m.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (pictureBox1.Image == null)
                        pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    Point currpoint = Cursor.Position;
                    Color c = Color.Black;
                    Brush aBrush = (Brush)Brushes.Red; 
                    Graphics g = Graphics.FromImage(pictureBox1.Image);
                    g.FillRectangle(aBrush, m.Location.X, m.Location.Y, 26,26);
                    pictureBox1.Invalidate();
                }
            }
            catch
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }
      
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void detectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (TestData == null)
            {
                return;
            }

            if (svm == null)
            {
                return;
            }
            try
            {

      













                var imgData = TransformUserInput();
                var temp = new List<float[]>();
                temp.Add(imgData.ToArray());
                var imageMatrix = new Matrix<float>(To2D(temp.ToArray()));
                Predict(imageMatrix);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private List<float> TransformUserInput()
        {

         Image<Bgr, byte> temp = new Image<Bgr, byte>(new Bitmap(pictureBox1.Image, 28, 28));
  
            Image<Gray, byte> gray = temp.Convert<Gray, byte>();
  
            var resized = gray.Bitmap;
          

            var pixelData = new List<float>();
          
           
            for (var i = 0; i < 28; i++)
            {
                for (var x = 0; x < 28; x++)
                {
                    var oc = resized.GetPixel(i, x);
                    var level = (byte)((oc.R + oc.G + oc.B) / 3);
                    pixelData.Add(oc.R);
                }

            }
          
            pictureBox1.Image = resized;
            return pixelData;
        }

        private List<float> TransformUserInput(Bitmap img, float label)
        {

            var pixelData = new List<float>();
            pixelData.Add(label);
            for (var i = 0; i < 28; i++)
            {
                for (var x = 0; x < 28; x++)
                {
                    var oc = img.GetPixel(i, x);
                    var level = (byte)((oc.R + oc.G + oc.B) / 3);
                    pixelData.Add(level);
                }

            }

            return pixelData;
        }

        private Bitmap TrimWhiteSpace(Bitmap img) {
 
            var rect = GetSubRectangle(img);
            var croppedImage = img.Clone(rect, img.PixelFormat);
            return new Bitmap(croppedImage, 28, 28);
        }
        private Rectangle GetSubRectangle(Bitmap img)
        {
            var rowStart = FirstWithData(WhatRowsHaveData(img));
            var rowEnd = LastWithData(WhatRowsHaveData(img));
            var colStart = FirstWithData(WhatRowsHaveData(img));
            var colEnd = LastWithData(WhatRowsHaveData(img));
            return new Rectangle(colStart, rowStart, colEnd - colStart, rowEnd - rowStart);
        }
        private int FirstWithData(bool[] input) {
            for(var i = 0; i < input.Length; i++)
            {
                if (input[i])
                    return i;// (i > 2)?i:i-2;
            }
            return 0;
        }
        private int LastWithData(bool[] input)
        {
            var len = input.Length - 1;
            for (var i = len; i >= 0; i--)
            {
                if (input[i])
                    return  (i < len - 3) ? i : i + 2;
            }
            return 0;
        }
        private bool[] WhatRowsHaveData(Bitmap img) {
            var ret =  new bool[28];
            for (var i = 0; i < 28; i++)
            {
                for (var x = 0; x < 28; x++)
                {
                    var oc = img.GetPixel(i, x);
                    var level = (byte)((oc.R + oc.G + oc.B) / 3);
                    if (level > 0)
                        ret[i] = true;
                }

            }
            return ret;
        }
        private bool[] WhatColsHaveData(Bitmap img)
        {
            var ret = new bool[28];
            for (var i = 0; i < 28; i++)
            {
                for (var x = 0; x < 28; x++)
                {
                    var oc = img.GetPixel(i, x);
                    var level = (byte)((oc.R + oc.G + oc.B) / 3);
                    if (level > 0)
                        ret[x] = true;
                }

            }
            return ret;
        }
        private void LoadData(string path, ref Matrix<float> outData, ref Matrix<int> outLabels)
        {
            List<float[]> dataList = new List<float[]>();
            List<int> labels = new List<int>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    var lineValues = line.Split(',');
                    int label = -1;
                    if (!int.TryParse(lineValues.First(), out label))
                        continue;
                    float[] data = lineValues.Skip(1).Select(x => float.Parse(x)).ToArray();
                    dataList.Add(data);
                    labels.Add(label);

                }

                outData = new Matrix<float>(To2D<float>(dataList.ToArray()));
                outLabels = new Matrix<int>(labels.ToArray());
            }
        }

        private void LoadTrainingData()
        {
            LoadData(TRAIN2, ref TrainingData, ref TrainingLabels);
        }
        private void LoadTestingData()
        {

            LoadData(TEST, ref TestData, ref TestLabels);
        }

        private void loadNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TestData == null)
            {
                return;
            }

            if (svm == null)
            {
                return;
            }
            try
            {
                Random rnd = new Random();
                var rowL = rnd.Next(0, TrainingData.Rows - 1);
                var label = TestLabels[rowL, 0];
                Matrix<float> row = TestData.GetRow(rowL);
                Predict(row, label.ToString());
                Image<Gray, byte> imgout = TestData.GetRow(rowL).Mat.Reshape(0, 28).ToImage<Gray, byte>().ThresholdBinary(new Gray(30), new Gray(255));
                pictureBox1.Image = imgout.Bitmap;
        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private List<List<float>> PrepTrainingData() {
            List<List<float>> ret = new List<List<float>>();
            for (var i = 0; i < TestData.Rows; i++)
            {

                Image<Gray, byte> imgout = TestData.GetRow(i).Mat.Reshape(0, 28).ToImage<Gray, byte>().ThresholdBinary(new Gray(30), new Gray(255));
                ret.Add(TransformUserInput(imgout.Bitmap,TestLabels[i, 0]));
            }
            return ret;

        }
        private void Predict(Matrix<float> row,string label = "") {
            float predict = svm.Predict(row);
            
            lABELToolStripMenuItem.Text = "Input: " + label.ToString();
            pREDICTIONToolStripMenuItem.Text = "Predicted: " + predict.ToString();
        }
        private SVM LoadSVM(string path)
        {
            SVM svm = new SVM();
            try
            {
                if (File.Exists(path))
                {

                    FileStorage file = new FileStorage(path, FileStorage.Mode.Read);
                    svm.Read(file.GetNode("opencv_ml_svm"));
                }
                else
                {

                    svm.C = 100;
                    svm.Type = SVM.SvmType.CSvc;
                    svm.Gamma = 0.005;
                    svm.SetKernel(SVM.SvmKernelType.Linear);
                    svm.TermCriteria = new MCvTermCriteria(1000, 1e-6);
                    svm.Train(TrainingData, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, TrainingLabels);
                    svm.Save(path);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return svm;
        }
        private T[,] To2D<T>(T[][] source)
        {
            try
            {
                int FirstDim = source.Length;
                int SecondDim = source.GroupBy(row => row.Length).Single().Key;
                var result = new T[FirstDim, SecondDim];
                for (int i = 0; i < FirstDim; ++i)
                    for (int j = 0; j < SecondDim; ++j)
                        result[i, j] = source[i][j];

                return result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }
        }

    }
}
