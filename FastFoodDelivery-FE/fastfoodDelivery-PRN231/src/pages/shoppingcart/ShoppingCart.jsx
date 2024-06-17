// src/components/ShoppingCart.js

import React, { useState } from "react";
import ShoppingCartItem from "./ShoppingCartItem";
import ShoppingCartSummary from "./ShoppingCartSummary";
import "./ShoppingCart.scss";

const initialItems = [
  {
    id: 1,
    name: "Chicken Cheese",
    description: "Deliciously Cheesy Chicken Delight!",
    price: 18,
    quantity: 1,
    image: "cheese.jpg",
  },
  {
    id: 2,
    name: "Chicken Mix",
    description: "Mix It Up with Flavorful Chicken Bliss!",
    price: 23,
    quantity: 1,
    image: "chickenmix.jpg",
  },
  {
    id: 3,
    name: "Burger Chicken",
    description: "Clucking Good Chicken Burgers!",
    price: 15,
    quantity: 1,
    image: "ga.jpg",
  },
];

const ShoppingCart = () => {
  const [items, setItems] = useState(initialItems);
  const [shipping, setShipping] = useState(5);

  const handleAdd = (id) => {
    setItems(
      items.map((item) =>
        item.id === id ? { ...item, quantity: item.quantity + 1 } : item
      )
    );
  };

  const handleRemove = (id) => {
    setItems(
      items.map((item) =>
        item.id === id && item.quantity > 1
          ? { ...item, quantity: item.quantity - 1 }
          : item
      )
    );
  };

  const handleDelete = (id) => {
    setItems(items.filter((item) => item.id !== id));
  };

  const handleCheckout = () => {
    alert("Checkout successful!");
  };

  const totalPrice = items.reduce(
    (total, item) => total + item.price * item.quantity,
    0
  );

  return (
    <div className="shopping-cart">
      <div className="cart-items">
        <h2>Shopping Cart</h2>
        {items.map((item) => (
          <ShoppingCartItem
            key={item.id}
            item={item}
            onAdd={handleAdd}
            onRemove={handleRemove}
            onDelete={handleDelete}
          />
        ))}
        <button className="back-to-shop">Back to shop</button>
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
