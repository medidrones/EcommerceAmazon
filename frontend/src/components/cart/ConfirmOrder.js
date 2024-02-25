import React from "react";
import { Fragment } from "react";
import MetaData from "../layout/MetaData";
import { CheckoutSteps } from "./CheckoutSteps";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { resetUpdateStatus } from "../../slices/orderSlice";
import { useAlert } from "react-alert";
import { saveOrder } from "../../actions/orderAction";

const ConfirmOrder = () => {
  const alert = useAlert();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {
    shoppingCartItems,
    shoppingCartId,
    total,
    cantidad,
    subtotal,
    precioEnvio,
    impuesto,
  } = useSelector((state) => state.cart);

  const items = shoppingCartItems.slice();

  const { isUpdated, errores } = useSelector((state) => state.order);

  const { user, direccionEnvio } = useSelector((state) => state.security);

  useEffect(() => {
    if (isUpdated) {
      //navegar hacia el siguiente step
      navigate("/payment");
      //alert.success("Se creo la orden de compra");
      dispatch(resetUpdateStatus({}));
    }

    if (errores) {
      errores.map((error) => alert.error(error));
    }
  }, [dispatch, isUpdated, errores]);

  const handlerSubmit = () => {
    const request = {
      shoppingCartId,
    };

    dispatch(saveOrder(request));
  };

  return (
    <Fragment>
      <MetaData titulo="Confirmacion de Orden" />
      <CheckoutSteps envio confirmarOrden />

      <div className="row d-flex justify-content-between">
        <div className="col-12 col-lg-8 mt-5 order-confirm">
          <h4 className="mb-3">Informacion de Envio</h4>
          <p>
            <b>Name:</b> {user.nombre + " " + user.apellido}
          </p>
          <p className="mb-4">
            <b>Direccion:</b>
            {(direccionEnvio ? direccionEnvio.direccion : "") +
              ", " +
              (direccionEnvio ? direccionEnvio.ciudad : "") +
              ", " +
              (direccionEnvio ? direccionEnvio.departamento : "") +
              ", " +
              (direccionEnvio ? direccionEnvio.codigoPostal : "") +
              ", " +
              (direccionEnvio ? direccionEnvio.pais : "")}{" "}
          </p>

          <hr />
          <h4 className="mt-4">Tu Carrito de Compras:</h4>

          {shoppingCartItems.map((item) => (
            <Fragment key={item.id}>
              <hr />
              <div className="cart-item my-1">
                <div className="row">
                  <div className="col-4 col-lg-2">
                    <img
                      src={item.imagen}
                      alt={item.producto}
                      height="45"
                      width="65"
                    />
                  </div>

                  <div className="col-5 col-lg-6">
                    <Link to={`/product/${item.productId}`}>
                      {item.producto}
                    </Link>
                  </div>

                  <div className="col-4 col-lg-4 mt-4 mt-lg-0">
                    <p>
                      {item.cantidad} x ${item.precio} ={" "}
                      <b>${item.totalLine}</b>
                    </p>
                  </div>
                </div>
              </div>
              <hr />
            </Fragment>
          ))}
        </div>

        <div className="col-12 col-lg-3 my-4">
          <div id="order_summary">
            <h4>Order de Compra</h4>
            <hr />
            <p>
              Subtotal:{" "}
              <span className="order-summary-values">${subtotal}</span>
            </p>
            <p>
              Precio de Envio:{" "}
              <span className="order-summary-values">${precioEnvio}</span>
            </p>
            <p>
              Impuesto:{" "}
              <span className="order-summary-values">${impuesto}</span>
            </p>

            <hr />

            <p>
              Total: <span className="order-summary-values">${total}</span>
            </p>

            <hr />
            <button
              id="checkout_btn"
              className="btn btn-primary btn-block"
              onClick={handlerSubmit}
            >
              Pagar Productos
            </button>
          </div>
        </div>
      </div>
    </Fragment>
  );
};

export default ConfirmOrder;
