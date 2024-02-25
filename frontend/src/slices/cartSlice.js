import { createSlice } from "@reduxjs/toolkit";
import {
  addItemShoppingCart,
  getShoppingCart,
  removeItemShoppingCart,
} from "../actions/cartAction";
import { confirmPayment } from "../actions/orderAction";

const initialState = {
  shoppingCartId: "",
  shoppingCartItems: [],
  loading: false,
  error: null,
  total: 0,
  cantidad: 0,
  subtotal: 0,
  impuesto: 0,
  precioEnvio: 0,
};

export const cartSlice = createSlice({
  name: "cartItems",
  initialState,
  extraReducers: {
    [getShoppingCart.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [getShoppingCart.fulfilled]: (state, { payload }) => {
      console.log("payload", payload);
      localStorage.setItem("shoppingCartId", payload.shoppingCartId);
      state.shoppingCartId = payload.shoppingCartId;
      state.shoppingCartItems = payload.shoppingCartItems ?? [];
      state.loading = false;
      state.error = null;
      state.total = payload.total;
      state.cantidad = payload.cantidad;
      state.subtotal = payload.subtotal;
      state.impuesto = payload.impuesto;
      state.precioEnvio = payload.precioEnvio;
    },
    [getShoppingCart.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },

    [addItemShoppingCart.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [addItemShoppingCart.fulfilled]: (state, { payload }) => {
      state.shoppingCartId = payload.shoppingCartId;
      state.shoppingCartItems = payload.shoppingCartItems ?? [];
      state.loading = false;
      state.error = null;
      state.total = payload.total;
      state.cantidad = payload.cantidad;
      state.subtotal = payload.subtotal;
      state.impuesto = payload.impuesto;
      state.precioEnvio = payload.precioEnvio;
    },
    [addItemShoppingCart.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },

    [removeItemShoppingCart.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [removeItemShoppingCart.fulfilled]: (state, { payload }) => {
      state.shoppingCartId = payload.shoppingCartId;
      state.shoppingCartItems = payload.shoppingCartItems ?? [];
      state.loading = false;
      state.error = null;
      state.total = payload.total;
      state.cantidad = payload.cantidad;
      state.subtotal = payload.subtotal;
      state.impuesto = payload.impuesto;
      state.precioEnvio = payload.precioEnvio;
    },
    [removeItemShoppingCart.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },

    [confirmPayment.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },

    [confirmPayment.fulfilled]: (state, { payload }) => {
      //state.shoppingCartId = '';
      state.shoppingCartItems = [];
      state.loading = false;
      state.error = null;
      state.total = 0;
      state.cantidad = 0;
      state.subtotal = 0;
      state.impuesto = 0;
      state.precioEnvio = 0;
    },
    [confirmPayment.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
  },
});

export const cartReducer = cartSlice.reducer;
