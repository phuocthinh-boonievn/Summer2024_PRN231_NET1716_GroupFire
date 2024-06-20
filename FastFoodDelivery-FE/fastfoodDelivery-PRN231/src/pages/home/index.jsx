import { Card } from "antd";
import Meta from "antd/es/card/Meta";
import React from "react";
import Carousel from "../../components/carousel";
import Header from "../../components/header";
import "./index.scss";

function HomePage() {
  return (
    <div className="HomePage">
      <Header />
      <div className="HomePage__image">
        <img src="/pannel.jpg" />
      </div>

      {/* <Carousel autoplay /> */}

      <Carousel  numberOfSlides={6} Category="Trending">   
      </Carousel>
      <Carousel numberOfSlides={6} Category="Burger"></Carousel>
    </div>
  );
}

export default HomePage;
