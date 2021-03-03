using System;
using System.IO;
using System.Windows.Forms;

namespace CambiaFechas
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            FormClosed += (s, e) =>
            {
                MessageBox.Show(@"Programa creado por Erick Orlando © para su amigo Walter 'Pooh' Vargas",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                SetDefaultCursor(true);
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Title = @"Seleccione el archivo";
                    ofd.Filter = @"Todos los archivos (*.*)|*.*";
                    ofd.FilterIndex = 1;
                    if (ofd.ShowDialog() != DialogResult.OK) return;
                    txtRuta.Text = ofd.FileName;
                    var fileInfo = new FileInfo(txtRuta.Text);

                    lblFechaCreacion.Text = fileInfo.CreationTime.ToString("F");
                    lblFechaModificacion.Text = fileInfo.LastWriteTime.ToString("F");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            finally
            {
                SetDefaultCursor(false);
            }
        }

        private void SetDefaultCursor(bool wait)
        {
            Cursor = wait ? Cursors.WaitCursor : Cursors.Default;
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRuta.Text)) return;

                SetDefaultCursor(true);

                var fileInfo = new FileInfo(txtRuta.Text)
                {
                    CreationTime = dtpFechaCreacion.Value,
                    LastWriteTime = dtpFechaModificacion.Value
                };

                MessageBox.Show(@"Se modificaron las fechas, revise archivo", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblFechaCreacion.Text = fileInfo.CreationTime.ToString("F");
                lblFechaModificacion.Text = fileInfo.LastWriteTime.ToString("F");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            finally
            {
                SetDefaultCursor(false);
            }
        }
    }
}