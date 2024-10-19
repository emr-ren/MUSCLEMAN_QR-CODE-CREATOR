using QRCoder;
using System.Windows.Forms;

namespace MUSCLEMAN_QR_CODE_CREATOR
{
    public partial class QRCODECREATOR : Form
    {
        private Bitmap qrCodeImage;
        public QRCODECREATOR()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // Üst çubuğu kaldırır
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TextBox'tan kullanıcıdan alınan metni veya URL'yi alıyoruz
            string data = textBox1.Text;

            // Boş veri kontrolü
            if (string.IsNullOrEmpty(data))
            {
                MessageBox.Show("Please enter a text or URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // QR Code Generator nesnesini oluştur
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            // QR kodunu Bitmap olarak oluştur
            qrCodeImage = qrCode.GetGraphic(20);

            // PictureBox içine QR kodu koy
            pictureBox1.Image = qrCodeImage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (qrCodeImage == null)
            {
                MessageBox.Show("Please generate a QR code first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Masaüstüne "QR Kodları" klasörü oluştur
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folderPath = Path.Combine(desktopPath, "QR Codes");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Dosya ismi olarak kullanıcıdan alınan link/metin kullanılacak
            string fileName = textBox1.Text.Replace(":", "").Replace("/", "").Replace("\\", ""); // Geçersiz karakterleri kaldırma
            string filePath = Path.Combine(folderPath, fileName + ".png");

            try
            {
                // Bitmap'i dosya olarak kaydet
                qrCodeImage.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                MessageBox.Show($"QR code saved successfully: {filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
