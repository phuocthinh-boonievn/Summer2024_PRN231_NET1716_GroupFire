import { Button, Form, Input } from "antd";
import { useForm } from "antd/es/form/Form";
import Password from "antd/es/input/Password";
import Link from "antd/es/typography/Link";
import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./index.scss";

function Register() {
  const [formVariable] = useForm();
  const [dataSource, setDataSource] = useState([]);

  const navigate = useNavigate();

  function handleOK() {
    formVariable.submit();
  }

  function handleSubmit(values) {
    try {
      if (dataSource.Password !== dataSource.confirmPassword) {
        alert("Passwords do not match");
        return;
      }
      const response = axios.post(
        "https://localhost:7173/api/User/register",
        values
      );

      setDataSource([...dataSource, values]);
      formVariable.resetFields();
      navigate("/login");
    } catch (err) {
      console.log(err);
      alert("Please input Correct Information");
    }
  }

  return (
    <div className="register">
      <img
        className="register__image"
        src="https://i.pinimg.com/originals/1a/04/9d/1a049d0db3c8a238ded1b4af745b1c1c.jpg"
        width="640"
        height="360"
        frameborder="0"
        allow="autoplay; fullscreen; picture-in-picture"
        allowfullscreen
      />
      <div className="wrapper">
        <div className="register__logo">
          <Link to="/">
            <img
              src="https://www.fastfoodcart.com/sites/default/files/logo_header_3/fastfoodcart2.png"
              alt=""
              width={200}
            />
          </Link>
        </div>
        <div className="line"></div>
        <form onSubmit={handleSubmit}>
          <div className="register__form">
            <h3>Register Your Acount</h3>
            <Form form={formVariable} onFinish={handleSubmit}>
              <Form.Item
                name={"email"}
                rules={[
                  {
                    required: true,
                    message: "Please Input Email",
                  },
                ]}
              >
                <Input type="text" placeholder="Email" />
              </Form.Item>
              <Form.Item
                name={"userName"}
                rules={[
                  {
                    required: true,
                    message: "Please Input UserName",
                  },
                ]}
              >
                <Input type="text" placeholder="UserName" />
              </Form.Item>
              <Form.Item
                name={"fullName"}
                rules={[
                  {
                    required: true,
                    message: "Please Input Full Name",
                  },
                ]}
              >
                <Input type="text" placeholder="Full Name" />
              </Form.Item>
              <Form.Item
                name={"address"}
                rules={[
                  {
                    required: true,
                    message: "Please Input Address",
                  },
                ]}
              >
                <Input type="text" placeholder="Address" />
              </Form.Item>
              <Form.Item
                name={"phoneNumber"}
                rules={[
                  {
                    required: true,
                    message: "Please Input PhoneNumber",
                  },
                ]}
              >
                <Input type="number" placeholder="PhoneNumber" />
              </Form.Item>
              <Form.Item
                name={"password"}
                rules={[
                  {
                    required: true,
                    message: "Please Input Passowrd",
                  },
                ]}
              >
                <Input type="password" placeholder="Passowrd" />
              </Form.Item>
              <Form.Item
                name={"confirmPassword"}
                rules={[
                  {
                    required: true,
                    message: "Please Input Confirm Password",
                  },
                ]}
              >
                <Input type="password" placeholder="Confirm Password" />
              </Form.Item>
              <Form.Item>
                <Button type="submit" onClick={handleOK}>
                  Register
                </Button>
              </Form.Item>
            </Form>
          </div>
        </form>
      </div>
    </div>
  );
}

export default Register;
