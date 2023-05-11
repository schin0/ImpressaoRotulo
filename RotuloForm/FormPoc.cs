using Newtonsoft.Json;
using RotuloForm.Model;
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
                                ""Width"": 400,
                                ""Height"": 30,
                                ""Cor"": ""#ff0000"",
                                ""Angulo"": 270.0,
                                ""X"": 10.0,
                                ""Y"": 50.0,
                                ""EstiloTexto"" : {
                                    ""NomeFonte"": ""Open Sans"",
                                    ""TamanhoFonte"": 15.0,
                                    ""Alinhamento"": ""center"",
                                    ""Negrito"": true,
                                    ""Italico"": false,
                                    ""Sublinhado"": false
                                }
                            },
                            {
                                ""Texto"": ""Texto 2"",
                                ""Width"": 38,
                                ""Height"": 14,
                                ""Cor"": ""#333333"",
                                ""Angulo"": 90.0,
                                ""X"": 100.0,
                                ""Y"": 200.0,
                                ""EstiloTexto"" : {
                                    ""NomeFonte"": ""Garamond"",
                                    ""TamanhoFonte"": 15.0,
                                    ""Alinhamento"": ""right"",
                                    ""Negrito"": false,
                                    ""Italico"": true,
                                    ""Sublinhado"": false
                                }
                            },
                            {
                                ""Texto"": ""Texto 3"",
                                ""Width"": 60,
                                ""Height"": 20,
                                ""Cor"": ""#1e66e3"",
                                ""Angulo"": 180.0,
                                ""X"": 500.0,
                                ""Y"": 300.0,
                                ""EstiloTexto"" : {
                                    ""NomeFonte"": ""Arial"",
                                    ""TamanhoFonte"": 25.0,
                                    ""Alinhamento"": ""left"",
                                    ""Negrito"": false,
                                    ""Italico"": false,
                                    ""Sublinhado"": true
                                }
                            },
                            {
                                ""Texto"": ""Schin"",
                                ""Width"": 60,
                                ""Height"": 20,
                                ""Cor"": ""rgb(247, 5, 255)"",
                                ""Angulo"": 315.0,
                                ""X"": 150.0,
                                ""Y"": 150.0,
                                ""EstiloTexto"" : {
                                    ""NomeFonte"": ""Georgia"",
                                    ""TamanhoFonte"": 15.0,
                                    ""Alinhamento"": ""center"",
                                    ""Negrito"": false,
                                    ""Italico"": false,
                                    ""Sublinhado"": false
                                }
                            }
                        ]";

            List<TextoModel> textos = JsonConvert.DeserializeObject<List<TextoModel>>(json);

            Bitmap bitmap = new(picImg.Width, picImg.Height);
            Graphics desenhador = Graphics.FromImage(bitmap);

            desenhador.Clear(Color.White);

            foreach (var texto in textos)
            {
                var estilo = texto.EstiloTexto;

                FontStyle estiloFonte = ObterEstiloFonte(estilo.Negrito, estilo.Italico, estilo.Sublinhado);
                Font fonte = new(texto.EstiloTexto.NomeFonte, texto.EstiloTexto.TamanhoFonte, estiloFonte);
                SolidBrush brush = new(texto.ConverterCor(texto.Cor));
                SizeF tamanhoTexto = desenhador.MeasureString(texto.Texto, fonte);

                StringFormat formatoTexto = ObterFormatoTexto(texto.EstiloTexto.Alinhamento);

                desenhador.TranslateTransform(texto.X, texto.Y);
                desenhador.RotateTransform(texto.Angulo);

                desenhador.DrawString(texto.Texto, fonte, brush, new PointF(0, 0), formatoTexto);

                desenhador.RotateTransform(-texto.Angulo);

                desenhador.TranslateTransform(-texto.X, -texto.Y);
            }

            picImg.Image = bitmap;
        }

        private static FontStyle ObterEstiloFonte(bool negrito, bool italico, bool sublinhado)
        {
            var estiloFonte = new FontStyle();

            if (negrito)
                estiloFonte |= FontStyle.Bold;
            if (italico)
                estiloFonte |= FontStyle.Italic;
            if (sublinhado)
                estiloFonte |= FontStyle.Underline;

            return estiloFonte;
        }

        private static StringFormat ObterFormatoTexto(string alinhamento)
        {
            return new StringFormat
            {
                Alignment = alinhamento switch
                {
                    "right" => StringAlignment.Far,
                    "center" => StringAlignment.Center,
                    "left" => StringAlignment.Near,
                    _ => StringAlignment.Near
                }
            };
        }

    }
}
