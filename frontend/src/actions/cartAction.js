import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";

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
