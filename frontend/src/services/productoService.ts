import { api } from "./api";

export const obtenerProductos = async () => {
  const response = await api.get("/Producto/ListarProductos");
  return response.data;
};

export const crearProducto = async (producto: any) => {
  const response = await api.post("/Producto/agregar-producto", producto);
  return response.data;
};
