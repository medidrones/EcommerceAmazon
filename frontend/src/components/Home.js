import React, { Fragment, useEffect } from "react";
import { useAlert } from "react-alert";
import { useDispatch, useSelector } from "react-redux";
import { getProducts } from "../actions/productsAction";
import Loader from "./layout/Loader";
import MetaData from "./layout/MetaData";
import Product from "./product/Product";

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

  if (loading) {
    return <Loader />;
  }

  return (
    <Fragment>
      <MetaData titulo={"Los mejores productos online"} />
      <section id="products" className="container mt-5">
        <div className="row">
          {products
            ? products.map((productElement) => (
                <Product
                  key={productElement.id}
                  product={productElement}
                  col={4}
                />
              ))
            : "No tiene elementos products"}
        </div>
      </section>
    </Fragment>
  );
};

export default Home;
