import { Button, Form, Image, Input, Modal, Table, Upload } from "antd";
import TextArea from "antd/es/input/TextArea";
import axios from "axios";
import { useEffect, useState } from "react";
import { PlusOutlined } from "@ant-design/icons";
import { useForm } from "antd/es/form/Form";

function FoodItemManagement() {
  const [formVariable] = useForm();

  const [dataSource, setDataSource] = useState([]);
  const [isOpen, setIsOpen] = useState(false);
  const columns = [
    {
      title: "Food Item",
      dataIndex: "name",
      key: "name",
    },
  ];
  const [previewOpen, setPreviewOpen] = useState(false);
  const [previewImage, setPreviewImage] = useState("");
  const [fileList, setFileList] = useState([]);

  const getBase64 = (file) =>
    new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
    });

  const handlePreview = async (file) => {
    if (!file.url && !file.preview) {
      file.preview = await getBase64(file.originFileObj);
    }
    setPreviewImage(file.url || file.preview);
    setPreviewOpen(true);
  };
  const handleChange = ({ fileList: newFileList }) => setFileList(newFileList);
  const uploadButton = (
    <button
      style={{
        border: 0,
        background: "none",
      }}
      type="button"
    >
      <PlusOutlined />
      <div
        style={{
          marginTop: 8,
        }}
      >
        Upload
      </div>
    </button>
  );

  function handleShowModal() {
    setIsOpen(true);
  }

  function handleHideModal() {
    setIsOpen(false);
  }

  function handleSubmit(values) {
    console.log(values);
  }

  function handleOk() {
    formVariable.submit();
  }

  async function fetchFastFood() {
    const response = await axios.get(
      "https://66472a9251e227f23ab155ec.mockapi.io/FastFood"
    );

    setDataSource(response.data);
  }

  useEffect(() => {
    fetchFastFood();
  }, []);

  return (
    <div>
      <Button type="primary" onClick={handleShowModal}>
        Add New Fast Food
      </Button>
      <Table columns={columns} dataSource={dataSource}></Table>

      <Modal
        open={isOpen}
        title="Add New Fast Food"
        onCancel={handleHideModal}
        onOk={handleOk}
      >
        <Form
          labelCol={{
            span: 24,
          }}
          form={formVariable}
          onFinish={handleSubmit}
        >
          <Form.Item label="Food name" name={"name"}>
            <Input />
          </Form.Item>
          <Form.Item label="Description" name={"Description"}>
            <TextArea rows={4} />
          </Form.Item>
          <Form.Item label="UnitPrice" name={"UnitPrice"}>
            <Input />
          </Form.Item>
          <Form.Item label="Category" name={"Category"}>
            <Input />
          </Form.Item>
          <Form.Item label="Status" name={"Status"}>
            <Input />
          </Form.Item>
          <Form.Item label="Image" name={"image"}>
            <Upload
              action="https://660d2bd96ddfa2943b33731c.mockapi.io/api/upload"
              listType="picture-card"
              fileList={fileList}
              onPreview={handlePreview}
              onChange={handleChange}
            >
              {fileList.length >= 8 ? null : uploadButton}
            </Upload>
          </Form.Item>
        </Form>
      </Modal>
      {previewImage && (
        <Image
          wrapperStyle={{
            display: "none",
          }}
          preview={{
            visible: previewOpen,
            onVisibleChange: (visible) => setPreviewOpen(visible),
            afterOpenChange: (visible) => !visible && setPreviewImage(""),
          }}
          src={previewImage}
        />
      )}
    </div>
  );
}

export default FoodItemManagement;
