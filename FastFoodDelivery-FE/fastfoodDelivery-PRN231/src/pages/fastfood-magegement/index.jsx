import {
  Button,
  Form,
  Image,
  Input,
  Modal,
  Popconfirm,
  Select,
  Table,
  Upload,
} from "antd";
import TextArea from "antd/es/input/TextArea";
import axios from "axios";
import { useEffect, useState } from "react";
import { PlusOutlined } from "@ant-design/icons";
import { useForm } from "antd/es/form/Form";
import uploadFile from "../../utils/upload";

function FoodItemManagement() {
  const [formVariable] = useForm();

  const [dataSource, setDataSource] = useState([]);
  const [isOpen, setIsOpen] = useState(false);

  const handleDeleteMovie = async (foodId) => {
    console.log("Delete Fast Food", foodId);

    await axios.delete(
      `https://localhost:7173/api/MenuItemFood/DeleteFood/${foodId}`
    );

    const listAfterDelete = dataSource.filter(
      (fastfoot) => fastfoot.foodId != foodId
    );
    setDataSource(listAfterDelete);
  };
  const columns = [
    {
      title: "foodName",
      dataIndex: "foodName",
      key: "foodName",
    },
    {
      title: "foodDescription",
      dataIndex: "foodDescription",
      key: "foodDescription",
    },
    {
      title: "foodStatus",
      dataIndex: "foodStatus",
      key: "foodStatus",
    },
    {
      title: "Image",
      dataIndex: "image",
      key: "image",
      render: (image) => <Image src={image} width={300} />,
    },
    {
      title: "Action",
      dataIndex: "foodId",
      key: "foodId",
      render: (foodId) => (
        <>
          <Popconfirm
            title="Delete the task"
            description="Are you sure to delete this task?"
            onConfirm={() => handleDeleteMovie(foodId)}
            okText="Yes"
            cancelText="No"
          >
            <Button danger>Delete</Button>
          </Popconfirm>
        </>
      ),
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

  async function handleSubmit(values) {
    console.log(values);
    console.log(values.image.file.originFileObj);

    const url = await uploadFile(values.image.file.originFileObj);
    values.image = url;
    console.log(values);

    const response = await axios.post(
      "https://localhost:7173/api/MenuItemFood/CreateFood",
      values
    );

    setDataSource([...dataSource, values]);

    // clear form
    formVariable.resetFields();

    //hide form
    handleHideModal();
  }

  function handleOk() {
    formVariable.submit();
  }

  async function fetchFastFood() {
    const response = await axios.get(
      "https://localhost:7173/api/MenuItemFood/ViewAllFoods"
    );
    console.log("===============================>>>>>");
    console.log(response.data);
    setDataSource(response.data.data);
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
          <Form.Item label="Food name" name={"foodName"}>
            <Input />
          </Form.Item>
          <Form.Item label="Description" name={"foodDescription"}>
            <TextArea rows={4} />
          </Form.Item>
          <Form.Item label="UnitPrice" name={"unitPrice"}>
            <Input />
          </Form.Item>
          <Form.Item label="Category" name="categoryId">
            <Select
              options={[
                {
                  value: "b7a13674-b134-4073-81bb-1fdf05e304d2",
                  label: <span>Trending</span>,
                },
                {
                  value: "347d1897-f698-47b8-9543-cce8c04de407",
                  label: <span>Burger</span>,
                },
              ]}
            />
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
