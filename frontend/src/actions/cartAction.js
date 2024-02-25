import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";

export const getShoppingCart = createAsyncThunk(
  "shoppingCart/GetById",
  async ({ rejectWithValue }) => {
    try {
      const shoppingCartId =
        localStorage.getItem("shoppingCartId") ??
        "00000000-0000-0000-0000-000000000000";

      const { data } = await axios.get(
        `/api/v1/ShoppingCart/${shoppingCartId}`
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const addItemShoppingCart = createAsyncThunk(
  "shoppingCart/update",
  async (params, { rejectWithValue }) => {
    try {
      const { shoppingCartItems, item, cantidad } = params;

      let items = [];

      items = shoppingCartItems.slice();

      const indice = shoppingCartItems.findIndex(
        (i) => i.productId === item.productId
      );

      if (indice === -1) {
        items.push(item);
      } else {
        let cantidad_ = items[indice].cantidad;
        var total = cantidad_ + cantidad;
        let itemNewClone = { ...items[indice] };
        itemNewClone.cantidad = total;
        items[indice] = itemNewClone;
      }

      var request = {
        shoppingCartItems: items,
      };

      const requestConfig = {
        headers: {
          "Content-Type": "application/json",
        },
      };

      const { data } = await axios.put(
        `/api/v1/ShoppingCart/${params.shoppingCartId}`,
        request,
        requestConfig
      );

      return data;
    } catch (err) {
      rejectWithValue(err.response.data.message);
    }
  }
);

export const removeItemShoppingCart = createAsyncThunk(
  "shoppingCart/removeItem",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "application/json",
        },
      };

      const { data } = await axios.delete(
        `/api/v1/ShoppingCart/item/${params.id}`,
        params,
        requestConfig
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const saveAddressInfo = createAsyncThunk(
  "shoppingCart/saveAddressInfo",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "application/json",
        },
      };

      const { data } = await axios.post(
        `api/v1/order/address`,
        params,
        requestConfig
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);
