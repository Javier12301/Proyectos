﻿using Datos.Negocio;
using Negocio.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Modal
{
    public partial class mdAjustarPrecioCategoria : Form
    {
        CategoriaDA lCategoria = new CategoriaDA();
        ProductoDA lProducto = new ProductoDA();
        public mdAjustarPrecioCategoria()
        {
            InitializeComponent();
        }

        private void mdAjustarPrecioCategoria_Load(object sender, EventArgs e)
        {
            CargarCombobox();
        }

        private List<Categoria> lstCategoria { get; set; }
        public void CargarCombobox()
        {
            lstCategoria = lCategoria.ObtenerCategoriasConProductos();
            cmbCategoria.Items.Add("Seleccione una categoría...");
            foreach (Categoria cat in lstCategoria)
            {
                cmbCategoria.Items.Add(cat.Nombre);
            }
            cmbCategoria.SelectedIndex = 0;
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedIndex > 0)
            {
                CargarLista();
            }
            else
            {
                // limpiar datagridview
                dgvProductos.DataSource = null;
            }
        }

        private void DiseñoTabla()
        {

            dgvProductos.Columns["ID"].Visible = false;

            dgvProductos.Columns["Producto"].Width = 150;
            dgvProductos.Columns["Talle"].Width = 50;
            dgvProductos.Columns["Equipo"].Width = 100;
            dgvProductos.Columns["Precio"].Width = 70;
            // poner en primera posición a dgvcCheck
            dgvProductos.Columns["dgvcCheck"].DisplayIndex = 0;
            dgvProductos.Columns["dgvcCheck"].Width = 40;
            // poner en la ultima posición a nuevoprecio
            dgvProductos.Columns["Nuevo Precio"].Width = 70;

        }

        private void InicializarDataGridView()
        {
            DataGridViewCheckBoxColumn dgvcCheck = new DataGridViewCheckBoxColumn();
            dgvcCheck.Name = "dgvcCheck";
            dgvcCheck.HeaderText = "";
            dgvcCheck.TrueValue = true;
            dgvcCheck.FalseValue = false;
            dgvcCheck.ReadOnly = false;
            dgvProductos.Columns.Add(dgvcCheck);
        }


        private BindingSource bsProducto = new BindingSource();
        private DataTable dtProducto = new DataTable();
        private DateTime lastMessageTime = DateTime.MinValue;
        private TimeSpan messageInterval = TimeSpan.FromMinutes(1); // Intervalo de 1 minuto
        private void CargarLista()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (cmbCategoria.SelectedIndex > 0)
                {
                    dtProducto.Clear();  // Limpiar el DataTable existente                 
                    dtProducto = lProducto.ObtenerProductosenCategoria(string.Empty, cmbCategoria.Text, txtPorcentaje.Text);

                    // Asigna la fuente de datos
                    bsProducto.DataSource = dtProducto;
                    dgvProductos.DataSource = bsProducto;

                    // Configurar la columna de CheckBox
                    if (!dgvProductos.Columns.Contains("dgvcCheck"))
                    {
                        InicializarDataGridView();
                    }
                    // Marcar todos los CheckBox como activados por defecto
                    foreach (DataGridViewRow row in dgvProductos.Rows)
                    {
                        row.Cells["dgvcCheck"].Value = true;
                    }

                    // Diseño de la tabla
                    DiseñoTabla();
                }
                Cursor.Current = Cursors.Default;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}, contacte con el adminsitrador del sistema", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProductos.Columns["dgvcCheck"].Index && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvProductos.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Value = !(bool)cell.Value;
            }
        }


        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != (char)Keys.Back)
            {
                // Cancelar el evento
                e.Handled = true;

            }
        }

        private void txtPorcentaje_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConfirmarPorcentaje_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar si el campo está vacío
                if (string.IsNullOrWhiteSpace(txtPorcentaje.Text))
                {
                    txtPorcentaje.Text = "0";
                }

                // Intentar convertir el texto a entero
                if (!int.TryParse(txtPorcentaje.Text, out int porcentaje))
                {
                    MessageBox.Show("Ingrese un valor numérico válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPorcentaje.Focus();
                    return;
                }

                // Validar rango de porcentaje
                if (porcentaje < 0)
                {
                    MessageBox.Show("El porcentaje no puede ser negativo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPorcentaje.Focus();
                    return;
                }

                if (porcentaje >= 1000)
                {
                    MessageBox.Show("El porcentaje no puede ser mayor o igual a 1000.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPorcentaje.Focus();
                    return;
                }

                // Ejecutar acción si todo es válido
                CargarLista();
            }
            catch (OverflowException)
            {
                MessageBox.Show("El número ingresado es demasiado grande.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPorcentaje.Clear();
                txtPorcentaje.Focus();
            }
            catch (Exception ex)
            {
                // Capturar cualquier otro error inesperado
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cmbCategoria.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(txtPorcentaje.Text))
                {
                    DialogResult result = MessageBox.Show("¿Está seguro que desea actualizar los precios?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    foreach (DataGridViewRow row in dgvProductos.Rows)
                    {
                        if ((bool)row.Cells["dgvcCheck"].Value)  // Verificar si la fila está seleccionada
                        {
                            int idProducto = Convert.ToInt32(row.Cells["ID"].Value);  // Obtener ID del producto
                            decimal precioNuevo;

                            // Validar si el valor de "Nuevo Precio" es convertible a decimal
                            if (decimal.TryParse(row.Cells["Nuevo Precio"].Value.ToString(), out precioNuevo))
                            {
                                precioNuevo = Math.Round(precioNuevo, 2);  // Redondear a 2 decimales

                                try
                                {
                                    // Actualizar el precio en la base de datos
                                    lProducto.ActualizarPrecio(idProducto, precioNuevo);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"No se pudo actualizar el producto con ID {idProducto}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                finally
                                {
                                    Cursor.Current = Cursors.Default;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"El precio nuevo del producto con ID {idProducto} no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    MessageBox.Show("Se actualizaron los precios correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Preguntar si quiere seguir actualizando
                    result = MessageBox.Show("¿Desea seguir actualizando precios?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        CargarLista();  // Volver a cargar la lista de productos
                    }
                    else
                    {
                        this.Close();  // Cerrar el formulario
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar un porcentaje válido.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una categoría.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Cursor.Current = Cursors.Default;
        }



        private Point mousePosicion;
        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePosicion = e.Location;
            }
        }

        private void pnlTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int deltaX = e.X - mousePosicion.X;
                int deltaY = e.Y - mousePosicion.Y;
                this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);
            }
        }

        private void txtPorcentaje_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPorcentaje.Text))
            {
                txtPorcentaje.Text = "0";
            }
        }
    }
}
