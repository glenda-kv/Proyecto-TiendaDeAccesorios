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
    <form
      onSubmit={guardarProducto}
      className="card shadow-sm border-0 p-4 mb-4"
    >
      <h3 className="mb-4 text-dark fw-bold">  
        Agregar Producto
      </h3>

      <div className="row">
        <div className="col-md-6 mb-3">
          <label className="form-label fw-semibold">
            Nombre
          </label>

          <input
            className="form-control"
            name="nombreProducto"
            placeholder="Ingrese nombre del producto"
            value={producto.nombreProducto}
            onChange={manejarCambio}
          />
        </div>

        <div className="col-md-6 mb-3">
          <label className="form-label fw-semibold">
            Marca
          </label>

          <input
            className="form-control"
            name="marca"
            placeholder="Ingrese marca"
            value={producto.marca}
            onChange={manejarCambio}
          />
        </div>
      </div>

      <div className="mb-3">
        <label className="form-label fw-semibold">
          Descripción
        </label>

        <input
          className="form-control"
          name="descripcion"
          placeholder="Ingrese descripción"
          value={producto.descripcion}
          onChange={manejarCambio}
        />
      </div>

      <div className="row">
        <div className="col-md-4 mb-3">
          <label className="form-label fw-semibold">
            Color
          </label>

          <input
            className="form-control"
            name="color"
            placeholder="Ingrese color"
            value={producto.color}
            onChange={manejarCambio}
          />
        </div>

        <div className="col-md-4 mb-3">
          <label className="form-label fw-semibold">
            Precio
          </label>

          <input
            className="form-control"
            name="precio"
            type="number"
            min="0"
            placeholder="0"
            value={producto.precio}
            onChange={manejarCambio}
          />
        </div>

        <div className="col-md-4 mb-3">
          <label className="form-label fw-semibold">
            Stock
          </label>

          <input
            className="form-control"
            name="stock"
            type="number"
            min="0"
            placeholder="0"
            value={producto.stock}
            onChange={manejarCambio}
          />
        </div>
      </div>

      <div className="mb-4">
        <label className="form-label fw-semibold">
          Categoría
        </label>

        <select
          className="form-select"
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
      </div>

      <button className="btn btn-dark w-100 fw-semibold" type="submit">   
        Guardar Producto
      </button>
    </form>
  );
}