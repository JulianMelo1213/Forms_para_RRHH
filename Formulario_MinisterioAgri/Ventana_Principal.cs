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
using DocumentFormat.OpenXml.Spreadsheet;

namespace Formulario_MinisterioAgri
{
    public partial class Ventana_Principal : Form
    {
        public Ventana_Principal()
        {
            InitializeComponent();
        }
        //Cerrar la ventana
        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Minimizar ventana
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

  
        //Mostrar la ventana de solicitud de cambio de designacion
        private void btnSolicitud_Cambio_Click(object sender, EventArgs e)
        {
            Solicitud_Cambio_Designacion Solicitud_Cambio_Designacion = new Solicitud_Cambio_Designacion();
            Solicitud_Cambio_Designacion.Show();
        }

        //Mostrar la ventana de solicitud de nombramiento
        private void btnSolicitud_Nombramiento_Click(object sender, EventArgs e)
        {
            Solicitud_de_nombramiento Solicitud_Nombramiento = new Solicitud_de_nombramiento();
            Solicitud_Nombramiento.Show();
        }

        //Cerrar sesion
        private void btn_CerrarSesion_Click(object sender, EventArgs e)
        {
            // Mostrar el formulario de inicio de sesión nuevamente
            Login formLogin = new Login();
            formLogin.Show();

            // Cerrar el formulario principal (este)
            this.Close();
        }

        private void btnReajuste_Click(object sender, EventArgs e)
        {
            Solicitud_de_reajuste solicitud_De_Reajuste = new Solicitud_de_reajuste();
            solicitud_De_Reajuste.Show();
        }
    }
}
