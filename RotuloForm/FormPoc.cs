using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RotuloForm
{
    public partial class FormRotulo : Form
    {

        public FormRotulo()
        {
            InitializeComponent();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            var json = @"
                        [
                            {
                                ""Texto"": ""Texto 1"",
                                ""Width"": 44,
                                ""Height"": 29,
                                ""Cor"": ""#ff0000"",
                                ""Angulo"": 270.0,
                                ""X"": 10.0,
                                ""Y"": 50.0
                            },
                            {
                                ""Texto"": ""Texto 2"",
                                ""Width"": 38,
                                ""Height"": 14,
                                ""Cor"": ""#333333"",
                                ""Angulo"": 90.0,
                                ""X"": 100.0,
                                ""Y"": 200.0
                            },
                            {
                                ""Texto"": ""Texto 3"",
                                ""Width"": 60,
                                ""Height"": 20,
                                ""Cor"": ""#1e66e3"",
                                ""Angulo"": 180.0,
                                ""X"": 500.0,
                                ""Y"": 300.0
                            },
                            {
                                ""Texto"": ""Schin"",
                                ""Width"": 60,
                                ""Height"": 20,
                                ""Cor"": ""#f705ff"",
                                ""Angulo"": 315.0,
                                ""X"": 150.0,
                                ""Y"": 150.0
                            }
                        ]";

            List<TextoModel> textos = JsonConvert.DeserializeObject<List<TextoModel>>(json);

            Bitmap bitmap = new(picImg.Width, picImg.Height);
            Graphics desenhador = Graphics.FromImage(bitmap);

            desenhador.Clear(Color.White);

            Font fonte = new("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);

            // TODO: pensar para pegar fonte no foreach
            // TODO: fazer map de rgb para hexadecimal - ConverterRgbParaHexadecimal()

            foreach (var texto in textos)
            {
                SolidBrush brush = new(ColorTranslator.FromHtml(texto.Cor));

                desenhador.TranslateTransform(texto.X, texto.Y);

                desenhador.RotateTransform(texto.Angulo);

                desenhador.DrawString(texto.Texto, fonte, brush, new PointF(0, 0));

                desenhador.RotateTransform(-texto.Angulo);
                desenhador.TranslateTransform(-texto.X, -texto.Y);
            }

            picImg.Image = bitmap;
        }

        private class TextoModel
        {
            public string Texto { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public string Cor { get; set; }
            public float Angulo { get; set; }
            public float X { get; set; }
            public float Y { get; set; }
        }

    }
}
