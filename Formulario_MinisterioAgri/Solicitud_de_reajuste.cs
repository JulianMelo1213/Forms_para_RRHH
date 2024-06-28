using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using ClosedXML.Excel;

namespace Formulario_MinisterioAgri
{
    public partial class Solicitud_de_reajuste : Form
    {
        public Solicitud_de_reajuste()
        {
            InitializeComponent();
        }

        private byte[] archivoBytes1;
        private string extensionArchivo1;

        //Cerrar la ventana
        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Minimizar la ventana
        private void btn_Minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Mover Ventana
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //Validar que un campo * este vacio
        private void Solicitud_de_reajuste_Load(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            AutoCompleteDepartamento();
            AutoCompleteCargo();
        }

        private void validarCampo()
        {
            var vr = !string.IsNullOrEmpty(txtNombreCompleto.Text) &&
                !string.IsNullOrEmpty(dateTimePicker1.Text) &&
                !string.IsNullOrEmpty(txtCedula.Text) &&
                !string.IsNullOrEmpty(txtCargoActual.Text) &&
                !string.IsNullOrEmpty(txtSalarioNuevo.Text) &&
                !string.IsNullOrEmpty(txtReferido.Text);
            btnGuardar.Enabled = vr;
        }

        private void txtNombreCompleto_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        private void txtCedula_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            validarCampo();
        }

        //Cargar y validar cargos
        private void txtCargoActual_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        private List<Cargo> buscarCargo(string buscarId)
        {
            using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
            {
                return db.Cargoes.Where(p => p.Nombre_Cargo.
                        Contains(buscarId)).ToList();
            }
        }

        private void AutoCompleteCargo()
        {
            AutoCompleteStringCollection colCargo = new AutoCompleteStringCollection();
            List<Cargo> cargos = buscarCargo(txtCargoActual.Text);
            foreach (Cargo cargo in cargos)
            {
                colCargo.Add(cargo.Nombre_Cargo);
            }
            txtCargoActual.AutoCompleteCustomSource = colCargo;
            txtCargoActual.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtCargoActual.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        //------------------------------------------------------------------------------------------------------------

        private void txtSalarioNuevo_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        private void txtReferido_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        //Cargar y validar departamentos
        private void txtDepartamentoPropuesto_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        private List<Departamento> buscarDepartamento(string buscarId)
        {
            using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
            {
                return db.Departamentoes.Where(p => p.Nombre_Departamento.
                        Contains(buscarId)).ToList();
            }
        }

        private void AutoCompleteDepartamento()
        {
            AutoCompleteStringCollection colDepartamento = new AutoCompleteStringCollection();
            List<Departamento> departamentos = buscarDepartamento(txtDepartamentoPropuesto.Text);
            foreach (Departamento departamento in departamentos)
            {
                colDepartamento.Add(departamento.Nombre_Departamento);
            }
            txtDepartamentoPropuesto.AutoCompleteCustomSource = colDepartamento;
            txtDepartamentoPropuesto.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtDepartamentoPropuesto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }


        //------------------------------------------------------------------------------------------------------------

        //Guardar los datos
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener la cédula del MaskedTextBox
            string cedula = txtCedula.Text;

            // Verificar si el MaskedTextBox está completamente lleno
            if (!txtCedula.MaskCompleted)
            {
                MessageBox.Show("Por favor, ingresa una cédula completa.");
                return;
            }

            // Verificar que el salario sea numérico
            if (!decimal.TryParse(txtSalarioNuevo.Text, out decimal salario))
            {
                MessageBox.Show("Por favor, ingresa un valor numérico válido para el salario.");
                return;
            }

            using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
            {
                // Resto de código para guardar la nueva solicitud
                // Continúa con las verificaciones de cargo y departamento
                var cargoActual = db.Cargoes.FirstOrDefault(c => c.Nombre_Cargo == txtCargoActual.Text);
                var departamentoPropuesto = db.Departamentoes.FirstOrDefault(d => d.Nombre_Departamento == txtDepartamentoPropuesto.Text);

                if (cargoActual == null || departamentoPropuesto == null)
                {
                    MessageBox.Show("Asegúrate de que todos los datos de cargo y departamento sean válidos.");
                    return;
                }

                Solicitud_reajuste Sreajuste = new Solicitud_reajuste
                {
                    Fecha = dateTimePicker1.Value,
                    Nombre_Completo = txtNombreCompleto.Text,
                    Cedula = cedula,
                    Id_Cargo_Actual = cargoActual.Id_Cargo,
                    Id_Departamento_Propuesto = departamentoPropuesto.Id_Departamento,
                    Salario_Propuesto = salario,
                    Referido_por = txtReferido.Text,
                    Observaciones = txtObservaciones.Text,
                    Archivo = archivoBytes1,
                    Extension = extensionArchivo1
                };

                db.Solicitud_reajuste.Add(Sreajuste);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Datos insertados correctamente.");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} falló la validación\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }
                    MessageBox.Show("No se pudo insertar los datos:\n" + sb.ToString(), "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }



        //Salario formatear como moneda
        private void txtSalarioNuevo_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (decimal.TryParse(txt.Text, out decimal value))
            {
                // Formatear como moneda
                txt.Text = value.ToString("N0"); // Usa "N2" si deseas incluir dos decimales
            }
        }

        // Coloca el cursor al inicio del primer carácter editable
        private void txtCedula_Click(object sender, EventArgs e)
        {
            MaskedTextBox mtb = (MaskedTextBox)sender;
            int pos = mtb.MaskedTextProvider.FindEditPositionFrom(0, true);
            mtb.Select(pos, 0);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario(this);
        }
        private void LimpiarFormulario(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else if (c is ComboBox)
                    ((ComboBox)c).SelectedIndex = -1;
                else if (c is CheckBox)
                    ((CheckBox)c).Checked = false;
                else if (c is ListBox)
                    ((ListBox)c).Items.Clear();
                else if (c is DataGridView)
                    ((DataGridView)c).Rows.Clear();
                else if (c is MaskedTextBox)
                    ((MaskedTextBox)c).Clear();
                else if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = ((NumericUpDown)c).Minimum;
                else if (c.HasChildren)
                    LimpiarFormulario(c);  // Recursivamente limpiar controles anidados
            }
        }

        //Adjuntar archivos
        private void btnAdjuntarArchivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Selecciona un Archivo",
                Filter = "Todos los Archivos|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lee el archivo y guarda los datos binarios
                archivoBytes1 = File.ReadAllBytes(openFileDialog.FileName);
                extensionArchivo1 = Path.GetExtension(openFileDialog.FileName);
                txtAdjuntarArchivo.Text = openFileDialog.SafeFileName;
            }
        }

        //Exportar a excel

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string carpetaDestino = SeleccionarCarpeta();
            if (string.IsNullOrEmpty(carpetaDestino))
            {
                MessageBox.Show("No se ha seleccionado ninguna carpeta. Por favor, seleccione una carpeta válida.");
                return;
            }

            string rutaCarpetaAdjuntos = Path.Combine(carpetaDestino, "ArchivosAdjuntos");
            if (!Directory.Exists(rutaCarpetaAdjuntos))
            {
                Directory.CreateDirectory(rutaCarpetaAdjuntos);
            }

            // Ruta predefinida para guardar el segundo archivo
            string rutaFija = "C:\\Users\\gusta\\OneDrive\\Escritorio\\Backup"; // Modifica esta línea con la ruta deseada
            string archivoExcelFijo = Path.Combine(rutaFija, "Copia_Solicitud_de_reajuste.xlsx");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Solicitud de reajuste");

                // Añadir encabezados
                worksheet.Cell(1, 1).Value = "Id_reajuste";
                worksheet.Cell(1, 2).Value = "Nombre completo";
                worksheet.Cell(1, 3).Value = "Fecha";
                worksheet.Cell(1, 4).Value = "Cedula";
                worksheet.Cell(1, 5).Value = "Cargo actual";
                worksheet.Cell(1, 6).Value = "Salario propuesto";
                worksheet.Cell(1, 7).Value = "Departamento propuesto";
                worksheet.Cell(1, 8).Value = "Referido por";
                worksheet.Cell(1, 9).Value = "Observaciones";
                worksheet.Cell(1, 10).Value = "Archivo adjunto";

                using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
                {
                    var solicitudes = db.Solicitud_reajuste.Include("Cargo").Include("Departamento").ToList();
                    int fila = 2;
                    foreach (var solicitud in solicitudes)
                    {
                        worksheet.Cell(fila, 1).Value = solicitud.Id_reajuste;
                        worksheet.Cell(fila, 2).Value = solicitud.Nombre_Completo;
                        worksheet.Cell(fila, 3).Value = solicitud.Fecha;
                        worksheet.Cell(fila, 4).Value = solicitud.Cedula;

                        var cargoActual = db.Cargoes.FirstOrDefault(c => c.Id_Cargo == solicitud.Id_Cargo_Actual);
                        var departamento = db.Departamentoes.FirstOrDefault(d => d.Id_Departamento == solicitud.Id_Departamento_Propuesto);

                        worksheet.Cell(fila, 5).Value = cargoActual != null ? cargoActual.Nombre_Cargo : "No encontrado";
                        worksheet.Cell(fila, 6).Value = solicitud.Salario_Propuesto;
                        worksheet.Cell(fila, 7).Value = departamento != null ? departamento.Nombre_Departamento : "No encontrado";
                        worksheet.Cell(fila, 8).Value = solicitud.Referido_por;
                        worksheet.Cell(fila, 9).Value = solicitud.Observaciones;

                        // Guardar archivo
                        if (solicitud.Archivo != null && solicitud.Extension != null)
                        {
                            string nombreArchivo1 = $"Solicitud_{solicitud.Id_reajuste}_1{solicitud.Extension}";
                            string rutaArchivo1 = Path.Combine(rutaCarpetaAdjuntos, nombreArchivo1);
                            File.WriteAllBytes(rutaArchivo1, solicitud.Archivo);
                            worksheet.Cell(fila, 10).SetHyperlink(new XLHyperlink(rutaArchivo1));
                            worksheet.Cell(fila, 10).Value = nombreArchivo1;
                        }
                        else
                        {
                            worksheet.Cell(fila, 10).Value = "Sin archivo adjunto";
                        }

                        fila++;
                    }
                }

                // Guardar en la ubicación seleccionada por el usuario
                string archivoExcel = Path.Combine(carpetaDestino, "Solicitud de reajuste.xlsx");
                try
                {
                    workbook.SaveAs(archivoExcel);
                    MessageBox.Show("Datos exportados a Excel correctamente.");
                }
                catch (IOException ex)
                {
                    if (ex.Message.Contains("being used by another process"))
                        MessageBox.Show("El archivo Excel está abierto. Por favor, cierre el archivo y vuelva a intentar.", "Archivo Abierto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Error al guardar el archivo Excel. Detalles: " + ex.Message, "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                try
                {
                    workbook.SaveAs(archivoExcelFijo);
                }
                catch (IOException ex)
                {

                }
            }
        }

        //Seleccionar carpeta
        private string SeleccionarCarpeta()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Seleccione la carpeta donde se guardará el archivo Excel";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderDialog.SelectedPath;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
