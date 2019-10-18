using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ParsecIntegrationClient
{
    public static class CommonFunctions
    {
        public static void ShowError(Exception ex)
        {
            if (ex == null)
                return;
            ShowErrorMessage(ex.Message);
        }

        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message ?? string.Empty, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult AskYesNoQuestion(string Message, string Caption)
        {
            return MessageBox.Show(Message ?? string.Empty, Caption ?? string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static Image LoadFromFile(string fileName)
        {
            byte[] buf = File.ReadAllBytes(fileName);
            MemoryStream ms = new MemoryStream(buf);
            return Image.FromStream(ms);
        }

        public static string GetImageCodecsFilesFilter(ImageCodecInfo[] codecs, bool addAllSupportedString)
        {
            StringBuilder allSupported = new StringBuilder();
            StringBuilder filter = new StringBuilder();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (allSupported.Length != 0)
                    allSupported.Append(";");

                allSupported.Append(codec.FilenameExtension);

                if (filter.Length != 0)
                    filter.Append("|");

                filter.Append(codec.CodecName);
                filter.Append("|");
                filter.Append(codec.FilenameExtension);
            }

            StringBuilder result = new StringBuilder();
            if (addAllSupportedString)
            {
                result.Append("All");
                result.Append("|");
                result.Append(allSupported.ToString());
                result.Append("|");
            }
            result.Append(filter.ToString());

            return result.ToString();
        }

        public static byte[] GetPhotoArray(Image photo)
        {
            if (photo == null)
                return null;
            MemoryStream ms = new MemoryStream();
            photo.Save(ms, ImageFormat.Png);
            byte[] buffer = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(buffer, 0, (int)ms.Length);
            ms.Position = 0;
            return buffer;
        }
    }
}
