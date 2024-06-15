import React, { useEffect, useRef, useState } from "react";
// Import Swiper React components
import { Swiper, SwiperSlide } from "swiper/react";

// Import Swiper styles
import "swiper/css";
import "swiper/css/pagination";

import "./index.scss";

// import required modules
import { Autoplay, Pagination } from "swiper/modules";
import axios from "axios";

//

export default function Carousel({
  numberOfSlides = 1,
  Category = "Trending",
  autoplay = false,
}) {
  console.log(numberOfSlides);

  const [fastfoods, setFastFood] = useState([]);

  const fetchFastFood = async () => {
    const response = await axios.get(
      "https://localhost:7173/api/MenuItemFood/ViewAllFoods"
    );

    setFastFood(response.data.data);
  };

  useEffect(() => {
    fetchFastFood();
  }, []);

  return (
    <>
      <Swiper
        slidesPerView={numberOfSlides}
        pagination={true}
        modules={autoplay ? [Pagination, Autoplay] : [Pagination]}
        className={`carousel ${numberOfSlides > 1 ? "multi-item" : ""}`}
        autoplay={{
          delay: 2500,
          disableOnInteraction: false,
        }}
      >
        {fastfoods
          .filter((fastfood) => fastfood.categoryName === Category)
          .map((fastfood) => (
            <SwiperSlide>
              <img src={fastfood.image} alt="" />
            </SwiperSlide>
          ))}
      </Swiper>
    </>
  );
}
