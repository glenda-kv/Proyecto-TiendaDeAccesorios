import { useEffect, useState } from "react";
import { obtenerProductos, crearProducto } from "./services/productoService";
import { Layout } from "./layout/Layout";
import { ListaProductos } from "./components/Productos/ListaProductos";
import { FormularioProducto } from "./components/Productos/FormularioProducto";
import "./App.css";

function App() {
  const [productos, setProductos] = useState<any[]>([]);

  const [producto, setProducto] = useState({
    nombreProducto: "",
    descripcion: "",
    marca: "",
    color: "",
    precio: 0,
    stock: 0,
    categoria: "",
  });

  const cargarProductos = () => {
    obtenerProductos()
      .then((data) => setProductos(data))
      .catch((error) => console.log(error));
  };

  useEffect(() => {
    cargarProductos();
  }, []);

  const manejarCambio = (e: any) => {
    setProducto({
      ...producto,
      [e.target.name]: e.target.value,
    });
  };

  const guardarProducto = async (e: any) => {
    e.preventDefault();

    if (
      producto.nombreProducto .trim() === "" ||
      producto.descripcion.trim() === "" ||
      producto.marca.trim() === "" ||
      producto.color.trim() === "" ||
      producto.categoria.trim() === ""
    ) {
      alert("Todos los campos son obligatorios");
      return;
    }

    if (Number(producto.precio) <= 0 ) {
      alert("El precio debe ser mayor a 0");
      return;
    }

    if (Number(producto.stock) < 0 ) {
      alert("El stock no puede ser negativo");
      return;
    }

    await crearProducto({
      ...producto,
      precio: Number(producto.precio),
      stock: Number(producto.stock),
    });

    setProducto({
      nombreProducto: "",
      descripcion: "",
      marca: "",
      color: "",
      precio: 0,
      stock: 0,
      categoria: "",
    });

    cargarProductos();
  };

  return (
    <Layout>
      <h1>Gestión de Productos</h1>

      <FormularioProducto
        producto={producto}
        manejarCambio={manejarCambio}
        guardarProducto={guardarProducto}
      />

      <ListaProductos productos={productos} />
    </Layout>
  );
}

export default App;