import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  fetchOrderHistory,
  updateOrderStatus,
} from "../../redux/features/ViewOrderHistory";
import { Button, Modal, Form } from "antd";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";

const ViewOrderHistory = () => {
  const dispatch = useDispatch();
  const { orders, status, error } = useSelector((state) => state.orderHistory);
  const UserId = useSelector((state) => state.accountmanage?.UserId);

  useEffect(() => {
    console.log("User ID:", UserId);
    if (UserId) {
      dispatch(fetchOrderHistory(UserId));
    }
  }, [dispatch, UserId]);

  const handleConfirmOrder = async (orderId) => {
    Modal.confirm({
      title: "Confirm Order",
      content: "Are you sure you want to confirm this order as received?",
      onOk: async () => {
        try {
          const response = await axios.delete(
            `https://localhost:7173/api/Orders/GetConfirmOrderByUser/${orderId}`
          );
          console.log("Confirm Order Response:", response.data);
          dispatch(updateOrderStatus({ orderId, status: "Received" }));
          dispatch(fetchOrderHistory(UserId)); // Refresh order history
        } catch (error) {
          console.error("Error confirming order:", error);
          alert("Failed to confirm order: " + error.message);
        }
      },
    });
  };

  const handleLeaveComment = (orderId) => {
    Modal.confirm({
      title: "Leave a Comment",
      content: (
        <Form
          onFinish={async (values) => {
            try {
              const response = await axios.post(
                "https://localhost:7173/api/FeedBacks/CreateFeedBack",
                {
                  userId: UserId,
                  orderId: orderId,
                  commentMsg: values.comment,
                }
              );
              console.log("Response:", response.data);
              Modal.destroyAll();
            } catch (error) {
              console.error("Error posting comment:", error);
              alert("Failed to post comment: " + error.message);
            }
          }}
        >
          <Form.Item
            name="comment"
            rules={[{ required: true, message: "Please input your comment!" }]}
          >
            <input type="text" placeholder="Leave a comment" />
          </Form.Item>
          <Button type="primary" htmlType="submit">
            Submit
          </Button>
        </Form>
      ),
    });
  };

  if (status === "loading") {
    return <div>Loading...</div>;
  }

  if (status === "failed") {
    return <div>{error}</div>;
  }

  if (!orders.length) {
    return (
      <div className="mt-24">
        <h2>Order History</h2>
        <p>No orders to view.</p>
      </div>
    );
  }

  return (
    <div className="mt-24">
      <h2>Order History</h2>
      <table className="table">
        <thead>
          <tr>
            <th>Order ID</th>
            <th>Order Date</th>
            <th>Status Order</th>
            <th>Delivery Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {orders.map((order) => (
            <tr key={order.orderId}>
              <td>{order.orderId}</td>
              <td>{order.orderDate}</td>
              <td>{order.statusOrder}</td>
              <td>{order.deliveryStatus ?? "NULL"}</td>
              <td>
                <Button
                  className="btn btn-primary me-2"
                  onClick={() => handleConfirmOrder(order.orderId)}
                  disabled={order.deliveryStatus !== "Delivered"}
                >
                  Confirm Received
                </Button>
                <Button
                  className="btn btn-secondary me-2"
                  onClick={() => handleLeaveComment(order.orderId)}
                  disabled={order.deliveryStatus !== "Received"}
                >
                  Leave Comment
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ViewOrderHistory;
