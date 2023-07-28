using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_ORM.Data;

namespace Proyecto_ORM
{
    public partial class Registro_Empleado : Form
    {
        public int? nempleado;
        Empleados empleados = null;

        public Registro_Empleado(int? nempleado=null)
        {
            InitializeComponent();
            this.nempleado = nempleado;
            if (nempleado != null )
            {
                CargarDatos();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (EmpleadosFEntities datos = new EmpleadosFEntities())
            {
                if (nempleado == null)
                {
                    empleados = new Empleados();
                }
                    empleados.Nombre = txtNombre.Text;
                    empleados.Apellidos = txtApellidos.Text;
                    empleados.Telefono = txtTelefono.Text;
                    empleados.Carrera = txtCarrera.Text;

                if (nempleado == null)
                {
                    datos.Empleados.Add(empleados);
                }
                else
                {
                    datos.Entry(empleados).State = System.Data.Entity.EntityState.Modified;
                }   
                
                datos.SaveChanges();

                this.Close();  
            }
        }

        private void Registro_Empleado_Load(object sender, EventArgs e)
        {

        }

        private void CargarDatos()
        {
            using (EmpleadosFEntities datos = new EmpleadosFEntities())
            {
                empleados = datos.Empleados.Find(nempleado);
                txtNombre.Text = empleados.Nombre;
                txtApellidos.Text = empleados.Apellidos;
                txtTelefono.Text = empleados.Telefono;
                txtCarrera.Text = empleados.Carrera;
            }
        }
    }
}
