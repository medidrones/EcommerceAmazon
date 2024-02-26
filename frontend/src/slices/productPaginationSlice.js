import { createSlice } from "@reduxjs/toolkit";
import { getProductPagination } from "../actions/productsAction";

export const initialState = {
  products: [],
  count: 0,
  pageIndex: 1,
  pageSize: 6,
  pageCount: 0,
  loading: false,
  resultByPage: 0,
  error: null,
  search: null,
  precioMax: null,
  precioMin: null,
  category: null,
  rating: null,
};

export const productPaginationSlice = createSlice({
  name: "getProductPagination",
  initialState,
  reducers: {
    searchPagination: (state, action) => {
      state.search = action.payload.search;
      state.pageIndex = 1;
    },

    setPageIndex: (state, action) => {
      state.pageIndex = action.payload.pageIndex;
    },

    updatePrecio: (state, action) => {
      state.precioMax = action.payload.precio[1];
      state.precioMin = action.payload.precio[0];
    },

    resetPagination: (state, action) => {
      state.precioMax = null;
      state.precioMin = null;
      state.pageIndex = 1;
      state.search = null;
      state.category = null;
      state.rating = null;
    },

    updateCategory: (state, action) => {
      state.category = action.payload.category;
    },

    updateRating: (state, action) => {
      state.rating = action.payload.rating;
    },
  },

  extraReducers: {
    [getProductPagination.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },

    [getProductPagination.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.products = payload.data;
      state.count = payload.count;
      state.pageIndex = payload.pageIndex;
      state.pageSize = payload.pageSize;
      state.pageCount = payload.pageCount;
      state.resultByPage = payload.resultByPage;
      state.error = null;
    },

    [getProductPagination.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
  },
});

export const {
  searchPagination,
  setPageIndex,
  updatePrecio,
  resetPagination,
  updateCategory,
  updateRating,
} = productPaginationSlice.actions;

export const productPaginationReducer = productPaginationSlice.reducer;
