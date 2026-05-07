interface Props {
  producto: any;
}

export function Producto({ producto }: Props) {
  return (
    <tr>
      <td className="fw-semibold">
        {producto.nombreProducto}
      </td>

      <td>{producto.marca}</td>

      <td>
        <span className="badge bg-secondary">
          {producto.color}
        </span>
      </td>

      <td className="text-success fw-bold">
        Bs. {producto.precio}
      </td>

      <td>
        {producto.stock > 5 ? (
          <span className="badge bg-success">
            {producto.stock}
          </span>
        ) : (
          <span className="badge bg-danger">
            {producto.stock}
          </span>
        )}
      </td>

      <td>{producto.categoria}</td>
    </tr>
  );
}