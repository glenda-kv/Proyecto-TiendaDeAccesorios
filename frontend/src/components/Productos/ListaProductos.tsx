import { Producto } from "./Producto";

interface Props {
  productos: any[];
}

export function ListaProductos({ productos }: Props) {
  return (
    <table className="table table-bordered table-striped mt-3">
      <thead>
        <tr>
          <th>Nombre</th>
          <th>Marca</th>
          <th>Color</th>
          <th>Precio</th>
          <th>Stock</th>
        </tr>
      </thead>

      <tbody>
        {productos.map((producto) => (
          <Producto key={producto.idProducto} producto={producto} />
        ))}
      </tbody>
    </table>
  );
}