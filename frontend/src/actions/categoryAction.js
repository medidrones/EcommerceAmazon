import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";

export const getCategories = createAsyncThunk(
  "category/getCategories",
  async (ThunkApi, { rejectWithValue }) => {
    try {
      return await axios.get(`/api/v1/category/list`);
    } catch (err) {
      return rejectWithValue(`Errores : ${err.message}`);
    }
  }
);
