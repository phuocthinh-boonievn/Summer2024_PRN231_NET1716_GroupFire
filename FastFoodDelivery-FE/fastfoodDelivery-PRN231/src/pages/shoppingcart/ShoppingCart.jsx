// src/components/ShoppingCart.js

import React, { useState } from "react";
import ShoppingCartItem from "./ShoppingCartItem";
import ShoppingCartSummary from "./ShoppingCartSummary";
import "./ShoppingCart.scss";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import {
  decreaseQuantity,
  increaseQuantity,
  removeFood,
} from "../../redux/features/fastfoodCart";

const ShoppingCart = () => {
  const initialItems = useSelector((state) => state.fastfoodcard);
  const [items, setItems] = useState([]);
  const [shipping, setShipping] = useState(5);
  const dispatch = useDispatch();
  const handleAdd = (id) => {
    dispatch(increaseQuantity(id));
  };

  const handleDecrease = (id) => {
    dispatch(decreaseQuantity(id));
  };

  const handleDelete = (id) => {
    // setItems(items.filter((item) => item.id !== id));
    dispatch(removeFood(id));
  };

  const handleCheckout = () => {
    alert("Checkout successful!");
  };

  const totalPrice = initialItems?.reduce(
    (total, item) => total + item.price * item.quantity,
    0
  );

  return (
    <div className="shopping-cart">
      <div className="cart-items">
        <h2>Shopping Cart</h2>
        {initialItems?.map((item) => (
          <ShoppingCartItem
            key={item.id}
            item={item}
            onAdd={handleAdd}
            onRemove={handleDecrease}
            onDelete={handleDelete}
          />
        ))}
        <Link to="/">
          <button className="back-to-shop">Back to MenuFood</button>
        </Link>
      </div>
      <ShoppingCartSummary
        items={items}
        totalPrice={totalPrice}
        shipping={shipping}
        onCheckout={handleCheckout}
      />
    </div>
  );
};

export default ShoppingCart;
