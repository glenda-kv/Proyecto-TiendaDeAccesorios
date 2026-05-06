import { api } from "./api";

export const obtenerProductos = async () => {
  const response = await api.get("/Producto");
  return response.data;
};

// tienes las funciones especificas de productos.
