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
    public partial class Solicitud_Cambio_Designacion : Form
    {
        public Solicitud_Cambio_Designacion()
        {
            InitializeComponent();
        }
        private byte[] archivoBytes1;
        private string extensionArchivo1;


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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //Validar que un campo * este vacio
        private void Solicitud_Cambio_Designacion_Load(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            AutoCompleteCargo();
            AutoCompleteCargoNuevo();
            AutoCompleteDepartamento();
            AutoCompleteBusqueda();
            btnEditar.Visible = false;
            lblEditar.Visible = false;
            btnCancelar.Visible = false;
        }

        private void validarCampo()
        {
            var vr = !string.IsNullOrEmpty(txtNombreCompleto.Text) &&
                !string.IsNullOrEmpty(dateTimePicker1.Text) &&
                !string.IsNullOrEmpty(txtCedula.Text) &&
                !string.IsNullOrEmpty(txtCargoActual.Text) &&
                !string.IsNullOrEmpty(txtSalarioNuevo.Text) &&
                !string.IsNullOrEmpty(txtReferido.Text) &&
                !string.IsNullOrEmpty(txtNuevoCargo.Text) &&
                !string.IsNullOrEmpty(cmbEstatus.Text);
            btnGuardar.Enabled = vr;
        }

        //Cargar Departamentos
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
        //----------------------------------------------------------------------------------------------------------------------
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
        //Cargar y validar cargo actual
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

        //----------------------------------------------------------------------------------------------------------------------

        //Cargar y validar nuevo cargo
        private void txtNuevoCargo_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }
        private List<Cargo> buscarCargoNuevo(string buscarId)
        {
            using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
            {
                return db.Cargoes.Where(p => p.Nombre_Cargo.
                        Contains(buscarId)).ToList();
            }
        }

        private void AutoCompleteCargoNuevo()
        {
            AutoCompleteStringCollection colCargo = new AutoCompleteStringCollection();
            List<Cargo> cargos = buscarCargoNuevo(txtNuevoCargo.Text);
            foreach (Cargo cargo in cargos)
            {
                colCargo.Add(cargo.Nombre_Cargo);
            }
            txtNuevoCargo.AutoCompleteCustomSource = colCargo;
            txtNuevoCargo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtNuevoCargo.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        //-----------------------------------------------------------------------------------------------------------------------

        private void txtSalarioNuevo_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        private void txtReferido_TextChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        private void cmbEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            validarCampo();
        }

        //Cargar cedula y nombre en busqueda

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            
        }
        private List<Cambio_designacion> buscarCedulaYNombre(string buscarTexto)
        {
            using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
            {
                var resultados = db.Cambio_designacion
                                   .Where(p => p.Cedula.Contains(buscarTexto) || p.Nombre_Completo.Contains(buscarTexto))
                                   .ToList();  // Primero obtenemos todos los resultados que coincidan con el texto

                return resultados.Where(p => p.Estatus.Equals("En proceso", StringComparison.OrdinalIgnoreCase)).ToList();  // Filtrado en memoria
            }
        }


        private void AutoCompleteBusqueda()
        {
            AutoCompleteStringCollection colBusqueda = new AutoCompleteStringCollection();
            List<Cambio_designacion> resultados = buscarCedulaYNombre(txtBusqueda.Text);

            foreach (Cambio_designacion resultado in resultados)
            {
                // Añadir la cédula al autocompletar
                colBusqueda.Add(resultado.Cedula);
                // Añadir el nombre completo al autocompletar
                colBusqueda.Add(resultado.Nombre_Completo);
            }

            txtBusqueda.AutoCompleteCustomSource = colBusqueda;
            txtBusqueda.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtBusqueda.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }


        //--------------------------------------------------------------------------------------------------------------------------
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
                // Verificar si hay una solicitud en proceso para la misma cédula
                bool existeSolicitudEnProceso = db.Cambio_designacion.Any(cd =>
                    cd.Cedula == cedula && cd.Estatus == "En proceso");

                if (existeSolicitudEnProceso)
                {
                    MessageBox.Show("Existe una solicitud en proceso para esta cédula. No se puede crear otra solicitud hasta que la actual esté aprobada o rechazada.");
                    return;
                }

                // Resto de código para guardar la nueva solicitud
                // Continúa con las verificaciones de cargo y departamento
                var cargoActual = db.Cargoes.FirstOrDefault(c => c.Nombre_Cargo == txtCargoActual.Text);
                var nuevoCargo = db.Cargoes.FirstOrDefault(c => c.Nombre_Cargo == txtNuevoCargo.Text);
                var departamentoPropuesto = db.Departamentoes.FirstOrDefault(d => d.Nombre_Departamento == txtDepartamentoPropuesto.Text);

                if (cargoActual == null || nuevoCargo == null || departamentoPropuesto == null)
                {
                    MessageBox.Show("Asegúrate de que todos los datos de cargo y departamento sean válidos.");
                    return;
                }

                // Creación y guardado de la nueva solicitud
                Cambio_designacion SCambio_designacion = new Cambio_designacion
                {
                    Fecha = dateTimePicker1.Value,
                    Nombre_Completo = txtNombreCompleto.Text,
                    Cedula = cedula,
                    Id_Cargo_Actual = cargoActual.Id_Cargo,
                    Id_Cargo_Nuevo = nuevoCargo.Id_Cargo,
                    Id_Departamento_Propuesto = departamentoPropuesto.Id_Departamento,
                    Salario_Propuesto = salario,
                    Referido_por = txtReferido.Text,
                    Observaciones = txtObservaciones.Text,
                    Estatus = cmbEstatus.SelectedItem.ToString(),
                    Archivo = archivoBytes1,
                    Extension = extensionArchivo1
                };

                db.Cambio_designacion.Add(SCambio_designacion);

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
            string archivoExcelFijo = Path.Combine(rutaFija, "Copia_Solicitud_Cambio_Designación.xlsx");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Solicitud cambio de designación");

                // Añadir encabezados
                worksheet.Cell(1, 1).Value = "Id_designacion";
                worksheet.Cell(1, 2).Value = "Nombre completo";
                worksheet.Cell(1, 3).Value = "Fecha";
                worksheet.Cell(1, 4).Value = "Cedula";
                worksheet.Cell(1, 5).Value = "Cargo actual";
                worksheet.Cell(1, 6).Value = "Nuevo cargo";
                worksheet.Cell(1, 7).Value = "Salario propuesto";
                worksheet.Cell(1, 8).Value = "Departamento propuesto";
                worksheet.Cell(1, 9).Value = "Referido por";
                worksheet.Cell(1, 10).Value = "Observaciones";
                worksheet.Cell(1, 11).Value = "Estado";
                worksheet.Cell(1, 12).Value = "Archivo adjunto";

                using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
                {
                    var solicitudes = db.Cambio_designacion.Include("Cargo").Include("Departamento").ToList();
                    int fila = 2;
                    foreach (var solicitud in solicitudes)
                    {
                        worksheet.Cell(fila, 1).Value = solicitud.Id_designacion;
                        worksheet.Cell(fila, 2).Value = solicitud.Nombre_Completo;
                        worksheet.Cell(fila, 3).Value = solicitud.Fecha;
                        worksheet.Cell(fila, 4).Value = solicitud.Cedula;

                        var cargoActual = db.Cargoes.FirstOrDefault(c => c.Id_Cargo == solicitud.Id_Cargo_Actual);
                        var cargoNuevo = db.Cargoes.FirstOrDefault(c => c.Id_Cargo == solicitud.Id_Cargo_Nuevo);
                        var departamento = db.Departamentoes.FirstOrDefault(d => d.Id_Departamento == solicitud.Id_Departamento_Propuesto);

                        worksheet.Cell(fila, 5).Value = cargoActual != null ? cargoActual.Nombre_Cargo : "No encontrado";
                        worksheet.Cell(fila, 6).Value = cargoNuevo != null ? cargoNuevo.Nombre_Cargo : "No encontrado";
                        worksheet.Cell(fila, 7).Value = solicitud.Salario_Propuesto;
                        worksheet.Cell(fila, 8).Value = departamento != null ? departamento.Nombre_Departamento : "No encontrado";
                        worksheet.Cell(fila, 9).Value = solicitud.Referido_por;
                        worksheet.Cell(fila, 10).Value = solicitud.Observaciones;
                        worksheet.Cell(fila, 11).Value = solicitud.Estatus;

                        // Guardar archivo
                        if (solicitud.Archivo != null && solicitud.Extension != null)
                        {
                            string nombreArchivo1 = $"Solicitud_{solicitud.Id_designacion}_1{solicitud.Extension}";
                            string rutaArchivo1 = Path.Combine(rutaCarpetaAdjuntos, nombreArchivo1);
                            File.WriteAllBytes(rutaArchivo1, solicitud.Archivo);
                            worksheet.Cell(fila, 12).SetHyperlink(new XLHyperlink(rutaArchivo1));
                            worksheet.Cell(fila, 12).Value = nombreArchivo1;
                        }
                        else
                        {
                            worksheet.Cell(fila, 12).Value = "Sin archivo adjunto";
                        }

                        fila++;
                    }
                }

                // Guardar en la ubicación seleccionada por el usuario
                string archivoExcel = Path.Combine(carpetaDestino, "Solicitud cambio de designación.xlsx");
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener el valor de búsqueda (nombre o cédula) desde el TextBox
            string filtroBusqueda = txtBusqueda.Text;

            using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
            {
                // Buscar por cédula o nombre (dependiendo del texto ingresado) y que el estado sea "En proceso"
                var solicitud = db.Cambio_designacion
                    .Where(s => (s.Cedula == filtroBusqueda || s.Nombre_Completo.Contains(filtroBusqueda)) && s.Estatus == "En proceso")
                    .FirstOrDefault();

                if (solicitud == null)
                {
                    MessageBox.Show("No se encontraron registros con el criterio proporcionado o no están 'En proceso'.");
                    return;
                }

                // Rellenar los campos en el formulario principal
                txtNombreCompleto.Text = solicitud.Nombre_Completo;
                txtCedula.Text = solicitud.Cedula;

                // Obtener los nombres de los cargos y el departamento
                var cargoActual = db.Cargoes.FirstOrDefault(c => c.Id_Cargo == solicitud.Id_Cargo_Actual);
                var nuevoCargo = db.Cargoes.FirstOrDefault(c => c.Id_Cargo == solicitud.Id_Cargo_Nuevo);
                var departamento = db.Departamentoes.FirstOrDefault(d => d.Id_Departamento == solicitud.Id_Departamento_Propuesto);

                txtCargoActual.Text = cargoActual != null ? cargoActual.Nombre_Cargo : "No encontrado";
                txtNuevoCargo.Text = nuevoCargo != null ? nuevoCargo.Nombre_Cargo : "No encontrado";
                txtDepartamentoPropuesto.Text = departamento != null ? departamento.Nombre_Departamento : "No encontrado";

                // Convertir el salario de decimal a string con formato
                decimal salarioPropuesto = solicitud.Salario_Propuesto;
                txtSalarioNuevo.Text = salarioPropuesto.ToString("F2");  // Formato con dos decimales

                txtReferido.Text = solicitud.Referido_por;
                txtObservaciones.Text = solicitud.Observaciones;
                cmbEstatus.SelectedItem = solicitud.Estatus;

                // Habilitar solo el campo `Estatus` para edición
                cmbEstatus.Enabled = true;

                // Deshabilitar otros campos
                txtNombreCompleto.Enabled = false;
                txtCedula.Enabled = false;
                txtCargoActual.Enabled = false;
                txtNuevoCargo.Enabled = false;
                txtSalarioNuevo.Enabled = false;
                txtDepartamentoPropuesto.Enabled = false;
                txtReferido.Enabled = false;
                txtObservaciones.Enabled = false;

                // Ocultar botón de guardar y mostrar botón de editar y cancelar
                btnGuardar.Visible = false;
                lblGuardar.Visible = false;
                btnEditar.Visible = true;
                lblEditar.Visible = true;
                btnCancelar.Visible = true;
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Asegúrate de que se seleccionó un estado válido antes de proceder
            if (cmbEstatus.SelectedItem == null || string.IsNullOrEmpty(cmbEstatus.SelectedItem.ToString()))
            {
                MessageBox.Show("Por favor, selecciona un estado válido antes de guardar.");
                return;
            }

            // Actualiza el registro en la base de datos que está "En proceso"
            using (BD_Recursos_HumanosEntities4 db = new BD_Recursos_HumanosEntities4())
            {
                var cedula = txtCedula.Text;
                // Asegúrate de seleccionar el registro que está actualmente "En proceso"
                var solicitud = db.Cambio_designacion.FirstOrDefault(s => s.Cedula == cedula && s.Estatus == "En proceso");
                if (solicitud != null)
                {
                    solicitud.Estatus = cmbEstatus.SelectedItem.ToString();

                    // Intenta guardar los cambios
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("El estado ha sido actualizado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar los cambios: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró un registro 'En proceso' para la cédula proporcionada.");
                }
            }

            // Limpiar los campos
            LimpiarFormulario();

            // Habilitar los campos para nueva entrada
            HabilitarCampos();

            // Ocultar el botón de editar y mostrar el de guardar
            btnEditar.Visible = false;
            lblEditar.Visible = false;
            btnGuardar.Visible = true;
            lblGuardar.Visible = true;
            btnCancelar.Visible = false;
        }
        //Funcion de limpiar el formulario
        private void LimpiarFormulario()
        {
            txtNombreCompleto.Clear();
            txtCedula.Clear();
            txtCargoActual.Clear();
            txtNuevoCargo.Clear();
            txtSalarioNuevo.Clear();
            txtDepartamentoPropuesto.Clear();
            txtReferido.Clear();
            txtObservaciones.Clear();
            txtBusqueda.Clear();
            txtAdjuntarArchivo.Clear();
            cmbEstatus.SelectedIndex = -1;  // Reiniciar el ComboBox
        }

        //Funcion de habilitar campos
        private void HabilitarCampos()
        {
            txtNombreCompleto.Enabled = true;
            txtCedula.Enabled = true;
            txtCargoActual.Enabled = true;
            txtNuevoCargo.Enabled = true;
            txtSalarioNuevo.Enabled = true;
            txtDepartamentoPropuesto.Enabled = true;
            txtReferido.Enabled = true;
            txtObservaciones.Enabled = true;
            cmbEstatus.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarCampos();
            btnGuardar.Visible = true;
            lblGuardar.Visible = true;
            btnEditar.Visible = false;
            lblEditar.Visible = false;
            btnCancelar.Visible = false;
        }
    }
}

