interface Props {
  producto: any;
}

export function Producto({ producto }: Props) {
  return (
    <tr>
      <td>{producto.nombreProducto}</td>
      <td>{producto.marca}</td>
      <td>{producto.color}</td>
      <td>{producto.precio}</td>
      <td>{producto.stock}</td>
    </tr>
  );
}