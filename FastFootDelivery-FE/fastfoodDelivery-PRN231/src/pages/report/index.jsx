import {
  BsFillArchiveFill,
  BsFillGrid3X3GapFill,
  BsPeopleFill,
} from "react-icons/bs";
import {
  BarChart,
  Bar,
  Rectangle,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
  LineChart,
  Line,
  Pie,
  PieChart,
  Cell,
} from "recharts";
import { FaSackDollar } from "react-icons/fa6";
import "./report.scss";
import { useEffect, useState } from "react";
import axios from "axios";
import { EffectCards } from "swiper/modules";
import { TruckOutlined } from "@ant-design/icons";

function Report() {
  //Food
  const [dataFood, setDateFood] = useState(0);
  const fetchFood = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/AdminDashboard/dashboard/total-food-menu"
      );
      console.log(response.data);
      setDateFood(response.data);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    fetchFood();
  }, []);
  //Category
  const [dataCategories, setDateCategories] = useState(0);
  const fetchCategories = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/AdminDashboard/dashboard/total-categories"
      );
      console.log(response.data);
      setDateCategories(response.data);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  //Order
  const [dataOrder, setDateOrder] = useState(0);
  const fetchOrder = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/AdminDashboard/dashboard/total-orders"
      );
      console.log(response.data);
      setDateOrder(response.data);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    fetchOrder();
  }, []);

  //User
  const [dataUser, setDataUser] = useState(0);
  const fetchUser = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/AdminDashboard/dashboard/active-user"
      );
      console.log(response.data);
      setDataUser(response.data);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    fetchUser();
  }, []);

  const data = [
    { name: "Page A", uv: 4000, pv: 2400, amt: 2400 },
    { name: "Page B", uv: 3000, pv: 1398, amt: 2210 },
    { name: "Page C", uv: 2000, pv: 9800, amt: 2290 },
    { name: "Page D", uv: 2780, pv: 3908, amt: 2000 },
    { name: "Page E", uv: 1890, pv: 4800, amt: 2181 },
    { name: "Page F", uv: 2390, pv: 3800, amt: 2500 },
    { name: "Page G", uv: 3490, pv: 4300, amt: 2100 },
  ];

  const renderCustomizedLabel = ({
    cx,
    cy,
    midAngle,
    innerRadius,
    outerRadius,
    percent,
    index,
  }) => {
    const radius = innerRadius + (outerRadius - innerRadius) * 0.5;
    const x = cx + radius * Math.cos(-midAngle * RADIAN);
    const y = cy + radius * Math.sin(-midAngle * RADIAN);

    return (
      <text
        x={x}
        y={y}
        fill="white"
        textAnchor={x > cx ? "start" : "end"}
        dominantBaseline="central"
      >
        {`${(percent * 100).toFixed(0)}%`}
      </text>
    );
  };
  const COLORS = ["#0088FE", "#00C49F", "#FFBB28", "#FF8042"];

  return (
    <main className="main-container-report">
      <div className="main-title-report">
        <h3>STATISTICS</h3>
      </div>
      <div className="main-cards-report">
        <div className="card-report">
          <div className="card-inner-report">
            <BsFillArchiveFill className="card_icon-report" />
            <h3>Product</h3>
            <h1>{dataFood}</h1>
          </div>
        </div>
        <div className="card-report">
          <div className="card-inner-report">
            <BsFillGrid3X3GapFill className="card_icon-report" />
            <h3>CATEGORIES</h3>
            <h1>{dataCategories}</h1>
          </div>
        </div>
        <div className="card-report">
          <div className="card-inner-report">
            <BsPeopleFill className="card_icon-report" />
            <h3>CUSTOMERS</h3>
            <h1>{dataUser}</h1>
          </div>
        </div>
        <div className="card-report">
          <div className="card-inner-report">
            <TruckOutlined className="card_icon-report" />
            <h3>TotalOrder</h3>
            <h1>{dataOrder}</h1>
          </div>
        </div>
      </div>

      <div className="charts-report">
        <ResponsiveContainer width="100%" height="100%">
          <BarChart
            width={500}
            height={300}
            data={data}
            margin={{
              top: 5,
              right: 30,
              left: 20,
              bottom: 5,
            }}
          >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="name" />
            <YAxis />
            <Tooltip />
            <Legend />
            <Bar dataKey="pv" fill="#8884d8" />
            <Bar dataKey="uv" fill="#82ca9d" />
          </BarChart>
        </ResponsiveContainer>

        <ResponsiveContainer width="100%" height="100%">
          <LineChart
            width={500}
            height={300}
            data={data}
            margin={{
              top: 5,
              right: 30,
              left: 20,
              bottom: 5,
            }}
          >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="name" />
            <YAxis />
            <Tooltip />
            <Legend />
            <Line type="monotone" dataKey="pv" stroke="#8884d8" />
            <Line type="monotone" dataKey="uv" stroke="#82ca9d" />
          </LineChart>
        </ResponsiveContainer>
      </div>
    </main>
  );
}

export default Report;
