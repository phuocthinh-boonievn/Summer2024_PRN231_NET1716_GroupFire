import "./index.scss";
import React from "react";
import { Link } from "react-router-dom";
import { SearchOutlined } from "@ant-design/icons";

function Header() {
  return (
    <header className="header">
      <div className="header__logo">
        <img
          src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRiRmpxBnb1N-F6bMLDGVAQXPbiK_66QIe5VA&s"
          alt=""
          width={80}
        />
      </div>

      <nav className="header__nav">
        <ul>
          <li>
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="/fastfood-magegement">FastFoodManagement</Link>
          </li>
          <li>
            <Link to="/">Contact</Link>
          </li>
          <li>
            <SearchOutlined />
          </li>
        </ul>
      </nav>
    </header>
  );
}

export default Header;
