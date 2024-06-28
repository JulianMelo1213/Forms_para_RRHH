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
    public partial class Solicitud_de_nombramiento : Form
    {
        public Solicitud_de_nombramiento()
        {
            InitializeComponent();
        }
        private byte[] archivoBytes1;
        private string extensionArchivo1;
        private byte[] archivoBytes2;
        private string extensionArchivo2;

        //Minimizar ventana
        private void btn_Minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //Cerrar la ventana
        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void Solicitud_de_nombramiento_Load(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            AutoCompleteCargo();
            AutoCompleteDepartamento();
        }

        private void validarCampo()
        {
            var vr = !string.IsNullOrEmpty(dateTimePicker1.Text) &&
                !string.IsNullOrEmpty(txtNombre.Text) &&
                !string.IsNullOrEmpty(txtApellido.Text) &&
                !string.IsNullOrEmpty(txtCedula.Text) &&
                !string.IsNullOrEmpty(cmbSexo.Text) &&
                !string.IsNullOrEmpty(txtCargo.Text) &&
                !string.IsNullOrEmpty(txtSalario.Text) &&
                !string.IsNullOrEmpty(txtDepartamento.Text) &&
                !string.IsNullOrEmpty(txtAutorizado.Text);
            btnGuardar.Enabled = vr;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        private void txtCedula_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            validarCampo();
        }

        private void cmbSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        //Cargar y validar el campo cargo
        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        private List<Cargo> buscarCargo(string buscarId)
        {
            using(BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
            {
                return db.Cargoes.Where(p => p.Nombre_Cargo.
                        Contains(buscarId)).ToList();
            }
        }

        private void AutoCompleteCargo()
        {
            AutoCompleteStringCollection colCargo = new AutoCompleteStringCollection();
            List<Cargo> cargos = buscarCargo(txtCargo.Text);
            foreach(Cargo cargo in cargos)
            {
                colCargo.Add(cargo.Nombre_Cargo);
            }
            txtCargo.AutoCompleteCustomSource = colCargo;
            txtCargo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtCargo.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        //--------------------------------------------------------------------------------------------------------------------//

        private void txtSalario_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        //Cargar y validar departamento
        private void txtDepartamento_TextChanged(object sender, EventArgs e)
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
            List<Departamento> departamentos = buscarDepartamento(txtDepartamento.Text);
            foreach (Departamento departamento in departamentos)
            {
                colDepartamento.Add(departamento.Nombre_Departamento);
            }
            txtDepartamento.AutoCompleteCustomSource = colDepartamento;
            txtDepartamento.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtDepartamento.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        //-----------------------------------------------------------------------------------------------------//

        private void txtAutorizado_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }


        //Guardar datos
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

            // Verificar si la cédula ya existe en la base de datos
            if (CedulaExiste(cedula))
            {
                MessageBox.Show("La cédula ya existe en la base de datos.");
                return;
            }

            // Verificar que el salario sea numérico
            if (!decimal.TryParse(txtSalario.Text, out decimal salario))
            {
                MessageBox.Show("Por favor, ingresa un valor numérico válido para el salario.");
                return;
            }

            // Buscar los IDs del Cargo y del Departamento a partir del texto ingresado
            using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
            {
                // Buscar el ID del Cargo
                var cargo = db.Cargoes.FirstOrDefault(c => c.Nombre_Cargo == txtCargo.Text);
                if (cargo == null)
                {
                    MessageBox.Show("El cargo especificado no existe.");
                    return;
                }

                // Buscar el ID del Departamento
                var departamento = db.Departamentoes.FirstOrDefault(d => d.Nombre_Departamento == txtDepartamento.Text);
                if (departamento == null)
                {
                    MessageBox.Show("El departamento especificado no existe.");
                    return;
                }

                // Crear la nueva solicitud de nombramiento con los IDs relacionados
                Solicitud_nombramiento Snombramiento = new Solicitud_nombramiento
                {
                    Fecha = dateTimePicker1.Value,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Cedula = cedula,
                    Sexo = cmbSexo.SelectedItem.ToString(),
                    Id_Cargo = cargo.Id_Cargo,
                    Id_Departamento = departamento.Id_Departamento,
                    Grupo_ocupacional = cmbGrupoOcupacional.SelectedItem.ToString(),
                    Salario = salario,
                    Direccion_de_residencia = txtDirecciondeResidencia.Text,
                    Autorizado = txtAutorizado.Text,
                    Observaciones = txtObservaciones.Text,
                    Sustitucion = txtSustitucion.Text,
                    Enviado_en_oficialNo = txtEnviadoenOficial.Text,
                    Pregunta = txtPregunta.Text,
                    NoOficioyFecha = txtOficioyFecha.Text,
                    Archivo_Cedula_identidad = archivoBytes1,
                    Extension_Cedula_identidad = extensionArchivo1,
                    Archivo_HojaVida_identidad = archivoBytes2,
                    Extension_HojaVida_identidad = extensionArchivo2
                };

                db.Solicitud_nombramiento.Add(Snombramiento);

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

        private bool CedulaExiste(string cedula)
        {
            using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
            {
                // Verificar si existe una cédula igual en la base de datos
                return db.Solicitud_nombramiento.Any(s => s.Cedula == cedula);
            }
        }
        //formatear como moneda
     
        private void txtSalario_Leave(object sender, EventArgs e)
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

        //Limpiar el formulario
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

        private void btnAdjuntarArchivo2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Selecciona el segundo archivo",
                Filter = "Todos los Archivos|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                archivoBytes2 = File.ReadAllBytes(openFileDialog.FileName);
                extensionArchivo2 = Path.GetExtension(openFileDialog.FileName);
                txtAdjuntarArchivo2.Text = openFileDialog.SafeFileName;
            }
        }
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
            string rutaFija = "C:\\Users\\gusta\\OneDrive\\Escritorio\\Backup"; 
            string archivoExcelFijo = Path.Combine(rutaFija, "Copia_Solicitud_Nombramiento.xlsx");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Solicitud de nombramiento");

                // Añadir encabezados y continuar como estaba...
                worksheet.Cell(1, 1).Value = "Id_solicitud";
                worksheet.Cell(1, 2).Value = "Fecha";
                worksheet.Cell(1, 3).Value = "Nombre";
                worksheet.Cell(1, 4).Value = "Apellido";
                worksheet.Cell(1, 5).Value = "Cedula";
                worksheet.Cell(1, 6).Value = "Sexo";
                worksheet.Cell(1, 7).Value = "Grupo ocupacional";
                worksheet.Cell(1, 8).Value = "Sustitucion";
                worksheet.Cell(1, 9).Value = "Salario";
                worksheet.Cell(1, 10).Value = "Departamento";
                worksheet.Cell(1, 11).Value = "Cargo";
                worksheet.Cell(1, 12).Value = "Direccion de residencia";
                worksheet.Cell(1, 13).Value = "Autorizado";
                worksheet.Cell(1, 14).Value = "Observaciones";
                worksheet.Cell(1, 15).Value = "Enviado en oficial al MAP No";
                worksheet.Cell(1, 16).Value = "Pregunta";
                worksheet.Cell(1, 17).Value = "Archivo adjunto 1";
                worksheet.Cell(1, 18).Value = "Archivo adjunto 2";
                // Código para llenar la hoja de cálculo...

                using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
                {
                    var solicitudes = db.Solicitud_nombramiento.Include("Cargo").Include("Departamento").ToList();
                    int fila = 2;
                    foreach (var solicitud in solicitudes)
                    {
                        // Agregar datos al worksheet...
                        worksheet.Cell(fila, 1).Value = solicitud.Id_solicitud;
                        worksheet.Cell(fila, 2).Value = solicitud.Fecha;
                        worksheet.Cell(fila, 3).Value = solicitud.Nombre;
                        worksheet.Cell(fila, 4).Value = solicitud.Apellido;
                        worksheet.Cell(fila, 5).Value = solicitud.Cedula;
                        worksheet.Cell(fila, 6).Value = solicitud.Sexo;
                        worksheet.Cell(fila, 7).Value = solicitud.Grupo_ocupacional;
                        worksheet.Cell(fila, 8).Value = solicitud.Sustitucion;
                        worksheet.Cell(fila, 9).Value = solicitud.Salario;

                        // Cargo y Departamento relacionados
                        worksheet.Cell(fila, 10).Value = solicitud.Departamento?.Nombre_Departamento ?? "Sin departamento";
                        worksheet.Cell(fila, 11).Value = solicitud.Cargo?.Nombre_Cargo ?? "Sin cargo";

                        worksheet.Cell(fila, 12).Value = solicitud.Direccion_de_residencia;
                        worksheet.Cell(fila, 13).Value = solicitud.Autorizado;
                        worksheet.Cell(fila, 14).Value = solicitud.Observaciones;
                        worksheet.Cell(fila, 15).Value = solicitud.Enviado_en_oficialNo;
                        worksheet.Cell(fila, 16).Value = solicitud.Pregunta;

                        // Guardar el primer archivo
                        if (solicitud.Archivo_Cedula_identidad != null && solicitud.Extension_Cedula_identidad != null)
                        {
                            string nombreArchivo1 = $"Solicitud_{solicitud.Id_solicitud}_1{solicitud.Extension_Cedula_identidad}";
                            string rutaArchivo1 = Path.Combine(rutaCarpetaAdjuntos, nombreArchivo1);
                            File.WriteAllBytes(rutaArchivo1, solicitud.Archivo_Cedula_identidad);

                            // Crear un hipervínculo al primer archivo en Excel
                            var celdaArchivo1 = worksheet.Cell(fila, 17);
                            celdaArchivo1.Value = nombreArchivo1;
                            celdaArchivo1.SetHyperlink(new XLHyperlink(rutaArchivo1));
                        }
                        else
                        {
                            worksheet.Cell(fila, 17).Value = "Sin archivo adjunto";
                        }

                        // Guardar el segundo archivo
                        if (solicitud.Archivo_HojaVida_identidad != null && solicitud.Extension_HojaVida_identidad != null)
                        {
                            string nombreArchivo2 = $"Solicitud_{solicitud.Id_solicitud}_2{solicitud.Extension_HojaVida_identidad}";
                            string rutaArchivo2 = Path.Combine(rutaCarpetaAdjuntos, nombreArchivo2);
                            File.WriteAllBytes(rutaArchivo2, solicitud.Archivo_HojaVida_identidad);

                            // Crear un hipervínculo al segundo archivo en Excel
                            var celdaArchivo2 = worksheet.Cell(fila, 18);
                            celdaArchivo2.Value = nombreArchivo2;
                            celdaArchivo2.SetHyperlink(new XLHyperlink(rutaArchivo2));
                        }
                        else
                        {
                            worksheet.Cell(fila, 18).Value = "Sin archivo adjunto";
                        }

                        fila++;
                    }
                }

                // Guardar en la ubicación seleccionada por el usuario
                string archivoExcel = Path.Combine(carpetaDestino, "Solicitud de nombramiento.xlsx");
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

                // Guardar en la ubicación fija predefinida
                try
                {
                    workbook.SaveAs(archivoExcelFijo);
                    
                }
                catch (IOException ex)
                {

                }
            }
        }



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

