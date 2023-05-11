using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace RotuloForm.Model
{
    public class TextoModel
    {
        public string Texto { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Cor { get; set; }
        public float Angulo { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public EstiloTextoModel EstiloTexto { get; set; }

        public Color ConverterCor(string cor)
        {
            if (VerificarCorHexadecimal(cor))
                return ColorTranslator.FromHtml(cor);

            int[] valoresRgb = ExtrairValoresRGB(cor);
            return Color.FromArgb(valoresRgb[0], valoresRgb[1], valoresRgb[2]);
        }

        private static bool VerificarCorHexadecimal(string cor)
        {
            if (string.IsNullOrEmpty(cor))
                return false;

            return Regex.IsMatch(cor, @"^#([a-fA-F0-9]{3}|[a-fA-F0-9]{6})$");
        }

        private static int[] ExtrairValoresRGB(string cor)
        {
            if (string.IsNullOrEmpty(cor))
                throw new ArgumentException("A cor não pode ser nula ou vazia.");

            string[] valoresRgb = cor.Replace("rgb(", "").Replace(")", "").Split(',');

            if (valoresRgb.Length != 3)
                throw new ArgumentException("A string de cor para RGB não está no formato correto.");

            int[] valores = new int[3];

            for (int i = 0; i < 3; i++)
            {
                if (!int.TryParse(valoresRgb[i].Trim(), out valores[i]) || valores[i] < 0 || valores[i] > 255)
                    throw new ArgumentException("A string de cor não está no formato correto.");
            }

            return valores;
        }
    }
}
