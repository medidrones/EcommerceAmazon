import { configureStore } from "@reduxjs/toolkit";
import { productsReducer } from "./slices/productsSlice";
import { productByIdReducer } from "./slices/productByIdSlice";
import { productPaginationReducer } from "./slices/productPaginationSlice";
import { categoryReducer } from "./slices/categorySlice";
import { securityReducer } from "./slices/securitySlice";

export default configureStore({
  reducer: {
    products: productsReducer,
    product: productByIdReducer,
    productPagination: productPaginationReducer,
    category: categoryReducer,
    security: securityReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({ serializableCheck: false }),
});
