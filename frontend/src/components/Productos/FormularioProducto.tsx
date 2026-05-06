interface Props {
  producto: any;
  manejarCambio: (e: any) => void;
  guardarProducto: (e: any) => void;
}

export function FormularioProducto({
  producto,
  manejarCambio,
  guardarProducto,
}: Props) {
  return (
    <form onSubmit={guardarProducto} className="card p-4 mb-4">
      <h3>Agregar Producto</h3>

      <input
        className="form-control mb-2"
        name="nombreProducto"
        placeholder="Nombre"
        value={producto.nombreProducto}
        onChange={manejarCambio}
      />

      <input
        className="form-control mb-2"
        name="descripcion"
        placeholder="Descripción"
        value={producto.descripcion}
        onChange={manejarCambio}
      />

      <input
        className="form-control mb-2"
        name="marca"
        placeholder="Marca"
        value={producto.marca}
        onChange={manejarCambio}
      />

      <input
        className="form-control mb-2"
        name="color"
        placeholder="Color"
        value={producto.color}
        onChange={manejarCambio}
      />

      <input
        className="form-control mb-2"
        name="precio"
        type="number"
        min="0"
        placeholder="Precio"
        value={producto.precio}
        onChange={manejarCambio}
      />

      <input
        className="form-control mb-2"
        name="stock"
        type="number"
        min="0"
        placeholder="Stock"
        value={producto.stock}
        onChange={manejarCambio}
      />

      <select
        className="form-control mb-3"
        name="categoria"
        value={producto.categoria}
        onChange={manejarCambio}
      >
        <option value="">Seleccione categoría</option>
        <option value="Mochilas">Mochilas</option>
        <option value="Carteras">Carteras</option>
        <option value="Gorras">Gorras</option>
        <option value="Bolsos">Bolsos</option>
      </select>

      <button className="btn btn-primary" type="submit">
        Guardar Producto
      </button>
    </form>
  );
}