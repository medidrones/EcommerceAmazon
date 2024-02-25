import { createSlice } from "@reduxjs/toolkit";
import { forgotSendPassword } from "../actions/userAction";

export const initialState = {
  message: null,
  errores: null,
  loading: false,
};

export const forgotPasswordSlice = createSlice({
  name: "forgotPassword",
  initialState,
  reducers: {
    resetError: (state, action) => {
      state.errores = null;
      state.message = null;
      state.loading = false;
    },
  },
  extraReducers: {
    [forgotSendPassword.pending]: (state) => {
      state.message = null;
      state.errores = null;
      state.loading = true;
    },

    [forgotSendPassword.fulfilled]: (state, { payload }) => {
      state.message = payload;
      state.errores = null;
      state.loading = false;
    },

    [forgotSendPassword.rejected]: (state, action) => {
      state.message = null;
      state.errores = action.payload;
      state.loading = false;
    },
  },
});

export const { resetError } = forgotPasswordSlice.actions;
export const forgotPasswordReducer = forgotPasswordSlice.reducer;
