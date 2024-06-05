import "./index.scss";
import React from "react";
import { Link } from "react-router-dom";
import { SearchOutlined, UserOutlined } from "@ant-design/icons";

function Header() {
  return (
    <header className="header">
      <div className="header__logo">
        <img
          src="https://www.fastfoodcart.com/sites/default/files/logo_header_3/fastfoodcart2.png"
          alt=""
          width={200}
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
          <li>
            <Link to="/login">
            <UserOutlined />
            </Link>           
          </li>
        </ul>
      </nav>
    </header>
  );
}

export default Header;
