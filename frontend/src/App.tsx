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
    precio: "",
    stock: "",
    categoria: "",
  });

  const cargarProductos = async () => {
    try {
      const data = await obtenerProductos();
      setProductos(data);
    } catch (error) {
      console.log(error);
      alert("Error al cargar productos");
    }
  };

  useEffect(() => {
    cargarProductos();
  }, []);   // Ejecuta cargarproductos automaticamente cuando carga la pagina

  const manejarCambio = (e: any) => {
    setProducto({
      ...producto,
      [e.target.name]: e.target.value, // Actualiza el estado del producto con el valor del campo que se está editando
    });
  };

  const guardarProducto = async (e: any) => {
    e.preventDefault();

    if (
      producto.nombreProducto.trim() === "" ||
      producto.descripcion.trim() === "" ||
      producto.marca.trim() === "" ||
      producto.color.trim() === "" ||
      producto.categoria.trim() === ""
    ) {
      alert("Todos los campos son obligatorios");
      return;
    }

    if (Number(producto.precio) <= 0) {
      alert("El precio debe ser mayor a 0");
      return;
    }

    if (Number(producto.stock) < 0) {
      alert("El stock no puede ser negativo");
      return;
    }

    try {
      await crearProducto({
        ...producto,
        precio: Number(producto.precio),
        stock: Number(producto.stock),
      });

      alert("Producto agregado correctamente");

      setProducto({
        nombreProducto: "",
        descripcion: "",
        marca: "",
        color: "",
        precio: "",
        stock: "",
        categoria: "",
      });

      cargarProductos();
    } catch (error) {
      console.log(error);
      alert("Error al guardar producto");
    }
  };

  return (
    <Layout>
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h1 className="fw-bold text-dark">
          Gestión de Productos
        </h1>
      </div>

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