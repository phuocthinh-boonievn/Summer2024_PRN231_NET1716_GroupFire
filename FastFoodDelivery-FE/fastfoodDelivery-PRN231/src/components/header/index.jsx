import "./index.scss";
import { Link } from "react-router-dom";
import { SearchOutlined, UserOutlined, CloseOutlined } from "@ant-design/icons";
import { useState } from "react";
import { Input } from "antd";

function Header() {
  const [isShowSearch, setIsShowSearch] = useState(false);
  return (
    <header className="header">
      <div className="header__logo">
        <Link to={"/"}>
          <img
            src="https://www.fastfoodcart.com/sites/default/files/logo_header_3/fastfoodcart2.png"
            alt=""
            width={200}
          />
        </Link>
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
          <li onClick={() => setIsShowSearch(true)}>
            <SearchOutlined />
          </li>
          <li>
            <Link to="/login">
              <UserOutlined />
            </Link>
          </li>
        </ul>
      </nav>
      <div className={`header__search ${isShowSearch ? "active" : ""}`}>
        <Input type={Text} placeholder="Search a fast food..." />
        <CloseOutlined onClick={() => setIsShowSearch(false)} />
      </div>
    </header>
  );
}

export default Header;
