import React, { Fragment, useEffect } from "react";
import { useAlert } from "react-alert";
import { useDispatch, useSelector } from "react-redux";
import { getProductPagination, getProducts } from "../actions/productsAction";
import Loader from "./layout/Loader";
import MetaData from "./layout/MetaData";
import Product from "./product/Product";
import Products from "./products/Products";
import Pagination from "react-js-pagination";
import { setPageIndex } from "../slices/productPaginationSlice";

const Home = () => {
  const dispatch = useDispatch();

  //const { products, loading, error } = useSelector((state) => state.products);
  const {
    products,
    count,
    pageIndex,
    loading,
    error,
    resultByPage,
    search,
    pageSize,
    precioMax,
    precioMin,
    category,
    rating,
  } = useSelector((state) => state.productPagination);

  const alert = useAlert();

  useEffect(() => {
    if (error != null) {
      return alert.error(error);
    }

    dispatch(
      getProductPagination({
        pageIndex: pageIndex,
        pageSize: pageSize,
        search: search,
        precioMax: precioMax,
        precioMin: precioMin,
        categoryId: category,
        rating: rating,
      })
    );
  }, [
    dispatch,
    error,
    alert,
    search,
    pageSize,
    pageIndex,
    precioMax,
    precioMin,
    category,
    rating,
  ]);

  function setCurrentPageNo(pageNumber) {
    dispatch(setPageIndex({ pageIndex: pageNumber }));
  }

  return (
    <Fragment>
      <MetaData titulo={"Los mejores productos online"} />
      <section id="products" className="container mt-5">
        <div className="row">
          <Products col={4} products={products} loading={loading} />
        </div>
      </section>

      <div className="d-flex justify-content-center mt-5">
        <Pagination
          activePage={pageIndex}
          itemsCountPerPage={pageSize}
          totalItemsCount={count}
          onChange={setCurrentPageNo}
          nextPageText={">"}
          prevPageText={"<"}
          firstPageText={"<<"}
          lastPageText={">>"}
          item-class="page-item"
          linkClass="page-link"
        />
      </div>
    </Fragment>
  );
};

export default Home;
