import { configureStore } from "@reduxjs/toolkit";
import userAccount from "./features/userAccount";

export const store = configureStore({
  reducer: {
    accountmanage: userAccount,
  },
});
