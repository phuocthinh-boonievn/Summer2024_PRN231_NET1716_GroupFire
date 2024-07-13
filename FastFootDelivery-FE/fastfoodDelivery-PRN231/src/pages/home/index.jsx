import { Card } from "antd";
import Meta from "antd/es/card/Meta";
import React, { useEffect, useState } from "react";
import Carousel from "../../components/carousel";
import Header from "../../components/header";
import "./index.scss";

function HomePage() {
  const [dataCategory, setDataCategory] = useState([]);
  const fetchCategories = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/Category/ViewAllCategorys"
      );
      console.log(response.data);
      setDataCategory(response.data);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  return (
    <div className="HomePage">
      <Header />
      <div className="HomePage__image">
        <img src="/pannel.jpg" />
      </div>

      {/* <Carousel autoplay /> */}

      <Carousel numberOfSlides={5} Category="Trending"></Carousel>
      <Carousel numberOfSlides={5} Category="FastFood"></Carousel>
      <Carousel numberOfSlides={5} Category="Burger"></Carousel>
    </div>
  );
}

export default HomePage;
