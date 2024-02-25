import React, { useState, Fragment, useEffect } from "react";
import { useAlert } from "react-alert";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { resetUpdateStatus } from "../../slices/securitySlice";
import { updatePassword } from "../../actions/userAction";
import MetaData from "../layout/MetaData";

const UpdatePassword = () => {
  const [oldPassword, setOldPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");

  const navigate = useNavigate();

  const alert = useAlert();
  const dispatch = useDispatch();

  const { errores, loading, isUpdated } = useSelector(
    (state) => state.security
  );

  useEffect(() => {
    if (errores) {
      errores.map((error) => alert.error(error));
    }

    if (isUpdated) {
      alert.success("El password fue actualizado exitosamente");
      navigate("/me");
      dispatch(resetUpdateStatus({}));
    }
  }, [dispatch, alert, errores, isUpdated, navigate]);

  const submitHandler = (e) => {
    e.preventDefault();
    dispatch(updatePassword({ oldPassword, newPassword }));
  };

  return (
    <Fragment>
      <MetaData title={"Actualizar Password"} />
      <div className="row wrapper">
        <div className="col-10 col-lg-5">
          <form className="shadow-lg" onSubmit={submitHandler}>
            <h1 className="mt-2 mb-5">Actualizar Password</h1>
            <div className="form-group">
              <label htmlFor="old_password_field">Actual Password</label>
              <input
                type="password"
                id="old_password_field"
                className="form-control"
                value={oldPassword}
                onChange={(e) => setOldPassword(e.target.value)}
              />
            </div>

            <div className="form-group">
              <label htmlFor="new_password_field">Nuevo Password</label>
              <input
                type="password"
                id="new_password_field"
                className="form-control"
                value={newPassword}
                onChange={(e) => setNewPassword(e.target.value)}
              />
            </div>

            <button
              type="submit"
              className="btn update-btn btn-block mt-4 mb-3"
              disabled={loading ? true : false}
            >
              Actualizar Password
            </button>
          </form>
        </div>
      </div>
    </Fragment>
  );
};

export default UpdatePassword;
