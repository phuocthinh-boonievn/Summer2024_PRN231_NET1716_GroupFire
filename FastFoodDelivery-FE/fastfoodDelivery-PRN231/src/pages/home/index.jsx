import React from "react";
import Carousel from "../../components/carousel";
import Header from "../../components/header";

function HomePage() {
  return (
    <div>
      <Header />
      <Carousel autoplay />
      <Carousel numberOfSlides={6} Category="Trending" />
    </div>
  );
}

export default HomePage;
