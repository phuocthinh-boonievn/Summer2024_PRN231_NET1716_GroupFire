import { async } from "@firebase/util";
import { Modal, Table, Form, Input, Button, Space, Popconfirm } from "antd";
import { useForm } from "antd/es/form/Form";
import axios from "axios";
import React, { useEffect, useState } from "react";
import "./index.scss";

function Category() {
  const [formVariable] = useForm();
  const [dataSource, setDataSource] = useState([]);
  const [isOpen, setIsOpen] = useState(false);

  const handleDeleteCategory = async (CategoryId) => {
    console.log("Delete Fast Food", CategoryId);

    // await axios.delete(
    //   `https://localhost:7173/api/Category/DeleteCategory/${CategoryId}`
    // );

    // const listAfterDelete = dataSource.filter(
    //   (category) => category.CategoryId != CategoryId
    // );
    // setDataSource(listAfterDelete);
  };
  const columns = [
    {
      title: "categoriesName",
      dataIndex: "categoriesName",
      key: "categoriesName",
    },
    {
      title: "Action",
      dataIndex: "CategoryId",
      key: "CategoryId",
      render: (CategoryId) => (
        <Space>
          <Popconfirm
            title="Delete Category"
            description="Are you sure to dele this Category?"
            onConfirm={() => handleDeleteCategory(CategoryId)}
            onText="Yes"
            cancelText="No"
          >
            <Button type="primary" danger>
              Delete
            </Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  function handleShowModal() {
    setIsOpen(true);
  }

  function handleHideModal() {
    setIsOpen(false);
  }

  async function handleSubmit(values) {
    console.log(values);

    const response = await axios.post(
      "https://localhost:7173/api/Category/CreateCategory",
      values
    );

    setDataSource([...dataSource, values]);

    formVariable.resetFields();

    handleHideModal();
  }

  function handleOk() {
    formVariable.submit();
  }

  async function fetchCategory() {
    const response = await axios.get(
      "https://localhost:7173/api/Category/ViewAllCategorys"
    );
    setDataSource(response.data.data);
  }

  useEffect(() => {
    fetchCategory();
  }, []);

  return (
    <div className="categoryPage">
      <Button type="primary" onClick={handleShowModal}>
        Add New Category
      </Button>
      <Table columns={columns} dataSource={dataSource}></Table>
      <Modal
        open={isOpen}
        title="Add New Category"
        onCancel={handleHideModal}
        onOk={handleOk}
      >
        <Form form={formVariable} onFinish={handleSubmit}>
          <Form.Item label="Category Name" name={"categoriesName"}>
            <Input />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}

export default Category;
