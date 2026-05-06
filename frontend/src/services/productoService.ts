import { api } from "./api";

export const obtenerProductos = async () => {
  const response = await api.get("/Producto");
  return response.data;
};

export const crearProducto = async (producto: any) => {
  const response = await api.post("/Producto", producto);
  return response.data;
};

// tienes las funciones especificas de productos.
