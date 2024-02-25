import React from "react";
import { Link } from "react-router-dom";

export const CheckoutSteps = ({ envio, confirmarOrden, payment }) => {
  return (
    <div className="checkout-progress d-flex justify-content-center mt-5">
      {envio ? (
        <Link to="shipping" className="float-right">
          <div className="triangle2-active"></div>
          <div className="step active-step">Envio</div>
          <div className="triangle-active"></div>
        </Link>
      ) : (
        <Link to="#!" disabled>
          <div className="triangle2-incomplete"></div>
          <div className="step incomplete">Envio</div>
          <div className="triangle-incomplete"></div>
        </Link>
      )}

      {confirmarOrden ? (
        <Link to="/order/confirm" className="float-right">
          <div className="triangle2-active"></div>
          <div className="step active-step">Confirmar Orden</div>
          <div className="triangle-active"></div>
        </Link>
      ) : (
        <Link to="#!" disabled>
          <div className="triangle2-incomplete"></div>
          <div className="step incomplete">Confirmar Orden</div>
          <div className="triangle-incomplete"></div>
        </Link>
      )}

      {payment ? (
        <Link to="/payment" className="float-right">
          <div className="triangle2-active"></div>
          <div className="step active-step">Medio de Pago</div>
          <div className="triangle-active"></div>
        </Link>
      ) : (
        <Link to="#!" disabled>
          <div className="triangle2-incomplete"></div>
          <div className="step incomplete">Medio de Pago</div>
          <div className="triangle-incomplete"></div>
        </Link>
      )}
    </div>
  );
};
