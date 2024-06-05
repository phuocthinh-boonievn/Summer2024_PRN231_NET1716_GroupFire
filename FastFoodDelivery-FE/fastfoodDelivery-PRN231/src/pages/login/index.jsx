import { Link } from "react-router-dom";
import "./index.scss";

function Login() {
  return (
    <div className="login">
      <img
        className="login__image"
        src="https://i.pinimg.com/originals/1a/04/9d/1a049d0db3c8a238ded1b4af745b1c1c.jpg"
        width="640"
        height="360"
        frameborder="0"
        allow="autoplay; fullscreen; picture-in-picture"
        allowfullscreen
      />
      <div className="wrapper">
        <div className="login__logo">
          <Link to="">
            <img
              src="https://www.fastfoodcart.com/sites/default/files/logo_header_3/fastfoodcart2.png"
              alt=""
              width={200}
            />
          </Link>
        </div>
        <div className="line"></div>
        <div className="login__form">
          <h3>Login into your account</h3>
          <input type="text" placeholder="Username" />
          <input type="password" placeholder="Password" />
          <button>Login</button>
        </div>
      </div>
    </div>
  );
}

export default Login;
