import { combineReducers } from "@reduxjs/toolkit";
import fastfoodCart from "./features/fastfoodCart";
import userAccount from "./features/userAccount";

export const rootReducer = combineReducers({
  accountmanage: userAccount,
  fastfoodcard: fastfoodCart,
});
