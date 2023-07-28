using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_ORM.Data;
using System.Windows.Forms;

namespace Proyecto_ORM
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Registro_Empleado registro_Empleado = new Registro_Empleado();
            registro_Empleado.ShowDialog(this);
            Refrescar();

        }

        private void Main_Load(object sender, EventArgs e)
        {
            Refrescar();
        }

        private void Refrescar()
        {
            using (EmpleadosFEntities datos = new EmpleadosFEntities())
            {
                var lConsulta = from empleados in datos.Empleados
                                select empleados;

                dgEmpleados.DataSource = lConsulta.ToList();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int? nempleado = ObtenerId();
            if(nempleado != null)
            {
                Registro_Empleado eregistrado = new Registro_Empleado(nempleado);
                eregistrado.ShowDialog(this);

                Refrescar();
            }

        }

        private int? ObtenerId()
        {
            try
            {
                return int.Parse(dgEmpleados.Rows[dgEmpleados.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch 
            {
                return null;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? nempleado = ObtenerId();
            if (nempleado != null)
            {
                using (EmpleadosFEntities datos = new EmpleadosFEntities())
                {
                    Empleados lDelete = datos.Empleados.Find(nempleado);
                    datos.Empleados.Remove(lDelete);
                    datos.SaveChanges();
                }
                Refrescar();
            }
        }
    }
}
