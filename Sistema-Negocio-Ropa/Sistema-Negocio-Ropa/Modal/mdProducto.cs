using Datos.Negocio;
using Negocio.Negocio;
using Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Negocio_Ropa.Modal
{
    public partial class mdProducto : Form
    {

        // Controladoras
        private ProductoDA lProducto;
        private CategoriaDA lCategoria;
        private Sesion lSesion = Sesion.ObtenerInstancia;
        private Utilidades uiUtilidades = Utilidades.ObtenerInstancia;

        // Modificar Producto
        private bool modificandoProducto { get; set; }
        private int productoID { get; set; }
        private Producto oProducto { get; set; }

        private List<Categoria> listaCategorias { get; set; }

        public mdProducto(bool modificar = false, int productoID = 0)
        {
            InitializeComponent();
            modificandoProducto = modificar;
            this.productoID = productoID;
            lProducto = new ProductoDA();
            lCategoria = new CategoriaDA();
        }

        private void mdProducto_Load(object sender, EventArgs e)
        {
            
            txtNombre.Select();
            try
            {
                CargarCategorias();
                CargarFiltros();
                if (modificandoProducto)
                {
                    if (productoID > 0)
                    {
                        oProducto = new Producto();
                        oProducto = lProducto.ObtenerProductoPorIDD(productoID);
                        // Cargamos categoría y proveedor utilizando la id obtenida de producto
                        Categoria categoria = listaCategorias.FirstOrDefault(cat => cat.CategoriaID == oProducto.oCategoria.CategoriaID);
                        oProducto.oCategoria = categoria;
                        CargarDatosDeProducto(oProducto);
                    }
                    else
                    {
                        throw new Exception("Ocurrió un error al intentar modificar el producto, contactar con el administrador del sistema si este error persiste.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void CargarFiltros()
        {
            // Obtener talles y configurar autocompletado
            List<string> listaTalles = lProducto.TallesEnProductos();
            ConfigurarAutoCompletarTextBox(txtTalle, listaTalles);

            // Obtener equipos y configurar autocompletado
            List<string> listaEquipos = lProducto.EquiposEnProductos();
            ConfigurarAutoCompletarTextBox(txtEquipo, listaEquipos);

            List<string> listaProducto = lProducto.NombresProductos();
            ConfigurarAutoCompletarTextBox(txtNombre, listaProducto);

        }

        private void ConfigurarAutoCompletarTextBox(TextBox textBox, List<string> lista)
        {
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(lista.ToArray());

            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox.AutoCompleteCustomSource = autoComplete;
            
        }

        private void CargarCategorias()
        {
            try
            {
                listaCategorias = lCategoria.ListarCategoriasD();

                // Agregar la opción predeterminada primero
                cmbCategoria.Items.Add(new Categoria { Nombre = "Seleccione una categoría...", CategoriaID = -1, Estado = false });

                foreach (Categoria oCategoria in listaCategorias)
                {
                    cmbCategoria.Items.Add(oCategoria);
                }

                // Establecer DisplayMember y ValueMember antes de seleccionar el índice
                cmbCategoria.DisplayMember = "Nombre";
                cmbCategoria.ValueMember = "CategoriaID";

                // Establecer el índice seleccionado a 0 (la opción predeterminada)
                cmbCategoria.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (!modificandoProducto)
                {
                    AltaProducto();
                }
                else
                {
                    ModificarProducto();
                }
                CargarFiltros();
                txtNombre.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ValidarCampos()
        {
            bool camposValidos = true;

            camposValidos &= uiUtilidades.VerificarTextbox(txtNombre, errorProvider, lblNombre);
            
            camposValidos &= uiUtilidades.VerificarCombobox(cmbCategoria, errorProvider, lblCategoria);
            camposValidos &= uiUtilidades.VerificarTextboxPrecio(txtPrecio, errorProvider);
            

            // Validaciones de Precio y Costo
            bool Precio = true;
            Precio &= uiUtilidades.VerificarFormatoMoneda(txtPrecio, errorProvider);

            if (Precio && camposValidos)
                return true;
            else
                return false;
        }

        private Producto CrearProducto()
        {

            Categoria categoria = (Categoria)cmbCategoria.SelectedItem;
            Producto producto = new Producto()
            {
                Nombre = txtNombre.Text,
                oCategoria = categoria,
                Talle = !string.IsNullOrEmpty(txtTalle.Text) ? txtTalle.Text : "No tiene",
                Equipo = !string.IsNullOrEmpty(txtEquipo.Text) ? txtEquipo.Text : "Sin equipo",
                PrecioVenta = Convert.ToDecimal(txtPrecio.Text),
                Stock = Convert.ToInt32(txtStock.Text),
                StockMinimo = 0,
                Estado = chkEstado.Checked
            };

            return producto;
        }

        private Producto CrearProductoModificado()
        {
            Categoria categoria = (Categoria)cmbCategoria.SelectedItem;
            Producto producto = new Producto()
            {
                ProductoID = productoID,
                Nombre = txtNombre.Text,
                oCategoria = categoria,
                Talle = !string.IsNullOrEmpty(txtTalle.Text) ? txtTalle.Text : "No tiene",
                Equipo = !string.IsNullOrEmpty(txtEquipo.Text) ? txtEquipo.Text : "Sin equipo",
                PrecioVenta = Convert.ToDecimal(txtPrecio.Text),
                Stock = Convert.ToInt32(txtStock.Text),
                StockMinimo = 0,
                Estado = chkEstado.Checked
            };
            return producto;
        }

        private void AltaProducto()
        {
            if (ValidarCampos())
            {
                Producto producto = CrearProducto();

                // Verificar si ya existe un producto con el mismo nombre
                bool productoExiste = lProducto.ExisteProductoD(producto.Nombre);
                if (productoExiste)
                {
                    errorProvider.SetError(lblNombre, "El producto con este nombre ya está registrado en el sistema.");
                }

                if (productoExiste)
                {
                    MessageBox.Show("El producto con el nombre ingresado ya está registrado en el sistema. Por favor, verifica los datos o ingresa otro nombre.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool resultado = lProducto.AltaProductoD(producto);

                if (resultado)
                {
                    MessageBox.Show("El producto fue dado de alta con éxito.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult respuesta = MessageBox.Show("¿Desea seguir agregando productos?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.No)
                    {
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        limpiarCampos();
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos obligatorios.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Modificar
        private void ModificarProducto()
        {
            if (ValidarCampos())
            {
                Producto producto = CrearProductoModificado();
                bool productoExiste = false;

                // Verificar si el nuevo nombre es diferente al original y si ya existe en el sistema
                if (oProducto.Nombre != producto.Nombre)
                {
                    productoExiste = lProducto.ExisteProductoD(producto.Nombre);
                    if (productoExiste)
                    {
                        errorProvider.SetError(lblNombre, "Ya existe un producto con este nombre en el sistema.");
                    }
                }

                if (productoExiste)
                {
                    MessageBox.Show("El producto con el nombre ingresado ya está registrado en el sistema. Por favor, verifica los datos o ingresa otro nombre.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool resultado = lProducto.ModificarProductoD(producto);

                if (resultado)
                {
                    MessageBox.Show("El producto fue modificado con éxito.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el producto.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void limpiarCampos()
        {
            DialogResult respuesta = MessageBox.Show("¿Desea limpiar los campos?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                uiUtilidades.LimpiarTextbox(txtNombre, txtTalle, txtEquipo);
                uiUtilidades.LimpiarCombobox(cmbCategoria);
                txtPrecio.Text = "0";
                txtStock.Text = "0";
                txtNombre.Select();
            }
        }

        private void CargarDatosDeProducto(Producto producto)
        {
            if (producto != null)
            {            
                txtID.Text = producto.ProductoID.ToString();
                txtNombre.Text = producto.Nombre;
                txtTalle.Text = producto.Talle;
                txtEquipo.Text = producto.Equipo;
                cmbCategoria.SelectedItem = producto.oCategoria;
                txtPrecio.Text = producto.PrecioVenta.ToString();
                txtStock.Text = producto.Stock.ToString();
                chkEstado.Checked = producto.Estado;
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void cmbSiguienteIndex_Enter(object sender, KeyPressEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            uiUtilidades.Combobox_ShortcutSiguienteIndex(combobox, e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Desea cancelar la operación?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void TextboxMoneda_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            uiUtilidades.TextboxMoneda_KeyPress(textbox, e, errorProvider);
        }

        private void TextboxMoneda_Leave(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            uiUtilidades.TextboxMoneda_Leave(textbox, e);
        }

        // Movimiento de ventana
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

        private void txtStock_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStock.Text))
            {
                txtStock.Text = "0";
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            uiUtilidades.SoloNumero(e);
        }

        private void txtTalle_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTalle.Text))
            {
                txtTalle.Text = "NO TIENE";
            }

        }

        private void txtEquipo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEquipo.Text))
            {
                txtEquipo.Text = "Sin equipo";
            }
        }

        private void txtTalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            // todo mayusculas y numeros permitidos
            uiUtilidades.SoloMayusculasYNumeros(e);
        }

        private void txtCantidadMinima_Leave(object sender, EventArgs e)
        {
            
        }

        private void chkEstado_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
