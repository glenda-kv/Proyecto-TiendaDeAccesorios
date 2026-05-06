import { useEffect, useState } from "react";
import { obtenerProductos } from "./services/productoService";
import { Layout } from "./layout/Layout";
import { ListaProductos } from "./components/Productos/ListaProductos";
import "./App.css";

function App() {
  const [productos, setProductos] = useState<any[]>([]);

  useEffect(() => {
    obtenerProductos()
      .then((data) => setProductos(data))
      .catch((error) => console.log(error));
  }, []);

  return (
    <Layout>
      <h1>Lista de Productos</h1>

      <ListaProductos productos={productos} />
    </Layout>
  );
}

export default App;