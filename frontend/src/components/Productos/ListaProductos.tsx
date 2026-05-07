import { Producto } from "./Producto";

interface Props {
  productos: any[];
}

export function ListaProductos({ productos }: Props) {
  return (
    <div className="card shadow-sm border-0">
      <div className="card-body">
        <h3 className="mb-4 text-dark fw-bold"> 
          Lista de Productos
        </h3>

        <div className="table-responsive">
          <table className="table table-hover align-middle">
            <thead className="table-dark"> 
              <tr>
                <th>Nombre</th>
                <th>Marca</th>
                <th>Color</th>
                <th>Precio</th>
                <th>Stock</th>
                <th>Categoría</th>
              </tr>
            </thead>

            <tbody>
              {productos.length > 0 ? (
                productos.map((producto) => (
                  <Producto
                    key={producto.idProducto}
                    producto={producto}
                  />
                ))
              ) : (
                <tr>
                  <td colSpan={6} className="text-center py-4">
                    No hay productos registrados
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}