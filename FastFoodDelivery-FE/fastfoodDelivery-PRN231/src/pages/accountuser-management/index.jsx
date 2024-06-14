// src/pages/inventory-management/AdminAccountManagement.jsx
import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min";
import "./index.scss";

const apiEndpoint =
  "https://666a91677013419182cfd2b3.mockapi.io/mockapi/FastFood/FastFood"; // Your MockAPI endpoint

function AdminAccountManagement() {
  const [accounts, setAccounts] = useState([]);
  const [selectedAccount, setSelectedAccount] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    fetchAccounts();
  }, []);

  const fetchAccounts = async () => {
    const { data } = await axios.get(apiEndpoint);
    setAccounts(data);
  };

  const handleDelete = async (id) => {
    const confirmed = window.confirm(
      "Are you sure you want to delete this account?"
    );
    if (confirmed) {
      await axios.delete(`${apiEndpoint}/${id}`);
      fetchAccounts();
    }
  };

  const handleSelectAccount = (account) => {
    setSelectedAccount(account);
    setShowModal(true); // Show modal
  };

  const handleEdit = (account) => {
    navigate(`/edit-shipper/${account.id}`, { state: { account } });
  };

  return (
    <div className="accountpage">
      <table className="table">
        <thead>
          <tr>
            <th>Username</th>
            <th>Role</th>
            <th>Phone Number</th>
            <th>Address</th>
            <th>Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {accounts.map((account) => (
            <tr key={account.id}>
              <td>{account.username}</td>
              <td>{account.role}</td>
              <td>{account.phoneNumber}</td>
              <td>{account.address}</td>
              <td>{account.status}</td>
              <td>
                <button
                  className="btn btn-info me-2"
                  onClick={() => handleSelectAccount(account)}
                >
                  View
                </button>
                <button
                  className="btn btn-warning me-2"
                  onClick={() => handleEdit(account)}
                >
                  Edit
                </button>
                <button
                  className="btn btn-danger"
                  onClick={() => handleDelete(account.id)}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* Modal for viewing account details */}
      {showModal && selectedAccount && (
        <div
          className="modal fade show"
          style={{ display: "block", backgroundColor: "rgba(0,0,0,0.5)" }}
          tabIndex="-1"
        >
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Account Details</h5>
                <button
                  type="button"
                  className="btn-close"
                  onClick={() => setShowModal(false)}
                  aria-label="Close"
                ></button>
              </div>
              <div className="modal-body">
                <p>
                  <strong>Username:</strong> {selectedAccount.username}
                </p>
                <p>
                  <strong>Role:</strong> {selectedAccount.role}
                </p>
                <p>
                  <strong>Phone Number:</strong> {selectedAccount.phoneNumber}
                </p>
                <p>
                  <strong>Address:</strong> {selectedAccount.address}
                </p>
                <p>
                  <strong>Status:</strong> {selectedAccount.status}
                </p>
              </div>
              <div className="modal-footer">
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={() => setShowModal(false)}
                >
                  Close
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

export default AdminAccountManagement;
