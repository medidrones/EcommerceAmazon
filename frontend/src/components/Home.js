import React, { Fragment, useEffect } from "react";
import { useAlert } from "react-alert";
import { useDispatch, useSelector } from "react-redux";
import { getProducts } from "../actions/productsAction";
import MetaData from "./layout/MetaData";
import Products from "./products/Products";

const Home = () => {
  const dispatch = useDispatch();

  const { products, loading, error } = useSelector((state) => state.products);

  const alert = useAlert();
  console.log("error===>", error);

  useEffect(() => {
    if (error != null) {
      return alert.error(error);
    }

    dispatch(getProducts());
  }, [dispatch, alert, error]);

  return (
    <Fragment>
      <MetaData titulo={"Los mejores productos online"} />
      <section id="products" className="container mt-5">
        <div className="row">
          <Products col={4} products={products} loading={loading} />
        </div>
      </section>
    </Fragment>
  );
};

export default Home;
