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

namespace Formulario_MinisterioAgri
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.AcceptButton = btnIniciarSesion;
        }
        //Minimizar la ventana
        private void btn_Minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //Cerrar la ventana
        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Mover Ventana
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Panel_Superior_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //Iniciar sesión
        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text;
            string contrasena = txtContraseña.Text;

            using (var db = new BD_Recursos_HumanosEntities4())
            {
                // Buscar usuario que coincida con el nombre de usuario y contraseña
                var usuario = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario && u.Contraseña == contrasena);

                if (usuario != null)
                {
                    // Credenciales válidas, abre la siguiente ventana o realiza la acción deseada
                    MessageBox.Show("Inicio de sesión correcto.");
                    Ventana_Principal formPrincipal = new Ventana_Principal();
                    formPrincipal.Show();
                    this.Hide(); // Ocultar el formulario de inicio de sesión
                }
                else
                {
                    // Credenciales incorrectas, muestra un mensaje de error y vacía los campos
                    MessageBox.Show("Nombre de usuario o contraseña incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Clear();
                    txtContraseña.Clear();
                    txtUsuario.Focus(); // Coloca el cursor nuevamente en el campo del nombre de usuario
                }
            }
        }
    }
}
