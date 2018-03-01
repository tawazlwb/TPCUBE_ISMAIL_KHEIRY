using System.IO;
using static ikheiry.FileWriter.Bitmap;

namespace ikheiry.FileWriter
{
    class PPMWriter
    {
        public static void WriteBitmapToPPM(string file, Bitmap bitmap)
        {
            //Use a streamwriter to write the text part of the encoding
            var writer = new StreamWriter(file);
            writer.Write("P6" + "\n");
            writer.Write(bitmap.Width + " " + bitmap.Height + "\n");
            writer.Write("255" + "\n");
            writer.Close();
            //Switch to a binary writer to write the data
            var writerB = new BinaryWriter(new FileStream(file, FileMode.Append));
            for (int x = 0; x < bitmap.Height; x++)
                for (int y = 0; y < bitmap.Width; y++)
                {
                    Color color = bitmap.GetPixel(y, x);
                    writerB.Write(color.Red);
                    writerB.Write(color.Green);
                    writerB.Write(color.Blue);
                }
            writerB.Close();
        }
    }
}
