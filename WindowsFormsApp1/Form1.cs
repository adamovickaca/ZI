using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ServiceReference1.IService1 client = new ServiceReference1.Service1Client();

        public Form1()
        {
            InitializeComponent();
        }

        byte[] fInfo1, fInfo2;


        ////////////RC6////////////////
        //Encrypt inout RC6
        private void encryptRC6_Click(object sender, EventArgs e)
        {
            string plaintext = inputRC6.Text;
            string kljuc = keyRC6.Text;
            string duzinaK = inputRoundRC6.Text;
            byte[] ciphertext = client.CryptRC6(plaintext, duzinaK, kljuc);
            //var r = Convert.ToInt32(inputRoundRC6.Text);
            outputRC6.Text = Encoding.UTF8.GetString(ciphertext);

        }

        //Decrypt input RC6
        private void decryptRC6_Click(object sender, EventArgs e)
        {
            byte[] ciphertext = Encoding.UTF8.GetBytes(outputRC6.Text);
            byte[] plaintext = client.DecryptRC6(ciphertext);
            inputRC6.Text = Encoding.UTF8.GetString(plaintext);
        }

        //Encrypt file RC6
        private void btnEncrFileRC6_Click(object sender, EventArgs e)
        {
            if (inputfile.Text != "" && outputfile.Text != "" && keyfile.Text != "")
            {
                byte[] readInputBytes;

                if (inputfile.Text.EndsWith(".txt") && outputfile.Text.EndsWith(".txt") && keyfile.Text.EndsWith(".txt"))
                {

                    string readInput = File.ReadAllText(inputfile.Text);
                    string readKey = File.ReadAllText(keyfile.Text);
                }


            }

        }

        //input file RC6
        private void inputfileRC6_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                inputfile.Text = putanja;

            }
        }

        //key file RC6
        private void keyfileRC6_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                keyfile.Text = putanja;
            }

        }

        //output file RC6
        private void outputfileRC6_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                outputfile.Text = putanja;
            }

        }

        //////////END RC6/////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //////////////START KNAPSACK/////////////
        ///

        private void generisiJavni_Click(object sender, EventArgs e)
        {
            //string priv = privatniK.Text;
            string[] priv = this.privatniK.Text.Split(',');
            //int[] priv = { 2, 3, 7, 14, 30, 57, 120, 251 };

            string n = inputN.Text;
            string m = inputM.Text;

            int Nn = Convert.ToInt32(n);
            int Mm = Convert.ToInt32(m);

            int[] result = new int[priv.Length];
            for (int i = 0; i < priv.Length; i++)
            {
                result[i] = Convert.ToInt32(priv[i]);
            }

            javniK.Text = client.KodirajJavni(result, Nn, Mm);
        }

        private void enkriptuj_Click(object sender, EventArgs e)
        {
            string[] priv = this.privatniK.Text.Split(',');
            //int[] priv = { 2, 3, 7, 14, 30, 57, 120, 251 };

            string n = inputN.Text;
            string m = inputM.Text;

            int Nn = Convert.ToInt32(n);
            int Mm = Convert.ToInt32(m);

            int[] result = new int[priv.Length];
            for (int i = 0; i < priv.Length; i++)
            {
                result[i] = Convert.ToInt32(priv[i]);
            }

            int[] javni = { };
            for (int i = 0; i < result.Length; i++)
            {
                javni[i] = result[i] * Mm % Nn;
            }

            string poruka = unosiTextKS.Text;

            byte[] porukaZaKript = System.Text.Encoding.ASCII.GetBytes(poruka);

            int vr;
            int[] vrNiz = { };
            foreach (byte b in porukaZaKript)
            {
                vr = Convert.ToInt32(b);
                vrNiz = ToBin(vr);
            }

            rezultatKS.Text = client.EncryptKS(javni, vrNiz);

        }
        public int[] ToBin(int broj)
        {
            int[] vrNiz = new int[8];
            for (int i = 0; i < 8; i++)
            {
                vrNiz[i] = 0;
            }

            int j = 7;
            if (broj > 128)
            {
                j = 7;
            }
            if (broj >= 64 && broj < 128)
            {
                j = 6;
            }
            if (broj >= 32 && broj < 64)
            {
                j = 5;
            }
            if (broj >= 16 && broj < 32)
            {
                j = 4;
            }
            if (broj >= 8 && broj < 16)
            {
                j = 3;
            }
            if (broj >= 4 && broj < 8)
            {
                j = 2;
            }
            if (broj >= 2 && broj < 4)
            {
                j = 1;
            }
            if (broj == 1)
            {
                j = 0;
            }

            for (int k = 0; k < j + 1; k++)
            {
                vrNiz[k] = broj % 2;
                broj = broj / 2;
            }


            return vrNiz;
        }

        private void buttonDecryptKS_Click(object sender, EventArgs e)
        {
            string[] priv = this.privatniK.Text.Split(',');
            //int[] priv = { 2, 3, 7, 14, 30, 57, 120, 251 };

            string n = inputN.Text;
            string m = inputM.Text;

            int Nn = Convert.ToInt32(n);
            int Mm = Convert.ToInt32(m);

            int[] result = new int[priv.Length];
            for (int i = 0; i < priv.Length; i++)
            {
                result[i] = Convert.ToInt32(priv[i]);
            }

            int[] javni = { };
            for (int i = 0; i < result.Length; i++)
            {
                javni[i] = result[i] * Mm % Nn;
            }

            int TC = 0;
            int vr;
            int im = inverznoM(Nn, Mm);
            string poruka = unosiTextKS.Text;

            int[] dobijeno = new int[8];
            String[] porukaZaDekript = poruka.Split(' ');
            foreach (string rec in porukaZaDekript)
            {
                if (String.IsNullOrEmpty(rec))
                    continue;
                for (int i = 0; i < 8; i++)
                {
                    dobijeno[i] = 0;
                }
                vr = Convert.ToInt32(rec);
                TC = (vr * im) % Nn;

            }

            rezultatKS.Text = client.DecryptKS(result, TC);

        }

        private int inverznoM(int n, int m)
        {
            int im = 0;
            int i = 1;


            if ((n * i + 1) % m != 0)
            {
                i++;
            }
            else
            {
                im = (n * i + 1) / m;
            }

            return im;


        }

        private void btnfileEncBF_Click(object sender, EventArgs e)
        {
            string[] priv = this.privatniK.Text.Split(',');
            //int[] priv = { 2, 3, 7, 14, 30, 57, 120, 251 };

            string n = inputN.Text;
            string m = inputM.Text;

            int Nn = Convert.ToInt32(n);
            int Mm = Convert.ToInt32(m);

            int[] result = new int[priv.Length];
            for (int i = 0; i < priv.Length; i++)
            {
                result[i] = Convert.ToInt32(priv[i]);
            }

            int[] javni = { };
            for (int i = 0; i < result.Length; i++)
            {
                javni[i] = result[i] * Mm % Nn;
            }

            if (pathInputKS.Text != "" && pathInputKS.Text != "")
            {
                if (pathInputKS.Text.EndsWith(".txt") && pathInputKS.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    //fajlovi su .txt
                    string poruka = File.ReadAllText(pathInputKS.Text);

                    byte[] porukaZaKript = System.Text.Encoding.ASCII.GetBytes(poruka);

                    int vr;
                    int[] vrNiz = { };
                    foreach (byte b in porukaZaKript)
                    {
                        vr = Convert.ToInt32(b);
                        vrNiz = ToBin(vr);
                    }
                    string res = client.EncryptKS(javni, vrNiz);

                    //upis
                    File.WriteAllText(otputPathKS.Text, res);

                }

            }
        }
        private void btnfileDecBF_Click(object sender, EventArgs e)
        {
            string[] priv = this.privatniK.Text.Split(',');
            //int[] priv = { 2, 3, 7, 14, 30, 57, 120, 251 };

            string n = inputN.Text;
            string m = inputM.Text;

            int Nn = Convert.ToInt32(n);
            int Mm = Convert.ToInt32(m);

            int[] result = new int[priv.Length];
            for (int i = 0; i < priv.Length; i++)
            {
                result[i] = Convert.ToInt32(priv[i]);
            }

            int TC = 0;
            int vr;
            int im = inverznoM(Nn, Mm);

            int[] dobijeno = new int[8];




            if (pathInputKS.Text != "" && otputPathKS.Text != "")
            {
                if (pathInputKS.Text.EndsWith(".txt") && otputPathKS.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    //fajlovi su .txt
                    string poruka = File.ReadAllText(pathInputKS.Text);
                    String[] porukaZaDekript = poruka.Split(' ');
                    foreach (string rec in porukaZaDekript)
                    {
                        if (String.IsNullOrEmpty(rec))
                            continue;
                        for (int i = 0; i < 8; i++)
                        {
                            dobijeno[i] = 0;
                        }
                        vr = Convert.ToInt32(rec);
                        TC = (vr * im) % Nn;

                    }

                    string res = client.DecryptKS(result, TC);

                    //upis
                    File.WriteAllText(otputPathKS.Text, res);

                }

            }
        }

        //input file KnapSack
        private void btnFileKS_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                pathInputKS.Text = putanja;
            }
        }

        //output file KnapSack
        private void outFileKS_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                otputPathKS.Text = putanja;
            }

        }

        /// <summary>
        /// ///////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// bifid
        private void btnEncrBF_Click(object sender, EventArgs e)
        {
            outputBF.Text = client.CryptBifid(inputBF.Text, inputKeyBF.Text);

        }

        private void btnDecrBF_Click(object sender, EventArgs e)
        {
            outputBF.Text = client.DecryptBifid(inputBF.Text, inputKeyBF.Text);
        }


        private void btnFileBF_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                keyPathBF.Text = putanja;
            }

        }

        private void outFileBF_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                otputPathBF.Text = putanja;
            }
        }

        private void keyFileBF_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                keyPathBF.Text = putanja;
            }

        }

        private void btnfileEncBF_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                pathInputBF.Text = putanja;
            }
        }



        private void btnfileDecBF_Click_1(object sender, EventArgs e)
        {
            if (pathInputBF.Text != "" && otputPathBF.Text != "" && keyPathBF.Text != "")
            {
                if (pathInputBF.Text.EndsWith(".txt") && otputPathBF.Text.EndsWith(".txt") && keyPathBF.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(pathInputBF.Text);
                    string readKey = File.ReadAllText(keyPathBF.Text);

                    //povratna vrednost enkripcije
                    string res = client.DecryptBifid(readInput, readKey);

                    //upis
                    File.WriteAllText(otputPathBF.Text, res);

                }
                if (otputPathBF != null)
                {
                    fInfo2 = client.TH(otputPathBF.Text);
                    if (fInfo1.SequenceEqual(fInfo2))
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl su isti");
                    }
                    else
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl nisu isti");
                    }
                }
            }
        }



        ///////////END BIFID///////////


        //Encrypt input CTR
        private void encrCTR_Click(object sender, EventArgs e)
        {
            var input = Encoding.UTF8.GetBytes(inputCTR.Text);
            outputCTR.Text = Convert.ToBase64String(client.EncryptCTR(input));
        }

        private void decrCTR_Click(object sender, EventArgs e)
        {
            var input = Convert.FromBase64String(inputCTR.Text);
            outputCTR.Text = Encoding.UTF8.GetString(client.DecryptCTR(input)).Trim();
        }

        //Input file CTR//
        private void btnfileInputCTR_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                inputFileCTR.Text = putanja;
            }
        }

        //Output file CTR//
        private void btnOutFileCTR_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                string putanja = d.FileName;
                outputFileCTR.Text = putanja;
            }

        }

        //Encrypt file CTR//
        private void encrFileCTR_Click(object sender, EventArgs e)
        {

            if (inputFileCTR.Text != "" && outputFileCTR.Text != "")
            {
                byte[] readInputBytes;
                if (inputFileCTR != null)
                {
                    fInfo1 = client.TH(inputFileCTR.Text);
                }
                if (inputFileCTR.Text.EndsWith(".txt") && outputFileCTR.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(inputFileCTR.Text);
                    readInputBytes = Encoding.UTF8.GetBytes(readInput);
                    byte[] res = client.EncryptCTR(readInputBytes);
                    File.WriteAllText(outputFileCTR.Text, Convert.ToBase64String(res));

                }
                if (inputFileCTR.Text.EndsWith(".bin") && outputFileCTR.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    readInputBytes = File.ReadAllBytes(inputFileCTR.Text);
                    byte[] res = client.EncryptCTR(readInputBytes);
                    File.WriteAllBytes(outputFileCTR.Text, res);

                }
            }
        }

        //Decrypt file CTR//
        private void decrFileCTR_Click(object sender, EventArgs e)
        {
            if (inputFileCTR.Text != "" && outputFileCTR.Text != "")
            {

                byte[] readInputBytes;
                if (inputFileCTR.Text.EndsWith(".txt") && outputFileCTR.Text.EndsWith(".txt"))
                {
                    //fajlovi su .txt
                    string readInput = File.ReadAllText(inputFileCTR.Text);
                    readInputBytes = Convert.FromBase64String(readInput);
                    byte[] res = client.DecryptCTR(readInputBytes);
                    File.WriteAllText(outputFileCTR.Text, Encoding.UTF8.GetString(res).Trim());

                }
                if (inputFileCTR.Text.EndsWith(".bin") && outputFileCTR.Text.EndsWith(".bin"))
                {
                    //fajlovi su .bin
                    readInputBytes = File.ReadAllBytes(inputFileCTR.Text);
                    byte[] res = client.DecryptCTR(readInputBytes);
                    File.WriteAllBytes(outputFileCTR.Text, res);
                }
                if (outputFileCTR != null)
                {
                    fInfo2 = client.TH(outputFileCTR.Text);
                    if (fInfo1.SequenceEqual(fInfo2))
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl su isti");
                    }
                    else
                    {
                        MessageBox.Show("Pocetni i dekriptovani fajl nisu isti");
                    }
                }
            }
        }
        //////////////BMP///////////
        //Encrypt BMP
        private void btnEncBMP_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Open Image";
            d.Filter = "bmp files (*.bmp)|*.bmp";
            PictureBox PictureBox1 = new PictureBox();
            string putanja = string.Empty;
            if (d.ShowDialog() == DialogResult.OK)
            {
                PictureBox1.Image = new Bitmap(d.FileName);
                putanja = Path.GetDirectoryName(d.FileName);
            }

            var image = PictureBox1.Image;
            var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            var imgArray = ms.ToArray();

            // 56 bajta je header za bitmapu, cuvamo ga da bi kasnije mogli da napravimo kriptovanu bitmapu i njega ne kriptujemo
            byte[] headerArr = new byte[56];
            for (int i = 0; i < 56; i++)
            {
                headerArr[i] = imgArray[i];
            }

            imgArray = imgArray.Skip(56).ToArray();
            byte[] res = client.EncryptCTR(imgArray);

            var encrByte = headerArr.Concat(res);
            using (MemoryStream ms1 = new MemoryStream(encrByte.ToArray()))
            {
                Bitmap bitmap = new Bitmap(ms1);
                pictureBMP.Image = bitmap;

                pictureBMP.Image.Save(putanja + "/encryptedImage.bmp", ImageFormat.Bmp);
            }
        }

        private void btnDecBMP_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Open Image";
            d.Filter = "bmp files (*.bmp)|*.bmp";
            PictureBox PictureBox1 = new PictureBox();
            string putanja = string.Empty;
            if (d.ShowDialog() == DialogResult.OK)
            {
                PictureBox1.Image = new Bitmap(d.FileName);
                putanja = Path.GetDirectoryName(d.FileName);
            }
            else
            {
                return;
            }

            var image = PictureBox1.Image;
            var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            var imgArray = ms.ToArray();
            // 56 bajta je header za bitmapu, cuvamo ga da bi kasnije mogli da napravimo dekriptovanu bitmapu i njega ne kriptujemo
            byte[] headerArr = new byte[56];
            for (int i = 0; i < 56; i++)
            {
                headerArr[i] = imgArray[i];

            }

            imgArray = imgArray.Skip(56).ToArray();
            byte[] res = client.DecryptCTR(imgArray);

            var encrByte = headerArr.Concat(res);
            using (MemoryStream ms1 = new MemoryStream(encrByte.ToArray()))
            {
                Bitmap bitmap = new Bitmap(ms1);
                pictureBMP.Image = bitmap;

                pictureBMP.Image.Save(putanja + "/decryptedImage.bmp", ImageFormat.Bmp);
            }

        }


        private void btnEncBMP_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Open Image";
            d.Filter = "bmp files (*.bmp)|*.bmp";
            PictureBox PictureBox1 = new PictureBox();
            string putanja = string.Empty;
            if (d.ShowDialog() == DialogResult.OK)
            {
                PictureBox1.Image = new Bitmap(d.FileName);
                putanja = Path.GetDirectoryName(d.FileName);
            }

            var image = PictureBox1.Image;
            var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            var imgArray = ms.ToArray();

            // 56 bajta je header za bitmapu, cuvamo ga da bi kasnije mogli da napravimo kriptovanu bitmapu i njega ne kriptujemo
            byte[] headerArr = new byte[56];
            for (int i = 0; i < 56; i++)
            {
                headerArr[i] = imgArray[i];
            }

            imgArray = imgArray.Skip(56).ToArray();
            byte[] res = client.EncryptCTR(imgArray);

            var encrByte = headerArr.Concat(res);
            using (MemoryStream ms1 = new MemoryStream(encrByte.ToArray()))
            {
                Bitmap bitmap = new Bitmap(ms1);
                pictureBMP.Image = bitmap;

                pictureBMP.Image.Save(putanja + "/encryptedImage.bmp", ImageFormat.Bmp);
            }
        }

    

        }
    }



