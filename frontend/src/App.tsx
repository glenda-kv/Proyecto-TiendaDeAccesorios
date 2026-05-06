import { useEffect, useState } from "react";
import { obtenerProductos } from "./services/productoService";
import "./App.css";

function App() {
  const [productos, setProductos] = useState<any[]>([]);

  useEffect(() => {
    obtenerProductos()
      .then((data) => setProductos(data))
      .catch((error) => console.log(error));
  }, []);

  return (
    <div className="container mt-4">
      <h1>Lista de Productos</h1>

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
            <tr key={producto.idProducto}>
              <td>{producto.nombreProducto}</td>
              <td>{producto.marca}</td>
              <td>{producto.color}</td>
              <td>{producto.precio}</td>
              <td>{producto.stock}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;