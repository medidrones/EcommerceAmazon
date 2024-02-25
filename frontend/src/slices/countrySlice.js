import { createSlice } from "@reduxjs/toolkit";
import { getCountries } from "../actions/countryAction";

export const initialState = {
  countries: [],
  loading: false,
  error: null,
};

export const countrySlice = createSlice({
  name: "countries",
  initialState,
  reducers: {},
  extraReducers: {
    [getCountries.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [getCountries.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.error = null;
      state.countries = payload;
    },
    [getCountries.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
  },
});

export const countryReducer = countrySlice.reducer;
